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
    public class RotateElement : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData,
            ref string message,
            ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            Element e  = doc.GetElement(uidoc.Selection.PickObject(ObjectType.Element));

            XYZ point = ((LocationPoint)e.Location).Point;
            XYZ point2 = point.Add(XYZ.BasisZ);

            Line axis = Line.CreateUnbound(point,point2);


            using (Transaction t = new Transaction(doc, "Rotate Element"))
            {
                t.Start();
                ElementTransformUtils.RotateElement(doc, e.Id, axis, degreesToRadians(45));
                t.Commit();
            }
            return Result.Succeeded;
        }
            private double degreesToRadians(double degrees)
            {
                return degrees * Math.PI / 180;
            }
    }
}



