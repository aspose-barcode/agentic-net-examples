using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating Code39FullASCII barcodes and saving them as SVG files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a set of barcodes and writes them to the file system.
    /// </summary>
    static void Main()
    {
        // Define sample barcode texts to be encoded.
        string[] texts = { "ABC123", "DEF456", "GHI789", "JKL012", "MNO345" };

        // Specify the output directory where SVG files will be saved.
        string outputDir = "Barcodes";

        // Ensure the output directory exists; create it if it does not.
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Iterate over each text value, generate a barcode, and save it.
        for (int i = 0; i < texts.Length; i++)
        {
            // Current barcode text.
            string codeText = texts[i];

            // Construct the full file path for the SVG output.
            string filePath = Path.Combine(outputDir, $"barcode_{i + 1}.svg");

            // Initialize the barcode generator with Code39FullASCII encoding.
            // This encoding is supported for SVG output in evaluation mode.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, codeText))
            {
                // Example parameter: set the module (X) dimension to 2 points.
                generator.Parameters.Barcode.XDimension.Point = 2f;

                try
                {
                    // Save the generated barcode as an SVG file.
                    generator.Save(filePath, BarCodeImageFormat.Svg);
                    Console.WriteLine($"Saved barcode {i + 1} to {filePath}");
                }
                catch (Exception ex)
                {
                    // Report any errors that occur during the save operation.
                    Console.WriteLine($"Failed to save barcode {i + 1}: {ex.Message}");
                }
            }
        }
    }
}