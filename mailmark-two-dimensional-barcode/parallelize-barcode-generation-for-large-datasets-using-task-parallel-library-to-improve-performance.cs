using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating Code128 barcodes for a list of items and saving them as PNG files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates barcodes in parallel and writes them to disk.
    /// </summary>
    static void Main()
    {
        // Sample dataset of code texts to be encoded as barcodes
        var data = new List<string>
        {
            "Item001",
            "Item002",
            "Item003",
            "Item004",
            "Item005"
        };

        // Define the output directory for generated barcode images
        string outputDir = "Barcodes";

        // Ensure the output directory exists; create it if it does not
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Generate barcodes in parallel to improve performance on multi-core systems
        Parallel.ForEach(data, codeText =>
        {
            // Each parallel task creates its own BarcodeGenerator instance to avoid thread‑safety issues
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Set image resolution (dots per inch) for higher quality output
                generator.Parameters.Resolution = 300f;

                // Build the full file path for the PNG image
                string filePath = Path.Combine(outputDir, $"{codeText}.png");

                // Save the generated barcode image to the specified path
                generator.Save(filePath);

                // Log progress to the console
                Console.WriteLine($"Generated barcode for {codeText} at {filePath}");
            }
        });

        // Indicate that all barcode generation tasks have completed
        Console.WriteLine("All barcodes have been generated.");
    }
}