// Title: Validate DataMatrix barcode module size for high‑density printing
// Description: Demonstrates how to check that a DataMatrix barcode's XDimension meets a minimum module size required for high‑density print quality.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and validation category. It shows usage of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to create a DataMatrix barcode, inspect its XDimension, and ensure it satisfies printing constraints. Developers often need to validate barcode dimensions before printing to avoid readability issues, especially for dense barcodes.
// Prompt: Validate DataMatrix barcode dimensions meet minimum module size requirement for high‑density printing.
// Tags: datamatrix, validation, dimensions, xdimension, barcode generation, aspose.barcode, png

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a DataMatrix barcode and validates its module size
/// (XDimension) against a minimum requirement for high‑density printing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a DataMatrix barcode, checks the XDimension, and saves the image.
    /// </summary>
    static void Main()
    {
        // Sample data to encode in the barcode
        const string codeText = "HighDensityDataMatrix";

        // Minimum acceptable module size (XDimension) in points for high‑density printing
        const float minModuleSize = 0.5f; // points

        // Initialize a DataMatrix barcode generator with the sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            // Intentionally set a module size that may be too small for demonstration
            generator.Parameters.Barcode.XDimension.Point = 0.4f;

            // Retrieve the actual module size that will be used
            float actualModuleSize = generator.Parameters.Barcode.XDimension.Point;

            // Compare the actual size with the minimum requirement and output a warning if needed
            if (actualModuleSize < minModuleSize)
            {
                Console.WriteLine($"Warning: XDimension ({actualModuleSize}pt) is below the minimum required ({minModuleSize}pt) for high‑density printing.");
            }
            else
            {
                Console.WriteLine($"XDimension ({actualModuleSize}pt) meets the minimum requirement.");
            }

            // Define the output file path and save the barcode as a PNG image
            const string outputPath = "datamatrix.png";
            generator.Save(outputPath, BarCodeImageFormat.Png);
            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }
}