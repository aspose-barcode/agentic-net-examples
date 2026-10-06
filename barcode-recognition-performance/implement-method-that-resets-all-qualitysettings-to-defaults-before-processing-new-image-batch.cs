// Title: Reset QualitySettings before barcode batch processing
// Description: Generates sample Code128 barcode images, then reads each image while resetting QualitySettings to defaults before processing each file.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It demonstrates how to use BarcodeGenerator to create barcodes, BarCodeReader to decode them, and how to reset QualitySettings (e.g., NormalQuality preset) for each image in a batch. Developers often need to ensure consistent decoding performance across varying image qualities, making QualitySettings management a common requirement in batch processing scenarios.
// Prompt: Implement a method that resets all QualitySettings to defaults before processing a new image batch.
// Tags: barcode, code128, generation, recognition, qualitysettings, batch processing, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a batch of Code128 barcodes, resetting QualitySettings before each read,
/// and cleaning up temporary files. Useful for scenarios where consistent decoding settings are required
/// across multiple images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates temporary barcode images, reads them with default QualitySettings,
    /// and then removes all generated files and folders.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the batch
        string batchFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // Generate sample Code128 barcode images and store their paths
        var generatedFiles = new System.Collections.Generic.List<string>();
        for (int i = 1; i <= 3; i++)
        {
            string filePath = Path.Combine(batchFolder, $"code128_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, $"Sample{i}"))
            {
                // Set X-dimension to control barcode module width
                generator.Parameters.Barcode.XDimension.Point = 2f;
                // Save the barcode as a PNG image
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            generatedFiles.Add(filePath);
        }

        // Process the batch: reset QualitySettings before each read operation
        foreach (string file in generatedFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            using (var reader = new BarCodeReader(file, DecodeType.Code128))
            {
                // Reset reader's quality settings to defaults
                ResetQualitySettings(reader);

                // Read and output barcode information
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {Path.GetFileName(file)} | CodeText: {result.CodeText} | Type: {result.CodeTypeName}");
                }
            }
        }

        // Clean up temporary files
        foreach (string file in generatedFiles)
        {
            try { File.Delete(file); } catch { }
        }

        // Remove the temporary batch folder
        try { Directory.Delete(batchFolder, true); } catch { }
    }

    /// <summary>
    /// Resets the QualitySettings of the provided BarCodeReader to the default NormalQuality preset.
    /// Additional default options can be set explicitly if required.
    /// </summary>
    /// <param name="reader">The BarCodeReader whose quality settings will be reset.</param>
    static void ResetQualitySettings(BarCodeReader reader)
    {
        // Apply the default preset (NormalQuality) and default options
        reader.QualitySettings = QualitySettings.NormalQuality;

        // Uncomment and adjust the following lines to set explicit defaults if needed:
        // reader.QualitySettings.AllowIncorrectBarcodes = false;
        // reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;
        // reader.QualitySettings.InverseImage = InverseImageMode.Auto;
    }
}