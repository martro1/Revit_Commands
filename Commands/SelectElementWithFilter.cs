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
    public class SelectElementWithFilter : IExternalCommand
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

            Reference myRef = uidoc.Selection
                .PickObject(ObjectType.Element, new WallSelectionFilter(),"Select a wall");
            Reference myRef2 = uidoc.Selection
                .PickObject(ObjectType.Element, new FloorSelectionFilter(),"Select a floor");
            /*Element e = doc.GetElement(myRef);

            string designOptionName = "<none>";

            if (e.DesignOption != null)
            {
                designOptionName = e.DesignOption.Name;
            }

            TaskDialog.Show("Element info", e.Name
                + Environment.NewLine + e.Id
                + "\n" + designOptionName);*/

          return Result.Succeeded;  
        }
    
    }
}
