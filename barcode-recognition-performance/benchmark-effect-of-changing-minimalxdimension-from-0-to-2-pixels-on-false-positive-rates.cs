// Title: MinimalXDimension Benchmark for Barcode Detection
// Description: Demonstrates how changing MinimalXDimension from 0 to 2 pixels influences false positive detection when scanning barcode and blank images.
// Prompt: Benchmark the effect of changing MinimalXDimension from 0 to 2 pixels on false positive rates.
// Tags: barcode, minimalxdimension, benchmark, false-positive, csharp, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that benchmarks the impact of the MinimalXDimension setting on barcode detection
/// and false positive rates using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates test images, runs detection with different
    /// MinimalXDimension values, and reports the results.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a temporary folder for generated images
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        if (!Directory.Exists(tempFolder))
        {
            Directory.CreateDirectory(tempFolder);
        }

        // --------------------------------------------------------------------
        // Define file paths for the barcode image and a blank image
        // --------------------------------------------------------------------
        string barcodePath = Path.Combine(tempFolder, "barcode.png");
        string blankPath   = Path.Combine(tempFolder, "blank.png");

        // --------------------------------------------------------------------
        // Generate a simple Code128 barcode image
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Use a reasonable XDimension for visibility (2 points)
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Create a blank white image (contains no barcode)
        // --------------------------------------------------------------------
        using (var bitmap = new Bitmap(200, 100))
        {
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Color.White);
            }
            bitmap.Save(blankPath, ImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Define test configurations: MinimalXDimension values and image metadata
        // --------------------------------------------------------------------
        float[] minimalXDimensions = new float[] { 0f, 2f };
        string[] imageLabels       = new string[] { "Barcode Image", "Blank Image" };
        string[] imagePaths        = new string[] { barcodePath, blankPath };

        // --------------------------------------------------------------------
        // Run detection for each image under each MinimalXDimension setting
        // --------------------------------------------------------------------
        Console.WriteLine("=== MinimalXDimension Benchmark ===");
        for (int i = 0; i < imagePaths.Length; i++)
        {
            Console.WriteLine($"\nTesting {imageLabels[i]}:");
            foreach (float minXDim in minimalXDimensions)
            {
                bool detected = TryDetectBarcode(imagePaths[i], minXDim);
                string result = detected ? "Detected" : "Not Detected";
                Console.WriteLine($"  MinimalXDimension = {minXDim} -> {result}");
            }
        }

        // --------------------------------------------------------------------
        // Summarize false positive rates on the blank image
        // --------------------------------------------------------------------
        int falsePositives = 0;
        foreach (float minXDim in minimalXDimensions)
        {
            if (TryDetectBarcode(blankPath, minXDim))
            {
                falsePositives++;
            }
        }
        Console.WriteLine($"\nFalse Positive Count on Blank Image: {falsePositives} out of {minimalXDimensions.Length} settings");
    }

    /// <summary>
    /// Attempts to read any barcode from the specified image using the given MinimalXDimension.
    /// Returns true if at least one barcode is recognized.
    /// </summary>
    /// <param name="imagePath">Full path to the image file.</param>
    /// <param name="minimalXDimension">Minimal X dimension value to apply during recognition.</param>
    /// <returns>True if a barcode is detected; otherwise, false.</returns>
    static bool TryDetectBarcode(string imagePath, float minimalXDimension)
    {
        // Verify that the image file exists before attempting recognition
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Warning: File not found - {imagePath}");
            return false;
        }

        // Initialize the barcode reader for all supported types
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Configure the reader to use MinimalXDimension mode with the supplied value
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            reader.QualitySettings.MinimalXDimension = minimalXDimension;

            // Perform the recognition
            BarCodeResult[] results = reader.ReadBarCodes();

            // Return true if any barcode was found
            return results != null && results.Length > 0;
        }
    }
}