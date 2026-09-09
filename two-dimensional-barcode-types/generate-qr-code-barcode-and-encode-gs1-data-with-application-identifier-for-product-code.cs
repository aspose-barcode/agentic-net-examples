// Title: Generate GS1 QR Code with Product Identifier
// Description: Demonstrates how to create a QR Code barcode that encodes GS1 data using the Application Identifier for a product's GTIN.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on GS1-compliant symbologies. It showcases the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to produce QR Code images that embed GS1 Application Identifiers, a common requirement for retail and supply‑chain applications where product codes must be machine‑readable.
// Prompt: Generate QR Code barcode and encode GS1 data with Application Identifier for product code.
// Tags: qr code, gs1, product code, barcode generation, aspose.barcode, encode types, png output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a GS1 QR Code containing a product GTIN.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates output folder, generates the barcode, and saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Determine the output folder path and ensure it exists
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputFolder);

        // Full path for the generated barcode image
        string outputPath = Path.Combine(outputFolder, "gs1qr.png");

        // GS1 Application Identifier (01) with a 14‑digit GTIN
        string gs1Code = "(01)00123456789012";

        // Initialize the generator for GS1 QR encoding with the specified data
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1QR, gs1Code))
        {
            // Set the X-dimension (module size) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 8f;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the location of the saved barcode image
        Console.WriteLine($"GS1 QR code saved to: {outputPath}");
    }
}