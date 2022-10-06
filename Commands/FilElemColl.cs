#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Autodesk.Revit.DB.Architecture;
#endregion

namespace Moje
{
    [Transaction(TransactionMode.Manual)]
    public class FilElemColl : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData,
            ref string message,
            ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;


            // logical OR filter
            /*string info = "";
            ElementCategoryFilter doorfilter = new ElementCategoryFilter(BuiltInCategory.OST_Doors);
            ElementCategoryFilter windowfilter = new ElementCategoryFilter(BuiltInCategory.OST_Windows);
            LogicalOrFilter orFilter = new LogicalOrFilter(doorfilter, windowfilter);

            IList<BuiltInCategory> catList = new List<BuiltInCategory>();
            catList.Add(BuiltInCategory.OST_Doors);
            catList.Add(BuiltInCategory.OST_Windows);
            ElementMulticategoryFilter mulkticatfilter = new ElementMulticategoryFilter(catList);
 
            foreach (Element e in new FilteredElementCollector(doc,doc.ActiveView.Id)
            .OfClass(typeof(FamilyInstance))
            .WherePasses(mulkticatfilter))
            {
                FamilyInstance fi = e as FamilyInstance;
                FamilySymbol fs = fi.Symbol;
                Family family = fs.Family;
                info += family.Name + ": " + fs.Name + ": " + fi.Name + Environment.NewLine;
            }*/

            // filter element by view
            /*ElementClassFilter classFilter = new ElementClassFilter(typeof(TextNote));
            ElementOwnerViewFilter viewFilter = new ElementOwnerViewFilter(doc.ActiveView.Id,true);
            string text = "";
            foreach (Element e in new FilteredElementCollector(doc)
                .WherePasses(classFilter)
                .WherePasses(viewFilter))
            {
                TextNote textnote = e as TextNote;
                text += textnote.Text + Environment.NewLine;
            }
            TaskDialog.Show("text",text);*/

            //filter spatial elements example room
            /* ElementClassFilter classFilter = new ElementClassFilter(typeof(SpatialElement));

             string text = "";
             foreach (Element e in new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Rooms)
                 .WherePasses(classFilter))
             {
                 text += e.Name + Environment.NewLine;
             }
             TaskDialog.Show("text",text);*/

            //

            XYZ pt1 = uidoc.Selection.PickPoint();
            XYZ pt2 = uidoc.Selection.PickPoint();

            Outline outline = new Outline(pt1, pt2);

            BoundingBoxIntersectsFilter bboxFilter = new BoundingBoxIntersectsFilter(outline);

            string text = "";
            foreach (Element e in new FilteredElementCollector(doc)
                .WherePasses(bboxFilter))
            {
                text += e.Name + Environment.NewLine;
            }
            TaskDialog.Show("text", text);





            return Result.Succeeded;
        }
    }
}



