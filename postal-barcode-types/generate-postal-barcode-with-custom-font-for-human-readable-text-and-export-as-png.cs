using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Define output file
        const string outputPath = "postal_barcode.png";

        // Create a barcode generator for Postnet (postal barcode)
        using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, "12345"))
        {
            // Set barcode parameters (optional: adjust size)
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;
            generator.Parameters.Resolution = 96f;

            // Customize human‑readable text font
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // Save as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}