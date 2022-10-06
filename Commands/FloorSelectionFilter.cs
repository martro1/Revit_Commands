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
    public class FloorSelectionFilter : ISelectionFilter
    {

        //Walls
        public bool AllowElement(Element e)
        {
            if (e.Category != null && e.Category.Name == "Floors")
                return true;
            return false;
        }
        public bool AllowReference(Reference r, XYZ point)
        {
            return true;
        }



    }
}
