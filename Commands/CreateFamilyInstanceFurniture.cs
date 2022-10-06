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
    public class CreateFamilyInstanceFurniture : IExternalCommand
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

            Level l = new FilteredElementCollector(doc).OfClass(typeof(Level))
                .Cast<Level>().OrderBy(x => x.Elevation).First();

            Family f = new FilteredElementCollector(doc).OfClass(typeof(Family))
                .FirstOrDefault(x => x.Name == "Desk") as Family;

            FamilySymbol fs = new FilteredElementCollector(doc)
                .OfClass(typeof(FamilySymbol))
                .Cast<FamilySymbol>()
                .FirstOrDefault(x => x.Name == "1525 x 762mm");

            XYZ point = uidoc.Selection.PickPoint("Pick point");

            using (Transaction t = new Transaction(doc, "CreateFamilyInstanceFurniture"))
            {
                t.Start();

                FamilyInstance fi = doc.Create.NewFamilyInstance(point,fs,l,
                    Autodesk.Revit.DB.Structure.StructuralType.NonStructural);

                t.Commit();
            }

            return Result.Succeeded;
        }
    }
}
