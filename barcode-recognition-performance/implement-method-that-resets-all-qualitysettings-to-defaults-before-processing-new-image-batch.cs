using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a barcode image, reading it back, and cleaning up temporary files.
/// </summary>
class Program
{
    /// <summary>
    /// Resets all QualitySettings of the provided <see cref="BarCodeReader"/> to their default values.
    /// </summary>
    /// <param name="reader">The barcode reader whose quality settings will be reset.</param>
    static void ResetQualitySettings(BarCodeReader reader)
    {
        // Default: do not allow incorrect barcodes.
        reader.QualitySettings.AllowIncorrectBarcodes = false;

        // Default quality mode (assumed Normal). Adjust if a different default is required.
        reader.QualitySettings.BarcodeQuality = BarcodeQualityMode.Normal;
    }

    /// <summary>
    /// Entry point of the application. Generates a sample barcode, reads it, and performs cleanup.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary folder for generated barcode images.
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        if (!Directory.Exists(tempDir))
        {
            Directory.CreateDirectory(tempDir);
        }

        // Generate a sample barcode (Code128) to be used in the batch.
        string barcodePath = Path.Combine(tempDir, "sample.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Use default settings; save as PNG.
            generator.Save(barcodePath);
        }

        // Simulate a small batch of image files containing barcodes.
        string[] imageBatch = new string[] { barcodePath };

        // Process each image in the batch.
        foreach (var imagePath in imageBatch)
        {
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"File not found: {imagePath}");
                continue;
            }

            // Create a BarCodeReader for the current image.
            using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
            {
                // Reset quality settings before processing this image.
                ResetQualitySettings(reader);

                // Read and output all detected barcodes.
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {Path.GetFileName(imagePath)}");
                    Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                    Console.WriteLine($"Code Text: {result.CodeText}");
                }
            }
        }

        // Cleanup temporary files (optional).
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            if (Directory.Exists(tempDir))
                Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignore cleanup errors.
        }
    }
}