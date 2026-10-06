// Title: Generate Code 128 barcode with checksum and save as JPEG
// Description: Demonstrates how to create a Code 128 barcode, enable its checksum, and export the image as a JPEG file using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, BarcodeParameters, and image export options. Developers often need to produce barcodes for labeling, inventory, or shipping, requiring checksum validation and specific image formats. The snippet shows typical steps: setting symbology, configuring parameters, and saving the result.
// Prompt: Instantiate BarcodeParameters, enable checksum, generate a Code 128 barcode, and export it as JPEG.
// Tags: barcode, code128, checksum, jpeg, generation, aspose.barcode, aspnet

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code 128 barcode with checksum enabled and saving it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Determine the full path for the output JPEG file in the current directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "code128.jpg");

        // Initialize the barcode generator with Code128 symbology and the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Enable checksum calculation for the barcode.
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Set image resolution (dots per inch) and disable anti-aliasing for a crisp output.
            generator.Parameters.Resolution = 72f;
            generator.Parameters.UseAntiAlias = false;

            // Save the generated barcode as a JPEG file.
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}