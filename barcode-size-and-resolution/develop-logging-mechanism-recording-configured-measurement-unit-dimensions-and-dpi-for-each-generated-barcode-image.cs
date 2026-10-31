// Title: Barcode Generation with Logging of Measurement Unit, Dimensions, and DPI
// Description: This example generates Code128 barcodes with different measurement units, saves them as PNG images, and logs each barcode's configured unit, XDimension, resolution (DPI), and resulting image size.
// Category-Description: Demonstrates Aspose.BarCode generation features, including setting XDimension in pixels or millimeters, configuring image resolution, and saving images. Typical use cases involve creating barcodes for print or digital media where precise sizing and DPI are required. Developers often need to log these settings for quality control or auditing across multiple barcode outputs.
// Prompt: Develop logging mechanism recording configured measurement unit, dimensions, and DPI for each generated barcode image.
// Tags: barcode, code128, generation, logging, measurement unit, resolution, dpi, png, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Program that generates barcodes with various measurement units and logs their configuration and image details.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes, saves images, and writes a log file with unit, dimension, DPI, and image size.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for barcode images and the log file
        string outputFolder = Path.Combine(Path.GetTempPath(), "BarcodeLog_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        string logFile = Path.Combine(outputFolder, "barcode_log.txt");

        // Define barcode configurations: text, symbology, measurement unit, and a configuration action
        var configs = new List<(string CodeText, BaseEncodeType EncodeType, string UnitName, Action<BarcodeGenerator> Configure)>
        {
            ("ABC123", EncodeTypes.Code128, "Pixels", gen =>
            {
                gen.Parameters.Barcode.XDimension.Pixels = 3f;
                gen.Parameters.Resolution = 96f;
            }),
            ("XYZ789", EncodeTypes.Code128, "Millimeters", gen =>
            {
                gen.Parameters.Barcode.XDimension.Millimeters = 2f;
                gen.Parameters.Resolution = 300f;
            }),
            ("LMN456", EncodeTypes.Code128, "Pixels", gen =>
            {
                gen.Parameters.Barcode.XDimension.Pixels = 5f;
                gen.Parameters.Resolution = 200f;
            })
        };

        int index = 1;
        foreach (var cfg in configs)
        {
            // Build the file path for the current barcode image
            string filePath = Path.Combine(outputFolder, $"barcode_{index}.png");

            // Generate the barcode with the specified configuration and save it as PNG
            using (var generator = new BarcodeGenerator(cfg.EncodeType, cfg.CodeText))
            {
                cfg.Configure(generator);
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Retrieve the dimensions of the saved image
            int imgWidth, imgHeight;
            using (Image img = Image.FromFile(filePath))
            {
                imgWidth = img.Width;
                imgHeight = img.Height;
            }

            // Build a log entry containing unit, XDimension, resolution, image size, and file path
            string logLine = $"Barcode {index}: Unit={cfg.UnitName}, XDimension={GetXDimensionValue(cfg.UnitName, cfg.Configure)}{cfg.UnitName}, Resolution={GetResolution(cfg.Configure)} DPI, ImageSize={imgWidth}x{imgHeight} pixels, File={filePath}";

            // Output the log line to console and append it to the log file
            Console.WriteLine(logLine);
            File.AppendAllText(logFile, logLine + Environment.NewLine);
            index++;
        }

        // Inform the user where the log file is located
        Console.WriteLine($"Log written to: {logFile}");
    }

    // Helper to extract XDimension value from the configuration action (re-evaluates the action)
    static float GetXDimensionValue(string unitName, Action<BarcodeGenerator> configure)
    {
        using (var gen = new BarcodeGenerator(EncodeTypes.Code128, "temp"))
        {
            configure(gen);
            return unitName == "Pixels" ? gen.Parameters.Barcode.XDimension.Pixels : gen.Parameters.Barcode.XDimension.Millimeters;
        }
    }

    // Helper to extract Resolution (DPI) from the configuration action
    static float GetResolution(Action<BarcodeGenerator> configure)
    {
        using (var gen = new BarcodeGenerator(EncodeTypes.Code128, "temp"))
        {
            configure(gen);
            return gen.Parameters.Resolution;
        }
    }
}