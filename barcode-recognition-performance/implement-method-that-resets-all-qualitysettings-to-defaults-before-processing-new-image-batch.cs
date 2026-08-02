// Title: Reset QualitySettings to defaults before processing barcode images
// Description: Demonstrates resetting BarCodeReader QualitySettings to the default NormalQuality preset before reading a batch of barcode images.
// Category-Description: This example belongs to the Aspose.BarCode image processing category, illustrating how to configure the BarCodeReader's QualitySettings for optimal decoding. It uses key classes such as BarCodeReader, QualitySettings, and BarcodeGenerator, typical for developers who need to batch‑process barcodes with consistent settings. The snippet shows generating sample barcodes, resetting reader settings, and decoding them, a common workflow in inventory or document automation solutions.
// Prompt: Implement a method that resets all QualitySettings to defaults before processing a new image batch.
// Tags: barcode symbology, reset qualitysettings, batch processing, aspose.barcode, generation, recognition

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates resetting QualitySettings to defaults and batch processing barcode images.
/// </summary>
class Program
{
    // Resets the reader's QualitySettings to the default NormalQuality preset.
    static void ResetQualitySettings(BarCodeReader reader)
    {
        // Assign the NormalQuality preset, which restores default values for all settings.
        reader.QualitySettings = QualitySettings.NormalQuality;

        // Explicitly ensure the AllowIncorrectBarcodes flag is set to its default (false).
        reader.QualitySettings.AllowIncorrectBarcodes = false;

        // Other properties (e.g., BarcodeQuality, InverseImage) revert to their standard defaults
        // when the preset is applied, so no additional code is required here.
    }

    /// <summary>
    /// Entry point that generates sample barcodes, resets reader settings, and decodes each image.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder to store generated barcode images.
        string folderPath = Path.Combine(Path.GetTempPath(), "AsposeBarcodeSample");
        Directory.CreateDirectory(folderPath);

        // Define sample texts to encode into barcodes.
        string[] sampleTexts = { "ABC123", "987XYZ", "Sample001" };

        // Generate barcode images using default generator settings.
        for (int i = 0; i < sampleTexts.Length; i++)
        {
            string filePath = Path.Combine(folderPath, $"barcode{i + 1}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, sampleTexts[i]))
            {
                // Save the barcode image to the temporary folder.
                generator.Save(filePath);
            }
        }

        // Retrieve all generated PNG files for processing.
        string[] imageFiles = Directory.GetFiles(folderPath, "*.png");

        // Iterate over each image file and decode its barcode.
        foreach (string imageFile in imageFiles)
        {
            // Verify the file exists before attempting to read it.
            if (!File.Exists(imageFile))
            {
                Console.WriteLine($"File not found: {imageFile}");
                continue;
            }

            // Initialize a reader for the current image, targeting Code128 barcodes.
            using (var reader = new BarCodeReader(imageFile, DecodeType.Code128))
            {
                // Reset quality settings to defaults before decoding.
                ResetQualitySettings(reader);

                // Read all detected barcodes and output their details.
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {Path.GetFileName(imageFile)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                }
            }
        }

        // Attempt to clean up the temporary folder; ignore any errors.
        try
        {
            Directory.Delete(folderPath, true);
        }
        catch
        {
            // Cleanup failure is non‑critical; the OS will eventually reclaim the temporary data.
        }
    }
}