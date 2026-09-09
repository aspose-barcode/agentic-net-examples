// Title: Barcode detection accuracy comparison between grayscale and color images
// Description: Demonstrates generating barcodes in grayscale and colored formats, then detecting them to compare accuracy across multiple symbologies.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator to create images, configure colors, and BarCodeReader with quality settings to recognize barcodes. Developers often need to evaluate detection performance for different image types, such as grayscale versus full‑color, across various symbologies like QR, Code128, DataMatrix, and PDF417.
// Prompt: Compare barcode detection accuracy between grayscale and full‑color input images across multiple symbologies.
// Tags: barcode detection, grayscale, color, symbology, qrcode, code128, datamatrix, pdf417, aspose.barcode, generation, recognition

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating barcodes in grayscale and colored images and comparing detection accuracy.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcode images, runs detection, and prints a comparison summary.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary working folder for generated images
        string workFolder = Path.Combine(Path.GetTempPath(), "BarcodeCompare_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workFolder);

        // Define the set of symbologies and the text to encode for each
        var symbologies = new List<(BaseEncodeType Encode, string Text)>
        {
            (EncodeTypes.QR, "TestQR123"),
            (EncodeTypes.Code128, "TestC128"),
            (EncodeTypes.DataMatrix, "TestDM"),
            (EncodeTypes.Pdf417, "TestPDF417")
        };

        // Lists to hold file paths for generated grayscale and color images
        var grayscaleFiles = new List<string>();
        var colorFiles = new List<string>();

        // Generate barcode images for each symbology
        foreach (var (encode, text) in symbologies)
        {
            string grayPath = Path.Combine(workFolder, $"{text}_gray.png");
            string colorPath = Path.Combine(workFolder, $"{text}_color.png");

            // Create a grayscale barcode (default colors)
            using (var generator = new BarcodeGenerator(encode, text))
            {
                generator.Save(grayPath, BarCodeImageFormat.Png);
            }
            grayscaleFiles.Add(grayPath);

            // Create a colored barcode (blue bars on yellow background)
            using (var generator = new BarcodeGenerator(encode, text))
            {
                generator.Parameters.Barcode.BarColor = Color.Blue;
                generator.Parameters.BackColor = Color.Yellow;
                generator.Save(colorPath, BarCodeImageFormat.Png);
            }
            colorFiles.Add(colorPath);
        }

        // Output header for the comparison results
        Console.WriteLine("Barcode Detection Accuracy Comparison");
        Console.WriteLine("------------------------------------");

        int total = symbologies.Count;
        int graySuccess = 0;
        int colorSuccess = 0;

        // Iterate over each generated image pair and evaluate detection
        for (int i = 0; i < total; i++)
        {
            string expectedText = symbologies[i].Text;
            string grayFile = grayscaleFiles[i];
            string colorFile = colorFiles[i];

            bool grayDetected = DetectBarcode(grayFile, expectedText, false);
            bool colorDetected = DetectBarcode(colorFile, expectedText, true);

            if (grayDetected) graySuccess++;
            if (colorDetected) colorSuccess++;

            // Display per‑symbology results
            Console.WriteLine($"Symbology: {symbologies[i].Encode.GetType().Name.Replace("EncodeType", "")}");
            Console.WriteLine($"  Grayscale detection: {(grayDetected ? "Success" : "Fail")}");
            Console.WriteLine($"  Color detection:     {(colorDetected ? "Success" : "Fail")}");
        }

        // Summarize overall detection success rates
        Console.WriteLine();
        Console.WriteLine($"Summary: Grayscale success {graySuccess}/{total}, Color success {colorSuccess}/{total}");
    }

    /// <summary>
    /// Attempts to read a barcode from the specified image and verifies it matches the expected text.
    /// </summary>
    /// <param name="imagePath">Path to the barcode image.</param>
    /// <param name="expectedText">The text that should be encoded in the barcode.</param>
    /// <param name="isColor">Indicates whether the image is a colored barcode (enables complex background handling).</param>
    /// <returns>True if the expected barcode is found; otherwise, false.</returns>
    static bool DetectBarcode(string imagePath, string expectedText, bool isColor)
    {
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return false;
        }

        try
        {
            // Initialize the reader for all supported barcode types
            using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
            {
                if (isColor)
                {
                    // Enable complex background mode for colored images to improve detection
                    reader.QualitySettings.ComplexBackground = ComplexBackgroundMode.Enabled;
                }

                // Perform detection
                var results = reader.ReadBarCodes();

                // Check each detected barcode against the expected text
                foreach (BarCodeResult result in reader.FoundBarCodes)
                {
                    if (result.CodeText == expectedText)
                    {
                        return true;
                    }
                }

                return false;
            }
        }
        catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
        {
            Console.WriteLine($"Unable to load image: {imagePath}");
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing {imagePath}: {ex.Message}");
            return false;
        }
    }
}