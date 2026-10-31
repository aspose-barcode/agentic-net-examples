// Title: DataMatrix Symbol Fallback Example
// Description: Demonstrates generating a DataMatrix barcode and automatically falling back to larger symbol versions when the data exceeds the capacity of smaller sizes.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on DataMatrix symbology. It shows how to use BarcodeGenerator, EncodeTypes, and DataMatrixVersion to select appropriate symbol sizes. Developers often need to handle variable data lengths and ensure successful encoding by iterating through available symbol versions.
// Prompt: Implement fallback to larger DataMatrix symbol when initial encoding fails due to data length.
// Tags: datamatrix, fallback, barcode, generation, aspose.barcode, png, encoding

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a DataMatrix barcode, trying progressively larger symbol versions until the data fits.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a DataMatrix barcode with automatic version fallback.
    /// </summary>
    static void Main()
    {
        // Sample data that may not fit into the smallest DataMatrix sizes (200 characters of 'A')
        string codeText = new string('A', 200);

        // Prepare a unique temporary output directory and file path
        string outputDir = Path.Combine(Path.GetTempPath(), "DataMatrixFallback_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "DataMatrix.png");

        // List of DataMatrix versions ordered from smallest to largest
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

        bool success = false;                                 // Tracks whether barcode generation succeeded
        DataMatrixVersion usedVersion = DataMatrixVersion.ECC200_10x10; // Will hold the version that succeeded

        // Iterate through each version, attempting to generate the barcode
        foreach (DataMatrixVersion version in versions)
        {
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
            {
                // Set the current DataMatrix version and error correction type
                generator.Parameters.Barcode.DataMatrix.Version = version;
                generator.Parameters.Barcode.DataMatrix.EccType = DataMatrixEccType.Ecc200;

                // Optional: define module (pixel) size for better readability
                generator.Parameters.Barcode.XDimension.Pixels = 4f;

                try
                {
                    // Attempt to save the barcode image; if data fits, this succeeds
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                    usedVersion = version;
                    success = true;
                    Console.WriteLine($"Successfully generated DataMatrix with version {version}.");
                    break; // Exit loop on first successful generation
                }
                catch (Exception ex)
                {
                    // Generation failed (likely due to insufficient symbol size); log and continue
                    Console.WriteLine($"Failed with version {version}: {ex.Message}");
                }
            }
        }

        // Report final outcome to the user
        if (!success)
        {
            Console.WriteLine("Failed to generate DataMatrix barcode with all attempted versions.");
        }
        else
        {
            Console.WriteLine($"Barcode saved to: {outputPath}");
        }
    }
}