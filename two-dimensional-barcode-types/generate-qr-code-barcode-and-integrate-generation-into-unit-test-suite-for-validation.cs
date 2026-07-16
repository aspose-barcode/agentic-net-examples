// Title: Generate and Validate QR Code using Aspose.BarCode
// Description: Demonstrates creating a QR Code image and verifying its content with Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating QR Code barcodes and BarCodeReader for decoding them. Typical scenarios include automated barcode creation for marketing materials and validation in CI pipelines. Developers often need to generate barcodes programmatically and confirm their correctness using the same library.
// Prompt: Generate a QR Code barcode and integrate generation into unit test suite for validation.
// Tags: qr code, generation, validation, unit test, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Provides methods to generate a QR Code barcode, save it as an image,
/// and validate the generated barcode by decoding it.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR Code, validates it,
    /// and writes the result to the console.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the QR Code image.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr.png");

        // Generate the QR Code image with the specified text.
        GenerateQrCode(outputPath, "Hello Aspose QR!");

        // Validate the generated QR Code by reading it back.
        bool isValid = ValidateQrCode(outputPath, "Hello Aspose QR!");

        // Output the validation result.
        Console.WriteLine(isValid
            ? "QR code generation and validation succeeded."
            : "QR code validation failed.");
    }

    /// <summary>
    /// Generates a QR Code barcode and saves it to the given file path.
    /// </summary>
    /// <param name="filePath">The full path where the QR Code image will be saved.</param>
    /// <param name="text">The text to encode in the QR Code.</param>
    static void GenerateQrCode(string filePath, string text)
    {
        // Initialize the barcode generator with QR encoding and the provided text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, text))
        {
            // Set a high error correction level to improve readability under damage.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the generated barcode as a PNG image.
            generator.Save(filePath, BarCodeImageFormat.Png);
        }
    }

    /// <summary>
    /// Validates a QR Code image by decoding it and comparing the result to the expected text.
    /// </summary>
    /// <param name="filePath">The path to the QR Code image file.</param>
    /// <param name="expectedText">The text expected to be encoded in the QR Code.</param>
    /// <returns>True if the decoded text matches the expected text; otherwise, false.</returns>
    static bool ValidateQrCode(string filePath, string expectedText)
    {
        // Ensure the file exists before attempting to read it.
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"File not found: {filePath}");
            return false;
        }

        // Initialize the barcode reader for QR Code decoding.
        using (var reader = new BarCodeReader(filePath, DecodeType.QR))
        {
            // Iterate through all decoded barcodes (should be one for a QR Code image).
            foreach (var result in reader.ReadBarCodes())
            {
                // Compare the decoded text with the expected value.
                if (result.CodeText == expectedText)
                {
                    return true;
                }
            }
        }

        // Return false if no matching barcode was found.
        return false;
    }
}