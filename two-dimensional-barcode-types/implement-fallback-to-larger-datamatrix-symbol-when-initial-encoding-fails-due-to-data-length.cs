// Title: DataMatrix Symbol Fallback Example
// Description: Demonstrates how to automatically select a larger DataMatrix symbol when the initial version cannot accommodate the data length.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on DataMatrix barcode creation with version control. It shows how to iterate through DataMatrixVersion enums using BarcodeGenerator and handle encoding capacity limits, a common need for developers generating high‑density DataMatrix codes for inventory or tracking applications.
// Prompt: Implement fallback to larger DataMatrix symbol when initial encoding fails due to data length.
// Tags: datamatrix, barcode, fallback, generation, image, aspose.barcode, encode, version

using System;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides an example of generating a DataMatrix barcode with automatic fallback to larger symbol versions
/// when the data exceeds the capacity of the initially selected version.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Attempts to encode a long string into a DataMatrix barcode,
    /// iterating through predefined symbol versions until a suitable one is found.
    /// </summary>
    static void Main()
    {
        // Sample data that exceeds the capacity of the smallest DataMatrix symbols
        string codeText = new string('A', 200);

        // List of DataMatrix versions ordered from smallest to largest
        var versions = new List<DataMatrixVersion>
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
            // Create a new generator for each attempt
            using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
            {
                // Force the specific DataMatrix version
                generator.Parameters.Barcode.DataMatrix.DataMatrixVersion = version;

                try
                {
                    // Attempt to save the barcode image
                    string fileName = $"DataMatrix_{version}.png";
                    generator.Save(fileName);
                    Console.WriteLine($"Successfully generated barcode with version {version} -> {fileName}");
                    generated = true;
                    break; // Exit loop on success
                }
                catch (Exception ex)
                {
                    // Expected when the data does not fit into the current symbol size
                    Console.WriteLine($"Failed with version {version}: {ex.Message}");
                }
            }
        }

        if (!generated)
        {
            Console.WriteLine("Unable to generate DataMatrix barcode with any of the provided versions.");
        }
    }
}