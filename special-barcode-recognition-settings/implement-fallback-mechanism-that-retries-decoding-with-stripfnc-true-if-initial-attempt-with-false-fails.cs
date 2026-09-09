// Title: Barcode decoding with fallback StripFNC setting
// Description: Demonstrates decoding a Code128 barcode and retrying with StripFNC enabled if the first attempt fails.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, showing how to use BarCodeReader and its BarcodeSettings to control FNC character stripping. Typical use cases include handling barcodes that may contain function characters; developers often need to toggle StripFNC to improve decoding reliability. The snippet illustrates generating a barcode, attempting decode, and implementing a fallback strategy.
// Prompt: Implement a fallback mechanism that retries decoding with StripFNC true if initial attempt with false fails.
// Tags: barcode, code128, decoding, stripfnc, fallback, aspose.barcode, generation, recognition

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates barcode generation and a fallback decoding strategy using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode, attempts to decode it with StripFNC disabled,
    /// and retries with StripFNC enabled if the first attempt fails.
    /// </summary>
    static void Main()
    {
        // Create a temporary directory to store the generated barcode image
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeFallback_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string imagePath = Path.Combine(tempDir, "code128.png");

        // Generate a simple Code128 barcode and save it as PNG
        using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.Code128, "Aspose"))
        {
            gen.Parameters.Barcode.XDimension.Pixels = 2;
            gen.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Define the decode type and a flag to track success
        BaseDecodeType decodeType = DecodeType.Code128;
        bool success = false;

        // First attempt: try decoding with StripFNC set to false
        using (BarCodeReader reader = new BarCodeReader(imagePath, decodeType))
        {
            reader.BarcodeSettings.StripFNC = false;
            BarCodeResult[] results = reader.ReadBarCodes();

            if (results != null && results.Length > 0)
            {
                Console.WriteLine("First attempt succeeded (StripFNC=false):");
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"CodeType:{result.CodeTypeName}");
                    Console.WriteLine($"CodeText:{result.CodeText}");
                }
                success = true;
            }
        }

        // Fallback attempt: if the first attempt failed, retry with StripFNC set to true
        if (!success)
        {
            using (BarCodeReader reader = new BarCodeReader(imagePath, decodeType))
            {
                reader.BarcodeSettings.StripFNC = true;
                BarCodeResult[] results = reader.ReadBarCodes();

                if (results != null && results.Length > 0)
                {
                    Console.WriteLine("Fallback attempt succeeded (StripFNC=true):");
                    foreach (BarCodeResult result in results)
                    {
                        Console.WriteLine($"CodeType:{result.CodeTypeName}");
                        Console.WriteLine($"CodeText:{result.CodeText}");
                    }
                }
                else
                {
                    Console.WriteLine("Failed to decode barcode even after fallback.");
                }
            }
        }

        // Optional cleanup: uncomment to delete the temporary directory after execution
        // Directory.Delete(tempDir, true);
    }
}