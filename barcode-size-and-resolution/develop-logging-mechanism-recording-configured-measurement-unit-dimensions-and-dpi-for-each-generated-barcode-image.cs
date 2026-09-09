// Title: Generate barcodes with different measurement units and DPI, logging settings
// Description: This example creates barcode images using Pixels, Millimeters, and Points as measurement units, applies specific DPI values, and records the configuration for each generated image.
// Category-Description: Demonstrates Aspose.BarCode generation features such as setting XDimension in various units, configuring image resolution, and saving to PNG. Developers working with barcode rendering often need to control size and DPI for printing or screen display; this snippet shows the key API classes (BarcodeGenerator, EncodeTypes, BarCodeImageFormat) and typical logging of parameters for audit or debugging purposes.
// Prompt: Develop logging mechanism recording configured measurement unit, dimensions, and DPI for each generated barcode image.
// Tags: barcode, measurement unit, dpi, logging, generation, png, aspose.barcode, aspose.drawing, datamatrix, code128, qr

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating barcode images with different measurement units and DPI settings,
/// and logs the configuration for each generated file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes, saves them, and writes a log file with the applied settings.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for output files
        string outputDir = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Path for the log file that will record barcode generation details
        string logPath = Path.Combine(outputDir, "log.txt");

        // Define configurations: measurement unit, unit value, DPI, barcode text, and symbology
        var configs = new[]
        {
            new { UnitType = "Pixels", UnitValue = 3f, Dpi = 96f, Code = "Sample1", Encode = EncodeTypes.DataMatrix },
            new { UnitType = "Millimeters", UnitValue = 2f, Dpi = 300f, Code = "Sample2", Encode = EncodeTypes.Code128 },
            new { UnitType = "Points", UnitValue = 1.5f, Dpi = 200f, Code = "Sample3", Encode = EncodeTypes.QR }
        };

        // Iterate over each configuration, generate the barcode, and log the settings
        foreach (var cfg in configs)
        {
            // Build the full file path for the PNG image
            string filePath = Path.Combine(outputDir, $"{cfg.Code}.png");

            // Initialize the barcode generator with the specified symbology and text
            using (var generator = new BarcodeGenerator(cfg.Encode, cfg.Code))
            {
                // Apply the appropriate XDimension based on the selected measurement unit
                if (cfg.UnitType == "Pixels")
                    generator.Parameters.Barcode.XDimension.Pixels = cfg.UnitValue;
                else if (cfg.UnitType == "Millimeters")
                    generator.Parameters.Barcode.XDimension.Millimeters = cfg.UnitValue;
                else if (cfg.UnitType == "Points")
                    generator.Parameters.Barcode.XDimension.Point = cfg.UnitValue;

                // Set the image resolution (DPI)
                generator.Parameters.Resolution = cfg.Dpi;

                // Save the generated barcode as a PNG file
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Prepare a log entry with file path, unit type, unit value, and DPI
            string logEntry = $"File: {filePath}, Unit: {cfg.UnitType}, Value: {cfg.UnitValue}, DPI: {cfg.Dpi}{Environment.NewLine}";

            // Output the log entry to the console for immediate feedback
            Console.WriteLine(logEntry.Trim());

            // Append the log entry to the log file
            File.AppendAllText(logPath, logEntry);
        }

        // Inform the user where the log file has been written
        Console.WriteLine($"Log written to {logPath}");
    }
}