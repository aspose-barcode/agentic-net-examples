// Title: Adjust DPI for barcode image generation and detection
// Description: Demonstrates how to set image resolution (DPI) when generating a barcode and how that affects accurate region detection during recognition.
// Category-Description: This example belongs to the Aspose.BarCode image processing category, illustrating the use of BarcodeGenerator to set resolution and BarCodeReader with quality settings for reliable barcode detection. Developers working with high‑resolution scans or needing precise barcode region coordinates commonly use these APIs to ensure correct decoding and positioning.
// Prompt: Adjust DPI settings when loading images to ensure accurate barcode region detection.
// Tags: barcode, dpi, resolution, barcode generation, barcode recognition, qualitysettings, region detection, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a barcode with a specific DPI and reading it with adjusted quality settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the sample files
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDPI_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the barcode image
        string imagePath = Path.Combine(tempFolder, "barcode.png");

        // Generate a barcode image with a specific DPI (resolution)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the image resolution to 300 DPI
            generator.Parameters.Resolution = 300f;
            // Save the generated barcode as a PNG file
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode from the image with adjusted settings
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Apply a high‑performance preset to speed up reading
            reader.QualitySettings = QualitySettings.HighPerformance;

            // Perform barcode detection
            BarCodeResult[] results = reader.ReadBarCodes();

            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
            }
            else
            {
                // Output details for each detected barcode
                foreach (var result in results)
                {
                    var bounds = result.Region.Rectangle;
                    Console.WriteLine($"CodeText: {result.CodeText}");
                    Console.WriteLine($"Symbology: {result.CodeTypeName}");
                    Console.WriteLine($"Region - X:{bounds.X}, Y:{bounds.Y}, Width:{bounds.Width}, Height:{bounds.Height}, Angle:{result.Region.Angle}");
                }
            }
        }

        // Clean up temporary files and folder
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore any errors during cleanup
        }
    }
}