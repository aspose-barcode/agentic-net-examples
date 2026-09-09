// Title: Generate MaxiCode barcodes from a list of strings and save as PNG files
// Description: Demonstrates how to create MaxiCode barcodes using Aspose.BarCode and write each barcode to a PNG image file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator with EncodeTypes.MaxiCode. It shows typical steps such as setting barcode parameters, defining output paths, and saving images, which developers often need when integrating MaxiCode generation into console utilities or batch processing pipelines.
// Prompt: Create a console utility that reads a list of codetext strings and outputs corresponding MaxiCode PNG files.
// Tags: maxicode, barcode generation, png output, aspose.barcode, console utility

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Console application that generates MaxiCode barcode images from predefined codetext strings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates PNG files for each codetext using Aspose.BarCode.
    /// </summary>
    static void Main()
    {
        // Define a sample list of MaxiCode codetext strings
        List<string> codetexts = new List<string>
        {
            "[)>\u001e01\u001dB1050\u001d056\u001d001\u001dADDITIONAL DATA\u0004",
            "123456789\u001d056\u001d001\u001dADDITIONAL DATA\u0004",
            "Åspóse.Barcóde©"
        };

        // Create a unique temporary output directory for the generated images
        string outputDir = Path.Combine(Path.GetTempPath(), "MaxiCodeOutput_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        int index = 1;
        // Iterate over each codetext string and generate a corresponding PNG file
        foreach (string text in codetexts)
        {
            // Build the full file path for the current image
            string filePath = Path.Combine(outputDir, $"MaxiCode_{index}.png");

            // Initialize the barcode generator for MaxiCode with the current codetext
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.MaxiCode, text))
            {
                // Optional: increase module size for better visibility in the output image
                generator.Parameters.Barcode.XDimension.Pixels = 15f;

                // Save the generated barcode as a PNG file
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Inform the user about the generated file
            Console.WriteLine($"Generated: {filePath}");
            index++;
        }

        // Final message indicating completion of the generation process
        Console.WriteLine("All MaxiCode images have been generated.");
    }
}