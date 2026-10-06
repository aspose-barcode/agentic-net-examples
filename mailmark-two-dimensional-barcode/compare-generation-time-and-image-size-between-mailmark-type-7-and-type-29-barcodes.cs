// Title: Compare generation time and image size of Mailmark Type 7 vs Type 29 barcodes
// Description: This example generates Mailmark 2‑D barcodes of type 7 and type 29, measures the time taken to create each image and reports the resulting file sizes. It helps developers evaluate performance and storage impact of different Mailmark variants.
// Category-Description: Demonstrates Aspose.BarCode complex barcode generation for Mailmark 2‑D symbology. The example uses Mailmark2DCodetext, ComplexBarcodeGenerator, and BarCodeImageFormat classes to create PNG images. Typical use cases include batch barcode creation, performance benchmarking, and size optimization for postal applications. Developers often need to compare different Mailmark types to choose the most suitable for their workflow.
// Prompt: Compare generation time and image size between Mailmark type 7 and type 29 barcodes.
// Tags: mailmark, barcode, performance, image-size, generation, aspose.barcode, complexbarcodegenerator

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generation time and file size comparison between Mailmark Type 7 and Type 29 barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates both barcode types, measures generation time and file size, and writes results to console.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder to store the generated images
        string basePath = Path.Combine(Path.GetTempPath(), "MailmarkCompare_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(basePath);

        // Define output file paths for each Mailmark type
        string type7Path = Path.Combine(basePath, "MailmarkType7.png");
        string type29Path = Path.Combine(basePath, "MailmarkType29.png");

        // Generate the barcodes and capture timing and size information
        var result7 = GenerateAndMeasure(Mailmark2DType.Type_7, type7Path);
        var result29 = GenerateAndMeasure(Mailmark2DType.Type_29, type29Path);

        // Output the comparison results
        Console.WriteLine($"Mailmark Type 7 - Generation Time: {result7.timeMs} ms, Image Size: {result7.fileSize} bytes");
        Console.WriteLine($"Mailmark Type 29 - Generation Time: {result29.timeMs} ms, Image Size: {result29.fileSize} bytes");
    }

    /// <summary>
    /// Generates a Mailmark barcode of the specified type, saves it to the given path, and returns the generation time (ms) and file size (bytes).
    /// </summary>
    /// <param name="type">The Mailmark 2‑D type to generate (e.g., Type_7 or Type_29).</param>
    /// <param name="outputPath">Full file path where the PNG image will be saved.</param>
    /// <returns>A tuple containing elapsed time in milliseconds and the resulting file size in bytes.</returns>
    private static (long timeMs, long fileSize) GenerateAndMeasure(Mailmark2DType type, string outputPath)
    {
        // Start timing the barcode generation process
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        // Configure the Mailmark data payload
        var mailmark = new Mailmark2DCodetext
        {
            UPUCountryID = "JGB ",
            InformationTypeID = "0",
            VersionID = "1",
            Class = "1",
            SupplyChainID = 123,
            ItemID = 1234,
            CustomerContent = "CUSTOM",
            DataMatrixType = type
        };

        // Generate the barcode image and save it as PNG
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Stop timing after the image has been saved
        stopwatch.Stop();

        // Determine the file size of the generated image
        long fileSize = 0;
        if (File.Exists(outputPath))
        {
            fileSize = new FileInfo(outputPath).Length;
        }

        // Return elapsed time and file size
        return (stopwatch.ElapsedMilliseconds, fileSize);
    }
}