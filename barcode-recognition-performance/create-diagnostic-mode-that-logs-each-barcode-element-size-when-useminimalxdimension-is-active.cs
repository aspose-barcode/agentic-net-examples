// Title: Barcode Generation and Recognition with Minimal X-Dimension Diagnostics
// Description: Demonstrates generating Code128 and QR barcodes, saving them as PNG, then reading them back while logging each barcode element size when UseMinimalXDimension is enabled.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the BarcodeGenerator for creating barcodes and BarCodeReader for decoding them, highlighting the QualitySettings.XDimension property to activate minimal X-dimension mode. Developers often need to fine‑tune barcode dimensions for printing or scanning constraints, and this snippet illustrates how to log element sizes for diagnostic purposes.
// Prompt: Create a diagnostic mode that logs each barcode element size when UseMinimalXDimension is active.
// Tags: barcode generation, barcode recognition, minimalxdimension, code128, qr, png, aspose.barcode, diagnostics

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates sample barcodes, saves them as PNG files, and then reads them back while
/// logging each barcode element's size when the minimal X‑dimension mode is active.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes barcode creation, saving, and diagnostic reading.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // 1. Prepare output directory for generated barcode images
        // --------------------------------------------------------------------
        string outputDir = "Barcodes";
        Directory.CreateDirectory(outputDir);

        // --------------------------------------------------------------------
        // 2. Define sample barcodes to generate (type, text, file name)
        // --------------------------------------------------------------------
        var samples = new (BaseEncodeType encode, string text, string file)[]
        {
            (EncodeTypes.Code128, "1234567890", "code128.png"),
            (EncodeTypes.QR, "https://example.com", "qr.png")
        };

        // --------------------------------------------------------------------
        // 3. Generate each barcode image and save as PNG
        // --------------------------------------------------------------------
        foreach (var sample in samples)
        {
            string path = Path.Combine(outputDir, sample.file);
            using (var generator = new BarcodeGenerator(sample.encode, sample.text))
            {
                // Set visual appearance
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Parameters.Barcode.FilledBars = false;
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Save the barcode image
                generator.Save(path, BarCodeImageFormat.Png);
                Console.WriteLine($"Saved barcode: {path}");
            }
        }

        // --------------------------------------------------------------------
        // 4. Read generated barcodes and log element sizes when UseMinimalXDimension is active
        // --------------------------------------------------------------------
        string[] imageFiles = Directory.GetFiles(outputDir, "*.png");
        foreach (string imagePath in imageFiles)
        {
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"File not found: {imagePath}");
                continue;
            }

            using (var reader = new BarCodeReader(imagePath))
            {
                // Activate minimal X-dimension mode for diagnostic purposes
                reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                reader.QualitySettings.MinimalXDimension = 2f; // example minimal size in pixels

                // Iterate through all detected barcodes in the image
                foreach (var result in reader.ReadBarCodes())
                {
                    var region = result.Region.Rectangle;
                    Console.WriteLine(
                        $"Image: {Path.GetFileName(imagePath)} | " +
                        $"Type: {result.CodeTypeName} | " +
                        $"Text: {result.CodeText} | " +
                        $"Width: {region.Width} | Height: {region.Height}");
                }
            }
        }
    }
}