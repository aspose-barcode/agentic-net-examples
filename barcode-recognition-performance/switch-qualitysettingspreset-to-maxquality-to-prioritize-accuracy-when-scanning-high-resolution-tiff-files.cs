// Title: High‑Resolution TIFF Barcode Scanning with MaxQuality
// Description: Demonstrates scanning a high‑resolution TIFF image for barcodes using Aspose.BarCode with the MaxQuality preset to improve accuracy.
// Prompt: Switch QualitySettings.Preset to MaxQuality to prioritize accuracy when scanning high‑resolution TIFF files.
// Tags: barcode, tiff, maxquality, recognition, aspnet, csharp

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that reads barcodes from a high‑resolution TIFF file
/// using the Aspose.BarCode library with the MaxQuality setting for
/// optimal recognition accuracy.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Scans the specified TIFF image,
    /// applies the MaxQuality preset, and prints detected barcode texts.
    /// </summary>
    static void Main()
    {
        // Define the path to the high‑resolution TIFF file
        string imagePath = "highres.tif";

        // Ensure the file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Create a barcode reader that attempts to decode all supported symbologies
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Apply the MaxQuality preset to prioritize recognition accuracy over speed
            reader.QualitySettings = QualitySettings.MaxQuality;

            // Iterate through each detected barcode and output its decoded text
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected barcode: {result.CodeText}");
            }
        }
    }
}