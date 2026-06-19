using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample program demonstrating generation and reading of Code11 barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates temporary barcode images, reads them without checksum validation, and cleans up.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for sample barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Sample Code11 barcode texts
        string[] sampleTexts = { "12345", "67890", "112233" };
        string[] imagePaths = new string[sampleTexts.Length];

        // Generate barcode images and store their paths
        for (int i = 0; i < sampleTexts.Length; i++)
        {
            // Build the full file path for the current barcode image
            string imagePath = Path.Combine(tempFolder, $"code11_{i}.png");

            // Create a barcode generator for Code11 with the current text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code11, sampleTexts[i]))
            {
                // Save the generated barcode image to the specified path
                generator.Save(imagePath);
            }

            // Store the generated image path for later processing
            imagePaths[i] = imagePath;
        }

        // Process each generated barcode image with checksum validation disabled
        foreach (string path in imagePaths)
        {
            // Verify that the file exists before attempting to read it
            if (!File.Exists(path))
            {
                Console.WriteLine($"File not found: {path}");
                continue;
            }

            // Initialize a barcode reader for Code11 format
            using (var reader = new BarCodeReader(path, DecodeType.Code11))
            {
                // Disable checksum verification to allow reading even if checksum is incorrect or omitted
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Off;

                // Read all barcodes found in the image
                foreach (var result in reader.ReadBarCodes())
                {
                    // Output the file name and detected barcode text
                    Console.WriteLine($"File: {Path.GetFileName(path)} | Detected CodeText: {result.CodeText}");
                }
            }
        }

        // Clean up temporary files and directory
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore any errors that occur during cleanup
        }
    }
}