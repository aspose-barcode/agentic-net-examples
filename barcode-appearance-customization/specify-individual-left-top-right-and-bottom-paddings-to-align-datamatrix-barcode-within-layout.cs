// Title: DataMatrix Barcode with Individual Padding Settings
// Description: Demonstrates how to set left, top, right, and bottom paddings for a DataMatrix barcode and save it as a PNG image. Useful for aligning the barcode within a layout or document.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize barcode appearance using the BarcodeGenerator and its Parameters properties. Developers often need to adjust padding, module size, and image format when embedding barcodes in reports, labels, or UI layouts. The snippet shows typical API usage for DataMatrix symbology with fine‑grained padding control.
// Prompt: Specify individual left, top, right, and bottom paddings to align a DataMatrix barcode within a layout.
// Tags: datamatrix, padding, image, generation, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a DataMatrix barcode with custom left, top, right, and bottom paddings
/// and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the output folder, configures the barcode,
    /// applies individual paddings, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Prepare output directory in the system temporary folder
        string outputDir = Path.Combine(Path.GetTempPath(), "DataMatrixPaddingDemo");
        Directory.CreateDirectory(outputDir);
        string outputFile = Path.Combine(outputDir, "DataMatrix.png");

        // Create a DataMatrix barcode generator with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "Sample123"))
        {
            // Set individual paddings (in points) for precise layout alignment
            generator.Parameters.Barcode.Padding.Left.Point = 5f;
            generator.Parameters.Barcode.Padding.Top.Point = 10f;
            generator.Parameters.Barcode.Padding.Right.Point = 5f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 10f;

            // Optional: adjust the module (dot) size of the barcode
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Save the generated barcode image as PNG
            generator.Save(outputFile, BarCodeImageFormat.Png);
        }

        // Inform the user where the image was saved
        Console.WriteLine($"DataMatrix barcode saved to: {outputFile}");
    }
}