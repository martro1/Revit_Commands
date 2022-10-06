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
    public class CreateDirectShape : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData,
            ref string message,
            ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

                List<Curve> profile = new List<Curve>();
                XYZ center = XYZ.Zero;
                double radius = 100;
                
                XYZ profile00 = center;
                XYZ profilePlus = center + new XYZ(0, radius, 0);
                XYZ profileMinus = center - new XYZ(0, radius, 0);
                profile.Add(Line.CreateBound(profilePlus, profileMinus));
                profile.Add(Arc.Create(profileMinus, profilePlus, center + new XYZ(radius, 0, 0)));
                CurveLoop curveLoop = CurveLoop.Create(profile);
                SolidOptions options = new SolidOptions(ElementId.InvalidElementId, ElementId.InvalidElementId);
                Frame frame = new Frame(center, XYZ.BasisX, -XYZ.BasisZ, XYZ.BasisY);
                if (Frame.CanDefineRevitGeometry(frame) == true)
                {
                    Solid sphere = GeometryCreationUtilities.CreateRevolvedGeometry(frame, new CurveLoop[] { curveLoop }, 0, 2 * Math.PI, options);
                    using (Transaction t = new Transaction(doc, "Create sphere direct shape"))
                    {
                        t.Start();
                        DirectShapeLibrary directShapeLibrary = DirectShapeLibrary.GetDirectShapeLibrary(doc);
                        DirectShapeType directShapeType = DirectShapeType.Create(doc, "Tested", new ElementId(BuiltInCategory.OST_GenericModel));
                        directShapeType.SetShape(new List<GeometryObject>() { sphere });
                        directShapeLibrary.AddDefinitionType("Tested", directShapeType.Id);
                        DirectShape ds = DirectShape.CreateElementInstance(doc, directShapeType.Id, directShapeType.Category.Id, "Tested", Transform.Identity);
                        ds.SetTypeId(directShapeType.Id);
                        ds.ApplicationId = "Application id";
                        ds.ApplicationDataId = "Geometry object id";
                        ds.SetShape(new GeometryObject[] { sphere });
                        t.Commit();
                    }
                }





            return Result.Succeeded;
        }
    }
}



