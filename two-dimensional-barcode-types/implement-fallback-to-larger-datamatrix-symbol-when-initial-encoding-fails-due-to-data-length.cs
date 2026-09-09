// Title: DataMatrix barcode generation with automatic fallback to larger symbol versions
// Description: Demonstrates how to generate a DataMatrix barcode and automatically fall back to larger symbol sizes when the data exceeds the capacity of the initially selected version.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on DataMatrix symbology. It showcases the use of BarcodeGenerator, EncodeTypes, and DataMatrixVersion to handle variable data lengths. Developers often need to ensure successful encoding for large payloads by iterating through supported symbol sizes, a common requirement in inventory, logistics, and manufacturing applications.
// Prompt: Implement fallback to larger DataMatrix symbol when initial encoding fails due to data length.
// Tags: datamatrix, barcode, fallback, generation, image, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a DataMatrix barcode with fallback to larger symbol versions when needed.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a long‑text DataMatrix barcode and writes the result path to the console.
    /// </summary>
    static void Main()
    {
        // Create a long string that exceeds the capacity of the smallest DataMatrix symbols.
        string longText = new string('A', 1500);

        // Build a unique temporary folder to store the generated image.
        string tempFolder = Path.Combine(Path.GetTempPath(), "DataMatrixFallback_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full output file path.
        string outputPath = Path.Combine(tempFolder, "DataMatrix.png");

        // Attempt to generate the barcode, falling back to larger versions if necessary.
        bool success = GenerateDataMatrixWithFallback(longText, outputPath);

        // Inform the user of the result.
        Console.WriteLine(success
            ? $"Barcode generated successfully: {outputPath}"
            : "Failed to generate barcode with all attempted versions.");
    }

    /// <summary>
    /// Tries to generate a DataMatrix barcode, first with automatic version selection,
    /// then iterating through a predefined list of larger versions if needed.
    /// </summary>
    /// <param name="text">The data to encode.</param>
    /// <param name="outputPath">File path where the barcode image will be saved.</param>
    /// <returns>True if generation succeeds; otherwise false.</returns>
    static bool GenerateDataMatrixWithFallback(string text, string outputPath)
    {
        // First attempt without specifying a version (auto‑selection).
        if (TryGenerate(text, outputPath, null))
            return true;

        // Ordered list of DataMatrix ECC200 square sizes from smallest to largest.
        DataMatrixVersion[] versions = new DataMatrixVersion[]
        {
            DataMatrixVersion.ECC200_10x10,
            DataMatrixVersion.ECC200_12x12,
            DataMatrixVersion.ECC200_14x14,
            DataMatrixVersion.ECC200_16x16,
            DataMatrixVersion.ECC200_18x18,
            DataMatrixVersion.ECC200_20x20,
            DataMatrixVersion.ECC200_22x22,
            DataMatrixVersion.ECC200_24x24,
            DataMatrixVersion.ECC200_26x26,
            DataMatrixVersion.ECC200_32x32,
            DataMatrixVersion.ECC200_36x36,
            DataMatrixVersion.ECC200_40x40,
            DataMatrixVersion.ECC200_44x44,
            DataMatrixVersion.ECC200_48x48,
            DataMatrixVersion.ECC200_52x52,
            DataMatrixVersion.ECC200_64x64,
            DataMatrixVersion.ECC200_72x72,
            DataMatrixVersion.ECC200_80x80,
            DataMatrixVersion.ECC200_88x88,
            DataMatrixVersion.ECC200_96x96,
            DataMatrixVersion.ECC200_104x104,
            DataMatrixVersion.ECC200_120x120,
            DataMatrixVersion.ECC200_132x132,
            DataMatrixVersion.ECC200_144x144
        };

        // Iterate through the versions, returning true on the first successful generation.
        foreach (var version in versions)
        {
            if (TryGenerate(text, outputPath, version))
                return true;
        }

        // All attempts failed.
        return false;
    }

    /// <summary>
    /// Attempts to generate a DataMatrix barcode with an optional explicit version.
    /// </summary>
    /// <param name="text">The data to encode.</param>
    /// <param name="outputPath">Destination file path for the PNG image.</param>
    /// <param name="version">Optional specific DataMatrix version; null for auto‑selection.</param>
    /// <returns>True if the barcode is generated and saved; otherwise false.</returns>
    static bool TryGenerate(string text, string outputPath, DataMatrixVersion? version)
    {
        try
        {
            // Initialize the generator with DataMatrix symbology.
            using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, text))
            {
                // Set a larger X‑dimension for better visual clarity.
                generator.Parameters.Barcode.XDimension.Pixels = 4f;

                // Apply the explicit version if one is provided.
                if (version.HasValue)
                {
                    generator.Parameters.Barcode.DataMatrix.Version = version.Value;
                }

                // Save the generated barcode as a PNG image.
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            return true;
        }
        catch (Exception ex)
        {
            // Log the failure reason; continue to the next version if applicable.
            Console.WriteLine($"Generation failed{(version.HasValue ? $" with version {version.Value}" : " without explicit version")}: {ex.Message}");
            return false;
        }
    }
}