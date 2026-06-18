using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a DataMatrix barcode with custom padding and image size using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a DataMatrix barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator for DataMatrix with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "Sample123"))
        {
            // Set padding for each side (in points) to position the barcode within the image.
            generator.Parameters.Barcode.Padding.Left.Point = 10f;
            generator.Parameters.Barcode.Padding.Top.Point = 20f;
            generator.Parameters.Barcode.Padding.Right.Point = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 20f;

            // Define the overall image dimensions (in points) to accommodate the barcode and padding.
            generator.Parameters.ImageWidth.Point = 200f;
            generator.Parameters.ImageHeight.Point = 200f;

            // Save the generated barcode image to a PNG file.
            generator.Save("datamatrix.png");
        }

        // Inform the user that the barcode has been created.
        Console.WriteLine("DataMatrix barcode generated: datamatrix.png");
    }
}