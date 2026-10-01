// Title: Read JPEG Barcode Image with HighQuality Preset
// Description: Demonstrates how to use Aspose.BarCode's BarCodeReader to decode barcodes from a JPEG file while applying the HighQuality preset for balanced speed and accuracy.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating the use of BarCodeReader, QualitySettings, and DecodeType to read barcodes from image files. Typical use cases include scanning product labels, documents, or any JPEG images containing barcodes. Developers often need to configure decoding options and quality presets to optimize performance and reliability.
// Prompt: Create a BarCodeReader instance that reads JPEG images and applies HighQuality preset for balanced speed.
// Tags: barcode, barcode recognition, jpeg, highquality, qualitysettings, decode type, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates reading a JPEG barcode image using BarCodeReader with HighQuality preset.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates a sample barcode image, reads it, and outputs detected barcode information.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Create a temporary JPEG barcode image to ensure the file exists.
        string tempImagePath = Path.Combine(Path.GetTempPath(), "sample_barcode.jpg");
        CreateSampleBarcodeImage(tempImagePath);

        // Verify the image file exists before attempting to read.
        if (!File.Exists(tempImagePath))
        {
            Console.WriteLine($"Image file not found: {tempImagePath}");
            return;
        }

        // Prepare the decode type (all supported symbologies).
        BaseDecodeType decodeType = DecodeType.AllSupportedTypes;

        // Create the BarCodeReader with the JPEG image and set HighQuality preset.
        using (var reader = new BarCodeReader(tempImagePath, decodeType))
        {
            // Apply the HighQuality preset for balanced speed and quality.
            reader.QualitySettings = QualitySettings.HighQuality;

            // Read all barcodes from the image.
            BarCodeResult[] results = reader.ReadBarCodes();

            if (results.Length == 0)
            {
                Console.WriteLine("No barcodes detected.");
            }
            else
            {
                foreach (var result in results)
                {
                    Console.WriteLine($"Code Text: {result.CodeText}");
                    Console.WriteLine($"Code Type: {result.CodeTypeName}");
                    Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
                    Console.WriteLine();
                }
            }
        }

        // Clean up the temporary image file.
        try
        {
            File.Delete(tempImagePath);
        }
        catch
        {
            // Ignored – file may be in use or deletion may fail on some platforms.
        }
    }

    // Helper method to generate a simple Code128 barcode and save it as JPEG.
    private static void CreateSampleBarcodeImage(string filePath)
    {
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Save the barcode as a JPEG image.
            generator.Save(filePath, BarCodeImageFormat.Jpeg);
        }
    }
}