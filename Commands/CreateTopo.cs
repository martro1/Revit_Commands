#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Moje.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


#endregion

namespace Moje 
{
    [Transaction(TransactionMode.Manual)]
    public class CreateTopo : IExternalCommand 
    {
        public Result Execute(ExternalCommandData commandData,
            ref string message,
            ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;



            IList<XYZ> xyzlist = new List<XYZ>();
            xyzlist.Add(new XYZ(0, 0, 0));
            xyzlist.Add(new XYZ(0, 20, 0));
            xyzlist.Add(new XYZ(20, 0, 0));
            xyzlist.Add(new XYZ(10, 10, 10));

/*            using (Transaction t = new Transaction(doc, "Create Topo"))
            {
                t.Start();
                //1.create surface
                //TopographySurface ts = TopographySurface.Create(doc, xyzlist);
                t.Commit();
            }*/
                //2.delete point
                TopographySurface ts = doc.GetElement
                    (uidoc.Selection.PickObject(ObjectType.Element)) as TopographySurface;

                XYZ highest = null;
                foreach (XYZ xyz in ts.GetPoints())
                {
                    if (highest == null)
                    {
                        highest = xyz;
                        continue;
                    }
                    if (xyz.Z > highest.Z)
                    {
                        highest = xyz;
                    }
                }
                IList<XYZ> toDelete = new List<XYZ>();
                toDelete.Add(highest);
               
            
            using (TopographyEditScope tes = new TopographyEditScope(doc,"edit topo"))
            {

                tes.Start(ts.Id); 
                using (Transaction t = new Transaction(doc,"Delete Point"))
                {
                    t.Start();
                    ts.DeletePoints(toDelete);
                    t.Commit();
                }
                tes.Commit(new iFailuresPreprocessor());
            }


            return Result.Succeeded;
        }
    }
}



