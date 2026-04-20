using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Sample data to encode – in real scenarios this could be thousands of items.
        List<string> dataSet = new List<string>
        {
            "Item001",
            "Item002",
            "Item003",
            "Item004",
            "Item005"
        };

        // Output directory (created if it does not exist).
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Parallel generation using TPL.
        Parallel.ForEach(dataSet, (codeText) =>
        {
            // Each iteration creates its own BarcodeGenerator (IDisposable).
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Optional: set some parameters (e.g., XDimension, BarHeight).
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Parameters.Barcode.BarHeight.Point = 50f;

                // Build a unique file name.
                string fileName = $"barcode_{codeText}.png";
                string filePath = Path.Combine(outputDir, fileName);

                // Save the barcode image.
                generator.Save(filePath);
                Console.WriteLine($"Generated: {filePath}");
            }
        });

        Console.WriteLine("All barcodes have been generated.");
    }
}