// Title: DataMatrix Barcode with Individual Padding
// Description: Demonstrates how to set left, top, right, and bottom paddings for a DataMatrix barcode to control its alignment within an image.
// Prompt: Specify individual left, top, right, and bottom paddings to align a DataMatrix barcode within a layout.
// Tags: datamatrix, padding, barcode, aspnet, image generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a DataMatrix barcode with custom padding values.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a DataMatrix barcode, applies individual paddings,
    /// sets image dimensions, saves the image, and writes a confirmation to the console.
    /// </summary>
    static void Main()
    {
        // Initialize a DataMatrix barcode generator with the desired text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "Aspose.DataMatrix"))
        {
            // Apply individual padding values (in points) to each side of the barcode.
            generator.Parameters.Barcode.Padding.Left.Point   = 10f; // 10 points on the left
            generator.Parameters.Barcode.Padding.Top.Point    = 20f; // 20 points on the top
            generator.Parameters.Barcode.Padding.Right.Point  = 15f; // 15 points on the right
            generator.Parameters.Barcode.Padding.Bottom.Point = 5f;  // 5 points at the bottom

            // Define the output image size to clearly see the effect of the padding.
            generator.Parameters.ImageWidth.Point  = 300f;
            generator.Parameters.ImageHeight.Point = 300f;

            // Save the generated barcode image to a file.
            generator.Save("datamatrix.png");
        }

        // Inform the user that the barcode has been generated.
        Console.WriteLine("DataMatrix barcode generated with custom paddings.");
    }
}