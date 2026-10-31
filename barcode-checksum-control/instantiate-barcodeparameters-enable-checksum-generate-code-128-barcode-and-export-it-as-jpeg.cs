// Title: Generate Code 128 Barcode with Checksum and Save as JPEG
// Description: This example creates a Code 128 barcode with checksum enabled using Aspose.BarCode, then saves it as a JPEG image.
// Category-Description: Demonstrates barcode generation using Aspose.BarCode's BarcodeGenerator and BarcodeParameters classes. Typical use cases include creating product labels, shipping tags, or any scenario requiring Code 128 symbology with checksum validation. Developers often need to configure barcode properties, enable checksums, and export the result to common image formats such as JPEG.
// Prompt: Instantiate BarcodeParameters, enable checksum, generate a Code 128 barcode, and export it as JPEG.
// Tags: code128, checksum, jpeg, barcode, generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Sample program that generates a Code 128 barcode with checksum enabled and saves it as a JPEG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory and ensure it exists.
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Build the full path for the resulting JPEG image.
        string outputPath = Path.Combine(outputDir, "code128.jpg");

        // Create a BarcodeGenerator for Code 128 symbology with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Aspose123"))
        {
            // Enable checksum calculation for the barcode.
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Save the generated barcode as a JPEG image.
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}