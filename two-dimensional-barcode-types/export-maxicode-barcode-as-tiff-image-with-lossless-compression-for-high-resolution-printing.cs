using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generation of a MaxiCode barcode and saves it as a lossless TIFF file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a MaxiCode barcode and writes it to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file name (saved in the current working directory)
        string outputPath = "maxicode.tiff";

        // Resolve the full directory path for the output file
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));

        // Ensure the target directory exists; create it if it does not
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Prepare the MaxiCode data: Mode 4 with a simple textual message
        var maxiCodeCodetext = new MaxiCodeStandardCodetext
        {
            Mode = MaxiCodeMode.Mode4,
            Message = "Sample MaxiCode Message"
        };

        // Generate the barcode using the complex barcode generator
        using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            // Set a high resolution (e.g., 300 DPI) suitable for printing
            generator.Parameters.Resolution = 300f;

            // Save the generated barcode as a TIFF image with lossless compression
            generator.Save(outputPath, BarCodeImageFormat.Tiff);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"MaxiCode barcode saved to: {Path.GetFullPath(outputPath)}");
    }
}