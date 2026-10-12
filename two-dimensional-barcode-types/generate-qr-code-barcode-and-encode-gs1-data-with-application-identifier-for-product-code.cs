// Title: Generate GS1 QR Code with Application Identifier and decode it
// Description: Demonstrates how to create a GS1 QR Code containing product and serial information using Aspose.BarCode, save it as PNG, and then read back the encoded data.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing the use of BarcodeGenerator for GS1 QR Code creation and BarCodeReader for decoding. Developers working with GS1 data, product identification, or QR Code integration can learn how to set X-dimension, specify EncodeTypes, and handle PNG output. Typical use cases include inventory labeling, traceability, and mobile scanning solutions.
// Prompt: Generate QR Code barcode and encode GS1 data with Application Identifier for product code.
// Tags: qr code, gs1, barcode generation, barcode recognition, png, aspose.barcode, encode types, decode types

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a GS1 QR Code with Application Identifiers,
/// saves it as a PNG image, and then reads back the encoded data.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, saves it, and decodes it.
    /// </summary>
    static void Main()
    {
        // Define GS1 QR Code content with Application Identifiers:
        // (01) – GTIN (product code), (21) – serial number, (30) – quantity.
        string codeText = "(01)12345678901231(21)ASPOSE(30)9876";

        // Build the output file path in the system's temporary folder.
        string outputPath = Path.Combine(Path.GetTempPath(), "gs1qr.png");

        // --------------------------------------------------------------------
        // Generate the GS1 QR Code and save it as a PNG image.
        // --------------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1QR, codeText))
        {
            // Set the X-dimension (module size) to 8 pixels for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 8f;

            // Save the generated barcode image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode saved to: {outputPath}");

        // --------------------------------------------------------------------
        // Decode the previously generated barcode image to verify its content.
        // --------------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1QR, codeText))
        {
            // Generate the barcode image in memory and pass it to the reader.
            using (BarCodeReader reader = new BarCodeReader(generator.GenerateBarCodeImage(), DecodeType.GS1QR))
            {
                // Iterate through all detected barcodes (should be one) and output the decoded text.
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Decoded CodeText: {result.CodeText}");
                }
            }
        }
    }
}