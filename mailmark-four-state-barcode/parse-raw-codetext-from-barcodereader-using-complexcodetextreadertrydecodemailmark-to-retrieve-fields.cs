// Title: Parse Mailmark barcode raw CodeText using ComplexCodetextReader
// Description: Demonstrates generating a Mailmark barcode, reading it, and decoding its raw CodeText with ComplexCodetextReader.TryDecodeMailmark to extract individual fields.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation and recognition category. It showcases key API classes such as MailmarkCodetext, ComplexBarcodeGenerator, BarCodeReader, and ComplexCodetextReader. Typical use cases include creating Mailmark barcodes for logistics, reading them from images, and parsing the encoded data for downstream processing. Developers working with postal or supply‑chain solutions often need to generate Mailmark symbols and reliably extract their constituent fields.
// Prompt: Parse raw CodeText from BarCodeReader using ComplexCodetextReader.TryDecodeMailmark to retrieve fields.
// Tags: mailmark, barcode, complexbarcode, decoding, codetext, aspose.barcode

using System;
using System.IO;
using System.Text;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates Mailmark barcode generation, reading, and raw CodeText decoding using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Mailmark barcode, reads it back, decodes the raw CodeText, and prints the extracted fields.
    /// </summary>
    static void Main()
    {
        // Ensure Unicode characters are displayed correctly in the console.
        Console.OutputEncoding = Encoding.Unicode;

        // Create a unique temporary directory to store the generated barcode image.
        string tempDir = Path.Combine(Path.GetTempPath(), "MailmarkDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string imagePath = Path.Combine(tempDir, "mailmark.png");

        // Define the Mailmark codetext with required fields.
        var mailmark = new MailmarkCodetext
        {
            Format = 4,
            VersionID = 1,
            Class = "1",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // Generate the Mailmark barcode image and save it as PNG.
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was created successfully.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the generated barcode using the Mailmark decode type.
        using (var reader = new BarCodeReader(imagePath, DecodeType.Mailmark))
        {
            var results = reader.ReadBarCodes();

            // If no barcode is detected, inform the user and exit.
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected (expected for unsupported recognition).");
                return;
            }

            // Retrieve the raw CodeText from the first detected barcode.
            var rawCodeText = results[0].CodeText;

            // Decode the raw CodeText into a strongly‑typed Mailmark object.
            var decoded = ComplexCodetextReader.TryDecodeMailmark(rawCodeText);
            if (decoded == null)
            {
                Console.WriteLine("Failed to decode Mailmark codetext.");
                return;
            }

            // Output each decoded field to the console.
            Console.WriteLine($"Format: {decoded.Format}");
            Console.WriteLine($"VersionID: {decoded.VersionID}");
            Console.WriteLine($"Class: {decoded.Class}");
            Console.WriteLine($"SupplychainID: {decoded.SupplychainID}");
            Console.WriteLine($"ItemID: {decoded.ItemID}");
            Console.WriteLine($"DestinationPostCodePlusDPS: {decoded.DestinationPostCodePlusDPS}");
        }

        // Cleanup temporary files and directories.
        try
        {
            if (File.Exists(imagePath))
                File.Delete(imagePath);
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Suppress any cleanup exceptions.
        }
    }
}