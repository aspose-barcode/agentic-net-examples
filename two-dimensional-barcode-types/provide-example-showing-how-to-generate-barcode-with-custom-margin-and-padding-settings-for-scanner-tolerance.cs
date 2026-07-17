// Title: Generate barcode with custom margin and padding for scanner tolerance
// Description: Demonstrates how to set custom padding (margin) and image size when generating a barcode, ensuring scanner tolerance.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode appearance using the BarcodeGenerator class. It covers setting padding, auto‑size mode, image dimensions, and colors—common tasks when preparing barcodes for printing or display where scanner tolerance is required. Developers can use these settings to fine‑tune barcode layout for various output formats.
// Prompt: Provide example showing how to generate barcode with custom margin and padding settings for scanner tolerance.
// Tags: barcode, generation, margin, padding, scanner tolerance, code128, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode image with custom margin and padding settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a barcode, applies custom padding, sets image size, and saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image
        string outputPath = "custom_margin_padding.png";

        // Initialize the barcode generator with Code128 symbology and sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Configure custom padding (margin) around the barcode in points
            generator.Parameters.Barcode.Padding.Left.Point = 10f;
            generator.Parameters.Barcode.Padding.Top.Point = 15f;
            generator.Parameters.Barcode.Padding.Right.Point = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 15f;

            // Use interpolation auto‑size mode and specify the desired image dimensions
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Optional: set foreground (barcode) and background colors
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Save the generated barcode image to the specified path in PNG format
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image file was successfully created
        if (File.Exists(outputPath))
        {
            Console.WriteLine($"Barcode image saved to: {outputPath}");
        }
        else
        {
            Console.WriteLine("Failed to generate the barcode image.");
        }
    }
}