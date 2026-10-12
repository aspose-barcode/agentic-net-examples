// Title: Custom Color Scheme for Barcode Generation and Recognition
// Description: Demonstrates generating a Code128 barcode with a dark foreground on a light background and configuring the reader to reliably detect it.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to customize barcode colors and BarCodeReader with quality settings to handle colored backgrounds. Developers often need to adjust visual appearance for branding or improve scan reliability in varied lighting conditions, making these APIs essential for creating and reading high‑contrast barcodes.
// Prompt: Apply a custom color scheme to enhance detection of dark barcodes on light backgrounds.
// Tags: barcode symbology, generation, recognition, color scheme, code128, png, aspose.barcode, barcodegenerator, barcodeReader, qualitysettings

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates applying a custom color scheme to a barcode and reading it with enhanced settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example.
    /// Generates a barcode with custom colors, reads it, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "ColorSchemeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the file path for the generated barcode image
        string barcodePath = Path.Combine(tempFolder, "barcode.png");
        string barcodeText = "DarkOnLight";

        // -------------------------------------------------
        // Generate a Code128 barcode with custom foreground and background colors
        // -------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, barcodeText))
        {
            // Set a dark blue foreground (barcode bars)
            generator.Parameters.Barcode.BarColor = Color.FromArgb(0, 0, 139); // DarkBlue

            // Set a light yellow background
            generator.Parameters.BackColor = Color.FromArgb(255, 255, 224); // LightYellow

            // Save the barcode image as PNG
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // -------------------------------------------------
        // Read the generated barcode using enhanced recognition settings
        // -------------------------------------------------
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            // Enable complex background detection to improve recognition on colored backgrounds
            reader.QualitySettings.ComplexBackground = ComplexBackgroundMode.Enabled;

            // Perform the recognition
            var results = reader.ReadBarCodes();

            Console.WriteLine($"Barcodes read: {results.Length}");
            foreach (BarCodeResult result in reader.FoundBarCodes)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // -------------------------------------------------
        // Clean up temporary files and directories
        // -------------------------------------------------
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);

            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored – cleanup failures should not affect program exit
        }
    }
}