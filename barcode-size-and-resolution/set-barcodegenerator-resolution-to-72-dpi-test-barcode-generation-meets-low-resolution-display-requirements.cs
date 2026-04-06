using System;
using Aspose.BarCode.Generation;

namespace BarcodeResolutionExample
{
    class Program
    {
        static void Main()
        {
            // Create a barcode generator for Code128 with sample text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
            {
                // Set the resolution to 72 dpi (low‑resolution display requirement).
                generator.Parameters.Resolution = 72f;

                // Optionally define image dimensions to ensure the barcode is visible.
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 150f;

                // Save the generated barcode to a PNG file.
                string outputFile = "barcode_72dpi.png";
                generator.Save(outputFile);

                Console.WriteLine($"Barcode saved to '{outputFile}' with resolution {generator.Parameters.Resolution} dpi.");
            }
        }
    }
}