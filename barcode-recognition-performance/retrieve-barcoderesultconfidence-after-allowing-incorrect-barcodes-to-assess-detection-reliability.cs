// Title: Retrieve Barcode Detection Confidence Using AllowIncorrectBarcodes
// Description: Demonstrates how to generate a Code128 barcode, read it while allowing incorrect barcodes, and obtain the confidence metric via BarCodeResult.ReadingQuality.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating the use of BarcodeGenerator for barcode creation and BarCodeReader with QualitySettings to enable AllowIncorrectBarcodes. Developers often need to assess detection reliability, especially when scanning imperfect images; the ReadingQuality property provides a confidence score (0‑100). The sample shows typical workflow for generating, reading, and evaluating barcode confidence.
// Prompt: Retrieve BarCodeResult.Confidence after allowing incorrect barcodes to assess detection reliability.
// Tags: barcode, code128, confidence, readingquality, allowincorrectbarcodes, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation, reading with AllowIncorrectBarcodes enabled,
/// and extraction of the confidence metric (ReadingQuality) from the detection result.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, reads it while allowing incorrect barcodes,
    /// prints the detected text and confidence, then cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for this demo
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Path for the generated barcode image
        string barcodePath = Path.Combine(tempFolder, "barcode.png");

        // Generate a simple Code128 barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ABC123"))
        {
            // No additional parameters are required for this demo
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the image was created before attempting to read it
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to generate the barcode image.");
            Cleanup(tempFolder);
            return;
        }

        // Read the barcode allowing incorrect barcodes to assess detection reliability
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            // Apply a high‑performance preset (optional) and enable incorrect barcode allowance
            reader.QualitySettings = QualitySettings.HighPerformance;
            reader.QualitySettings.AllowIncorrectBarcodes = true;

            // Iterate over all detected barcodes (there should be only one in this case)
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected CodeText: {result.CodeText}");
                // ReadingQuality (0‑100) serves as the confidence metric
                Console.WriteLine($"Confidence (ReadingQuality): {result.ReadingQuality}");
            }
        }

        // Clean up temporary files
        Cleanup(tempFolder);
    }

    // Helper method to delete the temporary folder and its contents
    private static void Cleanup(string folderPath)
    {
        try
        {
            if (Directory.Exists(folderPath))
            {
                Directory.Delete(folderPath, true);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cleanup failed: {ex.Message}");
        }
    }
}