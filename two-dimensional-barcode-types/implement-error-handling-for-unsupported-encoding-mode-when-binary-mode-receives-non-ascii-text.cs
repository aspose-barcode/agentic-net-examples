// Title: QR Code Generation in Binary Mode with Error Handling for Non‑ASCII Text
// Description: Demonstrates generating a QR barcode using Aspose.BarCode in Binary encoding mode and handling the exception thrown when the input contains non‑ASCII characters.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR code creation with specific encoding settings. It showcases the use of BarcodeGenerator, EncodeTypes, and QREncodeMode classes to produce QR symbols, and illustrates typical error‑handling patterns for unsupported characters in Binary mode. Developers working with barcode generation, especially those needing precise control over encoding modes, can refer to this snippet for guidance.
// Prompt: Implement error handling for unsupported encoding mode when Binary mode receives non‑ASCII text.
// Tags: qr code, binary encoding, error handling, aspose.barcode, barcode generation, c#

using System;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates QR barcode generation in Binary mode with error handling for unsupported characters.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR barcode in Binary mode using a sample non‑ASCII string and saves it to a file.
    /// </summary>
    static void Main()
    {
        // Sample non‑ASCII text that will cause an exception in Binary mode
        string nonAsciiText = "こんにちは"; // Japanese Hiragana
        string outputPath = "qr_binary.png";

        // Generate the QR barcode and handle any encoding issues
        GenerateQrBinaryBarcode(nonAsciiText, outputPath);
    }

    /// <summary>
    /// Generates a QR barcode in Binary mode. If the provided text contains characters
    /// not supported by Binary mode, the method catches the exception and reports it.
    /// </summary>
    /// <param name="text">The code text to encode.</param>
    /// <param name="filePath">File path where the barcode image will be saved.</param>
    static void GenerateQrBinaryBarcode(string text, string filePath)
    {
        // Create a BarcodeGenerator for QR codes
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Enable throwing an exception when the code text is incorrect.
            // This ensures that InvalidCodeException is raised for unsupported characters.
            generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;

            // Set Binary encoding mode.
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Binary;

            // Assign the code text (non‑ASCII in this case).
            generator.CodeText = text;

            try
            {
                // Attempt to generate and save the barcode.
                generator.Save(filePath);
                Console.WriteLine($"Barcode saved successfully to '{filePath}'.");
            }
            catch (InvalidCodeException ex)
            {
                // Specific handling for unsupported characters in Binary mode.
                Console.WriteLine("Error: The provided text contains characters not supported by Binary mode.");
                Console.WriteLine($"Exception message: {ex.Message}");
            }
            catch (BarCodeException ex)
            {
                // General Aspose.BarCode exceptions.
                Console.WriteLine("A barcode generation error occurred.");
                Console.WriteLine($"Exception message: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Fallback for any other unexpected errors.
                Console.WriteLine("An unexpected error occurred.");
                Console.WriteLine($"Exception message: {ex.Message}");
            }
        }
    }
}