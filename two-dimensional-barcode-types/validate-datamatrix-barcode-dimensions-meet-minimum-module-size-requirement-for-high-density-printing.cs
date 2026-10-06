// Title: Validate DataMatrix Module Size for High‑Density Printing
// Description: Demonstrates how to generate a DataMatrix barcode with a specific version and verify that its module size meets a minimum pixel requirement for high‑density print output.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on DataMatrix symbology. It shows how to configure the barcode version, set X‑dimension, generate an image, and programmatically validate module dimensions. Developers working with high‑resolution printing, label design, or quality control often need to ensure barcode modules meet size thresholds, and this snippet illustrates the typical API usage (BarcodeGenerator, EncodeTypes, DataMatrixVersion, Parameters.Barcode).
// Prompt: Validate DataMatrix barcode dimensions meet minimum module size requirement for high‑density printing.
// Tags: datamatrix, barcode, validation, module size, high density printing, aspose.barcode, generation, image

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates validation of DataMatrix barcode module dimensions against a minimum size requirement.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a DataMatrix barcode, calculates its module size, and validates it meets the specified minimum.
    /// </summary>
    static void Main()
    {
        // Sample data for DataMatrix barcode
        const string codeText = "ASPOSE";

        // Minimum required module size (in pixels) for high‑density printing
        const float minimumModuleSize = 2f;

        // Create and configure the DataMatrix generator
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            // Use a known square version: 32 × 32 modules
            generator.Parameters.Barcode.DataMatrix.Version = DataMatrixVersion.ECC200_32x32;

            // Set the intended module (X‑dimension) size
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Generate the barcode image
            using (var bitmap = generator.GenerateBarCodeImage())
            {
                // Image width in pixels (square barcode, so width == height)
                int imageWidth = bitmap.Width;

                // Number of modules per side for the selected version
                const int modulesPerSide = 32;

                // Actual module size derived from the rendered image
                float actualModuleSize = (float)imageWidth / modulesPerSide;

                // Validate against the minimum requirement
                if (actualModuleSize >= minimumModuleSize)
                {
                    Console.WriteLine($"Valid DataMatrix: module size {actualModuleSize:F2}px meets the minimum of {minimumModuleSize}px.");
                }
                else
                {
                    Console.WriteLine($"Invalid DataMatrix: module size {actualModuleSize:F2}px is below the minimum of {minimumModuleSize}px.");
                }
            }
        }
    }
}