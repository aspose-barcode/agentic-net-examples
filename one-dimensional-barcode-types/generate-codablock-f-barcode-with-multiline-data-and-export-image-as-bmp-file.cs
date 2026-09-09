// Title: Generate Codablock‑F barcode with multiline data and save as BMP
// Description: Demonstrates creating a Codablock‑F barcode containing multiple lines of text and exporting it to a BMP image file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator with EncodeTypes.CodablockF. It shows how to configure barcode parameters such as X‑dimension and aspect ratio, handle multiline input, and save the result in BMP format—common tasks for developers needing high‑density 2‑D barcodes in desktop or server applications.
// Prompt: Generate a Codablock‑F barcode with multiline data and export the image as a BMP file.
// Tags: codablock-f, barcode generation, multiline data, bmp output, aspose.barcode, encode types

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a Codablock‑F barcode with multiline data and saves it as a BMP image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode and writes the output file path to the console.
    /// </summary>
    static void Main()
    {
        // Build output file path in the current directory
        string outputPath = Path.Combine(Environment.CurrentDirectory, "CodablockF_Multiline.bmp");

        // Multiline data for the barcode; newline characters separate lines
        string codeText = "First line of data\nSecond line of data\nThird line of data";

        // Initialize the barcode generator with Codablock‑F symbology and the multiline text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.CodablockF, codeText))
        {
            // Set the X dimension (module width) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Adjust the aspect ratio for Codablock‑F to improve readability
            generator.Parameters.Barcode.Codablock.AspectRatio = 15;

            // Save the generated barcode as a BMP image
            generator.Save(outputPath, BarCodeImageFormat.Bmp);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"Codablock-F barcode saved to: {outputPath}");
    }
}