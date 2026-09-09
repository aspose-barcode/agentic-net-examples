// Title: Han Xin Barcode Generation with Version Handling
// Description: Demonstrates generating a Han Xin barcode, handling cases where data exceeds the capacity of a specific symbol version, and falling back to auto version.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on Han Xin symbology. It shows how to configure the HanXin version and error correction level using the BarcodeGenerator class, handle capacity exceptions, and save the resulting image. Developers working with high‑density 2‑D barcodes can use this pattern to ensure data fits within selected symbol sizes.
// Prompt: Implement exception handling for data exceeding maximum capacity of selected Han Xin symbol size.
// Tags: hanxin, barcode, generation, exception-handling, aspnet, aspose.barcode, png, versioning

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates Han Xin barcode generation with version selection and exception handling for data capacity limits.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode with a small fixed version and with auto version to illustrate handling of capacity overflow.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for output files
        string outputDir = Path.Combine(Path.GetTempPath(), "HanXinDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Sample data that likely exceeds the capacity of the smallest Han Xin version
        string data = new string('A', 500);

        // Try generating with a small fixed version (expected to fail if data is too large)
        TryGenerate(data, HanXinVersion.Version01, outputDir);

        // Try generating with auto version selection (should succeed)
        TryGenerate(data, HanXinVersion.Auto, outputDir);
    }

    /// <summary>
    /// Attempts to generate a Han Xin barcode with the specified version, handling any exceptions that occur.
    /// </summary>
    /// <param name="data">The data to encode in the barcode.</param>
    /// <param name="version">The Han Xin version to use (or Auto for automatic selection).</param>
    /// <param name="outputDir">Directory where the generated image will be saved.</param>
    static void TryGenerate(string data, HanXinVersion version, string outputDir)
    {
        // Determine a readable name for the version (Auto or specific enum value)
        string versionName = version == HanXinVersion.Auto ? "Auto" : version.ToString();
        Console.WriteLine($"Generating Han Xin barcode with version {versionName}...");

        try
        {
            // Initialize the barcode generator for Han Xin symbology with the provided data
            using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, data))
            {
                // Set the desired Han Xin version and error correction level
                generator.Parameters.Barcode.HanXin.Version = version;
                generator.Parameters.Barcode.HanXin.ErrorLevel = HanXinErrorLevel.L2;

                // Generate the barcode image
                using (Bitmap img = generator.GenerateBarCodeImage())
                {
                    // Build the output file path and save the image as PNG
                    string filePath = Path.Combine(outputDir, $"HanXin_{versionName}.png");
                    img.Save(filePath, ImageFormat.Png);
                    Console.WriteLine($"Saved barcode to: {filePath}");
                }
            }
        }
        catch (Exception ex)
        {
            // Log any errors, such as data exceeding the capacity of the selected version
            Console.WriteLine($"Error generating barcode for version {versionName}: {ex.Message}");
        }
    }
}