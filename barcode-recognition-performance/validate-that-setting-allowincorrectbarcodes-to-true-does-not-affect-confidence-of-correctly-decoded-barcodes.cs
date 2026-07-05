// Title: Validate AllowIncorrectBarcodes does not affect confidence of correct barcodes
// Description: Demonstrates generating a Code128 barcode, reading it with default settings and with AllowIncorrectBarcodes enabled, and comparing confidence values.
// Prompt: Validate that setting AllowIncorrectBarcodes to true does not affect confidence of correctly decoded barcodes.
// Tags: barcode symbology, operation type, output format, key api classes used

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates that enabling <c>AllowIncorrectBarcodes</c> does not change the confidence of correctly decoded barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, reads it with default and altered settings, and compares confidence values.
    /// </summary>
    static void Main()
    {
        // Define the data to encode in the barcode
        const string codeText = "1234567890";

        // Use a memory stream to avoid file I/O
        using (var ms = new MemoryStream())
        {
            // -------------------------------------------------
            // Generate a Code128 barcode and write it to the stream
            // -------------------------------------------------
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Save the barcode as a PNG image into the memory stream
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Reset the stream position so it can be read from the beginning
            ms.Position = 0;

            // -------------------------------------------------
            // Read the barcode with default settings (AllowIncorrectBarcodes = false)
            // -------------------------------------------------
            BarCodeConfidence confidenceDefault = BarCodeConfidence.None;
            using (var reader = new BarCodeReader(ms, DecodeType.Code128))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    confidenceDefault = result.Confidence;
                    Console.WriteLine($"Default AllowIncorrectBarcodes: {confidenceDefault}");
                }
            }

            // Reset the stream again for the second read operation
            ms.Position = 0;

            // -------------------------------------------------
            // Read the same barcode with AllowIncorrectBarcodes set to true
            // -------------------------------------------------
            BarCodeConfidence confidenceAllow = BarCodeConfidence.None;
            using (var reader = new BarCodeReader(ms, DecodeType.Code128))
            {
                // Enable the option that allows reading of incorrect barcodes
                reader.QualitySettings.AllowIncorrectBarcodes = true;

                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    confidenceAllow = result.Confidence;
                    Console.WriteLine($"AllowIncorrectBarcodes = true: {confidenceAllow}");
                }
            }

            // -------------------------------------------------
            // Compare the confidence values obtained from both reads
            // -------------------------------------------------
            if (confidenceDefault == confidenceAllow)
            {
                Console.WriteLine("Confidence is unchanged when AllowIncorrectBarcodes is true.");
            }
            else
            {
                Console.WriteLine("Confidence differs after setting AllowIncorrectBarcodes to true.");
            }
        }
    }
}