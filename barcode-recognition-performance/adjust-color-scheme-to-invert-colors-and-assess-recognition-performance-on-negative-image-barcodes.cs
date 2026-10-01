// Title: Invert Barcode Colors and Recognize Negative‑Image Barcode
// Description: Demonstrates generating a barcode with inverted colors (white bars on black background) and reading it using inverse image mode to evaluate recognition performance.
// Category-Description: This example belongs to the Aspose.BarCode image processing and recognition category. It shows how to use BarcodeGenerator to customize foreground and background colors, save PNG images, and employ BarCodeReader with QualitySettings and InverseImageMode to decode barcodes from negative‑image files. Developers working with barcode scanning under challenging lighting or printing conditions often need to handle inverted color schemes, making this pattern essential for robust barcode solutions.
// Prompt: Adjust color scheme to invert colors and assess recognition performance on negative‑image barcodes.
// Tags: barcode symbology, color inversion, negative image, recognition, aspose.barcode, code128, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates a normal and a negative‑image barcode, then reads the negative image
/// using inverse image processing to demonstrate recognition under inverted colors.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Creates temporary files, generates barcodes,
    /// performs recognition, and cleans up resources.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for this demo
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeNegDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Sample barcode data
        string codeText = "1234567890";
        string normalPath = Path.Combine(tempFolder, "barcode_normal.png");
        string negativePath = Path.Combine(tempFolder, "barcode_negative.png");

        // -------------------------------------------------
        // 1. Generate a normal barcode (black on white)
        // -------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Default colors are black foreground, white background
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Save the normal image (optional, just for reference)
            generator.Save(normalPath, BarCodeImageFormat.Png);
        }

        // -------------------------------------------------
        // 2. Generate a negative (inverted colors) barcode
        // -------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Invert colors: white bars on black background
            generator.Parameters.Barcode.BarColor = Color.White;
            generator.Parameters.BackColor = Color.Black;

            // Save the negative image for later recognition
            generator.Save(negativePath, BarCodeImageFormat.Png);
        }

        // -------------------------------------------------
        // 3. Read the negative image with inverse image mode enabled
        // -------------------------------------------------
        if (!File.Exists(negativePath))
        {
            Console.WriteLine("Negative barcode image not found.");
            return;
        }

        // Use DecodeType.AllSupportedTypes to detect any symbology
        using (var reader = new BarCodeReader(negativePath, DecodeType.AllSupportedTypes))
        {
            // Enable inverse image processing to handle negative colors
            reader.QualitySettings = QualitySettings.HighPerformance;
            reader.QualitySettings.InverseImage = InverseImageMode.Enabled;

            bool anyFound = false;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                anyFound = true;
                Console.WriteLine($"Detected CodeText: {result.CodeText}");
                Console.WriteLine($"Detected Symbology: {result.CodeTypeName}");
                Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
            }

            if (!anyFound)
            {
                Console.WriteLine("No barcode detected in the negative image.");
            }
        }

        // Cleanup: delete temporary files and folder
        try
        {
            File.Delete(normalPath);
            File.Delete(negativePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored – cleanup is best‑effort
        }
    }
}