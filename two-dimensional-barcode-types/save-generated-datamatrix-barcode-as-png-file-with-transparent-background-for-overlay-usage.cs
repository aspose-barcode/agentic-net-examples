using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a DataMatrix barcode and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a DataMatrix barcode with transparent background
    /// and saves it to a PNG file.
    /// </summary>
    static void Main()
    {
        // Output file name for the generated barcode image
        string outputPath = "datamatrix.png";

        // Create a barcode generator for DataMatrix encoding with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "Hello World"))
        {
            // Set the background to transparent so the barcode can be overlaid on other images
            generator.Parameters.BackColor = Color.Transparent;

            // Increase resolution to 300 DPI for higher quality output
            generator.Parameters.Resolution = 300f;

            // Save the generated barcode as a PNG image to the specified path
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"DataMatrix barcode saved to {outputPath}");
    }
}