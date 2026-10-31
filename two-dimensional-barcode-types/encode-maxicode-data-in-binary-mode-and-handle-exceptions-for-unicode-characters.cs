// Title: Encode MaxiCode in Binary mode with Unicode exception handling
// Description: Demonstrates generating a MaxiCode barcode in binary encoding and shows how the API throws an exception when Unicode characters are used in binary mode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on MaxiCode symbology. It illustrates using BarcodeGenerator, setting EncodeTypes.MaxiCode, configuring MaxiCodeEncodeMode, and handling errors when unsupported characters are supplied. Developers working with high‑density 2‑D barcodes for logistics and shipping can refer to this snippet for binary data encoding and proper exception handling.
// Prompt: Encode MaxiCode data in Binary mode and handle exceptions for Unicode characters.
// Tags: maxicode, binary, unicode, exception, barcode generation, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates MaxiCode generation in binary mode and proper handling of unsupported Unicode input.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a binary MaxiCode and attempts a Unicode MaxiCode to show exception handling.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a temporary output directory for the generated barcode images.
        // --------------------------------------------------------------------
        string outputDir = Path.Combine(Path.GetTempPath(), "MaxiCodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // --------------------------------------------------------------------
        // Example 1: Encode raw binary data using MaxiCode in Binary mode.
        // --------------------------------------------------------------------
        byte[] binaryData = { 0xFF, 0xFE, 0xFD, 0xFC, 0xFB, 0xFA, 0xF9 };
        string binaryPath = Path.Combine(outputDir, "MaxiCode_Binary.png");
        try
        {
            // Initialise the generator for MaxiCode without specifying text (binary data will be set later).
            using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode))
            {
                // Assign the binary payload.
                generator.SetCodeText(binaryData);
                // Switch to Binary encoding mode.
                generator.Parameters.Barcode.MaxiCode.EncodeMode = MaxiCodeEncodeMode.Binary;
                // Save the barcode as a PNG image.
                generator.Save(binaryPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Binary MaxiCode saved to: {binaryPath}");
            }
        }
        catch (Exception ex)
        {
            // Log any unexpected errors during binary barcode generation.
            Console.WriteLine($"Error generating binary MaxiCode: {ex.Message}");
        }

        // --------------------------------------------------------------------
        // Example 2: Attempt to encode Unicode text in Binary mode (expected to fail).
        // --------------------------------------------------------------------
        string unicodeText = "犬Right狗";
        string unicodePath = Path.Combine(outputDir, "MaxiCode_Unicode.png");
        try
        {
            // Initialise the generator with Unicode text.
            using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, unicodeText))
            {
                // Binary mode does not support Unicode characters; this will trigger an exception.
                generator.Parameters.Barcode.MaxiCode.EncodeMode = MaxiCodeEncodeMode.Binary;
                // Attempt to save; should raise an error.
                generator.Save(unicodePath, BarCodeImageFormat.Png);
                Console.WriteLine($"Unicode MaxiCode saved to: {unicodePath}");
            }
        }
        catch (Exception ex)
        {
            // Expected error handling for unsupported Unicode in binary mode.
            Console.WriteLine($"Expected error for Unicode in Binary mode: {ex.Message}");
        }
    }
}