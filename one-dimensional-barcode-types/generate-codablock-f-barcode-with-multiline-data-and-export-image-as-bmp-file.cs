// Title: Generate Codablock‑F Barcode with Multiline Data and Save as BMP
// Description: Demonstrates creating a Codablock‑F barcode containing multiple lines of text and exporting it to a BMP image file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.CodablockF. It shows setting multiline CodeText, configuring Codablock‑F specific parameters such as rows and columns, and saving the result in BMP format. Developers working on inventory, shipping labels, or any application requiring high‑density 2‑D barcodes can reference this pattern for quick implementation.
// Prompt: Generate a Codablock‑F barcode with multiline data and export the image as a BMP file.
// Tags: codablockf, barcode, generation, multiline, bmp, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Codablock‑F barcode with multiline data
/// and saves it as a BMP image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates the barcode, configures layout, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the BMP image.
        string outputPath = "codablockf.bmp";

        // Prepare multiline text to be encoded in the barcode.
        // Each line is separated by a carriage‑return/line‑feed sequence.
        string codeText = "First line\r\nSecond line\r\nThird line";

        // Initialize the barcode generator for the Codablock‑F symbology.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.CodablockF))
        {
            // Assign the multiline text to the generator.
            generator.CodeText = codeText;

            // Optional: fine‑tune the barcode layout by specifying rows and columns.
            generator.Parameters.Barcode.Codablock.Rows = 3;
            generator.Parameters.Barcode.Codablock.Columns = 30;

            // Save the generated barcode as a BMP file.
            generator.Save(outputPath, BarCodeImageFormat.Bmp);
        }

        // Inform the user where the file has been saved.
        Console.WriteLine($"Codablock‑F barcode saved to: {outputPath}");
    }
}