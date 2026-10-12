// Title: Parallel barcode generation using TPL
// Description: Demonstrates generating multiple barcodes concurrently with the Task Parallel Library to speed up processing of large datasets.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator, EncodeTypes, and related parameters for batch creation of barcodes. Typical use cases include bulk label printing, inventory tagging, and automated document processing where developers need high‑throughput barcode output.
// Prompt: Parallelize barcode generation for large datasets using Task Parallel Library to improve performance.
// Tags: barcode symbology, generation, parallel, tpl, aspose.barcode, png

using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Sample program that generates a set of barcodes in parallel using the Task Parallel Library (TPL).
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Prepares sample data, creates an output folder, and generates barcodes concurrently.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Prepare sample data – each item defines a symbology and its text.
        // ------------------------------------------------------------
        var items = new List<BarcodeTaskInfo>
        {
            new BarcodeTaskInfo { SymbologyName = "Code128", CodeText = "ABC123456" },
            new BarcodeTaskInfo { SymbologyName = "QR", CodeText = "https://example.com" },
            new BarcodeTaskInfo { SymbologyName = "DataMatrix", CodeText = "DM001" },
            new BarcodeTaskInfo { SymbologyName = "Aztec", CodeText = "AZTEC" },
            new BarcodeTaskInfo { SymbologyName = "Pdf417", CodeText = "PDF417_SAMPLE" }
        };

        // ------------------------------------------------------------
        // 2. Create a unique temporary output folder for the generated images.
        // ------------------------------------------------------------
        string outputFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        Console.WriteLine($"Barcodes will be saved to: {outputFolder}");

        // ------------------------------------------------------------
        // 3. Generate barcodes in parallel – each iteration processes one item.
        // ------------------------------------------------------------
        Parallel.ForEach(items, (item, state, index) =>
        {
            try
            {
                // Resolve the symbology name to a concrete EncodeType.
                BaseEncodeType encodeType = ResolveEncodeType(item.SymbologyName);
                if (encodeType == null)
                {
                    Console.WriteLine($"Unknown symbology: {item.SymbologyName}");
                    return;
                }

                // Build a unique file name for the current barcode.
                string fileName = $"{item.SymbologyName}_{index + 1}.png";
                string filePath = Path.Combine(outputFolder, fileName);

                // Create the generator, optionally adjust parameters, and save as PNG.
                using (var generator = new BarcodeGenerator(encodeType, item.CodeText))
                {
                    // Set a modest X dimension for better readability.
                    generator.Parameters.Barcode.XDimension.Point = 2f;
                    generator.Save(filePath, BarCodeImageFormat.Png);
                }

                Console.WriteLine($"Generated: {fileName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating {item.SymbologyName}: {ex.Message}");
            }
        });
    }

    // ------------------------------------------------------------
    // Helper: Resolve symbology name to BaseEncodeType using reflection.
    // ------------------------------------------------------------
    private static BaseEncodeType ResolveEncodeType(string symbologyName)
    {
        var field = typeof(EncodeTypes).GetField(symbologyName);
        if (field == null)
            return null;
        return field.GetValue(null) as BaseEncodeType;
    }

    // ------------------------------------------------------------
    // Simple DTO that holds information required for each barcode task.
    // ------------------------------------------------------------
    private class BarcodeTaskInfo
    {
        public string SymbologyName { get; set; }
        public string CodeText { get; set; }
    }
}