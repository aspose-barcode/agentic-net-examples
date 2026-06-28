using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode and saving it as a high‑resolution JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode and writes it to a file.
    /// </summary>
    static void Main()
    {
        // Choose the barcode symbology (Code128) and the text to encode.
        BaseEncodeType symbology = EncodeTypes.Code128;
        string codeText = "1234567890";

        // Initialize the barcode generator with the selected symbology and text.
        using (var generator = new BarcodeGenerator(symbology, codeText))
        {
            // Configure the output image resolution (300 DPI) for high quality.
            generator.Parameters.Resolution = 300f;

            // Define the output file path and save the barcode as a JPEG image.
            string outputPath = "barcode.jpg";
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);

            // Inform the user that the barcode has been saved.
            Console.WriteLine($"Barcode saved to {outputPath} with 300 DPI resolution.");
        }
    }
}