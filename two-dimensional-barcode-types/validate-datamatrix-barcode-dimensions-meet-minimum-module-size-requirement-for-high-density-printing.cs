// Title: Validate DataMatrix Module Size for High‑Density Printing
// Description: Demonstrates how to generate a DataMatrix barcode, set its module size, and verify that the size meets a minimum requirement for high‑density printing.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and validation category. It shows how to use BarcodeGenerator, set DataMatrix version, configure XDimension, and perform runtime checks on module dimensions. Developers working with barcode printing, especially high‑resolution output, often need to ensure module sizes meet printer specifications. The code illustrates typical use of EncodeTypes, DataMatrixVersion, BarCodeImageFormat, and image inspection via Aspose.Drawing.
/// Prompt: Validate DataMatrix barcode dimensions meet minimum module size requirement for high‑density printing.
/// Tags: datamatrix, module size, validation, high-density printing, barcode generation, aspose.barcode, aspose.drawing, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a DataMatrix barcode, saves it as PNG, and validates that its module size
/// meets a specified minimum for high‑density printing scenarios.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, checks module dimensions,
    /// and writes the image to a temporary file.
    /// </summary>
    static void Main()
    {
        // Sample data to encode in the DataMatrix barcode
        const string codeText = "Hello Aspose!";

        // Minimum acceptable module size (XDimension) in points for high‑density printing
        const float minModuleSizePoint = 0.5f; // 0.5 point ≈ 0.176 mm

        // Destination path for the generated PNG image
        string outputPath = Path.Combine(Path.GetTempPath(), "DataMatrix.png");

        try
        {
            // Initialize the barcode generator for DataMatrix with the sample text
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
            {
                // Select a specific DataMatrix version (e.g., 32x32 modules)
                generator.Parameters.Barcode.DataMatrix.Version = DataMatrixVersion.ECC200_32x32;

                // Define the module (XDimension) size; 1 point ≈ 0.352 mm
                generator.Parameters.Barcode.XDimension.Point = 1.0f;

                // Render the barcode to a memory stream in PNG format
                using (MemoryStream ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    ms.Position = 0; // Reset stream position for subsequent reading

                    // Optional: Load the image to display its pixel dimensions
                    using (Bitmap bitmap = new Bitmap(ms))
                    {
                        Console.WriteLine($"Generated image size: {bitmap.Width}×{bitmap.Height} pixels");
                    }

                    // Persist the PNG image to the file system for visual inspection
                    File.WriteAllBytes(outputPath, ms.ToArray());
                }

                // Retrieve the actual module size that was set on the generator
                float actualModuleSize = generator.Parameters.Barcode.XDimension.Point;

                // Compare the actual module size against the minimum requirement
                if (actualModuleSize < minModuleSizePoint)
                {
                    Console.WriteLine($"Warning: Module size ({actualModuleSize} pt) is below the minimum required ({minModuleSizePoint} pt) for high‑density printing.");
                }
                else
                {
                    Console.WriteLine($"Success: Module size ({actualModuleSize} pt) meets the minimum requirement ({minModuleSizePoint} pt).");
                }
            }

            // Inform the user where the barcode image was saved
            Console.WriteLine($"Barcode image saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during generation or file operations
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}