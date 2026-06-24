using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a GS1 DataMatrix barcode with a transparent background
/// and saving it as a PNG file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a GS1 DataMatrix barcode, configures a transparent background,
    /// saves the image as PNG, and writes a confirmation to the console.
    /// </summary>
    static void Main()
    {
        // Define the barcode text: AI 01 (GTIN) followed by a 14‑digit value.
        string codeText = "(01)12345678901231";

        // Initialize the barcode generator for GS1 DataMatrix with the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
        {
            // Configure the barcode's background to be fully transparent.
            generator.Parameters.BackColor = Color.Transparent;

            // Define the output file path for the PNG image.
            string outputPath = "gs1datamatrix.png";

            // Save the barcode image as PNG; transparency is preserved via the alpha channel.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user that the barcode image has been saved.
        Console.WriteLine("GS1 DataMatrix barcode saved to gs1datamatrix.png");
    }
}