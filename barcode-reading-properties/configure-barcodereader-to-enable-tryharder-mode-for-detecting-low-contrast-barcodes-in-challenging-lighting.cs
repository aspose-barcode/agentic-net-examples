// Title: Detect low‑contrast barcode using tryHarder mode
// Description: Demonstrates configuring BarCodeReader with high‑quality settings to read a low‑contrast barcode generated in the same program.
// Prompt: Configure BarCodeReader to enable tryHarder mode for detecting low‑contrast barcodes in challenging lighting.
// Tags: barcode, low-contrast, tryharder, qualitysettings, generation, recognition

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a low‑contrast barcode and reads it using try‑harder mode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a low‑contrast barcode image, then reads it with high‑quality settings.
    /// </summary>
    static void Main()
    {
        // Path for the sample barcode image
        string imagePath = "low_contrast_barcode.png";

        // -------------------------------------------------
        // Generate a low‑contrast barcode image
        // -------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "LowContrast"))
        {
            // Set bar color and background color to be similar (low contrast)
            generator.Parameters.Barcode.BarColor = Color.Gray;
            generator.Parameters.BackColor = Color.LightGray;

            // Save the barcode image to the specified path
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: Barcode image not found at '{imagePath}'.");
            return;
        }

        // -------------------------------------------------
        // Read the barcode using a high‑quality (try‑harder) setting
        // -------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Enable the high‑quality preset which is equivalent to a "try harder" mode
            reader.QualitySettings = QualitySettings.HighQuality;

            // Optionally, increase deconvolution for better low‑contrast handling
            // reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;

            // Iterate through all detected barcodes and output their details
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Decoded Text : {result.CodeText}");
            }
        }
    }
}