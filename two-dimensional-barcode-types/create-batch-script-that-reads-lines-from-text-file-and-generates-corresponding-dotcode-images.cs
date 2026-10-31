// Title: Batch generation of DotCode barcodes from a text file
// Description: Demonstrates reading lines from a text file and creating DotCode barcode images for each line using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category. It showcases the use of the BarcodeGenerator class with EncodeTypes.DotCode to produce PNG images. Typical scenarios include bulk creation of barcodes for inventory, labeling, or data encoding tasks where a list of values must be transformed into visual codes. Developers often need to automate file I/O, configure barcode dimensions, and handle errors during batch processing.
// Prompt: Create a batch script that reads lines from a text file and generates corresponding DotCode images.
// Tags: dotcode, barcode generation, batch processing, png, aspose.barcode, image output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides a console application that reads a text file and generates a set of DotCode barcode images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Performs file preparation, reads input lines, and creates barcode images.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a sample input file if it does not already exist.
        // --------------------------------------------------------------------
        string inputFile = Path.Combine(Path.GetTempPath(), "dotcode_input.txt");
        if (!File.Exists(inputFile))
        {
            string[] sampleLines = { "Hello", "Aspose", "DotCode", "12345", "Sample Text" };
            File.WriteAllLines(inputFile, sampleLines);
        }

        // --------------------------------------------------------------------
        // Create a unique output folder for the generated barcode images.
        // --------------------------------------------------------------------
        string outputFolder = Path.Combine(Path.GetTempPath(), "DotCodeBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // --------------------------------------------------------------------
        // Read all lines from the input file and limit processing to a maximum of 5 lines.
        // --------------------------------------------------------------------
        string[] lines = File.ReadAllLines(inputFile);
        int count = Math.Min(lines.Length, 5);

        // --------------------------------------------------------------------
        // Iterate over each line, generate a DotCode barcode, and save it as a PNG file.
        // --------------------------------------------------------------------
        for (int i = 0; i < count; i++)
        {
            string text = lines[i];
            string outputPath = Path.Combine(outputFolder, $"barcode_{i + 1}.png");
            try
            {
                // Initialize the barcode generator with DotCode symbology and the current text.
                using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DotCode, text))
                {
                    // Set the X-dimension (module size) to 10 pixels for better readability.
                    generator.Parameters.Barcode.XDimension.Pixels = 10f;

                    // Save the generated barcode image in PNG format.
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                }

                Console.WriteLine($"Generated: {outputPath}");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during barcode generation for the current line.
                Console.WriteLine($"Failed to generate barcode for line {i + 1}: {ex.Message}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}