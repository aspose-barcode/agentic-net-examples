// Title: Generate and Validate QR Code Barcode in a Console Application
// Description: Demonstrates how to generate a QR Code barcode using Aspose.BarCode, save it as a PNG, and validate it by reading the barcode back.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating QR Code symbology and BarCodeReader for decoding. Typical use cases include automated testing of barcode output, integration into CI pipelines, and validation of generated barcodes in unit tests. Developers often need to generate barcodes programmatically and verify their content without manual inspection.
// Prompt: Generate a QR Code barcode and integrate generation into unit test suite for validation.
// Tags: qr code,barcode generation,barcode recognition,unit test,aspose.barcode,png output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a QR Code barcode, saves it to a temporary PNG file,
/// and validates the generated barcode by reading it back using Aspose.BarCode APIs.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR Code, saves it, reads it back,
    /// and reports whether the decoded text matches the original input.
    /// </summary>
    static void Main()
    {
        // Sample data to encode into the QR Code
        const string originalText = "Hello Aspose QR!";

        // Determine a temporary file path for the generated PNG image
        string outputPath = Path.Combine(Path.GetTempPath(), "qr_test.png");

        // ------------------------------------------------------------
        // Generate QR Code and save it as a PNG image
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, originalText))
        {
            // Optional: set the QR Code error correction level to Medium (Level M)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the generated barcode to the specified file in PNG format
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Validate the generated QR Code by reading it back
        // ------------------------------------------------------------
        bool isValid = false;

        // Ensure the file was created before attempting to read it
        if (File.Exists(outputPath))
        {
            // Initialize a barcode reader for QR Code symbology
            using (var reader = new BarCodeReader(outputPath, DecodeType.QR))
            {
                // Iterate through all detected barcodes (should be only one)
                foreach (var result in reader.ReadBarCodes())
                {
                    // Compare the decoded text with the original input
                    if (result.CodeText == originalText)
                    {
                        isValid = true;
                        break;
                    }
                }
            }
        }

        // ------------------------------------------------------------
        // Report the outcome of the validation test
        // ------------------------------------------------------------
        Console.WriteLine(isValid
            ? "PASSED: QR code validated successfully."
            : "FAILED: QR code validation failed.");
    }
}