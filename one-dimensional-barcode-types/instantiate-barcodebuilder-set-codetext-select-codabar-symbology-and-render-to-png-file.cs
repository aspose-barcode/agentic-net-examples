// Title: Generate Codabar Barcode and Save as PNG
// Description: Demonstrates how to create a Codabar barcode using Aspose.BarCode, set the code text, and save the image as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of the BarcodeGenerator class with EncodeTypes to produce barcodes. Typical scenarios include creating shipping labels, inventory tags, or any application requiring Codabar symbology. Developers often need to set the encoded text, choose a symbology, and export the result to common image formats such as PNG.
// Prompt: Instantiate BarCodeBuilder, set CodeText, select Codabar symbology, and render to PNG file.
// Tags: barcode, codabar, generation, png, aspose.barcode, barcodegenerator, encodetypes

using System;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Codabar barcode and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a BarcodeGenerator, configures it, and writes the barcode to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated PNG image
        string outputPath = "codabar.png";

        // Initialize the barcode generator with the Codabar symbology
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Codabar))
        {
            // Assign the text that will be encoded in the barcode
            generator.CodeText = "A123456A";

            // Save the generated barcode image to the specified path in PNG format
            generator.Save(outputPath);
        }

        // Inform the user that the barcode has been successfully saved
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}