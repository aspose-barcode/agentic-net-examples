// Title: Generate and Decode Mailmark 4-State Barcode from Image Bytes
// Description: Demonstrates creating a Mailmark 4-state barcode, saving it to a PNG byte array, and decoding the barcode back into a MailmarkCodetext object.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation and recognition category. It showcases the use of ComplexBarcodeGenerator to produce Mailmark barcodes, BarCodeReader for barcode detection, and related classes such as MailmarkCodetext and ComplexCodetextReader. Typical use cases include encoding shipping information into Mailmark symbols and extracting that data from scanned images in logistics applications.
// Prompt: Create a function accepting raw barcode image bytes and returning a populated MailmarkCodetext object.
// Tags: mailmark, barcode, generation, recognition, csharp, aspose.barcode, complexbarcode, decode, png, bytes

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Mailmark 4‑state barcode, converts it to a byte array,
/// and decodes the barcode back into a <see cref="MailmarkCodetext"/> instance.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Mailmark barcode, saves it to memory,
    /// and attempts to decode it from the resulting byte array.
    /// </summary>
    static void Main()
    {
        // Prepare a sample Mailmark 4‑state codetext with realistic values.
        var mailmark = new MailmarkCodetext
        {
            Format = 4,
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // Use ComplexBarcodeGenerator to create the barcode image in a memory stream.
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            using (var ms = new MemoryStream())
            {
                // Save the generated barcode as PNG into the memory stream.
                generator.Save(ms, BarCodeImageFormat.Png);
                byte[] imageBytes = ms.ToArray();

                // Decode the barcode from the raw image bytes.
                MailmarkCodetext decoded = DecodeMailmarkFromBytes(imageBytes);
                if (decoded != null)
                {
                    Console.WriteLine("Decoded MailmarkCodetext:");
                    Console.WriteLine($"Format: {decoded.Format}");
                    Console.WriteLine($"VersionID: {decoded.VersionID}");
                    Console.WriteLine($"Class: {decoded.Class}");
                    Console.WriteLine($"SupplychainID: {decoded.SupplychainID}");
                    Console.WriteLine($"ItemID: {decoded.ItemID}");
                    Console.WriteLine($"DestinationPostCodePlusDPS: '{decoded.DestinationPostCodePlusDPS}'");
                }
                else
                {
                    Console.WriteLine("Failed to decode Mailmark barcode from image bytes.");
                }
            }
        }
    }

    /// <summary>
    /// Decodes a Mailmark barcode from a byte array containing an image.
    /// Returns a populated <see cref="MailmarkCodetext"/> instance or null if decoding fails.
    /// </summary>
    /// <param name="imageBytes">Raw image bytes (e.g., PNG) that contain the barcode.</param>
    /// <returns>The decoded <see cref="MailmarkCodetext"/>, or null on failure.</returns>
    static MailmarkCodetext DecodeMailmarkFromBytes(byte[] imageBytes)
    {
        // Validate input.
        if (imageBytes == null || imageBytes.Length == 0)
        {
            Console.WriteLine("Image byte array is null or empty.");
            return null;
        }

        // Load the image bytes into a memory stream for the reader.
        using (var ms = new MemoryStream(imageBytes))
        {
            // Note: Mailmark 4‑state barcode recognition from images is not fully supported in Aspose.BarCode.
            // The reader may return zero results; handle this scenario gracefully.
            using (var reader = new BarCodeReader(ms, DecodeType.Mailmark))
            {
                var results = reader.ReadBarCodes();
                if (results == null || results.Length == 0)
                {
                    Console.WriteLine("No Mailmark barcode detected in the image.");
                    return null;
                }

                // Extract the raw code text from the first detected barcode.
                string codeText = results[0].CodeText;

                // Convert the raw code text into a MailmarkCodetext object.
                MailmarkCodetext decoded = ComplexCodetextReader.TryDecodeMailmark(codeText);
                return decoded;
            }
        }
    }
}