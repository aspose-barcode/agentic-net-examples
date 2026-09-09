// Title: Generate QR Code in Binary Mode with Error Handling
// Description: Demonstrates creating a QR code using Aspose.BarCode in Binary encoding mode and handling errors when the input contains non‑ASCII characters.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on QR code creation and encoding mode configuration. It showcases the use of BarcodeGenerator, EncodeTypes, and QREncodeMode classes to produce barcode images. Developers often need to select appropriate encoding modes (e.g., Binary, Alphanumeric) based on data content; this snippet illustrates handling unsupported characters when Binary mode is chosen.
// Prompt: Implement error handling for unsupported encoding mode when Binary mode receives non‑ASCII text.
// Tags: qr code,binary encoding,error handling,aspose.barcode,generation,barcode image

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR code in Binary encoding mode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR code from sample text and saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Sample text containing non‑ASCII characters (will trigger error in Binary mode)
        string codeText = "Hello世界";

        // Determine a temporary file path for the generated image
        string outputPath = Path.Combine(Path.GetTempPath(), "qr_binary.png");

        // Generate the QR code with the specified text and output location
        GenerateBinaryQrCode(codeText, outputPath);
    }

    /// <summary>
    /// Generates a QR code using Binary encoding mode and saves it to a file.
    /// Handles exceptions when the provided text contains characters unsupported by Binary mode.
    /// </summary>
    /// <param name="text">The data to encode in the QR code.</param>
    /// <param name="outputFile">The full path where the PNG image will be saved.</param>
    static void GenerateBinaryQrCode(string text, string outputFile)
    {
        try
        {
            // Initialize the barcode generator for QR type with the supplied text
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, text))
            {
                // Configure the QR code to use Binary encoding (does not support Unicode characters)
                generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Binary;

                // Save the generated barcode image as PNG
                generator.Save(outputFile, BarCodeImageFormat.Png);
                Console.WriteLine($"Barcode generated successfully: {outputFile}");
            }
        }
        catch (Exception ex)
        {
            // Output a friendly error message indicating unsupported characters for Binary mode
            Console.WriteLine($"Error: Unsupported encoding mode for the provided text. {ex.Message}");
        }
    }
}