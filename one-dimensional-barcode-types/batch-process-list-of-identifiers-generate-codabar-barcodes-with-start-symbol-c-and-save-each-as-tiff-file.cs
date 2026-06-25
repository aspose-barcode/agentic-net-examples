using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating Codabar barcodes for a set of identifiers and saving them as TIFF images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates barcodes for predefined identifiers and writes them to disk.
    /// </summary>
    static void Main()
    {
        // Define a sample list of identifiers to encode as barcodes.
        string[] identifiers = new string[]
        {
            "12345",
            "67890",
            "ABCDE",
            "98765",
            "XYZ12"
        };

        // Determine the output folder path (a subfolder named "Barcodes" in the current directory).
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");

        // Ensure the output folder exists; create it if it does not.
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Iterate over each identifier and generate a corresponding Codabar barcode.
        foreach (string id in identifiers)
        {
            // Initialize a BarcodeGenerator for the current identifier using the Codabar symbology.
            using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, id))
            {
                // Configure the start and stop symbols for Codabar (C as start, D as stop).
                generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.C;
                generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.D;

                // Construct the full file path for the output TIFF image (e.g., "12345.tiff").
                string outputPath = Path.Combine(outputFolder, $"{id}.tiff");

                // Save the generated barcode image to the specified path in TIFF format.
                generator.Save(outputPath, BarCodeImageFormat.Tiff);

                // Log the successful creation of the barcode file to the console.
                Console.WriteLine($"Saved barcode for '{id}' to '{outputPath}'.");
            }
        }
    }
}