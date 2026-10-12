// Title: Performance Comparison of Mailmark Barcode Generation in PNG vs JPEG
// Description: Demonstrates how to generate a Mailmark 2‑D barcode as PNG and JPEG, measuring the time taken and resulting file size for each format.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcodes such as Mailmark. It showcases the use of ComplexBarcodeGenerator, Mailmark2DCodetext, and BarCodeImageFormat to create images in different formats. Developers often need to evaluate output size and rendering speed when choosing image formats for high‑volume barcode generation.
// Prompt: Compare performance of generating Mailmark barcodes as PNG versus JPEG by measuring file size and generation time.
// Tags: mailmark,barcode,generation,performance,png,jpeg,complexbarcode,aspose.barcode

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Generates Mailmark 2‑D barcodes in PNG and JPEG formats,
/// then reports generation time and file size for each format.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary output folder,
    /// builds a Mailmark 2‑D codetext, generates PNG and JPEG images,
    /// and prints performance metrics to the console.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary directory for the generated files
        string outputDir = Path.Combine(Path.GetTempPath(), "MailmarkPerf_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Configure the Mailmark 2‑D codetext with sample data
        var mailmark = new Mailmark2DCodetext
        {
            UPUCountryID = "JGB ",
            InformationTypeID = "0",
            VersionID = "1",
            Class = "1",
            SupplyChainID = 123,
            ItemID = 1234,
            DestinationPostCodeAndDPS = "EF61AH8T ",
            RTSFlag = "0",
            ReturnToSenderPostCode = " ",
            CustomerContent = "CUSTOM",
            DataMatrixType = Mailmark2DType.Type_7
        };

        // Define the image formats to be tested (PNG and JPEG)
        var formats = new (string Name, BarCodeImageFormat Format)[]
        {
            ("PNG", BarCodeImageFormat.Png),
            ("JPEG", BarCodeImageFormat.Jpeg)
        };

        // Iterate over each format, generate the barcode, and record metrics
        foreach (var (name, format) in formats)
        {
            // Build the full file path for the current format
            string filePath = Path.Combine(outputDir, $"Mailmark2D.{name.ToLower()}");

            // Use a Stopwatch to measure generation time
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // Generate the barcode image using ComplexBarcodeGenerator
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                // Set the X‑dimension (module size) in pixels
                generator.Parameters.Barcode.XDimension.Pixels = 4f;

                // Save the image in the selected format
                generator.Save(filePath, format);
            }

            stopwatch.Stop();

            // Retrieve the size of the generated file
            long fileSize = new FileInfo(filePath).Length;

            // Output the performance results to the console
            Console.WriteLine($"{name} - Generation Time: {stopwatch.ElapsedMilliseconds} ms, File Size: {fileSize} bytes");
        }

        // Inform the user where the generated files are stored
        Console.WriteLine($"Generated files are located in: {outputDir}");
    }
}