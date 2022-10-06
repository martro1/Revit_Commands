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
    public class MyForm : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData,
            ref string message,
            ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            Element e = doc.GetElement(uidoc.Selection.PickObject(ObjectType.Element));

            double distance = 0;
            bool isHoriz = false;

            using (myNewForm thisForm = new myNewForm())
            {
                thisForm.ShowDialog();
                if (thisForm.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                {
                    return Result.Cancelled;
                }
                distance = thisForm.getDistance();
                isHoriz = thisForm.isHorizontal();
            }

            XYZ moveVector = null;

            if (isHoriz)
            {
                moveVector = XYZ.BasisX.Multiply(distance);
            }
            else
            {
                moveVector = XYZ.BasisY.Multiply(distance);
            }

            using(Transaction t = new Transaction(doc,"Move Object"))
            {
                t.Start();
                ElementTransformUtils.MoveElement(doc, e.Id, moveVector);
                t.Commit();
            }

            return Result.Succeeded;
        }
    }
}



