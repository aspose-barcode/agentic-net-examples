// Title: DataMatrix Symbol Fallback Based on Data Length
// Description: Demonstrates how to automatically select a larger DataMatrix symbol when the initial encoding fails because the payload exceeds the capacity of smaller symbols.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes.DataMatrix, and DataMatrixVersion to handle variable‑length data. Developers often need to choose an appropriate DataMatrix size for dynamic content; this snippet shows a practical fallback loop that tries successive symbol versions until the data fits, a common pattern in inventory, logistics, and manufacturing applications.
// Prompt: Implement fallback to larger DataMatrix symbol when initial encoding fails due to data length.
// Tags: datamatrix, fallback, barcode generation, aspose.barcode, png, data length handling

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates automatic fallback to larger DataMatrix symbols when the supplied text does not fit in smaller versions.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point. Generates a DataMatrix barcode using a fallback strategy.
    /// </summary>
    static void Main()
    {
        // Sample long text that may not fit in the smallest DataMatrix symbol
        string longText = new string('A', 200);

        // Create a temporary directory to store generated images
        string outputDir = Path.Combine(Path.GetTempPath(), "DataMatrixFallbackDemo");
        Directory.CreateDirectory(outputDir);

        // Generate the barcode, trying larger versions until it succeeds
        GenerateDataMatrixWithFallback(longText, outputDir);
    }

    /// <summary>
    /// Attempts to generate a DataMatrix barcode using a list of versions ordered from smallest to largest.
    /// Stops at the first version that can encode the supplied text.
    /// </summary>
    /// <param name="codeText">The text to encode into the barcode.</param>
    /// <param name="outputDirectory">Folder where the generated PNG files will be saved.</param>
    static void GenerateDataMatrixWithFallback(string codeText, string outputDirectory)
    {
        // Ordered list of DataMatrix versions from smallest to larger (including rectangular and DMRE extensions)
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
            DataMatrixVersion.ECC200_144x144,
            // Rectangular ECC200
            DataMatrixVersion.ECC200_8x18,
            DataMatrixVersion.ECC200_8x32,
            DataMatrixVersion.ECC200_12x26,
            DataMatrixVersion.ECC200_12x36,
            DataMatrixVersion.ECC200_16x36,
            DataMatrixVersion.ECC200_16x48,
            // DMRE (rectangular extensions)
            DataMatrixVersion.DMRE_8x48,
            DataMatrixVersion.DMRE_8x64,
            DataMatrixVersion.DMRE_8x80,
            DataMatrixVersion.DMRE_8x96,
            DataMatrixVersion.DMRE_8x120,
            DataMatrixVersion.DMRE_8x144,
            DataMatrixVersion.DMRE_12x64,
            DataMatrixVersion.DMRE_12x88,
            DataMatrixVersion.DMRE_16x64,
            DataMatrixVersion.DMRE_20x36,
            DataMatrixVersion.DMRE_20x44,
            DataMatrixVersion.DMRE_20x64,
            DataMatrixVersion.DMRE_22x48,
            DataMatrixVersion.DMRE_24x48,
            DataMatrixVersion.DMRE_24x64,
            DataMatrixVersion.DMRE_26x40,
            DataMatrixVersion.DMRE_26x48,
            DataMatrixVersion.DMRE_26x64
        };

        bool generated = false;

        // Iterate through the versions, attempting to generate the barcode with each one
        foreach (DataMatrixVersion version in versions)
        {
            string filePath = Path.Combine(outputDirectory, $"DataMatrix_{version}.png");
            try
            {
                // Create a generator for the current version
                using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
                {
                    generator.Parameters.Barcode.DataMatrix.Version = version;
                    generator.Parameters.Barcode.DataMatrix.EccType = DataMatrixEccType.Ecc200;
                    // Optional: set module size (pixel density)
                    generator.Parameters.Barcode.XDimension.Point = 2f;
                    generator.Save(filePath, BarCodeImageFormat.Png);
                }

                Console.WriteLine($"Successfully generated DataMatrix with version {version} at: {filePath}");
                generated = true;
                break; // Exit loop once a suitable version is found
            }
            catch (Exception ex)
            {
                // Log the failure and continue to the next larger version
                Console.WriteLine($"Failed with version {version}: {ex.Message}");
            }
        }

        if (!generated)
        {
            Console.WriteLine("Unable to generate DataMatrix barcode with the provided text using all available versions.");
        }
    }
}