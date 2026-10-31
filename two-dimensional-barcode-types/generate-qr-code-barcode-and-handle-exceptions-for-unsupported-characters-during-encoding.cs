// Title: Generate QR Code with ECI Encoding and Handle Unsupported Characters
// Description: Demonstrates creating a QR Code barcode using Aspose.BarCode with a specific ECI encoding, and shows how to catch exceptions when the text contains characters not supported by that encoding.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR Code creation with ECI (Extended Channel Interpretation) encoding. It uses the BarcodeGenerator class together with EncodeTypes.QR, QREncodeMode.ECI, and the ThrowExceptionWhenCodeTextIncorrect property. Developers often need to generate QR codes for multilingual data and must handle cases where the selected ECI encoding cannot represent certain characters, making exception handling essential.
// Prompt: Generate a QR Code barcode and handle exceptions for unsupported characters during encoding.
// Tags: qr code, eci encoding, exception handling, barcode generation, aspose.barcode, png output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates QR Code generation with ECI encoding and exception handling for unsupported characters.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates QR codes for supported and unsupported text samples and saves them as PNG files.
    /// </summary>
    static void Main()
    {
        // Use the current working directory as the output folder.
        string outputDir = Directory.GetCurrentDirectory();

        // Example with supported characters (Greek text with Greek ECI encoding).
        string supportedText = "ΑΒΓΔΕ"; // Greek letters
        string supportedPath = Path.Combine(outputDir, "qr_supported.png");
        GenerateQr(supportedText, ECIEncodings.ISO_8859_7, supportedPath, true);

        // Example with unsupported characters (Chinese characters with Greek ECI encoding).
        string unsupportedText = "汉字";
        string unsupportedPath = Path.Combine(outputDir, "qr_unsupported.png");
        GenerateQr(unsupportedText, ECIEncodings.ISO_8859_7, unsupportedPath, true);
    }

    /// <summary>
    /// Generates a QR Code image using the specified text and ECI encoding.
    /// If <paramref name="throwOnError"/> is true, an exception is thrown when the text cannot be encoded with the selected ECI.
    /// </summary>
    /// <param name="text">The text to encode in the QR Code.</param>
    /// <param name="eciEncoding">The ECI encoding to apply.</param>
    /// <param name="filePath">The full path where the PNG image will be saved.</param>
    /// <param name="throwOnError">Whether to throw an exception for unsupported characters.</param>
    static void GenerateQr(string text, ECIEncodings eciEncoding, string filePath, bool throwOnError)
    {
        try
        {
            // Initialize the barcode generator for QR Code with the provided text.
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, text))
            {
                // Enable ECI mode and set the desired ECI encoding.
                generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;
                generator.Parameters.Barcode.QR.ECIEncoding = eciEncoding;

                // Configure the generator to throw an exception if the text is invalid for the chosen encoding.
                generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = throwOnError;

                // Generate the QR Code image and save it as a PNG file.
                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    bitmap.Save(filePath, ImageFormat.Png);
                }

                Console.WriteLine($"QR code generated successfully: {filePath}");
            }
        }
        catch (Exception ex)
        {
            // Output a friendly error message when generation fails.
            Console.WriteLine($"Failed to generate QR code for text \"{text}\": {ex.Message}");
        }
    }
}