using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generating MaxiCode barcodes using Aspose.BarCode and saving them as PNG files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a series of MaxiCode barcodes from sample text strings.
    /// </summary>
    static void Main()
    {
        // Define a sample list of codetext strings to encode as MaxiCode barcodes.
        string[] codetexts = new string[]
        {
            "Sample message 1",
            "Sample message 2",
            "Sample message 3",
            "Sample message 4",
            "Sample message 5"
        };

        // Specify the output directory for generated PNG files.
        string outputDir = "MaxiCodeOutput";

        // Create the output directory if it does not already exist.
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Iterate over each codetext string and generate a corresponding MaxiCode barcode.
        for (int i = 0; i < codetexts.Length; i++)
        {
            // Retrieve the current text to encode.
            string text = codetexts[i];

            // Configure a standard MaxiCode (Mode 4) which encodes only the message.
            var maxiCode = new MaxiCodeStandardCodetext
            {
                Mode = MaxiCodeMode.Mode4,
                Message = text
            };

            // Build the full file path for the output PNG file.
            string filePath = Path.Combine(outputDir, $"maxicode_{i + 1}.png");

            // Generate the barcode image and save it to the specified file.
            using (var generator = new ComplexBarcodeGenerator(maxiCode))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Output a confirmation message to the console.
            Console.WriteLine($"Generated: {filePath}");
        }
    }
}