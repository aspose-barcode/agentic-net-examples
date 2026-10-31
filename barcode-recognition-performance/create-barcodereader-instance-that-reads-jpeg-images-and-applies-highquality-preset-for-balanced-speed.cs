// Title: Read JPEG Barcode with HighQuality Preset using Aspose.BarCode
// Description: Demonstrates how to generate a Code128 barcode, save it as a JPEG image, and read it back using the BarCodeReader with the HighQuality quality preset for balanced speed and accuracy.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for decoding, and QualitySettings for controlling recognition performance. Typical use cases include scanning barcodes from image files in desktop or server applications where a trade‑off between speed and accuracy is required. Developers often need to adjust the QualitySettings to meet their performance constraints while maintaining reliable decoding.
// Prompt: Create a BarCodeReader instance that reads JPEG images and applies HighQuality preset for balanced speed.
// Tags: barcode, code128, jpeg, highquality, qualitysettings, barcodereader, barcodegenerator, aspnet, aspnetcore, aspose.barcode, image-processing

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Sample program that generates a barcode image, reads it using a high‑quality preset,
/// and outputs the decoded values to the console.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory to store the sample image.
        string tempDir = Path.Combine(Path.GetTempPath(), "BarCodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string imagePath = Path.Combine(tempDir, "sample.jpg");

        // Generate a Code128 barcode and save it as a JPEG file.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Jpeg);
        }

        // Verify that the image was created successfully before attempting to read it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create the barcode image.");
            return;
        }

        // Initialize the BarCodeReader for the JPEG image and enable the HighQuality preset.
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            reader.QualitySettings = QualitySettings.HighQuality; // Balanced speed and accuracy.
            var results = reader.ReadBarCodes();

            // Output the number of barcodes detected and their details.
            Console.WriteLine($"Barcodes read: {results.Length}");
            foreach (var result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // Attempt to clean up the temporary directory and its contents.
        try
        {
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Suppress any exceptions that occur during cleanup.
        }
    }
}