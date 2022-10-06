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
    public class CreateTextNote : IExternalCommand
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

            /*Reference myRef = uidoc.Selection.PickObject(ObjectType.Nothing);

            ElementId id = myRef.ElementId;*/

            XYZ point = uidoc.Selection.PickPoint();

            using (Transaction t = new Transaction(doc, "CreateTextNote"))
            {
                t.Start();

                ElementId defaultTextTypeId = doc.GetDefaultElementTypeId(ElementTypeGroup.TextNoteType);
                TextNoteOptions opts = new TextNoteOptions(defaultTextTypeId);
                opts.HorizontalAlignment = HorizontalTextAlignment.Left;
                opts.Rotation = 0;

                TextNote note = TextNote.Create(doc, doc.ActiveView.Id, point, 1,
                    "I am an Api created text note", opts);
                Leader l = note.AddLeader(TextNoteLeaderTypes.TNLT_ARC_L);

                t.Commit();
            }

            return Result.Succeeded;
        }
    }
}
