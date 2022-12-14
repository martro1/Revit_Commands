using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace Moje
{
    internal class ExternalApplication : IExternalApplication
    {
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            //create ribbon tab
            application.CreateRibbonTab("Moje komendy");

            //creaate push button
            string path = Assembly.GetExecutingAssembly().Location;
            PushButtonData button = new PushButtonData ("Button1", "TekstpPodButtonem", path, "Moje.CreateTopo");

            RibbonPanel panel = application.CreateRibbonPanel("Moje komendy", "panel 1");

            //add button image
            Uri imagePath = new Uri(@"X:\MARCIN\PROGRAMES\TUTORIALE\C#forRevit\DW_32x32.png");
            BitmapImage image = new BitmapImage(imagePath);

            PushButton pushButton = panel.AddItem(button) as PushButton;
            pushButton.LargeImage = image;

            return Result.Succeeded;    
        }
    }
}
