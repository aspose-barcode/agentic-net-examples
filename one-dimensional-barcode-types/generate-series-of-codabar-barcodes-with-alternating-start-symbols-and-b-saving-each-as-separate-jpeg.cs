// Title: Generate Codabar Barcodes with Alternating Start Symbols and Save as JPEG
// Description: Demonstrates how to create multiple Codabar barcodes, alternating the start/stop symbols between A and B, and save each as an individual JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and Codabar parameters to produce Codabar symbology images. Typical use cases include generating printable barcode labels, inventory tags, or batch barcode files where start/stop symbols vary. Developers often need to configure X-dimension, start/stop symbols, and export to common image formats such as JPEG.
// Prompt: Generate a series of Codabar barcodes with alternating start symbols A and B, saving each as a separate JPEG.
// Tags: codabar, barcode generation, jpeg, aspose.barcode, encode types, image output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating Codabar barcodes with alternating start symbols and saving them as JPEG files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates an output directory, generates barcodes, and saves them.
    /// </summary>
    static void Main()
    {
        // Determine output directory path relative to current working directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "CodabarOutput");

        // Ensure the output directory exists
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Barcode data to encode (Codabar requires start/stop symbols, which are set separately)
        string codeText = "-12345-";

        // Number of barcode images to generate
        int count = 4;

        // Loop to generate each barcode with alternating start/stop symbols
        for (int i = 0; i < count; i++)
        {
            // Choose start symbol A for even indexes, B for odd indexes; stop symbol matches start
            CodabarSymbol startSymbol = (i % 2 == 0) ? CodabarSymbol.A : CodabarSymbol.B;
            CodabarSymbol stopSymbol = startSymbol;

            // Initialize the barcode generator with Codabar symbology and the data string
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Codabar, codeText))
            {
                // Set the X-dimension (module width) in pixels
                generator.Parameters.Barcode.XDimension.Pixels = 2;

                // Apply the selected start and stop symbols
                generator.Parameters.Barcode.Codabar.StartSymbol = startSymbol;
                generator.Parameters.Barcode.Codabar.StopSymbol = stopSymbol;

                // Build the output file name reflecting the symbols used
                string fileName = $"Codabar_Start{startSymbol}_Stop{stopSymbol}.jpg";
                string filePath = Path.Combine(outputDir, fileName);

                // Save the generated barcode as a JPEG image
                generator.Save(filePath, BarCodeImageFormat.Jpeg);

                // Inform the user of the saved file location
                Console.WriteLine($"Saved: {filePath}");
            }
        }
    }
}