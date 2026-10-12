// Title: Encode MaxiCode in Binary Mode and Handle Unicode Exceptions
// Description: Demonstrates generating a MaxiCode barcode in binary mode using a byte array and shows how the API throws an exception when Unicode text is supplied in binary mode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on MaxiCode symbology. It illustrates the use of BarcodeGenerator, EncodeTypes.MaxiCode, and MaxiCodeEncodeMode.Binary to create barcodes from raw binary data. Developers often need to generate MaxiCode for shipping labels or logistics, and must handle unsupported character sets, such as Unicode, when binary encoding is required. The snippet serves as a reference for handling exceptions and proper parameter configuration.
// Prompt: Encode MaxiCode data in Binary mode and handle exceptions for Unicode characters.
// Tags: maxicode, binary mode, unicode exception, barcode generation, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates encoding MaxiCode barcodes in binary mode and handling Unicode encoding errors.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a binary MaxiCode and attempts a Unicode MaxiCode to show exception handling.
    /// </summary>
    static void Main()
    {
        // Define output directory in the temporary folder and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "MaxiCodeDemo");
        Directory.CreateDirectory(outputDir);

        // ------------------------------------------------------------
        // Binary mode with a valid byte array
        // ------------------------------------------------------------
        byte[] binaryData = { 0xFF, 0xFE, 0xFD, 0xFC, 0xFB, 0xFA, 0xF9 };
        string binaryPath = Path.Combine(outputDir, "MaxiCode_Binary.png");

        // Create a generator for MaxiCode and set binary data as the code text
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode))
        {
            generator.SetCodeText(binaryData);
            generator.Parameters.Barcode.MaxiCode.EncodeMode = MaxiCodeEncodeMode.Binary;
            generator.Save(binaryPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Binary MaxiCode saved to: {binaryPath}");

        // ------------------------------------------------------------
        // Attempt to encode Unicode text in Binary mode (expected to fail)
        // ------------------------------------------------------------
        string unicodePath = Path.Combine(outputDir, "MaxiCode_Unicode.png");

        // Initialize generator with Unicode string; binary mode will cause an exception
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "犬Right狗"))
        {
            generator.Parameters.Barcode.MaxiCode.EncodeMode = MaxiCodeEncodeMode.Binary;
            try
            {
                generator.Save(unicodePath, BarCodeImageFormat.Png);
                Console.WriteLine($"Unicode MaxiCode saved to: {unicodePath}");
            }
            catch (Exception ex)
            {
                // Capture and display the expected error message
                Console.WriteLine($"Failed to encode Unicode in Binary mode: {ex.Message}");
            }
        }
    }
}