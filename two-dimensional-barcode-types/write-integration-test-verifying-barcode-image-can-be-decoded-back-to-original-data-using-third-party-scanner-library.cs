using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode, saving it to a temporary file,
/// decoding it back, and verifying the result using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, decodes it, compares with original text, and cleans up.
    /// </summary>
    static void Main()
    {
        // Define the text that will be encoded into the barcode.
        const string originalText = "Test12345";

        // Build a temporary file path for the barcode image.
        string tempImagePath = Path.Combine(Path.GetTempPath(), "temp_barcode.png");

        // ------------------------------------------------------------
        // Generate the barcode image using Aspose.BarCode.
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, originalText))
        {
            // Set image resolution (dots per inch) – optional but improves quality.
            generator.Parameters.Resolution = 300f;

            // Save the generated barcode as a PNG file.
            generator.Save(tempImagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was successfully created.
        if (!File.Exists(tempImagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // ------------------------------------------------------------
        // Decode the barcode.
        // In a real project you might use a different library (e.g., ZXing.Net).
        // Here we use Aspose.BarCode for simplicity.
        // ------------------------------------------------------------
        string decodedText = null;
        using (var reader = new BarCodeReader(tempImagePath, DecodeType.AllSupportedTypes))
        {
            // Read all barcodes found in the image; we expect only one.
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                decodedText = result.CodeText;
                break; // Stop after the first barcode.
            }
        }

        // Compare the decoded text with the original text and output the result.
        if (decodedText == originalText)
        {
            Console.WriteLine("Success: Decoded text matches original.");
        }
        else
        {
            Console.WriteLine($"Failure: Decoded text '{decodedText ?? "null"}' does not match original '{originalText}'.");
        }

        // ------------------------------------------------------------
        // Clean up: delete the temporary barcode image file.
        // ------------------------------------------------------------
        try
        {
            File.Delete(tempImagePath);
        }
        catch
        {
            // Suppress any exceptions during cleanup.
        }
    }
}