// Title: Compare generation time and image size of Mailmark type 7 vs type 29 barcodes
// Description: This example generates Mailmark 2D barcodes of type 7 and type 29, measures the time required to create each image, and records the resulting file size.
// Category-Description: Demonstrates Aspose.BarCode ComplexBarcode generation for Mailmark 2D symbology. It showcases the use of Mailmark2DCodetext, ComplexBarcodeGenerator, and barcode parameters such as XDimension. Typical use cases include performance benchmarking and file‑size analysis when choosing between different Mailmark types. Developers working with postal barcodes often need to compare generation speed and output size for optimization.
// Prompt: Compare generation time and image size between Mailmark type 7 and type 29 barcodes.
// Tags: mailmark, barcode, generation, performance, image-size, complexbarcode, aspose.barcode

using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates Mailmark 2D barcodes of two different types, measures generation time,
/// and reports the resulting image file sizes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary output folder, runs the
    /// generation and measurement for both Mailmark types, and prints the results.
    /// </summary>
    static void Main(string[] args)
    {
        // Create a unique temporary directory for the output images.
        string outputDir = Path.Combine(Path.GetTempPath(), "MailmarkCompare_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Store results as a list of tuples: (type name, generation time in ms, file size in bytes).
        var results = new List<(string Type, long TimeMs, long SizeBytes)>();

        // Generate and measure Mailmark type 7.
        GenerateAndMeasure(Mailmark2DType.Type_7, Path.Combine(outputDir, "MailmarkType7.png"), results);
        // Generate and measure Mailmark type 29.
        GenerateAndMeasure(Mailmark2DType.Type_29, Path.Combine(outputDir, "MailmarkType29.png"), results);

        // Output the collected performance data to the console.
        foreach (var r in results)
        {
            Console.WriteLine($"{r.Type}: Generation time = {r.TimeMs} ms, File size = {r.SizeBytes} bytes");
        }
    }

    /// <summary>
    /// Generates a Mailmark 2D barcode of the specified type, saves it to a file,
    /// measures the generation time, and records the file size.
    /// </summary>
    /// <param name="type">The Mailmark 2D type (e.g., Type_7 or Type_29).</param>
    /// <param name="filePath">Full path where the generated PNG image will be saved.</param>
    /// <param name="results">Collection to which the measurement results are added.</param>
    static void GenerateAndMeasure(Mailmark2DType type, string filePath, List<(string, long, long)> results)
    {
        // Configure the Mailmark 2D codetext with sample data.
        var mailmark2D = new Mailmark2DCodetext
        {
            UPUCountryID = "JGB ",
            InformationTypeID = "0",
            VersionID = "1",
            Class = "1",
            SupplyChainID = 123,
            ItemID = 1234,
            DestinationPostCodeAndDPS = "EF61AH8T ",
            CustomerContent = "CUSTOM"
        };
        // Set the specific Mailmark type to be generated.
        mailmark2D.DataMatrixType = type;

        // Start timing the barcode generation.
        var stopwatch = Stopwatch.StartNew();

        // Generate the barcode and save it as a PNG image.
        using (var generator = new ComplexBarcodeGenerator(mailmark2D))
        {
            // Set the X-dimension (module size) to 4 pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            generator.Save(filePath, BarCodeImageFormat.Png);
        }

        // Stop timing after the image has been saved.
        stopwatch.Stop();

        // Determine the size of the generated file.
        long size = new FileInfo(filePath).Length;
        // Record the type name, elapsed time, and file size.
        results.Add((type.ToString(), stopwatch.ElapsedMilliseconds, size));
    }
}