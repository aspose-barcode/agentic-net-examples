using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Prepare a list of sample postal codes (Postnet expects 5, 6 or 9 digits)
        var postalCodes = new List<string>
        {
            "12345",
            "67890",
            "112233",
            "44556677",
            "987654321"
        };

        // Output directory for generated barcodes
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "PostalBarcodes");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Generate barcodes in parallel; each iteration creates its own BarcodeGenerator (thread‑safe)
        Parallel.ForEach(postalCodes, codeText =>
        {
            // Each thread works with its own generator instance
            using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, codeText))
            {
                // Example of setting a barcode parameter (optional)
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.Barcode.XDimension.Point = 2f; // small bar width
                generator.Parameters.Barcode.BarHeight.Point = 50f; // bar height

                // Build a file name based on the code text
                string fileName = $"Postnet_{codeText}.png";
                string filePath = Path.Combine(outputDir, fileName);

                // Save the barcode image
                generator.Save(filePath);
            }
        });

        Console.WriteLine($"Generated {postalCodes.Count} postal barcodes in '{outputDir}'.");
    }
}