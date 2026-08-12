// Title: DataMatrix Symbol Size Fallback Example
// Description: Demonstrates how to automatically select a larger DataMatrix symbol when the initial version cannot accommodate the input data.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on DataMatrix symbology version control. It shows how to use BarcodeGenerator, EncodeTypes, and DataMatrixVersion to handle variable data lengths, a common requirement for developers needing dynamic barcode sizing for packaging, inventory, or printing workflows.
// Prompt: Implement fallback to larger DataMatrix symbol when initial encoding fails due to data length.
// Tags: datamatrix, fallback, barcode, generation, image, aspose.barcode, encode, version

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates fallback to larger DataMatrix symbols when the data exceeds the capacity of smaller versions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a DataMatrix barcode, trying progressively larger symbol versions until successful.
    /// </summary>
    static void Main()
    {
        // Sample data that may exceed the capacity of small DataMatrix symbols
        string codeText = "This is a sample text that may be too long for small DataMatrix symbols. " +
                          "It will be used to demonstrate fallback to a larger symbol when needed.";

        // Ordered list of DataMatrix versions to try (from smallest to largest)
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

        bool generated = false;

        // Iterate through each version, attempting to generate the barcode
        foreach (var version in versions)
        {
            try
            {
                // Create a generator for DataMatrix with the provided text
                using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
                {
                    // Configure the specific DataMatrix version and error correction type
                    generator.Parameters.Barcode.DataMatrix.Version = version;
                    generator.Parameters.Barcode.DataMatrix.EccType = DataMatrixEccType.Ecc200;

                    // Save the generated barcode image; filename includes the version for clarity
                    string fileName = $"DataMatrix_{version}.png";
                    generator.Save(fileName);

                    Console.WriteLine($"Successfully generated DataMatrix with version {version} -> {fileName}");
                    generated = true;
                    break; // Exit loop after successful generation
                }
            }
            catch (Exception ex)
            {
                // Generation failed, likely because the data does not fit in the current symbol size
                Console.WriteLine($"Version {version} failed: {ex.Message}");
                // Continue to the next larger version
            }
        }

        // Inform the user if none of the attempted versions could accommodate the data
        if (!generated)
        {
            Console.WriteLine("Unable to generate DataMatrix barcode with any of the attempted versions.");
        }
    }
}