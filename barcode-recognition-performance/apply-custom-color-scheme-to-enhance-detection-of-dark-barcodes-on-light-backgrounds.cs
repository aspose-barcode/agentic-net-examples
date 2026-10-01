// Title: Custom Color Scheme for Dark Barcode on Light Background
// Description: Demonstrates how to generate a Code128 barcode with dark bars on a light background using Aspose.BarCode, then reads it back to verify detection.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing the use of BarcodeGenerator and BarCodeReader classes. Typical use cases include creating high‑contrast barcodes for printing or display and validating them programmatically. Developers often need to adjust colors, dimensions, and image formats to meet scanning environment requirements.
// Prompt: Apply a custom color scheme to enhance detection of dark barcodes on light backgrounds.
// Tags: code128, barcode generation, png, barcodereader, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a high‑contrast barcode image and verifying its readability.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a barcode with custom colors, saves it, and reads it back.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // --------------------------------------------------------------------
        // Set up a temporary folder to store the generated barcode image.
        // --------------------------------------------------------------------
        string outputFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Define the full path for the output PNG file.
        string barcodePath = Path.Combine(outputFolder, "dark_on_light.png");

        // --------------------------------------------------------------------
        // Generate a Code128 barcode with a dark foreground on a light background.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the bar (foreground) color to black.
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

            // Set the background color to white.
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Increase the module (X) dimension for better visibility.
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Save the barcode image as a PNG file.
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode image saved to: {barcodePath}");

        // --------------------------------------------------------------------
        // Verify that the generated barcode can be detected and read.
        // --------------------------------------------------------------------
        if (File.Exists(barcodePath))
        {
            using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Detected CodeText: {result.CodeText}");
                }
            }
        }
        else
        {
            Console.WriteLine("Failed to locate the generated barcode image.");
        }
    }
}