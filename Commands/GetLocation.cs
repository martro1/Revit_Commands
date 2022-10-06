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
    public class GetLocation : IExternalCommand
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

            Location location = e.Location;

            if (location is LocationCurve)
            {
                LocationCurve lc = location as LocationCurve;
                Curve c = lc.Curve;
                //TaskDialog.Show("Curve lenght: ", c.ApproximateLength.ToString());
                XYZ end0 = c.GetEndPoint(0);
                XYZ end1 = c.GetEndPoint(1);
                TaskDialog.Show("Curve endpoints", end0.ToString()
                    + Environment.NewLine + end1.ToString());

                if (c is Line)
                {
                    Line line = c as Line;
                    //TaskDialog.Show("Direction", line.Direction.ToString());
                }
                else
                {
                    Transform t = c.ComputeDerivatives(0.5, true);
                    XYZ tangent = t.BasisX;
                    XYZ tangentNormal = tangent.Normalize();
                    //TaskDialog.Show("Direction", tangent.ToString() 
                        //+ Environment.NewLine + tangentNormal.ToString());
                }
            }
                else
                {
                    LocationPoint lp = location as LocationPoint;
                    XYZ point = lp.Point;
                    TaskDialog.Show("Point", point.ToString());
                }

            return Result.Succeeded;
        }
    }
}



