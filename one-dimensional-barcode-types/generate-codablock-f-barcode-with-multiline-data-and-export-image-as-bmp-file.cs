// Title: Generate Codablock‑F barcode with multiline data and save as BMP
// Description: Demonstrates creating a Codablock‑F barcode containing multiple lines of text and exporting it to a BMP image file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.CodablockF. Developers often need to encode multiline data in 2‑D barcodes for inventory, shipping, or document tracking, and then output the result in common image formats such as BMP, PNG, or JPEG. The snippet shows setting the CodeText, optional matrix dimensions, and saving the image.
// Prompt: Generate a Codablock‑F barcode with multiline data and export the image as a BMP file.
// Tags: codablockf, barcode, generation, bmp, multiline, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Codablock‑F barcode with multiline data and saving it as a BMP image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode and writes a confirmation message.
    /// </summary>
    static void Main()
    {
        // Define multiline text that will be encoded in the barcode.
        string codeText = "First line\nSecond line\nThird line";

        // Initialize the barcode generator for the Codablock‑F symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.CodablockF))
        {
            // Assign the multiline text to the generator.
            generator.CodeText = codeText;

            // Optional: specify the number of rows and columns for the barcode matrix.
            // generator.Parameters.Barcode.CodablockRows = 3;
            // generator.Parameters.Barcode.CodablockColumns = 10;

            // Save the generated barcode as a BMP image file.
            generator.Save("codablockf.bmp");
        }

        // Inform the user that the barcode image has been saved.
        Console.WriteLine("Codablock‑F barcode saved as codablockf.bmp");
    }
}