// Title: Generate Barcode with Custom Margin and Padding
// Description: Demonstrates how to create a Code128 barcode image with custom padding (margin) settings to improve scanner tolerance.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to customize barcode appearance. Typical use cases include adjusting margins and module size for better readability and scanner compatibility. Developers often need to fine‑tune padding and X‑dimension when integrating barcodes into printed materials or labels.
// Prompt: Provide example showing how to generate barcode with custom margin and padding settings for scanner tolerance.
// Tags: barcode, code128, margin, padding, scanner tolerance, generation, aspnet, aspose.barcode, image, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode with custom margin and padding settings.
/// </summary>
class Program
{
    /// <summary>
    /// Generates the barcode image, saves it to a temporary folder, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the system's temporary directory
        string outputPath = Path.Combine(Path.GetTempPath(), "custom_margin_barcode.png");

        // Ensure the target directory exists before saving the image
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Initialize a barcode generator for the Code128 symbology with sample data
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Configure custom padding (margin) values to improve scanner tolerance.
            // Values are specified in points; adjust as needed for your scanner.
            generator.Parameters.Barcode.Padding.Left.Point = 15f;   // left margin
            generator.Parameters.Barcode.Padding.Top.Point = 10f;    // top margin
            generator.Parameters.Barcode.Padding.Right.Point = 15f;  // right margin
            generator.Parameters.Barcode.Padding.Bottom.Point = 10f; // bottom margin

            // Optionally adjust the module size (XDimension) for better readability.
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Save the generated barcode as a PNG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}