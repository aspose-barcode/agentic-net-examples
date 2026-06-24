using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a GS1 DataMatrix barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a GS1 DataMatrix barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the barcode text using GS1 Application Identifier (AI) 01 for GTIN.
        string codeText = "(01)12345678901231";

        // Initialize the barcode generator with GS1 DataMatrix symbology and the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
        {
            // Configure the barcode appearance: set the bar (foreground) color to blue.
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Set the background color of the image to white.
            generator.Parameters.BackColor = Color.White;

            // Define the output file path for the generated PNG image.
            string outputPath = "gs1datamatrix.png";

            // Save the generated barcode image to the specified path.
            generator.Save(outputPath);

            // Inform the user that the barcode has been saved.
            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }
}