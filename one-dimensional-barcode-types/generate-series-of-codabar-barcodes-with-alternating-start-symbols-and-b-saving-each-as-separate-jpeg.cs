// Title: Generate Codabar barcodes with alternating start symbols
// Description: Demonstrates creating multiple Codabar barcodes using start symbols A and B, each saved as a JPEG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure Codabar symbology with specific start/stop symbols, set barcode data, and export images. It uses BarcodeGenerator, EncodeTypes, CodabarSymbol, and BarCodeImageFormat classes—common tasks for developers needing custom barcode creation for labeling, inventory, or point‑of‑sale systems.
// Prompt: Generate a series of Codabar barcodes with alternating start symbols A and B, saving each as a separate JPEG.
// Tags: codabar, barcode generation, jpeg output, aspose.barcode, encode types, startstop symbols

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a series of Codabar barcodes with alternating start/stop symbols
/// (A for even indices, B for odd indices) and saves each barcode as a separate JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates the output directory, iterates to generate the
    /// requested number of barcodes, configures the Codabar start/stop symbols, and saves each
    /// image as a JPEG file.
    /// </summary>
    static void Main()
    {
        // Sample data to encode (Codabar allows digits and some symbols)
        const string data = "123456";

        // Number of barcodes to generate
        const int count = 5;

        // Ensure output directory exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "CodabarOutputs");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Generate each barcode with alternating start/stop symbols
        for (int i = 0; i < count; i++)
        {
            // Choose start/stop symbol: A for even index, B for odd index
            CodabarSymbol startStopSymbol = (i % 2 == 0) ? CodabarSymbol.A : CodabarSymbol.B;

            // Create a generator for Codabar symbology
            using (var generator = new BarcodeGenerator(EncodeTypes.Codabar))
            {
                // Apply the selected start and stop symbols
                generator.Parameters.Barcode.Codabar.StartSymbol = startStopSymbol;
                generator.Parameters.Barcode.Codabar.StopSymbol = startStopSymbol;

                // Assign the data to encode
                generator.CodeText = data;

                // Build the output file name (e.g., codabar_A_1.jpg)
                string fileName = $"codabar_{startStopSymbol}_{i + 1}.jpg";
                string filePath = Path.Combine(outputDir, fileName);

                // Save the barcode as a JPEG image
                generator.Save(filePath, BarCodeImageFormat.Jpeg);
                Console.WriteLine($"Saved {filePath}");
            }
        }
    }
}