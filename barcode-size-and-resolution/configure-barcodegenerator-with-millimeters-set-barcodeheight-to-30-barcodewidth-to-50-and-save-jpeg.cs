// Title: Generate Code128 Barcode with Millimeter Dimensions and Save as JPEG
// Description: Demonstrates how to configure Aspose.BarCode's BarcodeGenerator to use millimeter units, set specific barcode height and image width, and save the result as a JPEG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of the BarcodeGenerator class together with EncodeTypes, AutoSizeMode, and BarCodeImageFormat. Developers often need to create barcodes with precise physical dimensions for printing on labels or packaging, and this snippet shows the typical steps for setting measurement units, adjusting size parameters, and exporting to common image formats.
// Prompt: Configure BarcodeGenerator with Millimeters, set BarCodeHeight to 30, BarCodeWidth to 50, and save JPEG.
// Tags: code128, barcode generation, millimeters, dimensions, jpeg, aspose.barcode, image export

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode with specific dimensions and saving it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates and saves the barcode.
    /// </summary>
    static void Main()
    {
        // Define the output file path
        string outputPath = "barcode.jpg";

        // Initialize the generator with Code128 symbology and the data to encode
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set barcode height to 30 millimeters
            generator.Parameters.Barcode.BarHeight.Millimeters = 30f;

            // Set image width to 50 millimeters
            generator.Parameters.ImageWidth.Millimeters = 50f;

            // Ensure the specified image width is applied by using nearest auto-size mode
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;

            // Save the generated barcode as a JPEG file
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}