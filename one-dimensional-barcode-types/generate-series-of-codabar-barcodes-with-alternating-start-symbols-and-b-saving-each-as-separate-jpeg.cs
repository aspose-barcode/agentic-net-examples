// Title: Generate multiple Codabar barcodes with alternating start symbols
// Description: Demonstrates how to create a series of Codabar barcodes, alternating the start/stop symbols between A and B, and save each as a JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and Codabar parameters. Typical use cases include batch creation of barcodes for inventory, shipping, or point‑of‑sale systems where different start symbols are required. Developers often need to automate image output in common formats such as JPEG.
// Prompt: Generate a series of Codabar barcodes with alternating start symbols A and B, saving each as a separate JPEG.
// Tags: codabar, generation, jpeg, aspose.barcode, barcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Program that generates a set of Codabar barcodes with alternating start symbols and saves them as JPEG files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates the output folder, generates the barcodes, and writes status messages to the console.
    /// </summary>
    static void Main()
    {
        // Determine the folder where barcode images will be stored
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "CodabarBarcodes");
        if (!Directory.Exists(outputFolder))
        {
            // Create the folder if it does not already exist
            Directory.CreateDirectory(outputFolder);
        }

        // Define how many barcode images to generate
        int count = 6; // example count

        // Loop to generate each barcode
        for (int i = 0; i < count; i++)
        {
            // Choose start/stop symbol: A for even indexes, B for odd indexes
            CodabarSymbol startSymbol = (i % 2 == 0) ? CodabarSymbol.A : CodabarSymbol.B;

            // Initialize a Codabar barcode generator
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Codabar))
            {
                // Set the data to encode (digits only; start/stop symbols are set via parameters)
                generator.CodeText = "123456";

                // Apply the selected start and stop symbols
                generator.Parameters.Barcode.Codabar.StartSymbol = startSymbol;
                generator.Parameters.Barcode.Codabar.StopSymbol = startSymbol;

                // Build a unique file name that includes the index and start symbol
                string fileName = $"codabar_{i + 1}_{startSymbol}.jpg";
                string filePath = Path.Combine(outputFolder, fileName);

                // Save the generated barcode as a JPEG image
                generator.Save(filePath, BarCodeImageFormat.Jpeg);
                Console.WriteLine($"Saved {filePath}");
            }
        }

        Console.WriteLine("Barcode generation completed.");
    }
}