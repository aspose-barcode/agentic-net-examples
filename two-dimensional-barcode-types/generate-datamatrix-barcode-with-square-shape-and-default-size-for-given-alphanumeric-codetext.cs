// Title: Generate a square DataMatrix barcode with default size
// Description: Demonstrates creating a DataMatrix barcode using Aspose.BarCode with a simple alphanumeric code text and saving it as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing the use of the BarcodeGenerator class with EncodeTypes.DataMatrix. Developers commonly use these APIs to produce barcodes for labeling, inventory, and packaging applications, where automatic sizing and default square shape are often sufficient.
// Prompt: Generate a DataMatrix barcode with square shape and default size for given alphanumeric CodeText.
// Tags: datamatrix, barcode, generation, png, aspose.barcode, encode, symbology

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides an entry point that creates a DataMatrix barcode with default settings
/// and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a square DataMatrix barcode from an alphanumeric string and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define the alphanumeric text to encode in the barcode.
        string codeText = "ABC123";

        // Initialize the barcode generator for DataMatrix symbology with the provided text.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            // The generator's default configuration creates a square DataMatrix with automatic sizing.
            // No additional shape or version settings are required for this basic scenario.

            // Specify the output file name and save the barcode as a PNG image in the current directory.
            string outputPath = "DataMatrix.png";
            generator.Save(outputPath);

            // Inform the user where the barcode image has been saved.
            Console.WriteLine($"DataMatrix barcode saved to: {outputPath}");
        }
    }
}