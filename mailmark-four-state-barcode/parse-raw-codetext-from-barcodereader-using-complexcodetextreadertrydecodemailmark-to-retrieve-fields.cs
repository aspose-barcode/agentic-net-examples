// Title: Parse Mailmark barcode raw CodeText using ComplexCodetextReader
// Description: Demonstrates generating a Mailmark barcode, reading it, and parsing the raw CodeText with ComplexCodetextReader.TryDecodeMailmark to extract individual fields.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on complex barcode types such as Mailmark. It showcases the use of ComplexBarcodeGenerator, BarCodeReader, and ComplexCodetextReader to create a Mailmark barcode, read it from an image, and decode its structured codetext. Developers working with postal or logistics solutions often need to generate and parse Mailmark barcodes to retrieve routing and item information.
// Prompt: Parse raw CodeText from BarCodeReader using ComplexCodetextReader.TryDecodeMailmark to retrieve fields.
// Tags: mailmark, barcode, generation, recognition, complexcodetext, decoding, aspnet, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generating a Mailmark barcode, reading it, and decoding its raw CodeText using <see cref="ComplexCodetextReader"/>.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the sample image
        string tempFolder = Path.Combine(Path.GetTempPath(), "MailmarkDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "mailmark.png");

        // Build a Mailmark 4‑state codetext object with required fields
        var mailmark = new MailmarkCodetext
        {
            Format = 4,
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // Generate the barcode image and save it as PNG
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Read the barcode from the saved image and obtain the raw CodeText
        using (var reader = new BarCodeReader(imagePath, DecodeType.Mailmark))
        {
            var results = reader.ReadBarCodes();
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
                return;
            }

            string rawCodeText = results[0].CodeText;
            Console.WriteLine($"Raw CodeText: {rawCodeText}");

            // Decode the raw CodeText into a strongly‑typed MailmarkCodetext object
            MailmarkCodetext decoded = ComplexCodetextReader.TryDecodeMailmark(rawCodeText);
            if (decoded == null)
            {
                Console.WriteLine("Failed to parse Mailmark codetext.");
                return;
            }

            // Output the individual fields of the decoded Mailmark
            Console.WriteLine("Decoded Mailmark fields:");
            Console.WriteLine($"Format: {decoded.Format}");
            Console.WriteLine($"VersionID: {decoded.VersionID}");
            Console.WriteLine($"Class: {decoded.Class}");
            Console.WriteLine($"SupplychainID: {decoded.SupplychainID}");
            Console.WriteLine($"ItemID: {decoded.ItemID}");
            Console.WriteLine($"DestinationPostCodePlusDPS: {decoded.DestinationPostCodePlusDPS}");
        }

        // Clean up temporary files
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored – cleanup failure should not affect program exit
        }
    }
}