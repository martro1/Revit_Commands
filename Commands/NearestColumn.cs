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
    public class NearestColumn : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData,
            ref string message,
            ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;


            double shortestDist = Double.PositiveInfinity;
            FamilyInstance nearestColumn = null;

            XYZ xyzOfInterest = new XYZ(0,0,0);

            foreach (FamilyInstance fi in new FilteredElementCollector(doc)
                .OfClass(typeof(FamilyInstance))
                .OfCategory(BuiltInCategory.OST_StructuralColumns))
            {
                Location locationColumn = fi.Location;
                LocationPoint lPoint = locationColumn as LocationPoint;
                XYZ point = lPoint.Point;
                double lenght = fi.GetParameter(ParameterTypeId.InstanceLengthParam).AsDouble();
                XYZ lineEnd = point.Add(XYZ.BasisZ.Multiply(lenght));
                Line line = Line.CreateBound(point, lineEnd);
                double thisDistance = line.Distance(xyzOfInterest);

                if (thisDistance < shortestDist)
                {
                    shortestDist = thisDistance;
                    nearestColumn = fi; 

                }
                
            }
            ICollection<Element> iCOLL = new List<Element>();
            iCOLL.Add(nearestColumn);
            TaskDialog.Show("????0",
                 nearestColumn.Category
                + nearestColumn.Name
                + Environment.NewLine
                + shortestDist.ToString());

            return Result.Succeeded;
        }
    }
}



