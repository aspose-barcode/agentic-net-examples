// Title: Apply custom background color to ITF14 barcode and export as JPEG
// Description: Demonstrates how to set a custom background color for an ITF14 barcode, configure its appearance, and save the result as a JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize visual properties such as background and bar colors using the BarcodeGenerator class. Typical use cases include branding, UI integration, and printing where specific color schemes are required. Developers often need to adjust colors, dimensions, and export formats when generating barcodes programmatically.
// Prompt: Apply custom background color to ITF barcodes before rendering, export JPEG.
// Tags: itf, background-color, jpeg, barcodegenerator, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates an ITF14 barcode with a custom background color and saves it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates output directory, configures barcode settings, and writes the image file.
    /// </summary>
    static void Main()
    {
        // Define a temporary folder to store the generated barcode image.
        string outputDir = Path.Combine(Path.GetTempPath(), "ITFBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Full path for the resulting JPEG file.
        string outputPath = Path.Combine(outputDir, "itf_custom_bg.jpeg");

        // The data to encode in the ITF14 barcode.
        string codeText = "12345678901231";

        // Initialize the barcode generator with ITF14 symbology and the provided text.
        using (var generator = new BarcodeGenerator(EncodeTypes.ITF14, codeText))
        {
            // Set a light blue background for the entire image.
            generator.Parameters.BackColor = Color.LightBlue;

            // Set the barcode bars to black.
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Define the X-dimension (module width) in pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Render and save the barcode as a JPEG file.
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}