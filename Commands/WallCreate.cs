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
    public class WallCreate : IExternalCommand
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

            Line l = Line.CreateBound(XYZ.Zero, new XYZ(10, 10, 0));
            ElementId levelid = uidoc.Selection.PickObject(ObjectType.Element, "select a level").ElementId;

            using (Transaction t = new Transaction(doc,"create wall"))
            {
                t.Start();

                Wall w = Wall.Create(doc, l, levelid, false);

                t.Commit();
            }


            return Result.Succeeded;
        }
    }
}
