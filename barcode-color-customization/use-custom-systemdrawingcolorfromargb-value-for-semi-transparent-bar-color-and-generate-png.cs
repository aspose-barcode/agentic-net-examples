using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the text to encode
            generator.CodeText = "123ABC";

            // Apply a semi‑transparent red color to the bars (alpha = 128)
            generator.Parameters.Barcode.BarColor = Color.FromArgb(128, 255, 0, 0);

            // Define output file path
            string outputFile = "semiTransparentBarcode.png";

            // Save the barcode as a PNG image
            generator.Save(outputFile, BarCodeImageFormat.Png);

            Console.WriteLine($"Barcode image saved to: {Path.GetFullPath(outputFile)}");
        }
    }
}