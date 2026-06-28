using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating GS1 Code128 barcodes, saving them as images,
/// and reading them back while stripping FNC characters.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates sample barcode images, reads them, and outputs original and stripped code texts.
    /// </summary>
    static void Main()
    {
        // Determine a temporary directory to store generated barcode images.
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodes");
        if (!Directory.Exists(outputDir))
        {
            // Create the directory if it does not already exist.
            Directory.CreateDirectory(outputDir);
        }

        // Sample code texts that include GS1 Application Identifiers (AIs) and potential FNC characters.
        string[] sampleCodeTexts = new string[]
        {
            "(01)12345678901231(21)ABC123",          // Typical GS1 format without explicit FNC.
            "(02)04006664241007(37)1(400)7019590754", // Contains groups that resemble FNC characters.
            "(10)123456(17)210101(21)XYZ",           // Multiple AI groups.
        };

        // Array to hold the file paths of the generated barcode images.
        string[] imagePaths = new string[sampleCodeTexts.Length];

        // Generate a barcode image for each sample text.
        for (int i = 0; i < sampleCodeTexts.Length; i++)
        {
            // Construct a unique file name for each barcode image.
            string filePath = Path.Combine(outputDir, $"barcode_{i}.png");

            // Create a BarcodeGenerator for GS1 Code128 using the current sample text.
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1Code128, sampleCodeTexts[i]))
            {
                // Save the generated barcode as a PNG image.
                generator.Save(filePath);
            }

            // Store the generated image path for later processing.
            imagePaths[i] = filePath;
        }

        // Iterate over each generated image to read and process the barcode.
        foreach (string imagePath in imagePaths)
        {
            // Verify that the image file exists before attempting to read it.
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"File not found: {imagePath}");
                continue;
            }

            // Initialize a BarCodeReader to decode all supported barcode types from the image.
            using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
            {
                // Enable automatic stripping of FNC characters from the decoded text.
                reader.BarcodeSettings.StripFNC = true;

                // Read all barcodes present in the image.
                foreach (var result in reader.ReadBarCodes())
                {
                    // Output the image name and the decoded texts.
                    Console.WriteLine($"Image: {Path.GetFileName(imagePath)}");
                    Console.WriteLine($"Original CodeText: {result.CodeText}");
                    // Since StripFNC is true, the CodeText already has FNC characters removed.
                    Console.WriteLine($"Stripped CodeText: {result.CodeText}");
                    Console.WriteLine();
                }
            }
        }

        // Optional cleanup: delete the generated barcode images.
        // foreach (var path in imagePaths) { File.Delete(path); }
    }
}