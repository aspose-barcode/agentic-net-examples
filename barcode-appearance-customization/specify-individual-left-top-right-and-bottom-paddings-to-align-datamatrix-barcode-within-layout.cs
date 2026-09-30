// Title: Set Individual Padding for a DataMatrix Barcode
// Description: Demonstrates how to specify left, top, right, and bottom padding values for a DataMatrix barcode using Aspose.BarCode and save it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating layout customization of barcodes. It shows how to use the BarcodeGenerator class with EncodeTypes.DataMatrix, adjust padding via the Parameters.Barcode.Padding properties, and render the result to a Bitmap for saving. Developers frequently need to fine‑tune barcode positioning within documents or UI layouts, and this snippet provides a concise reference for those common tasks.
// Prompt: Specify individual left, top, right, and bottom paddings to align a DataMatrix barcode within a layout.
// Tags: datamatrix, padding, png, barcodegenerator, bitmap

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates setting individual padding values for a DataMatrix barcode and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a DataMatrix barcode with custom padding and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Text to encode in the DataMatrix barcode
        string codeText = "ABC123";

        // Determine the output file path in the current working directory
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "DataMatrix.png");

        // Initialize the barcode generator for the DataMatrix symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            // Configure individual padding values (points)
            generator.Parameters.Barcode.Padding.Left.Point = 5f;
            generator.Parameters.Barcode.Padding.Top.Point = 10f;
            generator.Parameters.Barcode.Padding.Right.Point = 5f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 10f;

            // Optional: set the module (X) dimension for the barcode
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Generate the barcode image as a Bitmap
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Save the Bitmap to a PNG file using a file stream
                using (var stream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    bitmap.Save(stream, ImageFormat.Png);
                }
            }
        }

        // Inform the user where the image was saved
        Console.WriteLine($"DataMatrix barcode saved to: {outputPath}");
    }
}