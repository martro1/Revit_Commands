#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

#endregion

namespace Moje
{
    [Transaction(TransactionMode.Manual)]
    public class WriteReadTextFile : IExternalCommand
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

            //write text file
            /*string fileName = @"X:\MARCIN\PROGRAMES\TUTORIALE\C#forRevit\test.txt";
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.WriteLine("First Line");
                writer.WriteLine("Second Line");
                //writer.Close();
            }*/

            //read text file
            string fileName = @"X:\MARCIN\PROGRAMES\TUTORIALE\C#forRevit\test.txt";
            string fileContents = "";
            using (StreamReader reader = new StreamReader(fileName))
            {
                string thisLine = "";
                while(thisLine != null)
                {
                    thisLine = reader.ReadLine();
                    fileContents = fileContents + thisLine + Environment.NewLine;
                }
                TaskDialog.Show(fileName, fileContents);

            }
            return Result.Succeeded;
        }
    }
}
