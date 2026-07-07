// Title: Parallel Barcode Generation Using TPL
// Description: Demonstrates generating Code128 barcodes in parallel for a list of strings, improving throughput for large datasets.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class with the EncodeTypes enumeration to create barcode images. Typical use cases include batch processing of inventory codes, ticket numbers, or any high‑volume identifier set where performance matters. Developers often need to parallelize generation to reduce overall processing time while ensuring thread‑safety of the generator instances.
// Prompt: Parallelize barcode generation for large datasets using Task Parallel Library to improve performance.
// Tags: barcode symbology, generation, parallel, task parallel library, png, aspose.barcode, encode types, code128

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

/// <summary>
/// Example program that generates Code128 barcodes in parallel using the Task Parallel Library (TPL).
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates barcodes for each item in the data set concurrently.
    /// </summary>
    static void Main()
    {
        // Sample data set (replace with real data in production)
        List<string> dataSet = new List<string>
        {
            "Item001",
            "Item002",
            "Item003",
            "Item004",
            "Item005"
        };

        // Output directory (current folder)
        string outputFolder = AppDomain.CurrentDomain.BaseDirectory;

        // Parallel generation of barcodes using TPL
        Parallel.ForEach(dataSet, (codeText) =>
        {
            // Each task creates its own generator instance because BarcodeGenerator is not thread‑safe
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Optional: customize appearance of the barcode
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.DarkBlue;
                generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
                generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 10f;

                // Build output file name (e.g., Item001.png)
                string filePath = System.IO.Path.Combine(outputFolder, $"{codeText}.png");

                // Save the barcode image to the specified path
                generator.Save(filePath);

                // Log the successful generation to the console
                Console.WriteLine($"Generated barcode for '{codeText}' -> {filePath}");
            }
        });

        // Indicate that all barcode generation tasks have completed
        Console.WriteLine("All barcodes have been generated.");
    }
}