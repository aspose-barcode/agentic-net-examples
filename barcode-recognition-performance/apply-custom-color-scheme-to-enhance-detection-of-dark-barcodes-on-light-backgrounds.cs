// Title: Custom Color Barcode Generation and Recognition
// Description: Demonstrates generating a Code128 barcode with a dark bar color on a light background and recognizing it using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing how to customize barcode appearance with BarColor and BackColor properties and adjust reader quality settings for improved detection. It uses BarcodeGenerator, BarCodeReader, and related parameter classes, useful for developers needing high‑contrast barcodes in varied lighting conditions.
// Prompt: Apply a custom color scheme to enhance detection of dark barcodes on light backgrounds.
// Tags: barcode symbology, generation, recognition, custom colors, code128, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a barcode with a custom color scheme and then reads it back.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a Code128 barcode with dark bars on a light background, saves it as PNG,
    /// and then uses BarCodeReader to detect and display the barcode information.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string imagePath = "custom_color_barcode.png";

        // Create a barcode generator for Code128 symbology with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "DarkOnLight"))
        {
            // Set the bar (foreground) color to a dark shade.
            generator.Parameters.Barcode.BarColor = Color.DarkBlue;

            // Set the background color to a light shade for high contrast.
            generator.Parameters.BackColor = Color.LightYellow;

            // Optional: increase the module (X) dimension for better visibility.
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Save the generated barcode image in PNG format.
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image file was successfully created.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Failed to create barcode image at '{imagePath}'.");
            return;
        }

        // Initialize a barcode reader to recognize any supported barcode type in the image.
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Enhance detection settings for dark bars on a light background.
            reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;
            reader.QualitySettings.AllowIncorrectBarcodes = true;

            // Iterate through all detected barcodes and output their details.
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeType}");
                Console.WriteLine($"Detected Text: {result.CodeText}");

                // Display the region (bounding rectangle) where the barcode was found.
                var bounds = result.Region.Rectangle;
                Console.WriteLine($"Region: X={bounds.X}, Y={bounds.Y}, Width={bounds.Width}, Height={bounds.Height}");
            }
        }
    }
}