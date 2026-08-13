// Title: Validate AllowIncorrectBarcodes does not affect confidence of correct barcodes
// Description: Demonstrates generating a Code128 barcode, reading it with default settings and with AllowIncorrectBarcodes enabled, and confirming that confidence remains unchanged for a valid barcode.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating how to configure QualitySettings such as AllowIncorrectBarcodes. It shows usage of BarcodeGenerator, BarCodeReader, and BarCodeConfidence to compare recognition results. Developers often need to ensure that enabling tolerance for incorrect barcodes does not degrade confidence for valid codes, a common requirement in batch scanning and validation pipelines.
// Prompt: Validate that setting AllowIncorrectBarcodes to true does not affect confidence of correctly decoded barcodes.
// Tags: code128, barcode generation, barcode recognition, confidence, allowincorrectbarcodes, aspnet, aspnetcore, aspose.barcode

using System;
using System.IO;
using System.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that validates the effect of the <c>AllowIncorrectBarcodes</c> quality setting on confidence values
/// for correctly decoded Code128 barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, reads it with two different settings,
    /// and verifies that the confidence values are identical.
    /// </summary>
    static void Main()
    {
        // Define the barcode text (Code128 with a valid checksum)
        const string codeText = "1234567890";

        // Generate the barcode image and store it in a byte array
        byte[] barcodeBytes;
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                barcodeBytes = ms.ToArray();
            }
        }

        // -----------------------------------------------------------------
        // Read the barcode with default settings (AllowIncorrectBarcodes = false)
        // -----------------------------------------------------------------
        BarCodeConfidence confidenceDefault = BarCodeConfidence.None;
        using (var ms = new MemoryStream(barcodeBytes))
        {
            using (var reader = new BarCodeReader(ms, DecodeType.Code128))
            {
                var result = reader.ReadBarCodes().FirstOrDefault();
                if (result != null)
                {
                    confidenceDefault = result.Confidence;
                    Console.WriteLine($"Default AllowIncorrectBarcodes = false, Confidence = {confidenceDefault}");
                }
                else
                {
                    Console.WriteLine("No barcode detected with default settings.");
                }
            }
        }

        // -----------------------------------------------------------------
        // Read the same barcode with AllowIncorrectBarcodes set to true
        // -----------------------------------------------------------------
        BarCodeConfidence confidenceAllow = BarCodeConfidence.None;
        using (var ms = new MemoryStream(barcodeBytes))
        {
            using (var reader = new BarCodeReader(ms, DecodeType.Code128))
            {
                // Enable recognition of incorrect barcodes (should not affect a correct one)
                reader.QualitySettings.AllowIncorrectBarcodes = true;

                var result = reader.ReadBarCodes().FirstOrDefault();
                if (result != null)
                {
                    confidenceAllow = result.Confidence;
                    Console.WriteLine($"AllowIncorrectBarcodes = true, Confidence = {confidenceAllow}");
                }
                else
                {
                    Console.WriteLine("No barcode detected with AllowIncorrectBarcodes = true.");
                }
            }
        }

        // -----------------------------------------------------------------
        // Validate that the confidence values are equal and non‑zero
        // -----------------------------------------------------------------
        if (confidenceDefault == confidenceAllow && confidenceDefault != BarCodeConfidence.None)
        {
            Console.WriteLine("Validation passed: confidence is unchanged when AllowIncorrectBarcodes is true.");
        }
        else
        {
            Console.WriteLine("Validation failed: confidence differs or barcode not recognized.");
        }
    }
}