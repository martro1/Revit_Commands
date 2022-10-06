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
    public class RotateWall : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData,
            ref string message,
            ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            Reference myRef = uidoc.Selection.PickObject(ObjectType.Element);
            Element ele = doc.GetElement(myRef) as Wall;

            bool rotated = false;
            using (Transaction t = new Transaction(doc, "Rotate Wall"))
            {
                t.Start();

                LocationCurve curve = ele.Location as LocationCurve;
                if (curve != null)
                    {
                        Curve line = curve.Curve;
                        XYZ aa = line.GetEndPoint(0);
                        XYZ cc = new XYZ(aa.X, aa.Y, aa.Z+10);
                        Line axis = Line.CreateBound(aa, cc);
                        rotated = curve.Rotate(axis, Math.PI/2.0);  
                    }

                t.Commit();
            }
            return Result.Succeeded;
        }
    }
}



