// Title: Generate Codabar Barcodes with Alternating Start Symbols and Save as JPEG
// Description: This example creates a set of Codabar barcodes, alternating the start/stop symbols between A and B, and writes each barcode to an individual JPEG file.
// Category-Description: Demonstrates Aspose.BarCode barcode generation for the Codabar symbology. It shows how to configure the BarcodeGenerator, set Codabar start/stop symbols, adjust image dimensions, and export to JPEG using BarCodeImageFormat. Developers working with barcode creation, especially for inventory or labeling systems, can use this pattern to produce multiple barcode images programmatically.
// Prompt: Generate a series of Codabar barcodes with alternating start symbols A and B, saving each as a separate JPEG.
// Tags: codabar, barcode generation, jpeg, aspose.barcode, encode types, image output, start symbol, stop symbol

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating Codabar barcodes with alternating start symbols and saving them as JPEG images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcode images and writes their paths to the console.
    /// </summary>
    static void Main()
    {
        // Define output directory and ensure it exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "CodabarOutput");
        Directory.CreateDirectory(outputDir);

        // Data to encode and the start symbols to alternate between
        string[] dataTexts = { "12345", "67890", "11223", "44556", "77889" };
        CodabarSymbol[] startSymbols = { CodabarSymbol.A, CodabarSymbol.B };

        // Loop through each data item, generate a barcode, and save it as JPEG
        for (int i = 0; i < dataTexts.Length; i++)
        {
            string codeText = dataTexts[i];
            // Alternate start symbol based on index
            CodabarSymbol startSymbol = startSymbols[i % startSymbols.Length];
            // Use the same symbol for the stop character
            CodabarSymbol stopSymbol = startSymbol;

            // Build file name and full path for the output image
            string fileName = $"Codabar_{startSymbol}_{i + 1}.jpeg";
            string filePath = Path.Combine(outputDir, fileName);

            // Create and configure the barcode generator
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Codabar, codeText))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 2f; // Set module width
                generator.Parameters.Barcode.Codabar.StartSymbol = startSymbol;
                generator.Parameters.Barcode.Codabar.StopSymbol = stopSymbol;

                // Save the generated barcode as a JPEG file
                generator.Save(filePath, BarCodeImageFormat.Jpeg);
            }

            // Output the location of the saved file
            Console.WriteLine($"Saved: {filePath}");
        }
    }
}