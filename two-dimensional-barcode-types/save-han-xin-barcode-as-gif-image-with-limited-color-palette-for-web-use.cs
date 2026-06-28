using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generation of a Han Xin barcode and saves it as a GIF image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a Han Xin barcode and writes it to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file name (saved in the current working directory)
        string outputPath = "HanXinBarcode.gif";

        // Text to encode in the barcode
        string codeText = "Sample HanXin 123";

        // Initialize the barcode generator for Han Xin symbology with the provided text
        using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, codeText))
        {
            // Set the error correction level (optional, improves readability)
            generator.Parameters.Barcode.HanXin.ErrorLevel = HanXinErrorLevel.L2;

            // Define image resolution suitable for web display (96 DPI)
            generator.Parameters.Resolution = 96f;

            // Save the generated barcode as a GIF image (limited color palette, web-friendly)
            generator.Save(outputPath, BarCodeImageFormat.Gif);
        }

        // Output the full path of the saved barcode image to the console
        Console.WriteLine($"Han Xin barcode saved to: {Path.GetFullPath(outputPath)}");
    }
}