using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating Postnet barcodes from text files in an input directory.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Processes each file in the Input folder,
    /// creates a Postnet barcode using the file name (without extension) as the
    /// barcode text, and saves the resulting image to the Output folder.
    /// </summary>
    static void Main()
    {
        // Define input and output directories relative to the current working directory.
        string inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Input");
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Output");

        // Ensure the input directory exists.
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
        }

        // Ensure the output directory exists.
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // If the input folder is empty, create a sample file to demonstrate the workflow.
        if (Directory.GetFiles(inputFolder).Length == 0)
        {
            string sampleFile = Path.Combine(inputFolder, "Sample.txt");
            File.WriteAllText(sampleFile, "12345");
        }

        // Process each file found in the input folder.
        foreach (string filePath in Directory.GetFiles(inputFolder))
        {
            // Use the file name (without extension) as the barcode text.
            string codeText = Path.GetFileNameWithoutExtension(filePath);
            if (string.IsNullOrWhiteSpace(codeText))
            {
                Console.WriteLine($"Skipping file '{filePath}' because it has no usable name for barcode text.");
                continue;
            }

            // Build the output file path for the generated barcode image.
            string outputPath = Path.Combine(outputFolder, $"{codeText}_postnet.png");

            // Generate a postal Postnet barcode for the code text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, codeText))
            {
                // Set a higher resolution for better image quality.
                generator.Parameters.Resolution = 300f;
                generator.Save(outputPath);
            }

            Console.WriteLine($"Generated barcode for '{filePath}' -> '{outputPath}'.");
        }
    }
}