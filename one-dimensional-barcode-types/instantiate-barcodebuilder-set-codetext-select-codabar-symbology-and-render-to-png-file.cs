// Title: Generate Codabar barcode and save as PNG
// Description: Demonstrates creating a Codabar barcode using Aspose.BarCode, setting the code text, and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator with EncodeTypes.Codabar. It shows typical steps such as initializing the generator, configuring CodeText, and exporting the barcode to a common image format. Developers working with barcode creation, especially for Codabar symbology in retail or logistics, can reference this pattern for quick implementation.
// Prompt: Instantiate BarCodeBuilder, set CodeText, select Codabar symbology, and render to PNG file.
// Tags: barcode, codabar, generation, png, aspose.barcode, encode types

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Codabar barcode and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a BarcodeGenerator for Codabar,
    /// sets the encoded text, and writes the barcode image to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "codabar.png";

        // Initialize a BarcodeGenerator for the Codabar symbology.
        // The using statement ensures proper disposal of resources.
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar))
        {
            // Set the text to be encoded.
            // Codabar requires start/stop symbols (e.g., 'A' and 'A') surrounding the data.
            generator.CodeText = "A123456A";

            // Save the generated barcode as a PNG file at the specified path.
            generator.Save(outputPath);
        }

        // Inform the user that the barcode has been saved.
        Console.WriteLine($"Codabar barcode saved to {outputPath}");
    }
}