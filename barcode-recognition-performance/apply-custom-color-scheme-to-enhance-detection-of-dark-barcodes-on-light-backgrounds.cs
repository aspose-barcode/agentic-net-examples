// Title: Custom Color Scheme for QR Barcode Generation and Recognition
// Description: Demonstrates how to generate a QR barcode with a dark bar color on a light background and read it using complex background detection.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, illustrating the use of BarcodeGenerator and BarCodeReader with custom color settings and ComplexBackgroundMode. Developers often need to adjust barcode colors for better contrast on varied backgrounds, and this snippet shows typical API classes and settings for such scenarios.
// Prompt: Apply a custom color scheme to enhance detection of dark barcodes on light backgrounds.
// Tags: qr barcode generation, color customization, complex background detection, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates applying a custom color scheme to a QR barcode and reading it with complex background detection.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a QR barcode with dark bars on a light background, saves it, reads it back using ComplexBackgroundMode, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "ColorSchemeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the barcode image file
        string barcodePath = Path.Combine(tempFolder, "barcode.png");

        // Generate a QR barcode with custom colors (dark bars on light background)
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "SampleText"))
        {
            // Set the dark bar color (DarkBlue) and the light background color (LightYellow)
            generator.Parameters.Barcode.BarColor = Color.FromArgb(0, 0, 139); // DarkBlue
            generator.Parameters.BackColor = Color.FromArgb(255, 255, 224);   // LightYellow

            // Save the generated barcode image as PNG
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image file was successfully created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to generate barcode image.");
            return;
        }

        // Read the barcode using ComplexBackground mode to improve detection on colored backgrounds
        using (var reader = new BarCodeReader(barcodePath, DecodeType.QR))
        {
            // Enable complex background handling
            reader.QualitySettings.ComplexBackground = ComplexBackgroundMode.Enabled;

            // Perform the barcode reading operation
            var results = reader.ReadBarCodes();
            Console.WriteLine($"Barcodes read: {results.Length}");

            // Output each detected barcode's type and text
            foreach (var result in reader.FoundBarCodes)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // Clean up temporary files and directory
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program outcome
        }
    }
}