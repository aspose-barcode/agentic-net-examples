using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code16K symbology with sample codetext
        using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, "12345678901234567890"))
        {
            // Configure quiet zones (left and right) in terms of xDimension coefficients
            generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef = 15;   // example: 15 * xDimension
            generator.Parameters.Barcode.Code16K.QuietZoneRightCoef = 5;   // example: 5 * xDimension

            // Optional: set resolution to improve PDF quality
            generator.Parameters.Resolution = 300;

            // Save the barcode as a PDF file
            generator.Save("Code16K_QuietZones.pdf");
        }

        Console.WriteLine("Barcode PDF generated successfully.");
    }
}