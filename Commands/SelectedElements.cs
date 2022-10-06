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
    public class SelectedElements : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData,
            ref string message,
            ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            List<Reference> pickObjects = uidoc.Selection.PickObjects(ObjectType.Element) as List<Reference>;
            string s = "";
            int countWalls = 0;

            foreach (Reference pickObject in pickObjects)
            {
                ElementId eleId = pickObject.ElementId;
                Element ele = doc.GetElement(eleId);
                s += ele.Category.Name + " " + ele.Name  + Environment.NewLine;
                
                if (ele.Category.Name == "Walls")
                {
                    countWalls++;
                    /*List<Element> list = new List<Element>();
                    list.Add(ele);*/  
                }
            }
            TaskDialog.Show("liczba zaznaczonych scian ", countWalls.ToString());
            TaskDialog.Show("calkowita liczba zaznaczonych elementow", pickObjects.Count.ToString());
            TaskDialog.Show("Informacje na temat zaznaczonych elementów: ", s);

            return Result.Succeeded;
        } 
    }
}



