#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#endregion

namespace Moje
{
    [Transaction(TransactionMode.Manual)]
    public class FanilyTypesAndParameters : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData,
            ref string message,
            ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            if (!doc.IsFamilyDocument)
            {
                return Result.Succeeded;
            }

            using (Transaction t = new Transaction(doc,"family test"))
            {
                t.Start();
                FamilyManager mgr = doc.FamilyManager;

                //create parameter
                //FamilyParameter param = mgr.AddParameter("New Parameter", BuiltInParameterGroup.PG_DATA, ParameterType.Text, false);
                FamilyParameter param = mgr.AddParameter("New Parameter", GroupTypeId.Data, SpecTypeId.String.Text, false);

                for (int i = 1; i < 5; i++)
                {
                    FamilyType newType = mgr.NewType(i.ToString());

                    mgr.CurrentType = newType;
                    mgr.Set(param, "this value " + i);
                }
                t.Commit();
            }


            return Result.Succeeded;
        }
    }
}



