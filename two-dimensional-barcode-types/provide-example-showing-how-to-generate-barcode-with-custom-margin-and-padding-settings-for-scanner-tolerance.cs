// Title: Generate Barcode with Custom Margin and Padding
// Description: Demonstrates how to create a Code128 barcode image with custom margin and padding settings to improve scanner tolerance.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and barcode parameter objects such as XDimension, Padding, and Border. Developers often need to adjust margins and padding to meet scanner requirements or to create visual space around the barcode. The snippet shows typical steps for configuring these parameters and saving the result as an image.
// Prompt: Provide example showing how to generate barcode with custom margin and padding settings for scanner tolerance.
// Tags: barcode, code128, margin, padding, scanner tolerance, generation, aspose.barcode, image, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code128 barcode with custom margin and padding settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a temporary output folder, configures barcode parameters,
    /// saves the barcode image, and writes the file path to the console.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Create a unique temporary directory for the output image
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeExample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define the full path for the generated PNG file
        string filePath = Path.Combine(outputDir, "custom_margin_padding.png");

        // Initialize the barcode generator with Code128 symbology and sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the module (X) dimension to control barcode size
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Apply custom padding on all sides to increase scanner tolerance
            generator.Parameters.Barcode.Padding.Left.Pixels = 20f;
            generator.Parameters.Barcode.Padding.Top.Pixels = 20f;
            generator.Parameters.Barcode.Padding.Right.Pixels = 20f;
            generator.Parameters.Barcode.Padding.Bottom.Pixels = 20f;

            // Optional: make a visible border to illustrate the effect of padding
            generator.Parameters.Border.Visible = true;
            generator.Parameters.Border.Width.Pixels = 2f;

            // Save the barcode as a PNG image to the specified path
            generator.Save(filePath, BarCodeImageFormat.Png);
        }

        // Output the location of the saved barcode image
        Console.WriteLine($"Barcode image saved to: {filePath}");
    }
}