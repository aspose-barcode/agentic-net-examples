// Title: Generate Code 128 Barcode with Checksum and Save as JPEG
// Description: Demonstrates creating a Code 128 barcode, enabling its checksum, and exporting the result as a JPEG image using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes, configure barcode parameters such as checksum, and save the output in common image formats. Developers building labeling, inventory, or tracking solutions frequently need to generate barcodes programmatically and control format‑specific options.
// Prompt: Instantiate BarcodeParameters, enable checksum, generate a Code 128 barcode, and export it as JPEG.
// Tags: code128, barcode, checksum, jpeg, generation, aspose.barcode, encode, image

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code 128 barcode with checksum enabled
/// and saves it as a JPEG image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output JPEG file.
        string outputPath = Path.Combine(Environment.CurrentDirectory, "code128.jpg");

        // Create a BarcodeGenerator for Code 128 with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            // Enable checksum calculation for the barcode.
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Save the generated barcode image as a JPEG file.
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}