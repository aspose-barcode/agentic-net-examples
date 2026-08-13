// Title: Parse Mailmark CodeText with ComplexCodetextReader
// Description: Demonstrates how to generate a Mailmark barcode, read it, and decode the raw CodeText using ComplexCodetextReader.TryDecodeMailmark to extract individual fields.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode operations collection. It showcases the use of ComplexBarcodeGenerator to create a Mailmark barcode, BarCodeReader to recognize the barcode, and ComplexCodetextReader to parse the encoded data. Typical scenarios include logistics and postal applications where Mailmark symbology is used to embed structured information. Developers often need to generate, read, and decode such barcodes to integrate with tracking systems.
// Prompt: Parse raw CodeText from BarCodeReader using ComplexCodetextReader.TryDecodeMailmark to retrieve fields.
// Tags: mailmark, barcode, decoding, complexcodetext, aspose.barcode, c#, generation, recognition

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that generates a Mailmark barcode, reads it, and decodes its raw CodeText
/// using <see cref="ComplexCodetextReader.TryDecodeMailmark"/> to retrieve individual fields.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Mailmark barcode, reads it from a memory stream,
    /// and outputs the decoded fields to the console.
    /// </summary>
    static void Main()
    {
        // Create a sample Mailmark codetext object with valid values.
        var mailmark = new MailmarkCodetext
        {
            Format = 4,               // Mailmark 4‑state format
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T " // trailing space is required
        };

        // Generate the barcode image into a memory stream.
        using (var imageStream = new MemoryStream())
        {
            // Use ComplexBarcodeGenerator to encode the Mailmark data.
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                generator.Save(imageStream, BarCodeImageFormat.Png);
                imageStream.Position = 0; // Reset stream position for reading.
            }

            // Read the barcode from the generated image.
            using (var reader = new BarCodeReader(imageStream, DecodeType.Mailmark))
            {
                var results = reader.ReadBarCodes();

                // Verify that at least one barcode was detected.
                if (results.Length == 0)
                {
                    Console.WriteLine("No barcode detected.");
                    return;
                }

                // Process each detected barcode.
                foreach (var result in results)
                {
                    // Decode the raw CodeText using ComplexCodetextReader.
                    var decoded = ComplexCodetextReader.TryDecodeMailmark(result.CodeText);
                    if (decoded == null)
                    {
                        Console.WriteLine("Failed to decode Mailmark codetext.");
                        continue;
                    }

                    // Output the extracted fields.
                    Console.WriteLine($"Format: {decoded.Format}");
                    Console.WriteLine($"VersionID: {decoded.VersionID}");
                    Console.WriteLine($"Class: {decoded.Class}");
                    Console.WriteLine($"SupplychainID: {decoded.SupplychainID}");
                    Console.WriteLine($"ItemID: {decoded.ItemID}");
                    Console.WriteLine($"DestinationPostCodePlusDPS: \"{decoded.DestinationPostCodePlusDPS}\"");
                }
            }
        }
    }
}