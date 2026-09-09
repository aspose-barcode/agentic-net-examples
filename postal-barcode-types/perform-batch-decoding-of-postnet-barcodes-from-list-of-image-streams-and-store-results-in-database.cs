// Title: Batch decode Postnet barcodes from image streams and store results in JSON
// Description: Demonstrates generating multiple Postnet barcode images, decoding them in a batch, and persisting the decoded data to a JSON file, simulating database storage.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating Postnet symbology, BarCodeReader for batch decoding, and handling of decoding results. Developers working with bulk barcode processing, such as postal automation or inventory systems, can use these APIs to generate, read, and store barcode information efficiently.
// Prompt: Perform batch decoding of Postnet barcodes from a list of image streams and store results in a database.
// Tags: postnet, barcode, batch decoding, json, aspose.barcode, generation, recognition, database simulation

using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates batch generation and decoding of Postnet barcodes, storing results in a JSON file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample Postnet barcodes, decodes them, and writes results to a JSON file.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for sample images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BatchPostnet_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define sample Postnet code texts
        List<string> sampleTexts = new List<string> { "12345", "67890", "24680", "13579", "00000" };
        List<string> imageFiles = new List<string>();

        // -----------------------------------------------------------------
        // Generate barcode images for each sample text
        // -----------------------------------------------------------------
        foreach (string text in sampleTexts)
        {
            string filePath = Path.Combine(tempFolder, $"Postnet_{text}.png");
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Postnet, text))
            {
                // Set barcode dimensions
                generator.Parameters.Barcode.XDimension.Pixels = 4f;
                generator.Parameters.Barcode.BarHeight.Pixels = 50f;

                // Save the generated barcode as PNG
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            imageFiles.Add(filePath);
        }

        // Prepare a list to hold decoding results
        List<DecodeRecord> records = new List<DecodeRecord>();

        // -----------------------------------------------------------------
        // Decode each generated image
        // -----------------------------------------------------------------
        foreach (string file in imageFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            try
            {
                // Specify that we are decoding Postnet barcodes
                BaseDecodeType decodeType = DecodeType.Postnet;
                using (BarCodeReader reader = new BarCodeReader(file, decodeType))
                {
                    // Optional: set quality settings for faster processing
                    reader.QualitySettings = QualitySettings.HighPerformance;

                    // Read all barcodes from the image
                    BarCodeResult[] results = reader.ReadBarCodes();
                    foreach (BarCodeResult result in results)
                    {
                        // Store each decoding result in the records list
                        records.Add(new DecodeRecord
                        {
                            FileName = Path.GetFileName(file),
                            CodeText = result.CodeText,
                            CodeType = result.CodeTypeName,
                            ReadingQuality = result.ReadingQuality
                        });
                    }
                }
            }
            catch (ArgumentException ex)
            {
                // Image loading failed or unsupported format
                Console.WriteLine($"Skipping file '{file}': {ex.Message}");
            }
        }

        // -----------------------------------------------------------------
        // Store results in a JSON file (simulating database storage)
        // -----------------------------------------------------------------
        string outputPath = Path.Combine(tempFolder, "PostnetDecodeResults.json");
        string json = JsonSerializer.Serialize(records, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(outputPath, json);

        Console.WriteLine($"Decoding completed. Results saved to: {outputPath}");

        // Cleanup: optionally delete generated images (keep results file)
        // foreach (string file in imageFiles) File.Delete(file);
    }

    // Simple DTO for holding decoding information
    class DecodeRecord
    {
        public string FileName { get; set; }
        public string CodeText { get; set; }
        public string CodeType { get; set; }
        public double ReadingQuality { get; set; }
    }
}