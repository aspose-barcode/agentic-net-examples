// Title: Scanning high‑resolution TIFF barcode with MaxQuality settings
// Description: Demonstrates generating a high‑resolution Code128 barcode saved as a TIFF file and scanning it using Aspose.BarCode with QualitySettings set to MaxQuality for improved accuracy.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator to create a high‑resolution image, BarCodeReader to decode barcodes, and QualitySettings to control scanning precision. Typical scenarios include processing high‑resolution scanned documents (e.g., TIFF files) where accuracy is critical. Developers often need to adjust resolution and quality settings to balance performance and detection reliability.
// Prompt: Switch QualitySettings.Preset to MaxQuality to prioritize accuracy when scanning high‑resolution TIFF files.
// Tags: barcode, symbology, generation, recognition, tiff, highresolution, maxquality, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a high‑resolution Code128 barcode, saves it as a TIFF,
/// and reads it back using MaxQuality settings for optimal scanning accuracy.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, scans it with MaxQuality, and outputs the results.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary folder and file path for the sample TIFF barcode
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string tiffPath = Path.Combine(tempFolder, "sample.tiff");

        // Generate a high‑resolution barcode image and save it as TIFF
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "HighResTest"))
        {
            // Set a high resolution (e.g., 600 DPI) to simulate a high‑resolution source image
            generator.Parameters.Resolution = 600f;

            // Save the barcode as a TIFF file
            generator.Save(tiffPath, BarCodeImageFormat.Tiff);
        }

        // Verify that the TIFF file was created
        if (!File.Exists(tiffPath))
        {
            Console.WriteLine("Failed to create the TIFF file.");
            return;
        }

        // Create a BarCodeReader for the TIFF file, using all supported decode types
        using (var reader = new BarCodeReader(tiffPath, DecodeType.AllSupportedTypes))
        {
            // Switch QualitySettings to MaxQuality for maximum scanning accuracy
            reader.QualitySettings = QualitySettings.MaxQuality;

            // Read barcodes from the image
            BarCodeResult[] results = reader.ReadBarCodes();

            // Output the results
            if (results.Length == 0)
            {
                Console.WriteLine("No barcodes were detected.");
            }
            else
            {
                foreach (var result in results)
                {
                    Console.WriteLine($"Code Text: {result.CodeText}");
                    Console.WriteLine($"Symbology: {result.CodeTypeName}");
                    Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
                    Console.WriteLine();
                }
            }
        }

        // Clean up the temporary files (optional)
        try
        {
            File.Delete(tiffPath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}