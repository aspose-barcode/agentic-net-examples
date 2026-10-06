// Title: Barcode Generation, Recognition, and Confidence Warning Example
// Description: Demonstrates generating a QR barcode, reading it, and logging a warning when the recognition confidence is moderate.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create barcodes and BarCodeReader to decode them. Developers often need to assess detection confidence using BarCodeResult.Confidence and provide guidance for image quality improvement when confidence is moderate.
// Prompt: Log a warning when BarCodeResult.Confidence equals Confidence.Moderate and suggest image enhancement to the user.
// Tags: qr, barcode generation, barcode recognition, confidence warning, aspose.barcode, image processing

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Sample program that creates a QR barcode, reads it back, and reports confidence levels.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, reads it, and logs a warning if confidence is moderate.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder to store the generated image.
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the sample barcode image.
        string imagePath = Path.Combine(tempFolder, "sample.png");

        // Generate a sample QR barcode and save it as a PNG file.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "SampleText"))
        {
            // Set a modest module size (X dimension) for better readability.
            generator.Parameters.Barcode.XDimension.Point = 2f;
            // Save the generated barcode image.
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Initialize a barcode reader to decode all supported types from the image.
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Read all barcodes found in the image.
            BarCodeResult[] results = reader.ReadBarCodes();

            // Iterate through each detection result.
            foreach (var result in results)
            {
                // Check if the confidence level is moderate.
                if (result.Confidence == BarCodeConfidence.Moderate)
                {
                    // Log a warning suggesting image enhancement.
                    Console.WriteLine("Warning: Barcode confidence is moderate. Consider enhancing the image for better recognition.");
                }
                else
                {
                    // Log the detected barcode text and its confidence level.
                    Console.WriteLine($"Barcode detected: {result.CodeText}, Confidence: {result.Confidence}");
                }
            }
        }

        // Attempt to clean up temporary files and folder.
        try
        {
            if (File.Exists(imagePath))
                File.Delete(imagePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program exit.
        }
    }
}