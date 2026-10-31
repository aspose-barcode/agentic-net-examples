// Title: Barcode generation with fallback from MaxiCode to DataMatrix
// Description: Demonstrates generating a MaxiCode barcode and automatically falling back to a DataMatrix barcode when the data exceeds MaxiCode capacity.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class with different EncodeTypes. It illustrates a common scenario where developers need to handle data size limitations by implementing a fallback mechanism, switching to an alternative symbology (DataMatrix) when the primary one (MaxiCode) cannot accommodate the input. Typical use cases include dynamic barcode creation for shipping labels or inventory systems where data length may vary.
// Prompt: Implement fallback mechanism that switches to DataMatrix when MaxiCode generation fails due to data size.
// Tags: barcode, fallback, maximcode, datamatrix, generation, png, aspose.barcode, encodetypes

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a MaxiCode barcode and falling back to a DataMatrix barcode when the data size exceeds MaxiCode limits.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a MaxiCode barcode; if it fails, generates a DataMatrix barcode instead.
    /// </summary>
    static void Main()
    {
        // Prepare sample data that exceeds the capacity of a MaxiCode symbol.
        string data = new string('A', 200);

        // Define an output directory in the temporary folder and ensure it exists.
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeFallbackDemo");
        Directory.CreateDirectory(outputDir);

        // Paths for the generated barcode images.
        string maxiCodePath = Path.Combine(outputDir, "maxicode.png");
        string dataMatrixPath = Path.Combine(outputDir, "datamatrix.png");

        bool generated = false; // Tracks whether MaxiCode generation succeeded.

        // Attempt to generate a MaxiCode barcode.
        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, data))
            {
                // Optional: increase X dimension for better visual clarity.
                generator.Parameters.Barcode.XDimension.Pixels = 10f;
                generator.Save(maxiCodePath, BarCodeImageFormat.Png);
                Console.WriteLine($"MaxiCode generated: {maxiCodePath}");
                generated = true;
            }
        }
        catch (Exception ex)
        {
            // Log the failure; fallback will be attempted.
            Console.WriteLine($"MaxiCode generation failed: {ex.Message}");
        }

        // If MaxiCode generation failed, fall back to generating a DataMatrix barcode.
        if (!generated)
        {
            try
            {
                using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, data))
                {
                    // Choose a DataMatrix version that can accommodate the data size.
                    generator.Parameters.Barcode.DataMatrix.Version = DataMatrixVersion.ECC200_32x32;
                    // Optional: set error correction level.
                    generator.Parameters.Barcode.DataMatrix.EccType = DataMatrixEccType.Ecc200;
                    generator.Save(dataMatrixPath, BarCodeImageFormat.Png);
                    Console.WriteLine($"DataMatrix generated as fallback: {dataMatrixPath}");
                }
            }
            catch (Exception ex)
            {
                // Log any failure in the fallback generation.
                Console.WriteLine($"DataMatrix generation also failed: {ex.Message}");
            }
        }
    }
}