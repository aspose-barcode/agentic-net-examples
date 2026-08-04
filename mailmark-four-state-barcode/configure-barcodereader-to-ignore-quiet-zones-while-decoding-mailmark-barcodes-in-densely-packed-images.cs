// Title: Decode Mailmark Barcodes While Ignoring Quiet Zones in Dense Images
// Description: Demonstrates how to configure Aspose.BarCode's BarCodeReader to decode Mailmark (4‑state) barcodes in a densely packed image, using settings that improve detection when quiet zones are ignored.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, focusing on Mailmark symbology and quality settings. It showcases the use of BarCodeReader, DecodeType.Mailmark, and QualitySettings (DeconvolutionMode, AllowIncorrectBarcodes) to handle challenging image conditions. Developers often need to read Mailmark codes from high‑density documents where quiet zones are minimal or absent, and this snippet provides a practical pattern for such scenarios.
// Prompt: Configure BarCodeReader to ignore quiet zones while decoding Mailmark barcodes in densely packed images.
// Tags: mailmark, barcode, decoding, quiet zones, deconvolution, allowincorrectbarcodes, c#, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Provides an example of decoding Mailmark barcodes while ignoring quiet zones.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates a Mailmark barcode, then reads it using BarCodeReader
    /// with quality settings to improve detection in dense images.
    /// </summary>
    static void Main()
    {
        // Create a sample Mailmark barcode (4‑state) using ComplexBarcodeGenerator
        var mailmark = new MailmarkCodetext
        {
            Format = 4,                 // 4‑state Mailmark
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T " // trailing space is required
        };

        // Generate the barcode image into a memory stream
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        using (var imageStream = new MemoryStream())
        {
            generator.Save(imageStream, BarCodeImageFormat.Png);
            imageStream.Position = 0; // Reset stream position for reading

            // Configure BarCodeReader for Mailmark decoding
            // Note: Aspose.BarCode does not expose a property to ignore quiet zones.
            // We improve detection in dense images by enabling fast deconvolution
            // and allowing incorrect barcodes.
            using (var reader = new BarCodeReader(imageStream, DecodeType.Mailmark))
            {
                reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;
                reader.QualitySettings.AllowIncorrectBarcodes = true;

                // Read and output all detected Mailmark codes
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Detected Mailmark CodeText: {result.CodeText}");
                }
            }
        }
    }
}