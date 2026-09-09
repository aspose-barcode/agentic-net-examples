// Title: Demonstrate false positives when MinimalXDimension is set too low
// Description: This example creates a blank image and scans it with Aspose.BarCode using two different MinimalXDimension settings to show how a low value can produce false positive detections.
// Category-Description: Shows how to use Aspose.BarCode's barcode recognition quality settings, specifically XDimensionMode and MinimalXDimension, to control false positive rates. Typical use cases include preprocessing scans of documents where background noise may be misinterpreted as barcodes. Developers often adjust these settings when working with BarCodeReader to balance detection sensitivity and accuracy.
// Prompt: Record the number of false positives encountered when MinimalXDimension is set too low.
// Tags: barcode, false positives, minimalxdimension, qualitysettings, barcodereader, aspnet, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates the impact of MinimalXDimension on false positive barcode detection.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Generates a blank image and measures false positives at two MinimalXDimension values.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "FalsePositivesDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Build the path for a blank white PNG image (contains no barcode)
        string blankImagePath = Path.Combine(tempFolder, "blank.png");

        // Generate a 200x200 white bitmap and save it as PNG
        using (Bitmap bmp = new Bitmap(200, 200, PixelFormat.Format24bppRgb))
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
            }
            bmp.Save(blankImagePath, ImageFormat.Png);
        }

        // Verify that the image file was successfully created
        if (!File.Exists(blankImagePath))
        {
            Console.WriteLine("Failed to create the test image.");
            return;
        }

        // Scan the blank image with a very low MinimalXDimension (likely to produce false positives)
        int falsePositivesLow = ReadBarcodes(blankImagePath, 0.5f);

        // Scan the same image with a reasonable MinimalXDimension (should yield zero false positives)
        int falsePositivesHigh = ReadBarcodes(blankImagePath, 2.0f);

        // Output the results
        Console.WriteLine($"False positives with MinimalXDimension = 0.5: {falsePositivesLow}");
        Console.WriteLine($"False positives with MinimalXDimension = 2.0: {falsePositivesHigh}");

        // Attempt to clean up temporary files and folder
        try
        {
            File.Delete(blankImagePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Cleanup failures are non‑critical for the demo
        }
    }

    /// <summary>
    /// Reads barcodes from the specified image using the given MinimalXDimension setting.
    /// </summary>
    /// <param name="imagePath">Path to the image file to be scanned.</param>
    /// <param name="minimalXDimension">The MinimalXDimension value to apply to the reader's quality settings.</param>
    /// <returns>The number of barcodes detected (false positives if the image contains none).</returns>
    static int ReadBarcodes(string imagePath, float minimalXDimension)
    {
        // Ensure the image file exists before attempting to read
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image not found: {imagePath}");
            return 0;
        }

        // Configure the reader to look for Code128 barcodes
        BaseDecodeType decodeType = DecodeType.Code128;

        // Initialize the barcode reader with the image and decode type
        using (BarCodeReader reader = new BarCodeReader(imagePath, decodeType))
        {
            // Apply quality settings: use MinimalXDimension mode and set the desired value
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            reader.QualitySettings.MinimalXDimension = minimalXDimension;

            // Perform the scan and return the count of detected barcodes
            BarCodeResult[] results = reader.ReadBarCodes();
            return results?.Length ?? 0;
        }
    }
}