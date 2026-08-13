// Title: Barcode generation with logging of measurement units, dimensions, and DPI
// Description: Demonstrates creating Code128 and QR barcodes while logging their configured measurement unit, image dimensions, and resolution (DPI) to a text file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to configure barcode parameters such as XDimension, image size, and resolution using the BarcodeGenerator class. Typical use cases include generating barcodes for product labeling, inventory systems, and marketing materials where precise sizing and DPI settings are required. Developers often need to record these settings for compliance, debugging, or documentation purposes.
// Prompt: Develop logging mechanism recording configured measurement unit, dimensions, and DPI for each generated barcode image.
// Tags: barcode, code128, qr, generation, logging, dimensions, resolution, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates barcodes (Code128 and QR) and logs their configuration settings such as measurement unit, dimensions, and DPI.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates barcode images and records their settings to a log file.
    /// </summary>
    static void Main()
    {
        // Define the path for the log file and ensure it starts empty.
        string logPath = "barcode_log.txt";
        File.WriteAllText(logPath, string.Empty);

        // -------------------- Generate Code128 barcode --------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
        {
            // Configure measurement unit (points) and size parameters.
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;
            generator.Parameters.Barcode.BarHeight.Point = 40f;
            generator.Parameters.Resolution = 300f; // DPI

            // Save the barcode image to a file.
            string outputPath = "code128.png";
            generator.Save(outputPath);

            // Log the configured settings for this barcode.
            LogSettings(generator, "Code128", outputPath, logPath);
        }

        // -------------------- Generate QR barcode --------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Configure measurement unit (points) and size parameters.
            generator.Parameters.Barcode.XDimension.Point = 3f;
            generator.Parameters.ImageWidth.Point = 250f;
            generator.Parameters.ImageHeight.Point = 250f;
            generator.Parameters.Resolution = 200f; // DPI

            // Save the barcode image to a file.
            string outputPath = "qr.png";
            generator.Save(outputPath);

            // Log the configured settings for this barcode.
            LogSettings(generator, "QR", outputPath, logPath);
        }

        // Inform the user that generation is complete and where the log can be found.
        Console.WriteLine("Barcode generation completed. Log written to " + Path.GetFullPath(logPath));
    }

    /// <summary>
    /// Appends a formatted entry to the log file containing the barcode type, image path, and configured parameters.
    /// </summary>
    /// <param name="generator">The BarcodeGenerator instance containing the current settings.</param>
    /// <param name="barcodeType">A friendly name for the barcode symbology.</param>
    /// <param name="imagePath">The file path where the barcode image was saved.</param>
    /// <param name="logPath">The file path of the log file to append to.</param>
    static void LogSettings(BarcodeGenerator generator, string barcodeType, string imagePath, string logPath)
    {
        // Build a multi-line log entry with all relevant settings.
        string entry = $"Barcode Type: {barcodeType}{Environment.NewLine}" +
                       $"Image File: {imagePath}{Environment.NewLine}" +
                       $"Resolution (DPI): {generator.Parameters.Resolution}{Environment.NewLine}" +
                       $"XDimension: {generator.Parameters.Barcode.XDimension.Point} pt{Environment.NewLine}" +
                       $"Image Width: {generator.Parameters.ImageWidth.Point} pt{Environment.NewLine}" +
                       $"Image Height: {generator.Parameters.ImageHeight.Point} pt{Environment.NewLine}" +
                       $"Bar Height: {generator.Parameters.Barcode.BarHeight.Point} pt{Environment.NewLine}" +
                       $"---{Environment.NewLine}";

        // Append the entry to the log file.
        File.AppendAllText(logPath, entry);
    }
}