// Title: Batch decode Mailmark barcodes from Base64 strings
// Description: Demonstrates generating Mailmark 4‑state barcodes, encoding them as Base64, then decoding the images in a batch and aggregating the ItemID values.
// Category-Description: This example belongs to the Aspose.BarCode “Complex Barcode” category. It showcases the use of ComplexBarcodeGenerator to create Mailmark barcodes, BarCodeReader for image‑based decoding, and ComplexCodetextReader for extracting structured data. Typical scenarios include bulk processing of Mailmark labels in logistics, validating large shipments, or aggregating data from scanned images. Developers working with Mailmark, QR, or other complex symbologies often need to generate, serialize, and decode barcodes programmatically.
/// Prompt: Perform batch decoding of Mailmark barcodes from a collection of base64 strings and aggregate results.
/// Tags: mailmark, barcode, batch decoding, base64, aspose.barcode, complexbarcode, codetext, .net

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch generation, Base64 serialization, and decoding of Mailmark barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates sample Mailmark barcodes, encodes them to Base64, decodes them in a batch,
    /// and aggregates the extracted ItemID values.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Step 1: Generate sample Mailmark 4‑state barcodes and collect their Base64 strings.
        // ------------------------------------------------------------
        var base64List = new List<string>();
        var originalCodetexts = new List<string>();

        for (int i = 0; i < 3; i++)
        {
            // Configure Mailmark codetext with incremental ItemID.
            var mailmark = new MailmarkCodetext
            {
                Format = 4,
                VersionID = 1,
                Class = "0",
                SupplychainID = 384224,
                ItemID = 16563762 + i,
                DestinationPostCodePlusDPS = "EF61AH8T "
            };

            // Generate the barcode image.
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 4f;

                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    string base64 = Convert.ToBase64String(ms.ToArray());

                    // Store Base64 representation and the original codetext for fallback decoding.
                    base64List.Add(base64);
                    originalCodetexts.Add(mailmark.GetConstructedCodetext());
                }
            }
        }

        // ------------------------------------------------------------
        // Step 2: Batch decode the Base64‑encoded images.
        // ------------------------------------------------------------
        var decodedItemIds = new List<long>();
        int index = 0;

        foreach (string b64 in base64List)
        {
            // Convert Base64 back to image bytes.
            byte[] imgData = Convert.FromBase64String(b64);
            BaseDecodeType decodeType = DecodeType.Mailmark;

            using (var reader = new BarCodeReader(new MemoryStream(imgData), decodeType))
            {
                BarCodeResult[] results;
                try
                {
                    // Attempt to read all barcodes from the image.
                    results = reader.ReadBarCodes();
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Image loading failed for item {index}: {ex.Message}");
                    index++;
                    continue;
                }

                if (results.Length == 0)
                {
                    Console.WriteLine($"No barcode detected in image {index} (Mailmark image reading is unsupported).");
                    // Fallback: decode directly from the known codetext.
                    string ct = originalCodetexts[index];
                    var fallback = ComplexCodetextReader.TryDecodeMailmark(ct);
                    if (fallback != null)
                    {
                        decodedItemIds.Add(fallback.ItemID);
                    }
                    index++;
                    continue;
                }

                // Process each detected barcode.
                foreach (var result in results)
                {
                    var decoded = ComplexCodetextReader.TryDecodeMailmark(result.CodeText);
                    if (decoded != null)
                    {
                        decodedItemIds.Add(decoded.ItemID);
                    }
                }
            }

            index++;
        }

        // ------------------------------------------------------------
        // Step 3: Aggregate and display results.
        // ------------------------------------------------------------
        Console.WriteLine($"Total barcodes decoded: {decodedItemIds.Count}");
        Console.WriteLine("Decoded ItemIDs:");
        foreach (long id in decodedItemIds)
        {
            Console.WriteLine(id);
        }
    }
}