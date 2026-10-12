// Title: Batch decode Mailmark barcodes from Base64 strings
// Description: Demonstrates generating Mailmark 4‑state barcodes, converting them to Base64, decoding them in batch, and aggregating the results.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation and recognition category. It showcases the use of ComplexBarcodeGenerator for creating Mailmark barcodes, BarCodeReader for decoding, and helper classes such as MailmarkCodetext and ComplexCodetextReader. Typical scenarios include bulk processing of Mailmark images received as Base64 payloads in logistics or postal applications, where developers need to extract structured data efficiently.
// Prompt: Perform batch decoding of Mailmark barcodes from a collection of base64 strings and aggregate results.
// Tags: mailmark, barcode, batch, decoding, base64, aspose.barcode, complexbarcode

using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch decoding of Mailmark barcodes that are supplied as Base64‑encoded images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates sample Mailmark barcodes, encodes them to Base64,
    /// decodes them in a batch, and prints a summary of the operation.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // 1. Create a temporary folder for generated barcode images
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "MailmarkBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // --------------------------------------------------------------------
        // 2. Generate sample Mailmark 4‑state barcodes and save them as PNG files
        // --------------------------------------------------------------------
        List<string> imagePaths = new List<string>();
        for (int i = 0; i < 5; i++)
        {
            MailmarkCodetext mailmark = new MailmarkCodetext
            {
                Format = 4,
                VersionID = 1,
                Class = "0",
                SupplychainID = 384224,
                ItemID = 16563762 + i, // vary ItemID for each barcode
                DestinationPostCodePlusDPS = "EF61AH8T "
            };

            using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(mailmark))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 4;
                string filePath = Path.Combine(tempFolder, $"mailmark_{i}.png");
                generator.Save(filePath, BarCodeImageFormat.Png);
                imagePaths.Add(filePath);
            }
        }

        // --------------------------------------------------------------------
        // 3. Convert generated images to Base64 strings
        // --------------------------------------------------------------------
        List<string> base64Strings = new List<string>();
        foreach (string path in imagePaths)
        {
            byte[] bytes = File.ReadAllBytes(path);
            string b64 = Convert.ToBase64String(bytes);
            base64Strings.Add(b64);
        }

        // --------------------------------------------------------------------
        // 4. Batch decode the Base64‑encoded Mailmark barcodes
        // --------------------------------------------------------------------
        List<MailmarkCodetext> decodedResults = new List<MailmarkCodetext>();
        int successCount = 0;

        foreach (string b64 in base64Strings)
        {
            byte[] imgBytes;
            try
            {
                imgBytes = Convert.FromBase64String(b64);
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Invalid Base64 string: {ex.Message}");
                continue;
            }

            using (MemoryStream ms = new MemoryStream(imgBytes))
            {
                BaseDecodeType decodeType = DecodeType.Mailmark;
                using (BarCodeReader reader = new BarCodeReader(ms, decodeType))
                {
                    BarCodeResult[] results;
                    try
                    {
                        results = reader.ReadBarCodes();
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Image loading failed: {ex.Message}");
                        continue;
                    }

                    if (results.Length == 0)
                    {
                        // Mailmark recognition may return zero results in some environments.
                        Console.WriteLine("No barcode detected (expected for Mailmark).");
                        continue;
                    }

                    foreach (BarCodeResult result in results)
                    {
                        MailmarkCodetext decoded = ComplexCodetextReader.TryDecodeMailmark(result.CodeText);
                        if (decoded != null)
                        {
                            decodedResults.Add(decoded);
                            successCount++;
                            Console.WriteLine($"Decoded Mailmark - ItemID: {decoded.ItemID}, SupplychainID: {decoded.SupplychainID}");
                        }
                    }
                }
            }
        }

        // --------------------------------------------------------------------
        // 5. Output a summary of the batch operation
        // --------------------------------------------------------------------
        Console.WriteLine($"Processed {base64Strings.Count} barcode images.");
        Console.WriteLine($"Successfully decoded {successCount} Mailmark barcodes.");

        // --------------------------------------------------------------------
        // 6. Cleanup temporary files and folder
        // --------------------------------------------------------------------
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to delete temporary folder: {ex.Message}");
        }
    }
}