// Title: Encode MaxiCode in Binary Mode and Handle Unicode Exceptions
// Description: Demonstrates generating a MaxiCode barcode in binary mode using a byte array and shows how the API throws an exception when Unicode characters are supplied.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on MaxiCode symbology. It illustrates using the BarcodeGenerator class with EncodeTypes.MaxiCode, setting the MaxiCode.EncodeMode to Binary, and handling errors when unsupported Unicode data is provided. Developers working with high‑density 2‑D barcodes can learn how to encode raw binary data and manage exception scenarios.
// Prompt: Encode MaxiCode data in Binary mode and handle exceptions for Unicode characters.
// Tags: maxicode, binary mode, unicode exception, barcode generation, aspose.barcode, png output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates encoding MaxiCode barcodes in binary mode and handling Unicode input errors.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a binary MaxiCode barcode and attempts an invalid Unicode barcode.
    /// </summary>
    static void Main()
    {
        // ---------- Binary mode with a valid byte array ----------
        // Prepare a temporary file path for the generated barcode image.
        string binaryPath = Path.Combine(Path.GetTempPath(), "maxicode_binary.png");
        // Example binary data to encode.
        byte[] binaryData = { 0xFF, 0xFE, 0xFD, 0xFC, 0xFB, 0xFA, 0xF9 };
        // Create a generator for MaxiCode without initial text.
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode))
        {
            // Set the raw binary data as the code text.
            generator.SetCodeText(binaryData);
            // Switch encoding mode to Binary.
            generator.Parameters.Barcode.MaxiCode.EncodeMode = MaxiCodeEncodeMode.Binary;
            try
            {
                // Save the barcode as a PNG image.
                generator.Save(binaryPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Binary MaxiCode saved to: {binaryPath}");
            }
            catch (Exception ex)
            {
                // Handle any generation errors.
                Console.WriteLine($"Error generating binary MaxiCode: {ex.Message}");
            }
        }

        // ---------- Attempt Unicode text in Binary mode (expected failure) ----------
        // Prepare a temporary file path for the Unicode attempt.
        string unicodePath = Path.Combine(Path.GetTempPath(), "maxicode_unicode.png");
        // Unicode string containing characters not allowed in binary mode.
        string unicodeText = "犬Right狗";
        // Initialize generator with Unicode text (will be rejected in binary mode).
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, unicodeText))
        {
            // Set encoding mode to Binary, which does not support Unicode characters.
            generator.Parameters.Barcode.MaxiCode.EncodeMode = MaxiCodeEncodeMode.Binary;
            try
            {
                // Attempt to save; this should throw an exception.
                generator.Save(unicodePath, BarCodeImageFormat.Png);
                Console.WriteLine($"Unicode MaxiCode saved to: {unicodePath}");
            }
            catch (Exception ex)
            {
                // Expected error handling for unsupported Unicode data.
                Console.WriteLine($"Unicode characters not allowed in Binary mode: {ex.Message}");
            }
        }
    }
}