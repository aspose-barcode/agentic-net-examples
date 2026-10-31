// Title: Generate QR Code with LightCoral Background
// Description: Demonstrates how to create a QR barcode with a custom LightCoral background color and save it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and drawing parameters to customize appearance. Developers often need to adjust colors, sizes, and formats when integrating barcodes into UI themes or printed materials. The snippet shows typical steps: setting background and bar colors, choosing an image format, and saving the result.
// Prompt: Apply a custom background color named “LightCoral” to match UI theme requirements.
// Tags: qr, background-color, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR barcode with a LightCoral background and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates the output folder, configures the barcode generator,
    /// applies custom colors, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define and ensure the output directory exists
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "barcode_lightcoral.png");

        // Initialize the barcode generator for QR symbology with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample Text"))
        {
            // Apply the custom LightCoral background color
            generator.Parameters.BackColor = Aspose.Drawing.Color.LightCoral;

            // Set the bar (foreground) color to black for contrast
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode generated with LightCoral background at: {outputPath}");
    }
}