// Title: Scanning High‑Resolution TIFF for Barcodes with MaxQuality Setting
// Description: Demonstrates reading barcodes from a high‑resolution TIFF file using Aspose.BarCode, switching to the MaxQuality preset to maximize recognition accuracy.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing how to configure QualitySettings for optimal scanning of high‑resolution images. It uses BarCodeReader, DecodeType, and QualitySettings classes, common in scenarios such as document processing, inventory management, and automated data capture where precise barcode detection is critical.
// Prompt: Switch QualitySettings.Preset to MaxQuality to prioritize accuracy when scanning high‑resolution TIFF files.
// Tags: barcode symbology, recognition, console output, barcodereader, qualitysettings, decodetype

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Entry point for the barcode scanning example.
/// </summary>
class Program
{
    /// <summary>
    /// Scans a high‑resolution TIFF image for barcodes using the MaxQuality preset.
    /// </summary>
    static void Main()
    {
        // Path to the high‑resolution TIFF file to be scanned
        string imagePath = "sample.tiff";

        // Verify that the file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Create a BarCodeReader for the image, detecting all supported symbologies
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Switch to the MaxQuality preset for maximum recognition accuracy
            reader.QualitySettings = QualitySettings.MaxQuality;

            // Iterate through all detected barcodes and output their text
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected: {result.CodeText}");
            }
        }
    }
}