// Title: Code128 Barcode Generation and Recognition with Variable DPI and XDimension
// Description: Demonstrates generating a Code128 barcode with XDimension set to 2 pixels at several DPI settings, then recognizing it to verify the encoded text.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them. Developers often need to adjust XDimension and image resolution to meet printing or scanning requirements, making this pattern common in inventory, shipping, and retail applications.
// Prompt: Test recognition of Code128 barcodes with XDimension set to 2 pixels across multiple image resolutions.
// Tags: code128, barcode generation, barcode recognition, png, xdimension, resolution, aspnet.barcode, aspnet.barcode.generator, aspnet.barcode.reader

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates Code128 barcodes at multiple DPI settings with a fixed XDimension,
/// saves them as PNG files, and then reads them back to verify successful recognition.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes barcode creation, saving, and recognition loops.
    /// </summary>
    static void Main()
    {
        // Text to encode in the barcode
        const string codeText = "CODE128TEST";

        // Array of DPI values to test (low, medium, high resolution)
        int[] resolutions = { 96, 150, 300 };

        // Prepare output directory for generated barcode images
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Iterate over each DPI setting
        foreach (int dpi in resolutions)
        {
            // Build file name that includes the DPI value
            string filePath = Path.Combine(outputDir, $"code128_{dpi}dpi.png");

            // ---------- Barcode Generation ----------
            // Create a generator for Code128 with the specified text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Set module size to 2 pixels (XDimension) and image resolution
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.Parameters.Resolution = dpi;

                // Save the generated barcode as a PNG image
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Verify that the image file was successfully created
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Failed to create barcode image at {filePath}");
                continue;
            }

            // ---------- Barcode Recognition ----------
            // Initialize a reader for Code128 barcodes from the saved image
            using (var reader = new BarCodeReader(filePath, DecodeType.Code128))
            {
                // Optional: adjust XDimension handling for low‑resolution images
                // reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;

                // Perform the recognition
                var results = reader.ReadBarCodes();

                // Output recognition results
                if (results.Length == 0)
                {
                    Console.WriteLine($"Resolution {dpi} DPI: No barcode detected.");
                }
                else
                {
                    foreach (var result in results)
                    {
                        Console.WriteLine($"Resolution {dpi} DPI: Detected CodeText = {result.CodeText}");
                    }
                }
            }
        }
    }
}