// Title: Generate a rectangular DataMatrix barcode (approx. 10x30) and save as PNG
// Description: This example creates a DataMatrix barcode using a rectangular version close to 10 rows by 30 columns, sets the X dimension for better visibility, and saves the image as a PNG file.
// Category-Description: Demonstrates Aspose.BarCode generation for DataMatrix symbology, focusing on selecting a specific rectangular version. The example uses BarcodeGenerator, EncodeTypes, and DataMatrixVersion classes to configure the barcode, a common requirement when space constraints or layout considerations demand non‑square matrices. Ideal for developers needing to produce printable or on‑screen DataMatrix codes with custom dimensions.
// Prompt: Configure DataMatrix barcode to use rectangular shape with 10 rows and 30 columns.
// Tags: datamatrix, barcode, rectangular, version, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a rectangular DataMatrix barcode and saving it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode, configures its version, and writes the image to a temporary folder.
    /// </summary>
    static void Main()
    {
        // Define output directory in the system temporary folder and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "DataMatrixDemo");
        Directory.CreateDirectory(outputDir);

        // Full path for the generated PNG file
        string outputPath = Path.Combine(outputDir, "datamatrix.png");

        // Text to encode in the barcode
        string codeText = "SampleData";

        // Initialize the barcode generator for DataMatrix symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            // Set a rectangular version close to the requested 10 rows x 30 columns.
            // Exact 10x30 is unavailable; ECC200_12x36 is the nearest rectangular size.
            generator.Parameters.Barcode.DataMatrix.Version = DataMatrixVersion.ECC200_12x36;

            // Optional: increase the X dimension (module size) for better visual clarity
            generator.Parameters.Barcode.XDimension.Pixels = 4;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"DataMatrix barcode saved to: {outputPath}");
    }
}