// Title: Parallel Barcode Generation with TPL
// Description: Demonstrates generating multiple Code128 barcodes concurrently using Aspose.BarCode and the Task Parallel Library to improve throughput for large datasets.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing how to create barcode images in bulk. It uses the BarcodeGenerator class with EncodeTypes.Code128, configures basic parameters, and saves PNG files. Developers often need to generate many barcodes quickly, and parallelizing the work with TPL is a common technique to reduce processing time.
// Prompt: Parallelize barcode generation for large datasets using Task Parallel Library to improve performance.
// Tags: barcode symbology, generation, parallel, tpl, png, aspose.barcode, code128

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates parallel generation of Code128 barcodes using Aspose.BarCode and TPL.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes for a sample list in parallel and saves them as PNG files.
    /// </summary>
    static void Main()
    {
        // Prepare a small sample dataset of code texts.
        var codeTexts = new List<string>
        {
            "12345",
            "ABCDE",
            "987654321",
            "CODE128",
            "Test123"
        };

        // Create output folder for the generated barcode images.
        var outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        Directory.CreateDirectory(outputFolder);

        // Parallelize barcode generation using the Task Parallel Library.
        var tasks = new List<Task>();
        for (int i = 0; i < codeTexts.Count; i++)
        {
            int index = i; // Capture loop variable for the task closure.
            tasks.Add(Task.Run(() =>
            {
                // Each barcode generation uses its own BarcodeGenerator instance.
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeTexts[index]))
                {
                    // Optional: set barcode appearance parameters.
                    generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                    generator.Parameters.Barcode.XDimension.Point = 2f;

                    // Define the output file path for this barcode.
                    var outputPath = Path.Combine(outputFolder, $"barcode_{index + 1}.png");

                    // Save the barcode image in PNG format.
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                }

                // Log progress to the console.
                Console.WriteLine($"Generated barcode {index + 1} for text '{codeTexts[index]}'");
            }));
        }

        // Wait for all barcode generation tasks to complete.
        Task.WaitAll(tasks.ToArray());

        // Indicate that the process has finished.
        Console.WriteLine("All barcodes have been generated.");
    }
}