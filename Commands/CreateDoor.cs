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
    public class CreateDoor : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData,
            ref string message,
            ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            Level l = new FilteredElementCollector(doc)
                .OfClass(typeof(Level))
                .Cast<Level>()
                .OrderBy(x => x.Elevation)
                .First();

            Family f = new FilteredElementCollector(doc)
                .OfClass(typeof(Family))
                .Cast<Family>()
                .FirstOrDefault
                (q => q.FamilyCategory.Name == "Doors" && q.Name == "M_Single-Flush");

            FamilySymbol fs = new FilteredElementCollector(doc)
                .OfClass(typeof(FamilySymbol))
                .Cast<FamilySymbol>()
                .FirstOrDefault(x => x.Name == "0915 x 2134mm");

            Reference myRef = uidoc.Selection.PickObject(ObjectType.Element,"Pick point on wall");
            XYZ point = myRef.GlobalPoint;
            Element host = doc.GetElement(myRef);

            using (Transaction t = new Transaction(doc,"Create Family Instance"))
            {
                t.Start();

                FamilyInstance fi = doc.Create.NewFamilyInstance
                    (point,fs,host,l,Autodesk.Revit.DB.Structure.StructuralType.NonStructural);

                t.Commit();
            }

            return Result.Succeeded;
        }
    }
}



