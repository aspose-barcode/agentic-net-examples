// Title: Reset QualitySettings and process barcode images batch
// Description: Demonstrates resetting BarCodeReader quality settings to defaults before reading each image in a batch, generating sample barcodes if needed.
// Prompt: Implement a method that resets all QualitySettings to defaults before processing a new image batch.
// Tags: barcode symbology, quality settings, batch processing, aspnet barcodereader, aspnet barcodegenerator

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates resetting quality settings and reading barcodes from a batch of images.
/// </summary>
class Program
{
    // Resets the QualitySettings of the given BarCodeReader to the default preset.
    static void ResetReaderQualitySettings(BarCodeReader reader)
    {
        // The default preset is NormalQuality.
        reader.QualitySettings = QualitySettings.NormalQuality;
    }

    // Generates a few sample barcode images if the input folder is empty.
    static void EnsureSampleImages(string folderPath)
    {
        // Define file paths for the sample images.
        string[] sampleFiles = new string[]
        {
            Path.Combine(folderPath, "sample_code128.png"),
            Path.Combine(folderPath, "sample_qr.png"),
            Path.Combine(folderPath, "sample_datamatrix.png")
        };

        // Code128 sample
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            generator.Save(sampleFiles[0]);
        }

        // QR sample
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            generator.Save(sampleFiles[1]);
        }

        // DataMatrix sample
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "DM12345"))
        {
            generator.Save(sampleFiles[2]);
        }
    }

    /// <summary>
    /// Entry point. Prepares input folder, generates sample images if needed, and processes up to five barcode images,
    /// resetting quality settings for each.
    /// </summary>
    static void Main()
    {
        // Define the folder that contains images to be processed.
        string inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "InputImages");

        // Ensure the folder exists.
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
        }

        // Retrieve existing PNG images in the folder.
        string[] existingImages = Directory.GetFiles(inputFolder, "*.png");

        // If there are no images, create a few sample ones.
        if (existingImages.Length == 0)
        {
            EnsureSampleImages(inputFolder);
            existingImages = Directory.GetFiles(inputFolder, "*.png");
        }

        // Limit processing to a safe number of files (max 5).
        int maxFiles = Math.Min(5, existingImages.Length);
        for (int i = 0; i < maxFiles; i++)
        {
            string imagePath = existingImages[i];
            Console.WriteLine($"Processing file: {Path.GetFileName(imagePath)}");

            // Create a reader for the current image.
            using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
            {
                // Reset quality settings to defaults before reading this image.
                ResetReaderQualitySettings(reader);

                // Perform barcode detection.
                try
                {
                    foreach (var result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"  Detected Type: {result.CodeTypeName}");
                        Console.WriteLine($"  Code Text   : {result.CodeText}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"  Error reading barcode: {ex.Message}");
                }
            }

            Console.WriteLine(); // Blank line between files.
        }

        Console.WriteLine("Batch processing completed.");
    }
}