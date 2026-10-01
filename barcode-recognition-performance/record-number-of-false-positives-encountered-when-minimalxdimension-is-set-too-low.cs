// Title: Demonstrate false positive detection with low MinimalXDimension
// Description: Shows how setting MinimalXDimension too low can cause false positives when scanning a blank image, and compares detection on a valid barcode.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It demonstrates using BarcodeGenerator to create a barcode, Bitmap to create a blank image, and BarCodeReader with QualitySettings (including XDimensionMode and MinimalXDimension) to read barcodes. Developers often need to tune MinimalXDimension for performance or accuracy, and this snippet illustrates the impact of setting it too low.
// Prompt: Record the number of false positives encountered when MinimalXDimension is set too low.
// Tags: barcode, code128, minimalxdimension, false positives, recognition, generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a barcode, creates a blank image, and
/// measures false positive detections when MinimalXDimension is set to a low value.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates test images, runs barcode detection,
    /// and outputs the count of false positives and true detections.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for the generated barcode image and the blank image
        string barcodePath = Path.Combine(tempFolder, "barcode.png");
        string blankPath = Path.Combine(tempFolder, "blank.png");

        // Generate a simple Code128 barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Create a blank white image (no barcode) and save it as PNG
        using (var bitmap = new Bitmap(200, 100))
        {
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Color.White);
            }
            bitmap.Save(blankPath, ImageFormat.Png);
        }

        // Local function that reads barcodes from an image using a low MinimalXDimension
        int CountFalsePositives(string imagePath)
        {
            // Verify that the image file exists before attempting to read it
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"File not found: {imagePath}");
                return 0;
            }

            int count = 0;

            // Initialize the barcode reader for Code128 symbology
            using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
            {
                // Apply a high‑performance preset (optional but speeds up processing)
                reader.QualitySettings = QualitySettings.HighPerformance;

                // Configure XDimension to use MinimalXDimension with an intentionally low value
                reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                reader.QualitySettings.MinimalXDimension = 0.5f; // low value to provoke false positives

                // Perform the barcode detection
                BarCodeResult[] results = reader.ReadBarCodes();

                // If any results are returned, count them
                if (results != null)
                {
                    count = results.Length;
                }
            }

            return count;
        }

        // Count false positives on the blank image
        int falsePositives = CountFalsePositives(blankPath);
        Console.WriteLine($"False positives on blank image with low MinimalXDimension: {falsePositives}");

        // Verify normal detection on the valid barcode image (still using low MinimalXDimension)
        int trueDetections = CountFalsePositives(barcodePath);
        Console.WriteLine($"Detections on valid barcode image with low MinimalXDimension: {trueDetections}");

        // Clean up temporary files (optional; ignore any errors in CI environments)
        try
        {
            File.Delete(barcodePath);
            File.Delete(blankPath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Suppress cleanup exceptions
        }
    }
}