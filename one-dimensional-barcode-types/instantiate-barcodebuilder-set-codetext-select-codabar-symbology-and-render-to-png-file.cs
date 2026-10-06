// Title: Generate Codabar barcode and save as PNG
// Description: Demonstrates creating a Codabar barcode with start/stop symbols using Aspose.BarCode and saving it to a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator with EncodeTypes.Codabar. It shows setting barcode parameters such as start and stop symbols, and exporting the result to an image format. Developers working with barcode creation for inventory, ticketing, or labeling often need to customize symbology settings and produce PNG outputs for web or print integration.
// Prompt: Instantiate BarCodeBuilder, set CodeText, select Codabar symbology, and render to PNG file.
// Tags: barcode, codabar, generation, png, aspose.barcode, encode types, image export

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Codabar barcode and saving it as a PNG image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a BarcodeGenerator, configures Codabar start/stop symbols, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Determine the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "codabar.png");
        // The data to encode; Codabar requires start/stop characters (A in this case).
        string codeText = "A12345A";

        // Initialize the generator with Codabar symbology and the code text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, codeText))
        {
            // Set the start and stop symbols to 'A' as required by the Codabar specification.
            generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.A;
            generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.A;

            // Save the generated barcode as a PNG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}