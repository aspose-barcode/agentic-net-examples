// Title: Generate QR Code and handle unsupported characters
// Description: Demonstrates creating a QR Code barcode with Aspose.BarCode, setting binary encoding mode, and catching exceptions for characters that cannot be encoded.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR Code creation. It showcases the use of BarcodeGenerator, EncodeTypes, QREncodeMode, and exception handling for invalid code text. Developers often need to generate QR codes for URLs, contact info, or custom data and must handle unsupported characters gracefully.
// Prompt: Generate a QR Code barcode and handle exceptions for unsupported characters during encoding.
// Tags: qr code, barcode generation, exception handling, binary encode mode, aspose.barcode, png output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR Code barcode and handling encoding exceptions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates QR code image and writes status to console.
    /// </summary>
    static void Main()
    {
        // Define the path for the generated PNG file in the temporary directory
        string outputPath = Path.Combine(Path.GetTempPath(), "qr.png");

        // Text to encode; includes Unicode characters that may not be supported in binary mode
        string codeText = "Hello世界";

        // Initialize the barcode generator for QR Code with the specified text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Set QR encoding mode to binary to handle raw byte data
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Binary;

            // Enable throwing an exception when the code text contains unsupported characters
            generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;

            try
            {
                // Attempt to save the QR code as a PNG image
                generator.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine($"QR code generated successfully: {outputPath}");
            }
            catch (Exception ex)
            {
                // Output any errors encountered during generation (e.g., unsupported characters)
                Console.WriteLine($"Error generating QR code: {ex.Message}");
            }
        }
    }
}