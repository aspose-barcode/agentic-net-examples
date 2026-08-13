// Title: Mailmark barcode generation and decoding example
// Description: Demonstrates creating a Mailmark barcode image from a MailmarkCodetext object, then decoding it back from raw image bytes.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on complex barcode symbologies such as Mailmark. It showcases the use of ComplexBarcodeGenerator for encoding and BarCodeReader with ComplexCodetextReader for decoding. Developers working with postal or logistics solutions often need to generate and read Mailmark barcodes to embed routing and tracking information.
// Prompt: Create a function accepting raw barcode image bytes and returning a populated MailmarkCodetext object.
// Tags: mailmark, barcode, generation, recognition, complexbarcode, aspnet, csharp

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates how to generate a Mailmark barcode, convert it to a byte array,
/// and decode it back into a <see cref="MailmarkCodetext"/> object using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Mailmark barcode, decodes it,
    /// and writes the decoded fields to the console.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // 1. Create a sample MailmarkCodetext instance with required fields.
        // --------------------------------------------------------------------
        var mailmark = new MailmarkCodetext
        {
            Format = 4,               // 4‑state Mailmark
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T " // trailing space required
        };

        // ---------------------------------------------------------------
        // 2. Generate a PNG image of the Mailmark barcode into a byte array.
        // ---------------------------------------------------------------
        byte[] imageBytes;
        using (var ms = new MemoryStream())
        {
            // ComplexBarcodeGenerator encodes the MailmarkCodetext into a barcode.
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                imageBytes = ms.ToArray(); // Capture the image data.
            }
        }

        // ---------------------------------------------------------------
        // 3. Decode the barcode image bytes back to a MailmarkCodetext object.
        // ---------------------------------------------------------------
        var decoded = DecodeMailmarkFromBytes(imageBytes);
        if (decoded != null)
        {
            // Output each decoded property for verification.
            Console.WriteLine($"Format: {decoded.Format}");
            Console.WriteLine($"VersionID: {decoded.VersionID}");
            Console.WriteLine($"Class: {decoded.Class}");
            Console.WriteLine($"SupplychainID: {decoded.SupplychainID}");
            Console.WriteLine($"ItemID: {decoded.ItemID}");
            Console.WriteLine($"DestinationPostCodePlusDPS: '{decoded.DestinationPostCodePlusDPS}'");
        }
        else
        {
            Console.WriteLine("Mailmark barcode could not be decoded.");
        }
    }

    /// <summary>
    /// Decodes a Mailmark barcode from raw image bytes.
    /// </summary>
    /// <param name="imageBytes">Raw image data containing a Mailmark barcode.</param>
    /// <returns>Populated <see cref="MailmarkCodetext"/> if decoding succeeds; otherwise <c>null</c>.</returns>
    static MailmarkCodetext DecodeMailmarkFromBytes(byte[] imageBytes)
    {
        // Validate input.
        if (imageBytes == null || imageBytes.Length == 0)
            throw new ArgumentException("Image bytes must be a non‑empty array.", nameof(imageBytes));

        // Load the image bytes into a memory stream for the reader.
        using (var ms = new MemoryStream(imageBytes))
        {
            // BarCodeReader scans the stream for any supported barcode types.
            using (var reader = new BarCodeReader(ms, DecodeType.AllSupportedTypes))
            {
                // Iterate through all detected barcodes.
                foreach (var result in reader.ReadBarCodes())
                {
                    // Attempt to interpret the code text as a Mailmark (4‑state) barcode.
                    var mailmark = ComplexCodetextReader.TryDecodeMailmark(result.CodeText);
                    if (mailmark != null)
                        return mailmark; // Successful decode.
                }
            }
        }

        // No Mailmark barcode was found in the provided image.
        return null;
    }
}