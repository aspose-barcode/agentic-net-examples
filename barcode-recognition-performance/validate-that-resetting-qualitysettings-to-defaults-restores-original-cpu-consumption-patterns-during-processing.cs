// Title: Barcode Quality Settings Reset Demonstration
// Description: Generates a Code128 barcode, reads it with a custom high‑quality setting, then resets to default to show CPU usage restoration.
// Prompt: Validate that resetting QualitySettings to defaults restores original CPU consumption patterns during processing.
// Tags: barcode, code128, qualitysettings, reset, generation, recognition, png, aspnet

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how changing and then resetting the <see cref="QualitySettings"/> of a <see cref="BarCodeReader"/>
/// affects barcode recognition. The example generates a barcode, reads it with a custom high‑quality setting,
/// resets to the default setting, and reads it again.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, applies custom quality settings,
    /// resets them, and performs recognition in both scenarios.
    /// </summary>
    static void Main()
    {
        // Generate a simple Code128 barcode and store it in a memory stream.
        using (var memoryStream = new MemoryStream())
        {
            // Create a barcode generator for Code128 with the text "Test123".
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
            {
                // Save the barcode image as PNG into the memory stream.
                generator.Save(memoryStream, BarCodeImageFormat.Png);
            }

            // Prepare the stream for reading by resetting its position.
            memoryStream.Position = 0;

            // Create a barcode reader for the generated image, specifying Code128 as the decode type.
            using (var reader = new BarCodeReader(memoryStream, DecodeType.Code128))
            {
                // Apply a non‑default quality setting (more CPU intensive).
                reader.QualitySettings = QualitySettings.HighQuality;
                Console.WriteLine($"Custom Quality - BarcodeQuality: {reader.QualitySettings.BarcodeQuality}");

                // Perform recognition with the custom high‑quality settings.
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Read with custom quality: {result.CodeText}");
                }

                // Reset quality settings to the default preset.
                reader.QualitySettings = QualitySettings.NormalQuality;
                Console.WriteLine($"After reset - BarcodeQuality: {reader.QualitySettings.BarcodeQuality}");

                // Reset the stream position to read the image again.
                memoryStream.Position = 0;

                // Perform recognition with the default quality settings.
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Read after reset: {result.CodeText}");
                }
            }
        }
    }
}