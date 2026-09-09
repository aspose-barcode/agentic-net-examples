// Title: Generate and Decode Mailmark Barcode from Image Bytes
// Description: Demonstrates creating a Mailmark 4-state barcode, converting it to a PNG byte array, and decoding it back to a MailmarkCodetext object.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation and recognition category. It showcases the use of ComplexBarcodeGenerator to create Mailmark barcodes, BarCodeReader for image decoding, and ComplexCodetextReader for extracting MailmarkCodetext. Developers commonly use these APIs to generate mail‑related barcodes for printing and later read them from scanned images or byte streams.
// Prompt: Create a function accepting raw barcode image bytes and returning a populated MailmarkCodetext object.
// Tags: mailmark, barcode, generation, recognition, png, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generation of a Mailmark barcode, conversion to a PNG byte array,
/// and decoding the image bytes back to a <see cref="MailmarkCodetext"/> instance.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Mailmark barcode, encodes it to PNG bytes,
    /// then decodes those bytes back into a <see cref="MailmarkCodetext"/> object and prints its fields.
    /// </summary>
    static void Main()
    {
        // Create a Mailmark 4‑state codetext with sample data
        var mailmark = new MailmarkCodetext
        {
            Format = 4,
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // Generate the barcode image and store it in a byte array
        byte[] imageBytes;
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Set the X‑dimension (module size) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 4;

            // Save the generated barcode to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                imageBytes = ms.ToArray();
            }
        }

        // Decode the PNG byte array back to a MailmarkCodetext instance
        var decoded = DecodeMailmarkFromImageBytes(imageBytes);
        if (decoded != null)
        {
            // Output each property of the decoded MailmarkCodetext
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

    /// <summary>
    /// Decodes a Mailmark barcode from raw PNG image bytes.
    /// </summary>
    /// <param name="imageBytes">The PNG image data containing the Mailmark barcode.</param>
    /// <returns>A populated <see cref="MailmarkCodetext"/> object if decoding succeeds; otherwise, <c>null</c>.</returns>
    static MailmarkCodetext DecodeMailmarkFromImageBytes(byte[] imageBytes)
    {
        // Write the image bytes to a temporary file because BarCodeReader works with file paths
        string tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N") + ".png");
        try
        {
            File.WriteAllBytes(tempPath, imageBytes);
            if (!File.Exists(tempPath))
                return null;

            // Use BarCodeReader to read Mailmark barcodes from the temporary image file
            using (var reader = new BarCodeReader(tempPath, DecodeType.Mailmark))
            {
                var results = reader.ReadBarCodes();
                if (results == null || results.Length == 0)
                    return null;

                // Extract the raw code text and attempt to parse it into a MailmarkCodetext object
                string codeText = results[0].CodeText;
                var mailmark = ComplexCodetextReader.TryDecodeMailmark(codeText);
                return mailmark;
            }
        }
        finally
        {
            // Clean up the temporary file, ignoring any errors during deletion
            try
            {
                if (File.Exists(tempPath))
                    File.Delete(tempPath);
            }
            catch
            {
                // ignore cleanup errors
            }
        }
    }
}