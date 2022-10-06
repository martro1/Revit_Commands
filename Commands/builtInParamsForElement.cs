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
    public class builtInParamsForElement : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData,
            ref string message,
            ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            Element e = doc.GetElement(uidoc.Selection.PickObject(ObjectType.Element));
            string data = "";
   
            foreach (BuiltInParameter bip in Enum.GetValues(typeof(BuiltInParameter)))
            {
                try
                {
                    
                    Parameter p = e.get_Parameter(bip); 
                    if (null != p)
                    {
                        data += (bip.ToString() + ": " + p.Definition.Name + ": "); 
                    if (p.StorageType == StorageType.String)
                        data += p.AsString();
                    else if (p.StorageType == StorageType.Integer)
                        data += p.AsInteger();
                    else if (p.StorageType == StorageType.Double)
                        data += p.AsDouble();
                    else if (p.StorageType == StorageType.ElementId)
                        data += "ID " + p.AsElementId().IntegerValue;
                    data += "\n";
                    }    

                }
                catch
                {
                }
            }
            TaskDialog.Show("BI Params",data);
            return Result.Succeeded;
        }
    }
}



