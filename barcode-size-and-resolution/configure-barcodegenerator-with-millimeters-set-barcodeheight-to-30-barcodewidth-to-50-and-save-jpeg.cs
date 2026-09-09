// Title: Generate Code128 Barcode with Millimeter Units and Save as JPEG
// Description: Demonstrates how to configure Aspose.BarCode's BarcodeGenerator to use millimeter measurement units, set specific barcode dimensions, and export the result as a JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Typical scenarios include creating product labels, inventory tags, or any printable barcode where precise physical dimensions are required. Developers often need to control measurement units, barcode size, and output format for integration with printing workflows.
// Prompt: Configure BarcodeGenerator with Millimeters, set BarCodeHeight to 30, BarCodeWidth to 50, and save JPEG.
// Tags: code128, barcode generation, millimeters, dimensions, jpeg, aspose.barcode, image export

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code128 barcode, configures its size using millimeter units,
/// and saves the image as a JPEG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output directory in the system's temporary folder and ensure it exists.
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeOutput");
        Directory.CreateDirectory(outputDir);

        // Build the full path for the resulting JPEG file.
        string outputPath = Path.Combine(outputDir, "barcode.jpg");

        // Create a BarcodeGenerator for Code128 with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set measurement unit to millimeters (default) and specify barcode dimensions.
            generator.Parameters.Barcode.BarHeight.Millimeters = 30f;   // Height = 30 mm
            generator.Parameters.ImageWidth.Millimeters = 50f;        // Width  = 50 mm

            // Adjust the image size to the nearest possible dimensions that fit the barcode.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;

            // Save the generated barcode as a JPEG image.
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}