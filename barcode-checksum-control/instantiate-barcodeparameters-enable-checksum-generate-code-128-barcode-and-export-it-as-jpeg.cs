// Title: Generate Code 128 barcode with checksum and save as JPEG
// Description: Demonstrates how to create a Code 128 barcode, enable its checksum, and export the image as a JPEG file using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, BarcodeParameters, and image export APIs. Developers commonly need to generate barcodes for inventory, shipping, or labeling, configure checksum validation, and produce image files in formats like JPEG, PNG, or BMP. The snippet shows the typical workflow for creating a barcode, adjusting parameters, and saving the result.
// Prompt: Instantiate BarcodeParameters, enable checksum, generate a Code 128 barcode, and export it as JPEG.
// Tags: code128, checksum, jpeg, barcode generation, aspose.barcode, encode types

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Entry point for the barcode generation example.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a Code 128 barcode with checksum enabled and saves it as a JPEG image.
    /// </summary>
    static void Main()
    {
        // Determine the full path for the output JPEG file.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "code128.jpg");

        // Create a BarcodeGenerator for Code128 symbology with the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Enable checksum calculation for the barcode.
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Save the generated barcode image as a JPEG file.
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Output the location of the saved barcode image.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}