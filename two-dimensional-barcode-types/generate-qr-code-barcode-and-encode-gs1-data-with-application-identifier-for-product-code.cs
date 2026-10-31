// Title: Generate GS1 QR Code with Application Identifier (01) for product GTIN-14
// Description: This example creates a GS1 QR Code that encodes a product identifier using the (01) Application Identifier and saves it as a PNG image. It then reads the barcode to verify the encoded data.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition of GS1 QR Code symbology. The example uses BarcodeGenerator with EncodeTypes.GS1QR and BarCodeReader with DecodeType.GS1QR, common for applications that need to embed product GTINs in QR codes for inventory, logistics, and retail. Developers often need to generate GS1‑compliant barcodes and validate them programmatically.
// Prompt: Generate QR Code barcode and encode GS1 data with Application Identifier for product code.
// Tags: qr code, gs1, barcode generation, barcode recognition, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a GS1 QR Code with an Application Identifier,
/// saves it as an image, and then reads the barcode to verify its content.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates and reads a GS1 QR Code.
    /// </summary>
    static void Main()
    {
        // Define the GS1 data string with Application Identifier (01) for GTIN‑14 and (21) for a serial number.
        string gs1CodeText = "(01)12345678901231(21)ASPOSE";

        // Build the output file path in the system's temporary folder.
        string outputPath = Path.Combine(Path.GetTempPath(), "gs1qr.png");

        // Generate the GS1 QR Code and save it as a PNG image.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1QR, gs1CodeText))
        {
            // Set the X‑dimension (module size) to 8 pixels for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 8f;

            // Save the generated barcode image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"GS1 QR barcode saved to: {outputPath}");

        // Initialize a reader to decode the saved GS1 QR Code.
        using (BarCodeReader reader = new BarCodeReader(outputPath, DecodeType.GS1QR))
        {
            // Iterate through all detected barcodes (should be one) and output the decoded text.
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Decoded CodeText: {result.CodeText}");
            }
        }
    }
}