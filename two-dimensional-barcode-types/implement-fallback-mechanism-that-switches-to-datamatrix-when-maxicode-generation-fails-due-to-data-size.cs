// Title: Barcode generation with MaxiCode fallback to DataMatrix
// Description: Demonstrates generating a MaxiCode barcode and automatically falling back to a DataMatrix barcode when the data exceeds MaxiCode capacity.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class with different EncodeTypes. It illustrates a common scenario where developers need to handle data size limitations by implementing a fallback mechanism, switching to an alternative symbology (DataMatrix) when the primary one (MaxiCode) cannot accommodate the payload. Typical use cases include logistics, inventory, and packaging systems that require robust barcode generation.
// Prompt: Implement fallback mechanism that switches to DataMatrix when MaxiCode generation fails due to data size.
// Tags: barcode, maxicode, datamatrix, fallback, generation, aspose.barcode, png, encode types

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a MaxiCode barcode and falls back to a DataMatrix barcode
/// if the data size exceeds MaxiCode limits.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a MaxiCode barcode; on failure, generates a DataMatrix barcode instead.
    /// </summary>
    static void Main()
    {
        // Sample data exceeding MaxiCode capacity (over 60 bytes)
        string data = new string('A', 100);

        // Create a unique temporary output folder
        string outputFolder = Path.Combine(Path.GetTempPath(), "BarcodeFallback_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Define file paths for the potential barcode images
        string maxiCodePath = Path.Combine(outputFolder, "maxicode.png");
        string dataMatrixPath = Path.Combine(outputFolder, "datamatrix.png");

        bool generated = false; // Tracks whether MaxiCode generation succeeded

        // Attempt MaxiCode generation
        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, data))
            {
                // Optional: set XDimension for better visibility
                generator.Parameters.Barcode.XDimension.Pixels = 10f;
                generator.Save(maxiCodePath, BarCodeImageFormat.Png);
                Console.WriteLine($"MaxiCode generated: {maxiCodePath}");
                generated = true;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"MaxiCode generation failed: {ex.Message}");
        }

        // Fallback to DataMatrix if MaxiCode generation failed
        if (!generated)
        {
            try
            {
                using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, data))
                {
                    // Choose a large enough version to accommodate the data
                    generator.Parameters.Barcode.DataMatrix.Version = DataMatrixVersion.ECC200_144x144;
                    generator.Save(dataMatrixPath, BarCodeImageFormat.Png);
                    Console.WriteLine($"DataMatrix generated as fallback: {dataMatrixPath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DataMatrix generation also failed: {ex.Message}");
            }
        }
    }
}