// Title: Barcode generation with logging of measurement unit, dimensions, and DPI
// Description: Demonstrates creating barcodes with specific size settings and logs the configured unit, dimensions, and resolution for each image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator, AutoSizeMode, and resolution settings. Developers often need to control image size, measurement units, and DPI when generating barcodes for print or digital media; this snippet shows typical configuration and logging patterns for such scenarios.
// Prompt: Develop logging mechanism recording configured measurement unit, dimensions, and DPI for each generated barcode image.
// Tags: barcode generation, logging, measurement unit, dimensions, dpi, aspose.barcode, encode types, png output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates barcodes with specific size and resolution settings,
/// then logs the configuration details for each generated image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Iterates over predefined barcode configurations,
    /// creates each barcode, saves it as a PNG file, and records its settings.
    /// </summary>
    static void Main()
    {
        // Define a simple list of barcode configurations to process
        var configs = new[]
        {
            new { Type = EncodeTypes.Code128, Text = "ABC123", Width = 300f, Height = 150f, XDim = 2f, BarH = 40f, Dpi = 300f, Unit = "Point" },
            new { Type = EncodeTypes.QR, Text = "https://example.com", Width = 200f, Height = 200f, XDim = 3f, BarH = 0f, Dpi = 200f, Unit = "Point" }
        };

        // Process each configuration
        foreach (var cfg in configs)
        {
            // Create and configure the barcode generator for the current settings
            using (var generator = new BarcodeGenerator(cfg.Type, cfg.Text))
            {
                // Use interpolation mode so ImageWidth/ImageHeight control the final size
                generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

                // Apply dimensions using the chosen measurement unit (Point in this example)
                generator.Parameters.ImageWidth.Point = cfg.Width;
                generator.Parameters.ImageHeight.Point = cfg.Height;
                generator.Parameters.Barcode.XDimension.Point = cfg.XDim;

                // BarHeight is ignored in Interpolation mode, but set it when a positive value is provided
                if (cfg.BarH > 0f)
                {
                    generator.Parameters.Barcode.BarHeight.Point = cfg.BarH;
                }

                // Set the image resolution (dots per inch)
                generator.Parameters.Resolution = cfg.Dpi;

                // Generate a unique file name and save the barcode image as PNG
                string fileName = $"{cfg.Type}_{Guid.NewGuid()}.png";
                generator.Save(fileName);

                // Log the configuration details for the generated image
                LogBarcodeSettings(fileName, cfg.Unit, cfg.Width, cfg.Height, cfg.XDim, cfg.BarH, cfg.Dpi);
            }
        }
    }

    /// <summary>
    /// Writes a log entry containing the barcode image name, measurement unit, dimensions, X-dimension,
    /// bar height, and DPI to both the console and a persistent text file.
    /// </summary>
    /// <param name="imagePath">Full path of the generated barcode image.</param>
    /// <param name="unit">Measurement unit used for dimensions (e.g., Point).</param>
    /// <param name="width">Configured image width.</param>
    /// <param name="height">Configured image height.</param>
    /// <param name="xDim">Configured X-dimension of barcode modules.</param>
    /// <param name="barHeight">Configured bar height (if applicable).</param>
    /// <param name="dpi">Configured image resolution in dots per inch.</param>
    static void LogBarcodeSettings(string imagePath, string unit, float width, float height, float xDim, float barHeight, float dpi)
    {
        // Build a formatted log entry string
        string logEntry = $"Image: {Path.GetFileName(imagePath)} | Unit: {unit} | Width: {width}{unit} | Height: {height}{unit} | XDimension: {xDim}{unit} | BarHeight: {barHeight}{unit} | DPI: {dpi}";

        // Output the log entry to the console for immediate visibility
        Console.WriteLine(logEntry);

        // Append the log entry to a persistent log file
        using (var writer = new StreamWriter("barcode_log.txt", true))
        {
            writer.WriteLine(logEntry);
        }
    }
}