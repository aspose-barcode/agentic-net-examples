// Title: High‑Resolution TIFF Barcode Generation and Recognition with MaxQuality Setting
// Description: Demonstrates generating a high‑resolution Code128 barcode saved as a TIFF file and then reading it using the MaxQuality preset for optimal accuracy.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator to create barcodes with custom resolution and BarCodeReader with QualitySettings to improve detection reliability, a common requirement when processing high‑resolution scanned images. Developers often need to adjust resolution and quality presets to balance performance and accuracy in image‑based barcode scanning scenarios.
// Prompt: Switch QualitySettings.Preset to MaxQuality to prioritize accuracy when scanning high‑resolution TIFF files.
// Tags: barcode, code128, tiff, highresolution, maxquality, generation, recognition, aspnet, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates a high‑resolution Code128 barcode, saves it as a TIFF file,
/// and reads it back using the MaxQuality preset for maximum recognition accuracy.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes barcode generation, recognition, and cleanup.
    /// </summary>
    static void Main()
    {
        // Define a temporary file path for the generated TIFF barcode.
        string tiffPath = Path.Combine(Path.GetTempPath(), "sample_barcode.tiff");

        // ------------------------------------------------------------
        // Generate a high‑resolution barcode and save it as a TIFF file.
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "HighRes123"))
        {
            // Set a high resolution (e.g., 600 DPI) to simulate a high‑resolution scan source.
            generator.Parameters.Resolution = 600f;

            // Save the barcode image in TIFF format.
            generator.Save(tiffPath, BarCodeImageFormat.Tiff);
        }

        // Verify that the TIFF file was successfully created.
        if (!File.Exists(tiffPath))
        {
            Console.WriteLine($"Failed to create barcode image at '{tiffPath}'.");
            return;
        }

        // ------------------------------------------------------------
        // Read the barcode from the TIFF using the MaxQuality preset.
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(tiffPath, DecodeType.AllSupportedTypes))
        {
            // Apply the MaxQuality preset for maximum recognition accuracy.
            reader.QualitySettings = QualitySettings.MaxQuality;

            // Perform barcode recognition.
            BarCodeResult[] results = reader.ReadBarCodes();

            // Output recognition results.
            if (results.Length == 0)
            {
                Console.WriteLine("No barcodes were detected.");
            }
            else
            {
                foreach (var result in results)
                {
                    Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                }
            }
        }

        // ------------------------------------------------------------
        // Clean up the temporary TIFF file.
        // ------------------------------------------------------------
        try
        {
            File.Delete(tiffPath);
        }
        catch
        {
            // Ignore any cleanup errors.
        }
    }
}