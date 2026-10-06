// Title: Compare barcode detection in color vs grayscale images
// Description: Demonstrates generating barcodes in full‑color and grayscale, then detecting them to compare detection accuracy across multiple symbologies.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator to create color and grayscale barcodes and BarCodeReader to detect them. Typical use cases include evaluating scanner performance, image preprocessing effects, and validating detection reliability across QR, Code128, and DataMatrix symbologies. Developers often need to compare detection results for different image formats and color depths.
// Prompt: Compare barcode detection accuracy between grayscale and full‑color input images across multiple symbologies.
// Tags: barcode, symbology, detection, grayscale, color, generation, recognition, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates color and grayscale barcodes for several symbologies,
/// detects them, and compares detection results.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates temporary images, runs detection,
    /// and outputs a comparison of detection success for color vs. grayscale images.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for generated images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeComparison_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the symbologies to test (QR, Code128, DataMatrix)
        BaseEncodeType[] symbologies = new BaseEncodeType[]
        {
            EncodeTypes.QR,
            EncodeTypes.Code128,
            EncodeTypes.DataMatrix
        };

        // Collect detection results for each symbology
        var results = new List<string>();

        // Iterate over each symbology, generate images, and detect them
        foreach (BaseEncodeType sym in symbologies)
        {
            string symName = sym.ToString();

            // Build file paths for the color and grayscale images
            string colorPath = Path.Combine(tempFolder, $"{symName}_color.png");
            string grayPath = Path.Combine(tempFolder, $"{symName}_gray.png");

            // Generate a full‑color barcode (blue on yellow)
            using (var generator = new BarcodeGenerator(sym, "Test"))
            {
                generator.Parameters.Barcode.BarColor = Color.Blue;
                generator.Parameters.BackColor = Color.Yellow;
                generator.Save(colorPath, BarCodeImageFormat.Png);
            }

            // Generate a grayscale barcode (black on white)
            using (var generator = new BarcodeGenerator(sym, "Test"))
            {
                generator.Parameters.Barcode.BarColor = Color.Black;
                generator.Parameters.BackColor = Color.White;
                generator.Save(grayPath, BarCodeImageFormat.Png);
            }

            // Detect barcodes in both images
            bool colorDetected = DetectBarcode(colorPath);
            bool grayDetected = DetectBarcode(grayPath);

            // Record the detection outcome
            results.Add($"{symName}: Color detected = {colorDetected}, Grayscale detected = {grayDetected}");
        }

        // Output the comparison results to the console
        Console.WriteLine("Barcode detection comparison (color vs. grayscale):");
        foreach (var line in results)
        {
            Console.WriteLine(line);
        }

        // Optional cleanup: delete the temporary folder and its contents
        // Directory.Delete(tempFolder, true);
    }

    /// <summary>
    /// Attempts to read any barcode from the specified image file.
    /// </summary>
    /// <param name="imagePath">Full path to the image containing a barcode.</param>
    /// <returns>True if at least one barcode is detected; otherwise, false.</returns>
    static bool DetectBarcode(string imagePath)
    {
        // Verify that the image file exists before attempting detection
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return false;
        }

        // Use BarCodeReader to detect all supported barcode types
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            var found = reader.ReadBarCodes();
            return found != null && found.Length > 0;
        }
    }
}