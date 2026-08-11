// Title: Generate Code 16K barcodes with varying quiet zone coefficients
// Description: This example creates Code 16K barcodes using different quiet‑zone left and right coefficient values and saves each barcode as a PNG image.
// Category-Description: Demonstrates Aspose.BarCode barcode generation techniques, focusing on parameter customization such as quiet‑zone coefficients and aspect ratio. The example uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes, which are commonly employed by developers to produce and export barcodes in various formats for labeling, inventory, and tracking applications.
// Prompt: Generate Code 16K barcodes with varying quiet zone coefficients in loop, store PNG files.
// Tags: code16k, quietzone, barcode, generation, png, aspose.barcode, encode-types, image-format

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

/// <summary>
/// Program that generates Code 16K barcodes with varying quiet‑zone coefficients and saves them as PNG files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates an output folder, iterates over quiet‑zone coefficient combinations,
    /// configures a <see cref="BarcodeGenerator"/> for each, and writes the resulting PNG image to disk.
    /// </summary>
    static void Main()
    {
        // Define the output folder for generated PNG files
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Code16K_Barcodes");
        Directory.CreateDirectory(outputFolder);

        // Sample codetext for the Code16K barcode
        string codeText = "123456789012";

        // Iterate over a range of quiet‑zone left and right coefficient values
        for (int leftCoef = 10; leftCoef <= 12; leftCoef++)          // left coefficient >= 10
        {
            for (int rightCoef = 1; rightCoef <= 3; rightCoef++)    // right coefficient >= 1
            {
                // Create a new barcode generator for the current configuration
                using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, codeText))
                {
                    // Apply quiet‑zone coefficient settings
                    generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef = leftCoef;
                    generator.Parameters.Barcode.Code16K.QuietZoneRightCoef = rightCoef;

                    // Optional: set aspect ratio (example value)
                    generator.Parameters.Barcode.Code16K.AspectRatio = 1.0f;

                    // Build a file name that reflects the current coefficients
                    string fileName = $"Code16K_L{leftCoef}_R{rightCoef}.png";
                    string filePath = Path.Combine(outputFolder, fileName);

                    // Save the generated barcode directly as a PNG image
                    generator.Save(filePath, BarCodeImageFormat.Png);
                }
            }
        }
    }
}