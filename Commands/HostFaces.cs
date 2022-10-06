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
    public class HostFaces : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData,
            ref string message,
            ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            string info = "";
            foreach (HostObject hostObj in new FilteredElementCollector(doc)
                .OfClass(typeof(HostObject)))
            {
                info += hostObj.Name + Environment.NewLine;
                try
                {
                IList<Reference> sideFaceRefs = HostObjectUtils
                    .GetSideFaces(hostObj, ShellLayerType.Exterior);
                    foreach (Reference r in sideFaceRefs)
                    {
                        Face f = hostObj.GetGeometryObjectFromReference(r) as Face;
                        info += f.Area + ", ";
                    }
                }
                catch (Autodesk.Revit.Exceptions.ArgumentException)
                {
                }
                try
                {
                    IList<Reference> sideFaceRefs = HostObjectUtils
                        .GetTopFaces(hostObj);
                    foreach (Reference r in sideFaceRefs)
                    {
                        Face f = hostObj.GetGeometryObjectFromReference(r) as Face;
                        info += f.Area + ", ";
                    }
                }
                catch (Autodesk.Revit.Exceptions.ArgumentException)
                {
                }
                try
                {
                    IList<Reference> sideFaceRefs = HostObjectUtils
                        .GetBottomFaces(hostObj);
                    foreach (Reference r in sideFaceRefs)
                    {
                        Face f = hostObj.GetGeometryObjectFromReference(r) as Face;
                        info += f.Area + ", ";
                    }
                }
                catch (Autodesk.Revit.Exceptions.ArgumentException)
                {
                }
                info += Environment.NewLine + Environment.NewLine;
            }
            TaskDialog.Show("Host Objects", info);



            return Result.Succeeded;
        }
    }
}



