// Title: DataMatrix Barcode with Individual Padding Settings
// Description: Demonstrates how to set left, top, right, and bottom padding values for a DataMatrix barcode using Aspose.BarCode and save it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of the BarcodeGenerator class and its Parameters.Barcode.Padding properties to control barcode layout. Typical use cases include aligning barcodes within forms, labels, or UI components where precise spacing is required. Developers often need to adjust individual padding sides to meet design specifications or printing constraints.
// Prompt: Specify individual left, top, right, and bottom paddings to align a DataMatrix barcode within a layout.
// Tags: datamatrix, padding, barcode generation, image output, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates setting individual padding values for a DataMatrix barcode and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a DataMatrix barcode with custom padding and writes the image to a temporary file.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the system's temporary folder
        string outputPath = Path.Combine(Path.GetTempPath(), "DataMatrixPadding.png");

        // Create a barcode generator for DataMatrix with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "Sample123"))
        {
            // Set individual padding values (in pixels) for each side of the barcode
            generator.Parameters.Barcode.Padding.Left.Pixels = 10f;
            generator.Parameters.Barcode.Padding.Top.Pixels = 20f;
            generator.Parameters.Barcode.Padding.Right.Pixels = 15f;
            generator.Parameters.Barcode.Padding.Bottom.Pixels = 5f;

            // Save the generated barcode image as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}