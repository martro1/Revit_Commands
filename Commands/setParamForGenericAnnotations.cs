#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

#endregion

namespace Moje
{
    [Transaction(TransactionMode.Manual)]
    public class setParamForGenericAnnotations : IExternalCommand
    {
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            string parameterTypeError = "";
            using (Transaction t = new Transaction(doc, "CreateTextNote"))
            {
                t.Start();

                foreach (FamilyInstance fi in new FilteredElementCollector(doc)
                    .OfClass(typeof(FamilyInstance))
                    .OfCategory(BuiltInCategory.OST_GenericAnnotation)
                    .Cast<FamilyInstance>())
                    {
                    //Parameter p = fi.get_Parameter("View");
                    Parameter p = fi.get_Parameter(BuiltInParameter.VIEW_NAME);

                        if (p == null)
                            continue;
                        if (p.StorageType != StorageType.String)
                        {
                            parameterTypeError = fi.Symbol.Family.Name + " - " + p.StorageType.ToString();
                            continue;
                        }
                        Element ownerView = doc.GetElement(fi.OwnerViewId);
                        p.Set(ownerView.Name);
                    }
                if (parameterTypeError != "")
                {
                    TaskDialog.Show("error", "Parameter must be a string.\n" + parameterTypeError);
                    t.RollBack();
                }
                else
                {
                    t.Commit();
                }

            }

            return Result.Succeeded;
        }
    }
}
