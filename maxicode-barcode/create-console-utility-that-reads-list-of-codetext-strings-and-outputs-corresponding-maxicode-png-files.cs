// Title: Generate MaxiCode PNG files from codetext list
// Description: Reads a text file where each line contains a codetext string and creates a MaxiCode barcode image (PNG) for each entry.
// Category-Description: Demonstrates Aspose.BarCode generation of MaxiCode symbology using the BarcodeGenerator class. This example belongs to the barcode creation category, showing how to configure encoding, handle invalid codetext, and save images in PNG format. Developers working with shipping, logistics, or inventory systems often need to produce MaxiCode barcodes programmatically.
// Prompt: Create a console utility that reads a list of codetext strings and outputs corresponding MaxiCode PNG files.
// Tags: barcode, maxicode, generation, png, console, aspose.barcode, aspnet

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Console utility that reads codetext strings from a file and generates MaxiCode PNG images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Processes each codetext line, generates a MaxiCode barcode, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Path to the text file containing codetext strings (one per line)
        const string inputFile = "codetexts.txt";

        // Verify that the input file exists before proceeding
        if (!File.Exists(inputFile))
        {
            Console.WriteLine($"Input file '{inputFile}' not found.");
            return;
        }

        // Read all lines from the file; each line represents a separate codetext
        string[] lines = File.ReadAllLines(inputFile);

        // Iterate through each line, generating a barcode for non‑empty entries
        for (int i = 0; i < lines.Length; i++)
        {
            string codeText = lines[i].Trim();

            // Skip empty lines to avoid generating empty barcodes
            if (string.IsNullOrEmpty(codeText))
                continue;

            // Create a MaxiCode generator with the current codetext
            using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, codeText))
            {
                // Throw an exception if the codetext is not valid for MaxiCode
                generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;

                // Optional: customize colors (commented out by default)
                // generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                // generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Save the generated image to a memory stream in PNG format
                using (var memoryStream = new MemoryStream())
                {
                    generator.Save(memoryStream, BarCodeImageFormat.Png);
                    memoryStream.Position = 0;

                    // Write the PNG file to disk with a sequential name
                    string outputPath = $"maxicode_{i + 1}.png";
                    File.WriteAllBytes(outputPath, memoryStream.ToArray());
                    Console.WriteLine($"Generated '{outputPath}' for codetext: {codeText}");
                }
            }
        }
    }
}