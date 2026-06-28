using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generation and batch decoding of Mailmark barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates sample Mailmark barcodes, encodes them to Base64, then decodes and displays the results.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // 1. Prepare sample Mailmark data (using known‑valid values)
        // --------------------------------------------------------------------
        var mailmarkSamples = new List<MailmarkCodetext>
        {
            CreateMailmark(4, 1, "0", 384224, 16563762, "EF61AH8T "),
            CreateMailmark(4, 1, "1", 384224, 16563763, "EF61AH8T "),
            CreateMailmark(4, 1, "2", 384224, 16563764, "EF61AH8T ")
        };

        // --------------------------------------------------------------------
        // 2. Generate barcode images and collect their Base64 representations
        // --------------------------------------------------------------------
        var base64Barcodes = new List<string>();
        foreach (var mailmark in mailmarkSamples)
        {
            // Create a generator for the current Mailmark instance
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                // Write the barcode image to a memory stream in PNG format
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    // Convert the image bytes to a Base64 string for later transport
                    string base64 = Convert.ToBase64String(ms.ToArray());
                    base64Barcodes.Add(base64);
                }
            }
        }

        // --------------------------------------------------------------------
        // 3. Batch decode the Base64 strings back into Mailmark objects
        // --------------------------------------------------------------------
        var decodedResults = new List<MailmarkCodetext>();
        int successful = 0;
        foreach (var base64 in base64Barcodes)
        {
            // Convert the Base64 string back to raw image bytes
            byte[] imageBytes = Convert.FromBase64String(base64);
            using (var ms = new MemoryStream(imageBytes))
            {
                // Initialise a barcode reader configured for Mailmark decoding
                using (var reader = new BarCodeReader(ms, DecodeType.Mailmark))
                {
                    // Iterate over all detected barcodes in the image
                    foreach (var result in reader.ReadBarCodes())
                    {
                        // Attempt to decode the raw CodeText into a structured Mailmark object
                        var decoded = ComplexCodetextReader.TryDecodeMailmark(result.CodeText);
                        if (decoded != null)
                        {
                            decodedResults.Add(decoded);
                            successful++;
                        }
                    }
                }
            }
        }

        // --------------------------------------------------------------------
        // 4. Output aggregation summary and decoded Mailmark details
        // --------------------------------------------------------------------
        Console.WriteLine($"Total barcodes processed: {base64Barcodes.Count}");
        Console.WriteLine($"Successfully decoded: {successful}");
        Console.WriteLine("Decoded Mailmark details:");
        foreach (var m in decodedResults)
        {
            Console.WriteLine(
                $"  Format={m.Format}, VersionID={m.VersionID}, Class={m.Class}, " +
                $"SupplychainID={m.SupplychainID}, ItemID={m.ItemID}, " +
                $"DestinationPostCodePlusDPS={m.DestinationPostCodePlusDPS}");
        }
    }

    /// <summary>
    /// Helper method to create a <see cref="MailmarkCodetext"/> instance with the required fields.
    /// </summary>
    /// <param name="format">Mailmark format identifier.</param>
    /// <param name="versionId">Version identifier.</param>
    /// <param name="class">Class identifier (as string).</param>
    /// <param name="supplyChainId">Supply chain identifier.</param>
    /// <param name="itemId">Item identifier.</param>
    /// <param name="destinationPostCodePlusDps">Destination postcode plus DPS.</param>
    /// <returns>A populated <see cref="MailmarkCodetext"/> object.</returns>
    static MailmarkCodetext CreateMailmark(int format, int versionId, string @class, int supplyChainId, int itemId, string destinationPostCodePlusDps)
    {
        var mailmark = new MailmarkCodetext
        {
            Format = format,
            VersionID = versionId,
            Class = @class,
            SupplychainID = supplyChainId,
            ItemID = itemId,
            DestinationPostCodePlusDPS = destinationPostCodePlusDps
        };
        return mailmark;
    }
}