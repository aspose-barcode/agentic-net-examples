using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

class Program
{
    static void Main()
    {
        // Sample base64 strings representing barcode images.
        // In a real scenario replace these with actual base64-encoded images containing Mailmark barcodes.
        var base64Images = new List<string>
        {
            // 1x1 pixel PNG (no barcode) – used as a placeholder.
            "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mP8/x8AAwMCAO+XK2cAAAAASUVORK5CYII="
        };

        var totalImages = base64Images.Count;
        var totalBarcodesFound = 0;
        var totalMailmarkDecoded = 0;
        var decodedMailmarks = new List<MailmarkCodetext>();

        foreach (var base64 in base64Images)
        {
            // Convert base64 string to a memory stream.
            byte[] imageBytes;
            try
            {
                imageBytes = Convert.FromBase64String(base64);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid base64 string encountered; skipping.");
                continue;
            }

            using (var stream = new MemoryStream(imageBytes))
            using (var reader = new BarCodeReader(stream, DecodeType.AllSupportedTypes))
            {
                // Perform recognition.
                var results = reader.ReadBarCodes();

                if (results == null || results.Length == 0)
                {
                    Console.WriteLine("No barcodes detected in the current image.");
                    continue;
                }

                totalBarcodesFound += results.Length;

                foreach (var result in results)
                {
                    // Attempt to decode Mailmark specific codetext.
                    var mailmark = ComplexCodetextReader.TryDecodeMailmark(result.CodeText);
                    if (mailmark != null)
                    {
                        totalMailmarkDecoded++;
                        decodedMailmarks.Add(mailmark);
                        Console.WriteLine("Decoded Mailmark:");
                        Console.WriteLine($"  Format: {mailmark.Format}");
                        Console.WriteLine($"  VersionID: {mailmark.VersionID}");
                        Console.WriteLine($"  Class: {mailmark.Class}");
                        Console.WriteLine($"  SupplychainID: {mailmark.SupplychainID}");
                        Console.WriteLine($"  ItemID: {mailmark.ItemID}");
                        Console.WriteLine($"  DestinationPostCodePlusDPS: {mailmark.DestinationPostCodePlusDPS}");
                    }
                    else
                    {
                        Console.WriteLine($"Barcode detected but not a Mailmark type. CodeText: {result.CodeText}");
                    }
                }
            }
        }

        // Summary of the batch processing.
        Console.WriteLine();
        Console.WriteLine("Batch processing summary:");
        Console.WriteLine($"  Total images processed: {totalImages}");
        Console.WriteLine($"  Total barcodes found: {totalBarcodesFound}");
        Console.WriteLine($"  Total Mailmark barcodes decoded: {totalMailmarkDecoded}");
    }
}