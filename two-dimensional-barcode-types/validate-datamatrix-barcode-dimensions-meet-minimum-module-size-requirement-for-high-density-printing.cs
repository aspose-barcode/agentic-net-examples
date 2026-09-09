// Title: Validate DataMatrix Module Size for High‑Density Printing
// Description: Demonstrates how to generate a DataMatrix barcode, retrieve its XDimension (module size), and verify it meets a minimum size required for high‑density printing.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and validation category. It shows how to use BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to create a DataMatrix symbol, access barcode parameters such as XDimension, and perform simple validation checks. Developers working with barcode rendering, quality control, or print preparation often need to ensure module dimensions meet printer specifications.
// Prompt: Validate DataMatrix barcode dimensions meet minimum module size requirement for high‑density printing.
// Tags: datamatrix, module size, validation, high-density printing, barcode generation, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a DataMatrix barcode, reads its module size,
/// and validates the size against a minimum requirement for high‑density printing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, saves it, and performs the size validation.
    /// </summary>
    static void Main()
    {
        // Sample data for validation
        string codeText = "ASPOSE";
        float minimumModuleSizePoints = 2f; // Minimum XDimension in points

        // Prepare a temporary output directory and file path
        string outputDir = Path.Combine(Path.GetTempPath(), "DataMatrixValidation_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);
        string imagePath = Path.Combine(outputDir, "datamatrix.png");

        // ------------------------------------------------------------
        // Generate DataMatrix barcode and save it to a PNG file
        // ------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            // Set a module size (XDimension) for the barcode; this value is for demonstration
            generator.Parameters.Barcode.XDimension.Point = 1f;

            // Save the generated barcode image
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Load the generated barcode parameters to inspect the XDimension value
        // ------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            float actualModuleSize = generator.Parameters.Barcode.XDimension.Point;

            Console.WriteLine($"Generated DataMatrix barcode at: {imagePath}");
            Console.WriteLine($"Actual module size (XDimension): {actualModuleSize} points");

            // Validate the module size against the minimum requirement
            if (actualModuleSize < minimumModuleSizePoints)
            {
                Console.WriteLine($"Warning: Module size is below the minimum required of {minimumModuleSizePoints} points for high‑density printing.");
            }
            else
            {
                Console.WriteLine("Module size meets the minimum requirement for high‑density printing.");
            }
        }
    }
}