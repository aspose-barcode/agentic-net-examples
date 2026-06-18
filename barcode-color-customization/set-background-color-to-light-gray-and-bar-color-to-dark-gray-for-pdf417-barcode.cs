using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a PDF417 barcode using Aspose.BarCode and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a PDF417 barcode with sample text,
    /// customizes its colors, saves it to a file, and writes a confirmation to the console.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator for PDF417 format with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "Sample PDF417 Text"))
        {
            // Configure the background color of the barcode image to light gray.
            generator.Parameters.BackColor = Color.LightGray;

            // Configure the foreground (bar) color of the barcode to dark gray.
            generator.Parameters.Barcode.BarColor = Color.DarkGray;

            // Save the generated barcode image as a PNG file named "pdf417.png".
            generator.Save("pdf417.png");
        }

        // Output a message indicating successful generation and location of the file.
        Console.WriteLine("PDF417 barcode generated: pdf417.png");
    }
}