// Title: Save Code128 barcode as BMP with custom color
// Description: Demonstrates generating a Code128 barcode, applying a dark green foreground, and saving it as a BMP image file.
// Prompt: Save a Code128 barcode to a BMP file using a custom foreground color.
// Tags: barcode, code128, bmp, color, generation, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Code128 barcode, sets a custom bar color,
/// and saves the result as a BMP image file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates the barcode and writes it to disk.
    /// </summary>
    static void Main()
    {
        // Text to encode in the barcode
        const string codeText = "ABC123";

        // Output file name (BMP format)
        const string outputFile = "code128.bmp";

        // Initialize the barcode generator for Code128 with the specified text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Apply a custom foreground (bar) color – dark green in this case
            generator.Parameters.Barcode.BarColor = Color.DarkGreen;

            // Save the generated barcode image as a BMP file
            generator.Save(outputFile, BarCodeImageFormat.Bmp);
        }

        // Inform the user that the file has been created
        Console.WriteLine($"Code128 barcode saved to '{outputFile}'.");
    }
}