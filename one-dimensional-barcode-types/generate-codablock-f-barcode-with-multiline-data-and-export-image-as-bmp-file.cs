using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generation of a Codablock‑F barcode with multiline text
/// and saves it as a BMP image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates the barcode and writes the output file path to the console.
    /// </summary>
    static void Main()
    {
        // Define the output file name (saved in the current working directory)
        string outputPath = "codablockf.bmp";

        // Multiline text to encode; CRLF (\r\n) denotes line breaks
        string codeText = "First line\r\nSecond line\r\nThird line";

        // Initialize the barcode generator for Codablock‑F with the provided text
        using (var generator = new BarcodeGenerator(EncodeTypes.CodablockF, codeText))
        {
            // Optional: customize the barcode matrix size
            // generator.Parameters.Barcode.CodablockRows = 3;      // number of rows
            // generator.Parameters.Barcode.CodablockColumns = 1;   // number of columns

            // Render and save the barcode image in BMP format
            generator.Save(outputPath, BarCodeImageFormat.Bmp);
        }

        // Inform the user where the image was saved
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}