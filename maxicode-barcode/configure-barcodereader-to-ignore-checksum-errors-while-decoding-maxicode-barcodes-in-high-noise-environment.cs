// Title: Decode MaxiCode with checksum validation disabled
// Description: Demonstrates configuring BarcodeReader to ignore checksum errors when decoding MaxiCode barcodes, useful in noisy environments.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, focusing on reading complex symbologies such as MaxiCode. It showcases key API classes like BarCodeReader, BarcodeSettings, and QualitySettings, illustrating how to adjust checksum validation and quality parameters for high‑noise scenarios. Developers working with barcode scanning in challenging conditions can use this pattern to improve detection reliability.
// Prompt: Configure BarcodeReader to ignore checksum errors while decoding MaxiCode barcodes in a high‑noise environment.
// Tags: maxicode, checksum, barcodereader, decoding, qualitysettings, aspnet, csharp

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that generates a MaxiCode barcode and reads it while ignoring checksum errors.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a MaxiCode (Mode 2) barcode, then reads it with checksum validation turned off.
    /// </summary>
    static void Main()
    {
        // Create a sample MaxiCode (Mode 2) codetext with postal, country, and service information.
        var maxiCodeData = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",
            CountryCode = 56,
            ServiceCategory = 999,
            SecondMessage = new MaxiCodeStandardSecondMessage { Message = "Test" }
        };

        // Generate the barcode image into a memory stream (PNG format).
        using (var generator = new ComplexBarcodeGenerator(maxiCodeData))
        using (var ms = new MemoryStream())
        {
            generator.Save(ms, BarCodeImageFormat.Png);
            ms.Position = 0; // Reset stream position for reading.

            // Initialize the reader for MaxiCode symbology.
            using (var reader = new BarCodeReader(ms, DecodeType.MaxiCode))
            {
                // Disable checksum validation to tolerate errors in noisy captures.
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Off;

                // Adjust quality settings to allow incorrect barcodes and speed up processing.
                reader.QualitySettings.AllowIncorrectBarcodes = true;
                reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;

                // Perform recognition and output results.
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Detected type: {result.CodeTypeName}");
                    Console.WriteLine($"Code text: {result.CodeText}");
                }
            }
        }
    }
}