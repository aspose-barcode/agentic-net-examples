// Title: Generate Code128 Barcode with Millimeter Dimensions and Save as JPEG
// Description: Demonstrates how to configure Aspose.BarCode's BarcodeGenerator to use millimeter units, set specific barcode height and image width, and export the result as a JPEG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to work with measurement units, size settings, and image output. It showcases key API classes such as BarcodeGenerator, EncodeTypes, BarCodeImageFormat, and AutoSizeMode. Typical use cases include creating barcodes for print media where precise physical dimensions are required, and developers often need to control size and format programmatically.
// Prompt: Configure BarcodeGenerator with Millimeters, set BarCodeHeight to 30, BarCodeWidth to 50, and save JPEG.
// Tags: barcode, code128, millimeters, dimensions, jpeg, generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Code128 barcode, configures its size using millimeter units,
/// and saves the image as a JPEG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the output folder, configures the barcode generator,
    /// and writes the resulting JPEG image to disk.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary output directory for the generated barcode image
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeExample");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Define the full path for the JPEG file to be saved
        string outputPath = Path.Combine(outputDir, "barcode.jpg");

        // Initialize the barcode generator with Code128 symbology and sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set barcode height to 30 millimeters
            generator.Parameters.Barcode.BarHeight.Millimeters = 30f;

            // Set overall image width to 50 millimeters
            generator.Parameters.ImageWidth.Millimeters = 50f;

            // Ensure the manually specified width is applied by using Nearest auto‑size mode
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;

            // Save the generated barcode as a JPEG image
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}