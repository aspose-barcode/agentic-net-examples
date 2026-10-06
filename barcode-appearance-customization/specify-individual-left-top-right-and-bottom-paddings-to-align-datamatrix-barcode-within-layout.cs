// Title: Set Individual Padding for a DataMatrix Barcode
// Description: Demonstrates how to specify left, top, right, and bottom padding values for a DataMatrix barcode using Aspose.BarCode and save it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and barcode parameter settings such as Padding and XDimension. Developers often need to control barcode layout within images for precise positioning in documents, labels, or UI components. The snippet shows typical API usage for creating, configuring, and exporting barcodes.
// Prompt: Specify individual left, top, right, and bottom paddings to align a DataMatrix barcode within a layout.
// Tags: datamatrix, padding, barcode-generation, png, aspose.barcode, image-output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a DataMatrix barcode with custom padding on each side and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the output directory, configures barcode padding,
    /// optionally sets the module size, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define a temporary folder for the generated image
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Full path for the resulting PNG file
        string outputPath = Path.Combine(outputDir, "DataMatrixPadding.png");

        // Initialize the barcode generator for a DataMatrix symbology with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "1234567890"))
        {
            // Set individual paddings (in points) for precise alignment
            generator.Parameters.Barcode.Padding.Left.Point = 10f;
            generator.Parameters.Barcode.Padding.Top.Point = 5f;
            generator.Parameters.Barcode.Padding.Right.Point = 15f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 20f;

            // Optional: define the size of a single module (pixel density)
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Render and save the barcode image as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}