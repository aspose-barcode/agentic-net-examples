// Title: Batch decode Mailmark barcodes from Base64 strings
// Description: Demonstrates generating Mailmark 4‑state barcodes, encoding them as Base64 strings, then decoding the collection and aggregating the extracted information.
// Category-Description: This example belongs to the Aspose.BarCode batch processing category. It showcases the use of ComplexBarcodeGenerator to create Mailmark barcodes, BarCodeReader for bulk decoding, and ComplexCodetextReader to interpret Mailmark codetext. Typical scenarios include processing large volumes of Mailmark images received from external systems, extracting shipment details, and consolidating results for reporting or further business logic.
// Prompt: Perform batch decoding of Mailmark barcodes from a collection of base64 strings and aggregate results.
// Tags: mailmark, barcode, batch decoding, base64, aspose.barcode, complexbarcode, codetext

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates batch generation, Base64 encoding, and decoding of Mailmark barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates sample Mailmark barcodes, encodes them to Base64,
    /// decodes each image, extracts key fields, and prints an aggregated summary.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Generate sample Mailmark 4‑state barcodes and collect Base64 strings
        // ------------------------------------------------------------
        List<string> base64Barcodes = new List<string>();
        for (int i = 0; i < 3; i++)
        {
            // Create Mailmark codetext with varying ItemID
            MailmarkCodetext mailmark = new MailmarkCodetext
            {
                Format = 4,
                VersionID = 1,
                Class = "0",
                SupplychainID = 384224,
                ItemID = 16563762 + i,
                DestinationPostCodePlusDPS = "EF61AH8T "
            };

            // Generate barcode image into a memory stream
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 4;
                generator.Parameters.Barcode.BarHeight.Pixels = 50;

                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    string base64 = Convert.ToBase64String(ms.ToArray());
                    base64Barcodes.Add(base64);
                }
            }
        }

        // ------------------------------------------------------------
        // 2. Batch decode the Base64 strings and collect decoded information
        // ------------------------------------------------------------
        int totalDecoded = 0;
        List<string> decodedTexts = new List<string>();

        foreach (string base64 in base64Barcodes)
        {
            byte[] imageBytes = Convert.FromBase64String(base64);
            using (var imageStream = new MemoryStream(imageBytes))
            {
                BaseDecodeType decodeType = DecodeType.Mailmark;
                try
                {
                    using (var reader = new BarCodeReader(imageStream, decodeType))
                    {
                        // Required by API to associate the image stream with the reader
                        reader.SetBarCodeImage(imageStream);

                        BarCodeResult[] results = reader.ReadBarCodes();
                        if (results != null && results.Length > 0)
                        {
                            foreach (var result in results)
                            {
                                // Decode Mailmark codetext using ComplexCodetextReader
                                MailmarkCodetext decoded = ComplexCodetextReader.TryDecodeMailmark(result.CodeText);
                                if (decoded != null)
                                {
                                    string info = $"VersionID:{decoded.VersionID}, Class:{decoded.Class}, ItemID:{decoded.ItemID}";
                                    decodedTexts.Add(info);
                                    totalDecoded++;
                                }
                            }
                        }
                    }
                }
                catch (ArgumentException)
                {
                    // Handle cases where the image cannot be processed
                    Console.WriteLine("Skipped an invalid image.");
                }
            }
        }

        // ------------------------------------------------------------
        // 3. Output aggregation results
        // ------------------------------------------------------------
        Console.WriteLine($"Total barcodes decoded: {totalDecoded}");
        for (int i = 0; i < decodedTexts.Count; i++)
        {
            Console.WriteLine($"Barcode {i + 1}: {decodedTexts[i]}");
        }
    }
}