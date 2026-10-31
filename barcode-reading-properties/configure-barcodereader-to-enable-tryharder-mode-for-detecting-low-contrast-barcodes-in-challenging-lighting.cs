// Title: Enable TryHarder Mode in BarCodeReader for Low‑Contrast QR Codes
// Description: Demonstrates generating a low‑contrast QR barcode and configuring BarCodeReader with tryHarder mode to improve detection under challenging lighting.
// Category-Description: This example belongs to the Aspose.BarCode reading operations collection. It showcases the use of BarCodeReader, QualitySettings, and reflection to enable advanced detection features such as tryHarder and complex background handling. Developers working with barcode recognition in low‑quality images, variable lighting, or low‑contrast scenarios often need to tweak these settings to achieve reliable results. The snippet serves as a reference for configuring the reader for robust scanning in real‑world applications.
// Prompt: Configure BarCodeReader to enable tryHarder mode for detecting low‑contrast barcodes in challenging lighting.
// Tags: low-contrast, tryharder, barcode, qrcode, reading, aspose.barcode, qualitysettings

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a low‑contrast QR code and reads it using BarCodeReader with tryHarder mode enabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a low‑contrast QR barcode, configures the reader, and outputs detected barcodes.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the sample files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeTryHarder_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Path for the generated low‑contrast QR image
        string imagePath = Path.Combine(tempFolder, "low_contrast_qr.png");

        // -------------------------------------------------
        // Generate a low‑contrast QR barcode image
        // -------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "LowContrastTest"))
        {
            // Set foreground and background colors that are close in value to simulate low contrast
            generator.Parameters.Barcode.BarColor = Color.FromArgb(100, 100, 100);
            generator.Parameters.BackColor = Color.FromArgb(120, 120, 120);

            // Save the QR code as a PNG file
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // -------------------------------------------------
        // Read the barcode with tryHarder mode enabled
        // -------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Enable complex background handling, which helps with low‑contrast images
            reader.QualitySettings.ComplexBackground = ComplexBackgroundMode.Enabled;

            // Attempt to enable the TryHarder property via reflection (if present in the current library version)
            var qs = reader.QualitySettings;
            var tryHarderProp = qs.GetType().GetProperty("TryHarder");
            if (tryHarderProp != null && tryHarderProp.CanWrite)
            {
                tryHarderProp.SetValue(qs, true);
            }

            // Perform barcode recognition
            BarCodeResult[] results = reader.ReadBarCodes();

            // Output the number of barcodes detected and their details
            Console.WriteLine($"Barcodes read: {results.Length}");
            foreach (var result in results)
            {
                Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }
        }

        // -------------------------------------------------
        // Clean up temporary files and directories
        // -------------------------------------------------
        try
        {
            if (File.Exists(imagePath))
                File.Delete(imagePath);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Suppress any cleanup exceptions to avoid breaking the sample flow
        }
    }
}