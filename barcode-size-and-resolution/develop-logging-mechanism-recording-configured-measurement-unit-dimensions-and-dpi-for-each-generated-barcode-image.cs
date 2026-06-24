using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating barcodes with different unit settings and logging their properties.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates output directory, generates sample barcodes,
    /// and logs configuration and image details.
    /// </summary>
    static void Main()
    {
        // Ensure the output directory exists.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Example 1: Code128 barcode using point units.
        GenerateAndLog(
            symbologyName: "Code128",
            codeText: "ABC123",
            outputPath: Path.Combine(outputDir, "code128.png"),
            configure: generator =>
            {
                // Set image dimensions in points.
                generator.Parameters.ImageWidth.Point = 200f;
                generator.Parameters.ImageHeight.Point = 100f;

                // Set barcode module size and bar height in points.
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Parameters.Barcode.BarHeight.Point = 40f;

                // Set resolution (DPI) and enable interpolation mode.
                generator.Parameters.Resolution = 300f;
                generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            });

        // Example 2: QR barcode using millimeter units.
        GenerateAndLog(
            symbologyName: "QR",
            codeText: "https://example.com",
            outputPath: Path.Combine(outputDir, "qr.png"),
            configure: generator =>
            {
                // Set image dimensions in millimeters.
                generator.Parameters.ImageWidth.Millimeters = 50f;
                generator.Parameters.ImageHeight.Millimeters = 50f;

                // Set module size in millimeters.
                generator.Parameters.Barcode.XDimension.Millimeters = 0.5f;

                // QR does not use BarHeight, but set it for demonstration.
                generator.Parameters.Barcode.BarHeight.Millimeters = 10f;

                // Set resolution (DPI) and enable interpolation mode.
                generator.Parameters.Resolution = 200f;
                generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            });
    }

    /// <summary>
    /// Generates a barcode image using the specified symbology and configuration,
    /// saves it to disk, and logs both the configured settings and the actual image properties.
    /// </summary>
    /// <param name="symbologyName">Name of the barcode symbology (e.g., "Code128", "QR").</param>
    /// <param name="codeText">Text to encode in the barcode.</param>
    /// <param name="outputPath">File path where the barcode image will be saved.</param>
    /// <param name="configure">Action that applies custom configuration to the generator.</param>
    static void GenerateAndLog(string symbologyName, string codeText, string outputPath, Action<BarcodeGenerator> configure)
    {
        // Resolve the symbology name to a BaseEncodeType using reflection.
        var field = typeof(EncodeTypes).GetField(symbologyName);
        if (field == null)
        {
            Console.WriteLine($"Unknown symbology: {symbologyName}");
            return;
        }

        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // Create the barcode generator and apply custom settings.
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            configure?.Invoke(generator);

            // Save the generated barcode image.
            generator.Save(outputPath, BarCodeImageFormat.Png);

            // Log the configured parameters.
            Console.WriteLine($"--- Barcode: {symbologyName} ---");
            Console.WriteLine($"Output Path: {outputPath}");
            LogUnit("ImageWidth", generator.Parameters.ImageWidth);
            LogUnit("ImageHeight", generator.Parameters.ImageHeight);
            LogUnit("XDimension", generator.Parameters.Barcode.XDimension);
            LogUnit("BarHeight", generator.Parameters.Barcode.BarHeight);
            Console.WriteLine($"Resolution (DPI): {generator.Parameters.Resolution} dpi");
        }

        // Load the saved image to retrieve actual pixel dimensions and DPI.
        using (var image = Image.FromFile(outputPath))
        {
            Console.WriteLine($"Actual Pixel Width: {image.Width}");
            Console.WriteLine($"Actual Pixel Height: {image.Height}");
            Console.WriteLine($"Image Horizontal DPI: {image.HorizontalResolution}");
            Console.WriteLine($"Image Vertical DPI: {image.VerticalResolution}");
            Console.WriteLine();
        }
    }

    /// <summary>
    /// Logs the value of a <see cref="Unit"/> in the appropriate unit of measurement.
    /// </summary>
    /// <param name="name">Name of the property being logged.</param>
    /// <param name="unit">The unit value to log.</param>
    static void LogUnit(string name, Unit unit)
    {
        // Determine which unit member has a non‑zero value and log it.
        if (unit.Point != 0f)
            Console.WriteLine($"{name}: {unit.Point} pt");
        else if (unit.Pixels != 0f)
            Console.WriteLine($"{name}: {unit.Pixels} px");
        else if (unit.Inches != 0f)
            Console.WriteLine($"{name}: {unit.Inches} in");
        else if (unit.Millimeters != 0f)
            Console.WriteLine($"{name}: {unit.Millimeters} mm");
        else
            Console.WriteLine($"{name}: not set");
    }
}