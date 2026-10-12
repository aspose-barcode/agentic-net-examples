// Title: Generate and Decode Mailmark Barcode from Image Bytes
// Description: Demonstrates creating a Mailmark barcode, converting it to a PNG byte array, and decoding the barcode back into a MailmarkCodetext object.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing the use of ComplexBarcodeGenerator for Mailmark symbology and BarCodeReader for decoding. Developers working with postal and logistics solutions often need to embed Mailmark barcodes in documents and later extract their data programmatically. The code illustrates typical steps: configure barcode parameters, render to an image stream, and read the codetext using the Mailmark decode type.
// Prompt: Create a function accepting raw barcode image bytes and returning a populated MailmarkCodetext object.
// Tags: mailmark, barcode, generation, recognition, aspose.barcode, complexbarcode, c#

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Mailmark barcode, converts it to a byte array,
/// and then decodes the barcode back into a <see cref="MailmarkCodetext"/> object.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Mailmark barcode, saves it to a memory stream,
    /// and decodes it from the raw image bytes.
    /// </summary>
    static void Main()
    {
        // Prepare a sample MailmarkCodetext with required fields.
        var mailmark = new MailmarkCodetext
        {
            Format = 4,
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // Use ComplexBarcodeGenerator to create the barcode image.
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Set the X-dimension (module width) in pixels for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Render the barcode into a memory stream as PNG.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                byte[] imageBytes = ms.ToArray();

                // Decode the barcode from the raw image bytes.
                MailmarkCodetext decoded = DecodeMailmarkFromImageBytes(imageBytes);

                // Output the decoded values if successful.
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
                    Console.WriteLine("Failed to decode Mailmark from image bytes.");
                }
            }
        }
    }

    /// <summary>
    /// Decodes a Mailmark barcode from a byte array containing the barcode image.
    /// </summary>
    /// <param name="imageBytes">Raw PNG image bytes of the Mailmark barcode.</param>
    /// <returns>A populated <see cref="MailmarkCodetext"/> object if decoding succeeds; otherwise, null.</returns>
    static MailmarkCodetext DecodeMailmarkFromImageBytes(byte[] imageBytes)
    {
        // BarCodeReader works with file paths, so write the bytes to a temporary file.
        string tempPath = Path.Combine(Path.GetTempPath(), "temp_mailmark_" + Guid.NewGuid().ToString("N") + ".png");
        File.WriteAllBytes(tempPath, imageBytes);

        try
        {
            // Initialize the reader for Mailmark symbology.
            using (var reader = new BarCodeReader(tempPath, DecodeType.Mailmark))
            {
                BarCodeResult[] results = reader.ReadBarCodes();

                // Verify that at least one barcode was detected.
                if (results.Length == 0)
                {
                    Console.WriteLine("No barcode detected in the provided image.");
                    return null;
                }

                // Extract the codetext and attempt to parse it into a MailmarkCodetext object.
                string codeText = results[0].CodeText;
                MailmarkCodetext decoded = ComplexCodetextReader.TryDecodeMailmark(codeText);
                return decoded;
            }
        }
        catch (Exception ex)
        {
            // Log any exceptions that occur during reading.
            Console.WriteLine($"Error during barcode reading: {ex.Message}");
            return null;
        }
        finally
        {
            // Clean up the temporary file.
            if (File.Exists(tempPath))
            {
                try { File.Delete(tempPath); } catch { /* ignore cleanup errors */ }
            }
        }
    }
}