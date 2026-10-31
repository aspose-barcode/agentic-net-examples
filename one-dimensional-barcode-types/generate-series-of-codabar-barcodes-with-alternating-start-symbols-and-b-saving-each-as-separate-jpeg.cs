// Title: Generate Codabar Barcodes with Alternating Start Symbols and Save as JPEG
// Description: This example creates a series of Codabar barcodes, alternating the start/stop symbols between A and B, and saves each barcode as an individual JPEG image.
// Category-Description: Demonstrates barcode generation using Aspose.BarCode, focusing on the Codabar symbology. It utilizes the BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to configure barcode parameters such as X‑dimension and start/stop symbols. Developers commonly use these APIs to produce printable barcodes for inventory, shipping, and point‑of‑sale applications.
// Prompt: Generate a series of Codabar barcodes with alternating start symbols A and B, saving each as a separate JPEG.
// Tags: codabar, barcode, generation, jpeg, aspose.barcode, encode, startsymbol, stopsymbol

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating multiple Codabar barcodes with alternating start symbols and saving them as JPEG files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates an output directory, generates barcodes, and saves them.
    /// </summary>
    static void Main()
    {
        // Determine output directory and ensure it exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "CodabarOutput");
        Directory.CreateDirectory(outputDir);

        // Number of barcodes to generate
        int count = 6;

        // Loop through each barcode index
        for (int i = 0; i < count; i++)
        {
            // Build the barcode text (e.g., "123450", "123451", ...)
            string codeText = $"12345{i}";

            // Initialize generator with Codabar symbology and the text
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Codabar, codeText))
            {
                // Set X-dimension (module width) for the barcode
                generator.Parameters.Barcode.XDimension.Pixels = 2f;

                // Choose start/stop symbol based on index parity (A for even, B for odd)
                CodabarSymbol symbol = (i % 2 == 0) ? CodabarSymbol.A : CodabarSymbol.B;

                // Apply the selected start and stop symbols to the generator parameters
                generator.Parameters.Barcode.Codabar.StartSymbol = symbol;
                generator.Parameters.Barcode.Codabar.StopSymbol = symbol;

                // Build file path for the JPEG image
                string filePath = Path.Combine(outputDir, $"Codabar_{symbol}_{i}.jpeg");

                // Save the generated barcode as a JPEG file
                generator.Save(filePath, BarCodeImageFormat.Jpeg);

                // Log the saved file path
                Console.WriteLine($"Saved {filePath}");
            }
        }
    }
}