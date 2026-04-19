using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeRazorDemo
{
    class Program
    {
        static void Main()
        {
            // Path for the generated QR code image
            string imagePath = "qr.png";

            // Generate QR Code barcode
            using (var generator = new BarcodeGenerator(EncodeTypes.QR))
            {
                generator.CodeText = "https://example.com";
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;
                generator.Save(imagePath);
            }

            // Create a simple Razor page that references the QR code image using the img tag helper
            string razorContent = @"@page
@{
    Layout = null;
}
<img asp-src=""~/" + imagePath + @""" alt=""QR Code"" />";

            File.WriteAllText("Index.cshtml", razorContent);
        }
    }
}