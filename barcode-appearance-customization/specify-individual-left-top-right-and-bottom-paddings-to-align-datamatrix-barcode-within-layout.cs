// Title: DataMatrix Barcode with Individual Padding Settings
// Description: Demonstrates how to set left, top, right, and bottom paddings for a DataMatrix barcode and save it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to customize barcode layout using the BarcodeGenerator class and its Parameters property. Typical use cases include aligning barcodes within forms, labels, or UI components where precise padding is required. Developers often need to control individual padding values to meet design specifications or printing constraints.
/// <summary>
/// Provides an example of configuring individual padding values for a DataMatrix barcode using Aspose.BarCode.
/// </summary>
using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Entry point for the DataMatrix padding demonstration.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a DataMatrix barcode with custom left, top, right, and bottom paddings and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output file name and location.
        string outputPath = "datamatrix_padding.png";

        // Text that will be encoded into the barcode.
        string codeText = "Hello Aspose";

        // Initialize the barcode generator for DataMatrix symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            // Set individual padding values (in points) to control barcode positioning.
            generator.Parameters.Barcode.Padding.Left.Point = 10f;    // left padding
            generator.Parameters.Barcode.Padding.Top.Point = 20f;     // top padding
            generator.Parameters.Barcode.Padding.Right.Point = 30f;   // right padding
            generator.Parameters.Barcode.Padding.Bottom.Point = 40f;  // bottom padding

            // Optional: increase module size for better visual clarity.
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Save the generated barcode image in PNG format.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"DataMatrix barcode saved to {outputPath}");
    }
}