// Title: Mailmark Barcode Generation and Decoding with Quiet Zone Considerations
// Description: Demonstrates generating a Mailmark 4‑state barcode, saving it as PNG, and decoding it while configuring quality settings to handle densely packed images where quiet zones may be minimal.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of ComplexBarcodeGenerator for creating Mailmark barcodes and BarCodeReader for decoding them. Developers working with postal and logistics solutions often need to generate Mailmark symbols and read them from images that may contain multiple barcodes close together, requiring fine‑tuned quality settings to mitigate quiet‑zone issues.
// Prompt: Configure BarCodeReader to ignore quiet zones while decoding Mailmark barcodes in densely packed images.
// Tags: mailmark, barcode generation, barcode recognition, quiet zone, deconvolution, aspnet.barcode, complexbarcode, decode, generate

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates creating a Mailmark barcode, saving it, and reading it back while adjusting quality settings to handle dense image scenarios.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Mailmark barcode, reads it, and outputs decoded fields.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo
        string tempDir = Path.Combine(Path.GetTempPath(), "MailmarkDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string imagePath = Path.Combine(tempDir, "mailmark.png");

        // Build Mailmark 4‑state codetext
        var mailmark = new MailmarkCodetext
        {
            Format = 4,
            VersionID = 1,
            Class = "1",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // Generate the Mailmark barcode image
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Set X‑dimension for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode. No public API exists to ignore quiet zones; quality settings are used instead.
        using (var reader = new BarCodeReader(imagePath, DecodeType.Mailmark))
        {
            // Adjust quality settings to improve detection in densely packed images
            reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;
            reader.QualitySettings.AllowIncorrectBarcodes = true;

            // Read all barcodes from the image
            var results = reader.ReadBarCodes();
            Console.WriteLine($"Barcodes read: {results.Length}");

            // Process each detected barcode
            foreach (var result in results)
            {
                Console.WriteLine($"Detected: {result.CodeTypeName} - {result.CodeText}");

                // Decode the Mailmark codetext string into its structured object
                var decoded = ComplexCodetextReader.TryDecodeMailmark(result.CodeText);
                if (decoded != null)
                {
                    Console.WriteLine("Decoded Mailmark fields:");
                    Console.WriteLine($"  Format: {decoded.Format}");
                    Console.WriteLine($"  VersionID: {decoded.VersionID}");
                    Console.WriteLine($"  Class: {decoded.Class}");
                    Console.WriteLine($"  SupplychainID: {decoded.SupplychainID}");
                    Console.WriteLine($"  ItemID: {decoded.ItemID}");
                    Console.WriteLine($"  DestinationPostCodePlusDPS: {decoded.DestinationPostCodePlusDPS}");
                }
                else
                {
                    Console.WriteLine("Failed to decode Mailmark codetext.");
                }
            }
        }

        // Cleanup (optional)
        try
        {
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}