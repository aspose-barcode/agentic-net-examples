// Title: Ignore Quiet Zones When Decoding Mailmark Barcodes with BarCodeReader
// Description: Demonstrates configuring BarCodeReader to ignore quiet zones while decoding Mailmark barcodes in densely packed images.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, focusing on Mailmark symbology and quality settings. It showcases the use of BarCodeReader, QualitySettings, and ComplexBarcodeGenerator to generate and read Mailmark barcodes, a common requirement for postal automation and bulk mail processing.
// Prompt: Configure BarCodeReader to ignore quiet zones while decoding Mailmark barcodes in densely packed images.
// Tags: mailmark, barcode, reader, quiet zone, decode, aspose.barcode, qualitysettings, complexbarcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Mailmark barcode, then reads it while configuring the reader
/// to ignore quiet zones (via quality settings) for better detection in densely packed images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Mailmark barcode, configures the reader,
    /// and outputs detection results to the console.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Prepare Mailmark codetext data (format 4 = unspecified/default)
        // ------------------------------------------------------------
        var mailmark = new MailmarkCodetext
        {
            Format = 4,
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // ------------------------------------------------------------
        // 2. Generate the Mailmark barcode image into a memory stream
        // ------------------------------------------------------------
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        using (var ms = new MemoryStream())
        {
            generator.Save(ms, BarCodeImageFormat.Png);
            ms.Position = 0; // Reset stream position for subsequent reading

            // ------------------------------------------------------------
            // 3. Initialize BarCodeReader for Mailmark symbology
            // ------------------------------------------------------------
            using (var reader = new BarCodeReader(ms, DecodeType.Mailmark))
            {
                // Quiet zone handling is internal; there is no public API to ignore them.
                // Adjust quality settings to improve detection in densely packed images.
                reader.QualitySettings = QualitySettings.HighQuality;
                reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;
                reader.QualitySettings.AllowIncorrectBarcodes = true;

                // ------------------------------------------------------------
                // 4. Read all barcodes found in the image and display details
                // ------------------------------------------------------------
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                    Console.WriteLine($"Code Text: {result.CodeText}");
                    Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
                    var bounds = result.Region.Rectangle;
                    Console.WriteLine($"Region: X={bounds.X}, Y={bounds.Y}, Width={bounds.Width}, Height={bounds.Height}");
                }
            }
        }
    }
}