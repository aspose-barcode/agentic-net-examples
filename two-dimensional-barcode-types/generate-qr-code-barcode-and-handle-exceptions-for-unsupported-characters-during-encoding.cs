// Title: Generate QR Code with ECI Encoding and Handle Unsupported Characters
// Description: Demonstrates generating a QR Code barcode using Aspose.BarCode, first with ISO‑8859‑1 encoding which fails for non‑Latin characters, then with UTF‑8 encoding which succeeds.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR Code creation with Extended Channel Interpretation (ECI) encoding. It showcases how to configure the QR encoder, handle encoding exceptions, and produce PNG images. Developers working with international text, custom encodings, or needing robust error handling will find this pattern useful.
// Prompt: Generate a QR Code barcode and handle exceptions for unsupported characters during encoding.
// Tags: qr code, barcode generation, eci encoding, unsupported characters, exception handling, aspose.barcode, png output

using System;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates QR Code barcodes using different ECI encodings
/// and demonstrates exception handling for characters unsupported by the chosen encoding.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a QR Code with ISO‑8859‑1 (expected to fail) and then with UTF‑8 (expected to succeed).
    /// </summary>
    static void Main()
    {
        // Text containing both Latin and Chinese characters.
        string codeText = "Hello 世界";

        // --------------------------------------------------------------------
        // Attempt to generate a QR Code using ISO‑8859‑1 encoding.
        // This encoding cannot represent the Chinese characters, so an exception is expected.
        // --------------------------------------------------------------------
        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.QR))
            {
                // Enable ECI mode and set the target encoding to ISO‑8859‑1 (Latin‑1).
                generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;
                generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.ISO_8859_1;

                // Assign the text to be encoded.
                generator.CodeText = codeText;

                // Save the generated barcode image to a PNG file.
                generator.Save("qr_unsupported.png");
                Console.WriteLine("QR code generated successfully (unexpected).");
            }
        }
        catch (BarCodeException ex)
        {
            // Expected failure: characters cannot be encoded with ISO‑8859‑1.
            Console.WriteLine("Failed to generate QR code due to unsupported characters:");
            Console.WriteLine(ex.Message);
        }

        // --------------------------------------------------------------------
        // Generate the same QR Code using UTF‑8 encoding, which supports all Unicode characters.
        // This should succeed without exceptions.
        // --------------------------------------------------------------------
        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.QR))
            {
                // Enable ECI mode and set the target encoding to UTF‑8.
                generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;
                generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;

                // Assign the same text.
                generator.CodeText = codeText;

                // Save the barcode image to a PNG file.
                generator.Save("qr_utf8.png");
                Console.WriteLine("QR code generated successfully with UTF-8 encoding.");
            }
        }
        catch (BarCodeException ex)
        {
            // Any unexpected error during UTF‑8 generation will be reported here.
            Console.WriteLine("Unexpected error during UTF-8 QR generation:");
            Console.WriteLine(ex.Message);
        }
    }
}