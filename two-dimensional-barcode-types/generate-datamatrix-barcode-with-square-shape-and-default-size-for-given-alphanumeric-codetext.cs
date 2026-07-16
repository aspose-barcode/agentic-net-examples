// Title: Generate square DataMatrix barcode with default size
// Description: Demonstrates creating a DataMatrix barcode with a square shape and default size for an alphanumeric string.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to configure DataMatrix symbology using the BarcodeGenerator class. It shows setting aspect ratio and version to produce a square ECC200 barcode, a common requirement for labeling and inventory systems. Developers often need to customize size, shape, and encoding options when generating barcodes programmatically.
// Prompt: Generate a DataMatrix barcode with square shape and default size for given alphanumeric CodeText.
// Tags: datamatrix, barcode, generation, square, png, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a square DataMatrix barcode and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a DataMatrix barcode with a square shape and default size,
    /// then writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Alphanumeric text to encode
        string codeText = "ABC123XYZ";

        // Initialize the barcode generator for DataMatrix with the provided text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            // Set the barcode to have a square aspect ratio (1:1)
            generator.Parameters.Barcode.DataMatrix.AspectRatio = 1f;

            // Choose a square ECC200 version (20x20 modules) for default size
            generator.Parameters.Barcode.DataMatrix.DataMatrixVersion = DataMatrixVersion.ECC200_20x20;

            // Define the output file name and format (PNG)
            string outputFile = "datamatrix.png";

            // Save the generated barcode image to the specified file
            generator.Save(outputFile, BarCodeImageFormat.Png);
        }

        // Inform the user that the barcode was generated successfully
        Console.WriteLine("DataMatrix barcode generated successfully.");
    }
}