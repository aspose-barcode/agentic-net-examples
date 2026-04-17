using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

class Program
{
    static void Main()
    {
        // Define output file path
        const string outputPath = "highres_barcode.png";

        // Create a barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set a high resolution (e.g., 300 DPI) for high‑resolution printing
            generator.Parameters.Resolution = 300f;

            // Optionally, adjust image size to maintain visual dimensions at higher DPI
            // Here we set width and height in points (1 point = 1/72 inch)
            generator.Parameters.ImageWidth.Point = 300f;   // approx 4.17 inches wide
            generator.Parameters.ImageHeight.Point = 100f;  // approx 1.39 inches tall

            // Save the barcode image to the specified file
            generator.Save(outputPath);
        }

        Console.WriteLine($"Barcode generated with 300 DPI and saved to '{outputPath}'.");
    }
}