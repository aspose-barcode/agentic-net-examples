// Title: Decode Mailmark barcodes while ignoring quiet zones in dense images
// Description: Demonstrates configuring Aspose.BarCode's BarCodeReader to decode Mailmark barcodes, noting that quiet zones are handled internally, and shows how to adjust quality settings for densely packed images.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, focusing on Mailmark symbology. It illustrates using BarCodeReader, QualitySettings, and ComplexCodetextReader to read and parse Mailmark codes from images. Developers working with postal and logistics solutions often need to decode Mailmark barcodes in high‑density scans, requiring fine‑tuned decoding options.
// Prompt: Configure BarCodeReader to ignore quiet zones while decoding Mailmark barcodes in densely packed images.
// Tags: mailmark, barcode, decoding, quiet zones, aspose.barcode, barcodereader, qualitysettings, complexcodetext

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Mailmark barcode, reads it back,
/// and demonstrates how to configure the BarCodeReader for dense image scenarios.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Mailmark image, decodes it,
    /// and outputs the extracted information to the console.
    /// </summary>
    static void Main()
    {
        // -----------------------------------------------------------------
        // Create a temporary folder for generated files
        // -----------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "MailmarkDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "mailmark.png");

        // -----------------------------------------------------------------
        // Build a Mailmark 4‑state codetext object
        // -----------------------------------------------------------------
        var mailmark = new MailmarkCodetext
        {
            Format = 4,
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // -----------------------------------------------------------------
        // Generate the Mailmark barcode image and save it as PNG
        // -----------------------------------------------------------------
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // -----------------------------------------------------------------
        // Configure BarCodeReader for Mailmark decoding
        // Note: Aspose.BarCode handles quiet zones internally; no explicit property exists.
        // -----------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.Mailmark))
        {
            // Adjust quality settings to improve detection in densely packed images
            reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;
            reader.QualitySettings.AllowIncorrectBarcodes = true;

            // Perform the decoding operation
            BarCodeResult[] results = reader.ReadBarCodes();

            Console.WriteLine($"Barcodes detected: {results.Length}");
            foreach (var result in results)
            {
                Console.WriteLine($"Type: {result.CodeTypeName}");
                Console.WriteLine($"Text: {result.CodeText}");

                // Decode the Mailmark codetext string into its structured object
                var decoded = ComplexCodetextReader.TryDecodeMailmark(result.CodeText);
                if (decoded != null)
                {
                    Console.WriteLine($"Decoded Format: {decoded.Format}");
                    Console.WriteLine($"Decoded VersionID: {decoded.VersionID}");
                    Console.WriteLine($"Decoded Class: {decoded.Class}");
                    Console.WriteLine($"Decoded SupplychainID: {decoded.SupplychainID}");
                    Console.WriteLine($"Decoded ItemID: {decoded.ItemID}");
                    Console.WriteLine($"Decoded DestinationPostCodePlusDPS: {decoded.DestinationPostCodePlusDPS}");
                }
                Console.WriteLine();
            }
        }

        // -----------------------------------------------------------------
        // Cleanup temporary files (optional)
        // -----------------------------------------------------------------
        try
        {
            if (File.Exists(imagePath))
                File.Delete(imagePath);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}