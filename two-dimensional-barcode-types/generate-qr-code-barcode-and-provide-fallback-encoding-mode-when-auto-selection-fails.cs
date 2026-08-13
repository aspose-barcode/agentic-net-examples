// Title: Generate QR Code with fallback encoding mode
// Description: Demonstrates creating a QR Code barcode, using Auto encoding mode and falling back to Binary mode when auto selection fails.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure QR Code encoding modes via the BarcodeGenerator and QREncodeMode classes. Typical use cases include handling Unicode text where automatic mode selection may not succeed, requiring a manual fallback. Developers often need to switch encoding modes programmatically to ensure reliable barcode creation across diverse data sets.
// Prompt: Generate a QR Code barcode and provide fallback encoding mode when auto selection fails.
// Tags: qr code, fallback encoding, auto mode, binary mode, aspose.barcode, barcode generation, csharp

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Entry point for the QR Code generation example demonstrating fallback encoding mode.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a QR Code barcode using Auto mode, and if it fails, retries with Binary mode.
    /// </summary>
    static void Main()
    {
        // Sample text containing Unicode characters.
        const string codeText = "Sample文字";

        // Initialize a QR Code generator with the desired symbology.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Assign the text to be encoded.
            generator.CodeText = codeText;

            // Set the primary encoding mode to Auto.
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Auto;

            try
            {
                // Attempt to save the barcode using Auto mode.
                generator.Save("qr_auto.png");
                Console.WriteLine("QR code saved with Auto mode: qr_auto.png");
            }
            catch (Exception ex)
            {
                // Auto mode failed; log the error and switch to Binary mode as a fallback.
                Console.WriteLine($"Auto mode failed ({ex.Message}). Switching to Binary mode.");

                // Update the encoding mode to Binary.
                generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Binary;

                // Save the barcode using the fallback mode.
                generator.Save("qr_fallback.png");
                Console.WriteLine("QR code saved with fallback Binary mode: qr_fallback.png");
            }
        }
    }
}