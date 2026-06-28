using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a DataMatrix barcode and saving it as an image file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a DataMatrix barcode with specific settings and writes it to a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "datamatrix.png";

        // Initialize a BarcodeGenerator for DataMatrix with the desired encoded text.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "ABC123XYZ"))
        {
            // Set the aspect ratio to 1 to enforce a square shape for the barcode.
            generator.Parameters.Barcode.DataMatrix.AspectRatio = 1f;

            // Choose a specific DataMatrix version (20x20 modules) to control the size.
            generator.Parameters.Barcode.DataMatrix.DataMatrixVersion = DataMatrixVersion.ECC200_20x20;

            // Save the barcode image to the specified path (PNG format is used by default).
            generator.Save(outputPath);
        }

        // Inform the user that the barcode has been saved.
        Console.WriteLine($"DataMatrix barcode saved to {outputPath}");
    }
}