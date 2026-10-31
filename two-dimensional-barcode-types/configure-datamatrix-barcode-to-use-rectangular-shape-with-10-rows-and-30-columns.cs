// Title: Generate Rectangular DataMatrix Barcode (10 rows x 30 columns)
// Description: Demonstrates how to create a DataMatrix barcode with a rectangular shape approximating 10 rows and 30 columns and save it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on DataMatrix symbology. It showcases the use of BarcodeGenerator, EncodeTypes, and DataMatrixVersion to configure barcode dimensions. Developers often need to produce compact, rectangular DataMatrix codes for labeling small items or fitting specific layout constraints, and this snippet illustrates the typical API pattern for such scenarios.
// Prompt: Configure DataMatrix barcode to use rectangular shape with 10 rows and 30 columns.
// Tags: datamatrix, rectangular, png, barcodegenerator, datamatrixversion

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides an entry point for generating a rectangular DataMatrix barcode and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a DataMatrix barcode with a rectangular version close to 10 rows by 30 columns,
    /// then writes the resulting image to a temporary directory.
    /// </summary>
    static void Main()
    {
        // Define the output directory inside the system's temporary folder.
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Build the full file path for the resulting PNG image.
        string outputPath = Path.Combine(outputDir, "DataMatrix_Rectangular.png");

        // Create a BarcodeGenerator for DataMatrix with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "Sample"))
        {
            // Choose a rectangular DataMatrix version that approximates 10 rows x 30 columns.
            // ECC200_12x36 provides a 12‑row by 36‑column matrix, which is the closest standard size.
            generator.Parameters.Barcode.DataMatrix.Version = DataMatrixVersion.ECC200_12x36;

            // Save the generated barcode image in PNG format.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}