using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

class Program
{
    static void Main(string[] args)
    {
        // Determine files to process: use command‑line arguments or fallback to sample names
        string[] files = args.Length > 0
            ? args
            : new[] { "sample1.tif", "sample2.tif", "sample3.tif" };

        foreach (string filePath in files)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            // Open the TIFF image for barcode reading, focusing on Mailmark symbology
            using (var reader = new BarCodeReader(filePath, DecodeType.Mailmark))
            {
                // Read all barcodes present in the image
                BarCodeResult[] results = reader.ReadBarCodes();

                if (results.Length == 0)
                {
                    Console.WriteLine($"No barcodes detected in: {filePath}");
                    continue;
                }

                foreach (BarCodeResult result in results)
                {
                    // Attempt to decode the Mailmark codetext into its structured fields
                    MailmarkCodetext mailmark = ComplexCodetextReader.TryDecodeMailmark(result.CodeText);

                    if (mailmark != null)
                    {
                        Console.WriteLine($"File: {filePath}");
                        Console.WriteLine($"  Format: {mailmark.Format}");
                        Console.WriteLine($"  VersionID: {mailmark.VersionID}");
                        Console.WriteLine($"  Class: {mailmark.Class}");
                        Console.WriteLine($"  SupplychainID: {mailmark.SupplychainID}");
                        Console.WriteLine($"  ItemID: {mailmark.ItemID}");
                        Console.WriteLine($"  DestinationPostCodePlusDPS: {mailmark.DestinationPostCodePlusDPS}");
                    }
                    else
                    {
                        Console.WriteLine($"File: {filePath} - Barcode found but not a decodable Mailmark. CodeText: {result.CodeText}");
                    }
                }
            }
        }
    }
}