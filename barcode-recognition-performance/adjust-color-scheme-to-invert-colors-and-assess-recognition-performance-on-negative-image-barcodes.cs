// Title: Invert barcode colors and evaluate recognition performance
// Description: Demonstrates generating a normal and a negative‑image barcode, then reads them with optional inverse image mode to assess detection on inverted colors.
// Prompt: Adjust color scheme to invert colors and assess recognition performance on negative‑image barcodes.
// Tags: barcode, inversion, code128, generation, recognition, aspnet, csharp

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode color inversion and recognition using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates normal and negative barcodes, then reads them with optional inverse image mode.
    /// </summary>
    static void Main()
    {
        // Define barcode content and file names
        const string codeText = "1234567890";
        const string originalPath = "barcode_original.png";
        const string negativePath = "barcode_negative.png";

        // ------------------------------------------------------------
        // Generate original barcode (black bars on white background)
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Parameters.Barcode.BarColor = Color.Black;   // Bar color
            generator.Parameters.BackColor = Color.White;          // Background color
            generator.Save(originalPath);                          // Save to file
        }

        // ------------------------------------------------------------
        // Generate negative barcode (white bars on black background)
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Parameters.Barcode.BarColor = Color.White;   // Inverted bar color
            generator.Parameters.BackColor = Color.Black;          // Inverted background
            generator.Save(negativePath);                          // Save to file
        }

        // ------------------------------------------------------------
        // Local function: reads a barcode image and prints detection results
        // ------------------------------------------------------------
        void ReadAndReport(string imagePath, bool enableInverse)
        {
            // Verify that the image file exists before attempting to read
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"File not found: {imagePath}");
                return;
            }

            // Initialize the reader for Code128 barcodes
            using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
            {
                if (enableInverse)
                {
                    // Enable additional recognition on inverted (negative) images
                    reader.QualitySettings.InverseImage = InverseImageMode.Enabled;
                }

                // Iterate through all detected barcodes in the image
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Image: {Path.GetFileName(imagePath)}");
                    Console.WriteLine($"  Detected Type: {result.CodeType}");
                    Console.WriteLine($"  CodeText: {result.CodeText}");
                    Console.WriteLine($"  ReadingQuality: {result.ReadingQuality}");
                }
            }
        }

        // ------------------------------------------------------------
        // Read the original barcode (no inverse mode required)
        // ------------------------------------------------------------
        ReadAndReport(originalPath, enableInverse: false);

        // ------------------------------------------------------------
        // Read the negative barcode with inverse image mode enabled
        // ------------------------------------------------------------
        ReadAndReport(negativePath, enableInverse: true);
    }
}