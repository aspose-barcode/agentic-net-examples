// Title: Batch decode Mailmark barcodes from Base64 strings
// Description: Demonstrates how to generate Mailmark barcodes, encode them to Base64, decode them in bulk, and aggregate the parsed results.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on Mailmark symbology. It showcases the use of BarcodeGenerator, BarCodeReader, and ComplexCodetextReader to create, encode, and decode Mailmark barcodes, a common requirement for postal and logistics applications where bulk processing of barcode data is needed.
// Prompt: Perform batch decoding of Mailmark barcodes from a collection of base64 strings and aggregate results.
// Tags: mailmark, barcode, decoding, batch, base64, aspose.barcode, generation, recognition, complexcodetext

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that generates Mailmark barcodes, encodes them as Base64 strings,
/// decodes them in a batch operation, and prints aggregated results.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs the full workflow from creation to batch decoding.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Prepare sample Mailmark codetext objects for demonstration.
        // ------------------------------------------------------------
        var samples = new List<MailmarkCodetext>
        {
            CreateMailmark(4, 1, "0", 384224, 16563762, "EF61AH8T "),
            CreateMailmark(4, 1, "1", 384225, 16563763, "EF61AH8T "),
            CreateMailmark(4, 1, "2", 384226, 16563764, "EF61AH8T ")
        };

        // ------------------------------------------------------------
        // 2. Encode each Mailmark barcode image to a Base64 string.
        // ------------------------------------------------------------
        var base64Barcodes = new List<string>();
        foreach (var mailmark in samples)
        {
            base64Barcodes.Add(EncodeMailmarkToBase64(mailmark));
        }

        // ------------------------------------------------------------
        // 3. Batch decode the Base64 strings back into Mailmark objects.
        // ------------------------------------------------------------
        var decodedResults = new List<MailmarkCodetext>();
        foreach (var base64 in base64Barcodes)
        {
            // Convert the Base64 string back to raw image bytes.
            byte[] imageBytes = Convert.FromBase64String(base64);
            using (var ms = new MemoryStream(imageBytes))
            using (var reader = new BarCodeReader(ms, DecodeType.Mailmark))
            {
                // Allow recognition of potentially imperfect barcodes.
                reader.QualitySettings.AllowIncorrectBarcodes = true;

                // Read all barcodes found in the image (normally one per image here).
                foreach (var result in reader.ReadBarCodes())
                {
                    // Decode the raw codetext into a structured Mailmark object.
                    MailmarkCodetext decoded = ComplexCodetextReader.TryDecodeMailmark(result.CodeText);
                    if (decoded != null)
                    {
                        decodedResults.Add(decoded);
                    }
                }
            }
        }

        // ------------------------------------------------------------
        // 4. Aggregate and display the decoding results.
        // ------------------------------------------------------------
        Console.WriteLine($"Total barcodes processed: {base64Barcodes.Count}");
        Console.WriteLine($"Successfully decoded: {decodedResults.Count}");
        Console.WriteLine();

        int index = 1;
        foreach (var m in decodedResults)
        {
            Console.WriteLine($"Barcode #{index++}");
            Console.WriteLine($"  Format: {m.Format}");
            Console.WriteLine($"  VersionID: {m.VersionID}");
            Console.WriteLine($"  Class: {m.Class}");
            Console.WriteLine($"  SupplychainID: {m.SupplychainID}");
            Console.WriteLine($"  ItemID: {m.ItemID}");
            Console.WriteLine($"  DestinationPostCodePlusDPS: {m.DestinationPostCodePlusDPS}");
            Console.WriteLine();
        }
    }

    /// <summary>
    /// Helper method to create a MailmarkCodetext instance with the required fields.
    /// </summary>
    /// <param name="format">Mailmark format identifier.</param>
    /// <param name="versionId">Version identifier.</param>
    /// <param name="classCode">Class code (single character).</param>
    /// <param name="supplyChainId">Supply chain identifier.</param>
    /// <param name="itemId">Item identifier.</param>
    /// <param name="destinationPostCodePlusDps">Destination postcode plus DPS.</param>
    /// <returns>Configured MailmarkCodetext object.</returns>
    private static MailmarkCodetext CreateMailmark(int format, int versionId, string classCode, int supplyChainId, int itemId, string destinationPostCodePlusDps)
    {
        var mailmark = new MailmarkCodetext
        {
            Format = format,
            VersionID = versionId,
            Class = classCode,
            SupplychainID = supplyChainId,
            ItemID = itemId,
            DestinationPostCodePlusDPS = destinationPostCodePlusDps
        };
        return mailmark;
    }

    /// <summary>
    /// Generates a Mailmark barcode image from the provided codetext and returns its Base64 representation.
    /// </summary>
    /// <param name="mailmark">Mailmark codetext object to encode.</param>
    /// <returns>Base64 string of the generated PNG barcode image.</returns>
    private static string EncodeMailmarkToBase64(MailmarkCodetext mailmark)
    {
        // Construct the raw codetext string required by the generator.
        string codetext = mailmark.GetConstructedCodetext();

        // Generate the barcode image and write it to a memory stream.
        using (var generator = new BarcodeGenerator(EncodeTypes.Mailmark, codetext))
        using (var ms = new MemoryStream())
        {
            generator.Save(ms, BarCodeImageFormat.Png);
            // Convert the image bytes to a Base64 string for transport/storage.
            return Convert.ToBase64String(ms.ToArray());
        }
    }
}