using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a GS1 DataMatrix barcode and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a GS1 DataMatrix barcode and writes it to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path where the barcode image will be saved.
        string outputPath = "gs1datamatrix.png";

        // Sample GS1 DataMatrix code text (example with GTIN).
        // The parentheses denote Application Identifiers as per GS1 standards.
        string codeText = "(01)12345678901231";

        // Create a barcode generator configured for GS1 DataMatrix encoding.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
        {
            // Set the DataMatrix error correction level to ECC200 (standard for GS1 DataMatrix).
            generator.Parameters.Barcode.DataMatrix.DataMatrixEcc = DataMatrixEccType.Ecc200;

            // Optional: set the image resolution to 300 DPI for higher quality output.
            generator.Parameters.Resolution = 300f;

            // Save the generated barcode as a PNG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user that the barcode has been successfully saved.
        Console.WriteLine($"GS1 DataMatrix barcode saved to: {outputPath}");
    }
}