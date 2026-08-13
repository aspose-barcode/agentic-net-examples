// Title: Batch decode Mailmark barcodes from Base64 images
// Description: Demonstrates how to decode multiple Mailmark barcodes supplied as Base64‑encoded PNG images and collect the decoded Mailmark objects.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, focusing on Mailmark symbology. It showcases the use of BarCodeReader with DecodeType.Mailmark and ComplexCodetextReader to extract structured Mailmark data from images. Typical scenarios include processing batches of scanned mail items, aggregating Mailmark information for tracking, and integrating barcode data into downstream systems. Developers working with bulk barcode processing, especially Mailmark, will find this pattern useful.
// Prompt: Perform batch decoding of Mailmark barcodes from a collection of base64 strings and aggregate results.
// Tags: mailmark, barcode, decoding, batch, base64, aspose.barcode, complexcodetext

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that reads a collection of Base64‑encoded PNG images,
/// decodes any Mailmark barcodes they contain, and aggregates the resulting
/// Mailmark codetext objects.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs batch decoding of Mailmark barcodes.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare a list of Base64‑encoded PNG images.
        // Replace the placeholder strings with actual barcode images as needed.
        // ------------------------------------------------------------
        var base64Images = new List<string>
        {
            // 1x1 transparent PNG (no barcode)
            "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mP8/x8AAwMCAO+XG6cAAAAASUVORK5CYII=",
            // Add more base64 strings here
        };

        // Collection that will hold successfully decoded Mailmark objects.
        var decodedResults = new List<object>();

        // ------------------------------------------------------------
        // Iterate over each Base64 string, decode it to an image,
        // and attempt to read Mailmark barcodes.
        // ------------------------------------------------------------
        foreach (var base64 in base64Images)
        {
            // Skip empty or whitespace strings.
            if (string.IsNullOrWhiteSpace(base64))
            {
                Console.WriteLine("Skipped empty base64 string.");
                continue;
            }

            // Convert the Base64 string to a byte array.
            byte[] imageBytes;
            try
            {
                imageBytes = Convert.FromBase64String(base64);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid base64 string, skipping.");
                continue;
            }

            // Use a memory stream to feed the image bytes to the barcode reader.
            using (var ms = new MemoryStream(imageBytes))
            {
                // Initialize BarCodeReader for Mailmark decoding.
                using (var reader = new BarCodeReader(ms, DecodeType.Mailmark))
                {
                    BarCodeResult[] results;
                    try
                    {
                        // Attempt to read all barcodes in the image.
                        results = reader.ReadBarCodes();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error during barcode reading: {ex.Message}");
                        continue;
                    }

                    // If no barcodes were found, move to the next image.
                    if (results.Length == 0)
                    {
                        Console.WriteLine("No barcode detected in this image.");
                        continue;
                    }

                    // Process each detected barcode.
                    foreach (var result in results)
                    {
                        Console.WriteLine($"Detected barcode type: {result.CodeTypeName}");
                        Console.WriteLine($"Raw CodeText: {result.CodeText}");

                        // Try to decode the Mailmark codetext into a structured object.
                        var mailmark = ComplexCodetextReader.TryDecodeMailmark(result.CodeText);
                        if (mailmark != null)
                        {
                            Console.WriteLine("Mailmark codetext successfully decoded.");
                            decodedResults.Add(mailmark);
                        }
                        else
                        {
                            Console.WriteLine("Failed to decode Mailmark codetext.");
                        }
                    }
                }
            }
        }

        // ------------------------------------------------------------
        // Output the total number of successfully decoded Mailmark objects.
        // ------------------------------------------------------------
        Console.WriteLine();
        Console.WriteLine($"Total Mailmark codetext objects decoded: {decodedResults.Count}");
    }
}