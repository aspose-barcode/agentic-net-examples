// Title: Read High‑Resolution TIFF Barcode with MaxQuality Setting
// Description: Demonstrates generating a Code128 barcode, saving it as a high‑resolution TIFF, and reading it using the MaxQuality preset for optimal accuracy.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create barcodes, BarCodeReader to decode them, and QualitySettings to control scanning precision. Developers often need to process high‑resolution image files (e.g., TIFF) where accuracy is critical, such as in inventory management or document scanning solutions. The code illustrates typical API usage for creating, saving, and reliably reading barcodes from complex image formats.
// Prompt: Switch QualitySettings.Preset to MaxQuality to prioritize accuracy when scanning high‑resolution TIFF files.
// Tags: barcode symbology, generation, recognition, tiff, maxquality, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation, saving to a high‑resolution TIFF, and reading it with MaxQuality settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, saves it as TIFF, reads it using MaxQuality, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the sample TIFF file
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string tiffPath = Path.Combine(tempFolder, "sample.tiff");

        // Generate a Code128 barcode and save it as a high‑resolution TIFF image
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "HighResTest123"))
        {
            generator.Save(tiffPath, BarCodeImageFormat.Tiff);
        }

        // Verify that the TIFF file was created successfully
        if (!File.Exists(tiffPath))
        {
            Console.WriteLine("Failed to create the TIFF file.");
            return;
        }

        // Initialize a barcode reader for multiple symbologies and set the quality to MaxQuality for best accuracy
        using (var reader = new BarCodeReader(tiffPath, DecodeType.Code128, DecodeType.QR, DecodeType.DataMatrix, DecodeType.Aztec, DecodeType.Pdf417))
        {
            reader.QualitySettings = QualitySettings.MaxQuality;
            BarCodeResult[] results = reader.ReadBarCodes();

            // Output the number of barcodes detected and their details
            Console.WriteLine($"Barcodes read: {results.Length}");
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // Attempt to delete the temporary folder and its contents; ignore any errors during cleanup
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Cleanup errors are non‑critical; they are intentionally ignored
        }
    }
}