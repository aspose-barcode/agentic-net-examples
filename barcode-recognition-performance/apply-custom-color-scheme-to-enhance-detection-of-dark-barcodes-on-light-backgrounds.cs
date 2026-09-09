// Title: Custom Color Scheme for QR Barcodes on Light Backgrounds
// Description: Demonstrates generating a QR barcode with a dark bar color on a light background and reading it using Aspose.BarCode with enhanced background detection.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator, BarCodeReader, and QualitySettings to handle colored backgrounds, a common requirement when integrating barcodes into visually rich documents or UI designs. Developers often need to customize bar and background colors and adjust detection settings for reliable scanning.
// Prompt: Apply a custom color scheme to enhance detection of dark barcodes on light backgrounds.
// Tags: qr, color, generation, recognition, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates applying a custom color scheme to a QR barcode and reading it with enhanced background detection.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR barcode with navy bars on a light yellow background,
    /// saves it as PNG, reads it back using complex background detection, and outputs the results.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the generated barcode image
        string barcodePath = Path.Combine(tempFolder, "barcode.png");

        // Generate a QR barcode with custom dark bar color on a light background
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "DarkOnLight"))
        {
            // Set the bar (foreground) color to Navy
            generator.Parameters.Barcode.BarColor = Color.Navy;
            // Set the background color to LightYellow
            generator.Parameters.BackColor = Color.LightYellow;

            // Save the barcode image as a PNG file
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image file was successfully created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to generate barcode image.");
            return;
        }

        // Read the barcode using the generated image and enable complex background detection
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.QR))
        {
            // Enable detection of barcodes on colored or complex backgrounds
            reader.QualitySettings.ComplexBackground = ComplexBackgroundMode.Enabled;

            // Perform the barcode recognition
            BarCodeResult[] results = reader.ReadBarCodes();

            // Output the number of barcodes detected and their details
            Console.WriteLine($"Barcodes read: {results.Length}");
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // Clean up temporary files (optional)
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore any errors that occur during cleanup
        }
    }
}