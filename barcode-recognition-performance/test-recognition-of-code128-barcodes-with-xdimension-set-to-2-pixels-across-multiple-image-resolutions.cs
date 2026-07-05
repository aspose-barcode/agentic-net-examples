// Title: Code128 Barcode Generation and Recognition with Fixed XDimension
// Description: Demonstrates generating Code128 barcodes with a 2‑pixel XDimension at various DPI settings and then recognizing them using minimal XDimension detection.
// Prompt: Test recognition of Code128 barcodes with XDimension set to 2 pixels across multiple image resolutions.
// Tags: code128, barcode generation, barcode recognition, xdimension, dpi, aspnet.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates Code128 barcodes with a fixed XDimension at multiple resolutions,
/// then reads them back using minimal XDimension detection.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates barcode images and validates their recognition.
    /// </summary>
    static void Main()
    {
        // Sample Code128 text to encode
        const string codeText = "Test123";

        // Desired XDimension (module width) in pixels
        const float xDimensionPixels = 2f;

        // Different image resolutions (DPI) to test
        int[] resolutions = { 100, 200, 300 };

        // Directory to store generated barcode images
        string outputDir = "Barcodes";
        if (!Directory.Exists(outputDir))
        {
            // Create the output folder if it does not exist
            Directory.CreateDirectory(outputDir);
        }

        // -------------------------------------------------
        // Generation Phase: create barcodes with same XDimension but different DPI
        // -------------------------------------------------
        foreach (int dpi in resolutions)
        {
            // Build file name based on DPI
            string filePath = Path.Combine(outputDir, $"code128_{dpi}dpi.png");

            // Initialize barcode generator for Code128
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Set XDimension to 2 pixels (point unit)
                generator.Parameters.Barcode.XDimension.Point = xDimensionPixels;

                // Set image resolution (dots per inch)
                generator.Parameters.Resolution = dpi;

                // Save the generated barcode image to file
                generator.Save(filePath);
                Console.WriteLine($"Generated barcode: {filePath} (Resolution: {dpi} DPI)");
            }
        }

        Console.WriteLine();
        Console.WriteLine("=== Recognition Phase ===");

        // -------------------------------------------------
        // Recognition Phase: read each generated barcode using minimal XDimension detection
        // -------------------------------------------------
        foreach (int dpi in resolutions)
        {
            // Path to the previously generated image
            string filePath = Path.Combine(outputDir, $"code128_{dpi}dpi.png");

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Warning: File not found - {filePath}");
                continue;
            }

            // Initialize barcode reader for Code128
            using (var reader = new BarCodeReader(filePath, DecodeType.Code128))
            {
                // Apply normal quality preset
                reader.QualitySettings = QualitySettings.NormalQuality;

                // Configure XDimension detection to use the minimal size (2 pixels)
                reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                reader.QualitySettings.MinimalXDimension = xDimensionPixels;

                bool found = false;

                // Iterate through all detected barcodes (should be one per image)
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Resolution {dpi} DPI: Detected CodeText = '{result.CodeText}'");
                    found = true;
                }

                if (!found)
                {
                    Console.WriteLine($"Resolution {dpi} DPI: No barcode detected.");
                }
            }
        }

        Console.WriteLine();
        Console.WriteLine("Processing completed.");
    }
}