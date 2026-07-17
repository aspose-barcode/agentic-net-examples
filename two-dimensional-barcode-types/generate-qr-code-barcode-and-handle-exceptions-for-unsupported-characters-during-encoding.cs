// Title: Generate QR Code with ECI Encoding and Exception Handling
// Description: Demonstrates creating a QR Code barcode using Aspose.BarCode, specifying ECI encoding, and handling errors when the input contains characters unsupported by the chosen encoding.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR Code creation with custom encoding settings. It showcases the use of BarcodeGenerator, EncodeTypes, QREncodeMode, and ECIEncodings classes to control encoding behavior, a common requirement when generating barcodes for diverse character sets. Developers often need to handle unsupported characters gracefully, making this pattern useful for robust barcode generation workflows.
// Prompt: Generate a QR Code barcode and handle exceptions for unsupported characters during encoding.
// Tags: qr code, barcode generation, eci encoding, exception handling, aspnet, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code barcode using ECI encoding
/// and demonstrates handling of unsupported characters during encoding.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a QR Code, saves it to a file, and handles any encoding errors.
    /// </summary>
    static void Main()
    {
        // Output file for the QR code image
        const string outputFile = "qr.png";

        // Sample text containing a character (emoji) that is not supported by ISO-8859-1 encoding
        const string codeText = "Hello 😊";

        // Create a QR code generator with QR symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Assign the text to be encoded
            generator.CodeText = codeText;

            // Configure QR code to use ECI encoding mode with ISO-8859-1 (ASCII)
            // This encoding cannot represent the emoji, which will trigger an exception
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECIEncoding;
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.ISO_8859_1;

            // Optional: set error correction level to Medium
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            try
            {
                // Attempt to generate the QR code and save it to the specified file
                generator.Save(outputFile);
                Console.WriteLine($"QR code saved to '{outputFile}'.");
            }
            catch (BarCodeException ex)
            {
                // Handle encoding errors caused by unsupported characters
                Console.WriteLine("Failed to generate QR code: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Handle any other unexpected errors
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
            }
        }
    }
}