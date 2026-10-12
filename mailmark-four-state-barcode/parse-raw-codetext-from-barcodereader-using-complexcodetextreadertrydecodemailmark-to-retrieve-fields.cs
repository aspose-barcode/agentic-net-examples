// Title: Parse Mailmark barcode and decode raw CodeText using ComplexCodetextReader
// Description: Demonstrates generating a Mailmark barcode, reading it back, and extracting its fields by parsing the raw CodeText with ComplexCodetextReader.TryDecodeMailmark.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on complex barcode types such as Mailmark. It showcases the use of ComplexBarcodeGenerator for creating a Mailmark barcode, BarCodeReader for detection, and ComplexCodetextReader for parsing raw codetext into a MailmarkCodetext object. Developers working with postal or logistics solutions often need to generate and decode Mailmark barcodes to exchange item and routing information.
// Prompt: Parse raw CodeText from BarCodeReader using ComplexCodetextReader.TryDecodeMailmark to retrieve fields.
// Tags: mailmark, barcode, generation, recognition, complexcodetextreader, decoding, csharp, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Mailmark barcode, reading it, and decoding its raw CodeText into individual fields.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a temporary Mailmark barcode image, reads it, decodes the codetext, outputs the fields, and cleans up.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary file path for the generated Mailmark barcode image
        string imagePath = Path.Combine(Path.GetTempPath(), "MailmarkSample.png");

        // Create Mailmark codetext with required fields
        var mailmark = new MailmarkCodetext
        {
            Format = 4,                         // 4-state Mailmark
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T " // exact valid value with trailing space
        };

        // Generate the Mailmark barcode image
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify the image file exists before attempting to read
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode and decode the raw CodeText using ComplexCodetextReader
        using (var reader = new BarCodeReader(imagePath, DecodeType.Mailmark))
        {
            BarCodeResult[] results = reader.ReadBarCodes();
            if (results.Length == 0)
            {
                Console.WriteLine("No Mailmark barcode detected (recognition may be unsupported).");
                return;
            }

            // Parse the raw CodeText into a MailmarkCodetext object
            MailmarkCodetext decoded = ComplexCodetextReader.TryDecodeMailmark(results[0].CodeText);
            if (decoded == null)
            {
                Console.WriteLine("Failed to decode Mailmark codetext.");
                return;
            }

            // Output the decoded fields
            Console.WriteLine($"Format: {decoded.Format}");
            Console.WriteLine($"VersionID: {decoded.VersionID}");
            Console.WriteLine($"Class: {decoded.Class}");
            Console.WriteLine($"SupplychainID: {decoded.SupplychainID}");
            Console.WriteLine($"ItemID: {decoded.ItemID}");
            Console.WriteLine($"DestinationPostCodePlusDPS: {decoded.DestinationPostCodePlusDPS}");
        }

        // Clean up the temporary image file
        try
        {
            File.Delete(imagePath);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}