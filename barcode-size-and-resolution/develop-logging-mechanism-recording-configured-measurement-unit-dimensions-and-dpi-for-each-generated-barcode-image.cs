// Title: Barcode Generation with Unit Configuration and Logging
// Description: Demonstrates creating DataMatrix, Code128, and QR barcodes using Aspose.BarCode while configuring dimensions in different measurement units and logging the applied settings.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to set measurement units (pixels, millimeters, inches) and resolution (DPI) for barcode images. It uses BarcodeGenerator, BarCodeParameters, and related classes, typical for developers needing precise control over barcode size and quality in automated image creation pipelines.
// Prompt: Develop logging mechanism recording configured measurement unit, dimensions, and DPI for each generated barcode image.
// Tags: barcode, generation, logging, measurement units, dpi, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation with configurable measurement units and logs the configuration details.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates sample barcodes, logs their configuration, and saves them as PNG files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for output files
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeLogDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define sample barcode configurations with different measurement units
        var samples = new[]
        {
            new
            {
                CodeText = "Sample1",
                Encode = (BaseEncodeType)EncodeTypes.DataMatrix,
                Configure = new Action<BarcodeGenerator>(gen =>
                {
                    // Configure dimensions in pixels and set resolution
                    gen.Parameters.Barcode.XDimension.Pixels = 3f;
                    gen.Parameters.Resolution = 96f;
                    gen.Parameters.ImageWidth.Pixels = 300f;
                    gen.Parameters.ImageHeight.Pixels = 150f;
                }),
                FileName = "barcode1.png"
            },
            new
            {
                CodeText = "Sample2",
                Encode = (BaseEncodeType)EncodeTypes.Code128,
                Configure = new Action<BarcodeGenerator>(gen =>
                {
                    // Configure dimensions in millimeters and set higher resolution
                    gen.Parameters.Barcode.XDimension.Millimeters = 2f;
                    gen.Parameters.Resolution = 300f;
                    gen.Parameters.ImageWidth.Millimeters = 50f;
                    gen.Parameters.ImageHeight.Millimeters = 25f;
                }),
                FileName = "barcode2.png"
            },
            new
            {
                CodeText = "Sample3",
                Encode = (BaseEncodeType)EncodeTypes.QR,
                Configure = new Action<BarcodeGenerator>(gen =>
                {
                    // Configure dimensions in inches and set custom resolution
                    gen.Parameters.Barcode.XDimension.Inches = 0.1f;
                    gen.Parameters.Resolution = 72f;
                    gen.Parameters.ImageWidth.Inches = 2f;
                    gen.Parameters.ImageHeight.Inches = 2f;
                }),
                FileName = "barcode3.png"
            }
        };

        // Iterate over each sample, generate the barcode, log its settings, and save the image
        foreach (var sample in samples)
        {
            string filePath = Path.Combine(outputDir, sample.FileName);
            using (var generator = new BarcodeGenerator(sample.Encode, sample.CodeText))
            {
                // Apply the specific configuration for this sample
                sample.Configure(generator);

                // Log configuration details to the console
                Console.WriteLine($"Generating '{sample.FileName}':");
                Console.WriteLine($"  CodeText: {sample.CodeText}");
                Console.WriteLine($"  XDimension - Pixels: {generator.Parameters.Barcode.XDimension.Pixels} " +
                                  $"Millimeters: {generator.Parameters.Barcode.XDimension.Millimeters} " +
                                  $"Inches: {generator.Parameters.Barcode.XDimension.Inches}");
                Console.WriteLine($"  ImageWidth - Pixels: {generator.Parameters.ImageWidth.Pixels} " +
                                  $"Millimeters: {generator.Parameters.ImageWidth.Millimeters} " +
                                  $"Inches: {generator.Parameters.ImageWidth.Inches}");
                Console.WriteLine($"  ImageHeight - Pixels: {generator.Parameters.ImageHeight.Pixels} " +
                                  $"Millimeters: {generator.Parameters.ImageHeight.Millimeters} " +
                                  $"Inches: {generator.Parameters.ImageHeight.Inches}");
                Console.WriteLine($"  Resolution (DPI): {generator.Parameters.Resolution}");
                Console.WriteLine();

                // Save the generated barcode as a PNG file
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // Output the location of the saved barcode images
        Console.WriteLine($"Barcodes saved to: {outputDir}");
    }
}