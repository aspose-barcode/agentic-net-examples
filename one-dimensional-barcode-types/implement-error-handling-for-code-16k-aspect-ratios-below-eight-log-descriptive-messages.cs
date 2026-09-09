// Title: Generate Code 16K Barcodes with Aspect Ratio Validation
// Description: Demonstrates creating Code 16K barcodes using Aspose.BarCode, enforcing a minimum aspect ratio of eight and logging warnings for invalid values.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing how to configure barcode parameters such as X‑Dimension and AspectRatio for Code 16K symbology. Typical use cases include producing high‑density barcodes for inventory or tracking systems where specific size constraints are required. Developers often need to validate input parameters and log informative messages to ensure compliant barcode output.
// Prompt: Implement error handling for Code 16K aspect ratios below eight, log descriptive messages.
// Tags: barcode, code16k, generation, aspectratio, errorhandling, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating Code 16K barcodes with aspect‑ratio validation using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes for a set of aspect ratios, saving them to a temporary folder.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for output images
        string outputDir = Path.Combine(Path.GetTempPath(), "Code16KDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Sample barcode data and aspect ratios to test
        string codeText = "Aspose.BarCode";
        float[] aspectRatios = { 5f, 10f, 20f };

        // Iterate over each aspect ratio and generate the corresponding barcode
        foreach (float ratio in aspectRatios)
        {
            string filePath = Path.Combine(outputDir, $"Code16K_Aspect_{ratio}.png");
            GenerateCode16K(codeText, ratio, filePath);
        }

        Console.WriteLine($"Barcode images saved to: {outputDir}");
    }

    /// <summary>
    /// Generates a Code 16K barcode with the specified aspect ratio, handling invalid ratios.
    /// </summary>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="aspectRatio">Desired aspect ratio; must be >= 8.</param>
    /// <param name="outputPath">File path where the generated PNG will be saved.</param>
    static void GenerateCode16K(string codeText, float aspectRatio, string outputPath)
    {
        // Validate aspect ratio; log a warning and skip generation if below the recommended minimum
        if (aspectRatio < 8f)
        {
            Console.WriteLine($"[Warning] Aspect ratio {aspectRatio} is below the recommended minimum of 8. Generation skipped for \"{outputPath}\".");
            return;
        }

        // Configure the barcode generator and save the image
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code16K, codeText))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2;               // Set module width
            generator.Parameters.Barcode.Code16K.AspectRatio = aspectRatio; // Apply validated aspect ratio
            generator.Save(outputPath, BarCodeImageFormat.Png);               // Save as PNG
            Console.WriteLine($"[Info] Generated Code 16K barcode with aspect ratio {aspectRatio} at \"{outputPath}\".");
        }
    }
}