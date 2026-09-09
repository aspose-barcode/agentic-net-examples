// Title: Generate Postnet Barcodes from Text Files
// Description: This example creates temporary input and output directories, reads postal codes from text files, and generates Postnet barcode images using Aspose.BarCode.
// Category-Description: Demonstrates Aspose.BarCode barcode generation for postal symbologies. It showcases the BarcodeGenerator class, EncodeTypes enumeration, and image saving with BarCodeImageFormat. Developers building mailing solutions often need to convert numeric postal codes into machine‑readable barcodes for printing or electronic processing; this sample provides a quick reference for such scenarios.
// Prompt: Develop a Windows service that monitors a folder and generates postal barcodes for new files automatically.
// Tags: postnet, barcode generation, image output, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Sample console application that generates Postnet barcodes from text files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates temporary folders, writes sample code files, reads them, and generates PNG barcode images.
    /// </summary>
    static void Main()
    {
        // Create a unique base folder in the temp directory
        string baseFolder = Path.Combine(Path.GetTempPath(), "PostalBatch_" + Guid.NewGuid().ToString("N"));
        // Define subfolders for input text files and output barcode images
        string inputFolder = Path.Combine(baseFolder, "Input");
        string outputFolder = Path.Combine(baseFolder, "Output");
        Directory.CreateDirectory(inputFolder);
        Directory.CreateDirectory(outputFolder);

        // Sample postal codes for Postnet (numeric strings)
        string[] sampleCodes = new string[]
        {
            "1159628792", // 10 digits (valid for Postnet)
            "123456789",  // 9 digits
            "12345"       // 5 digits
        };

        // Write each sample code to a separate text file in the input folder
        for (int i = 0; i < sampleCodes.Length; i++)
        {
            string filePath = Path.Combine(inputFolder, $"Sample{i + 1}.txt");
            File.WriteAllText(filePath, sampleCodes[i]);
        }

        // List of files to process (could be discovered dynamically in a real service)
        string[] files = new string[]
        {
            Path.Combine(inputFolder, "Sample1.txt"),
            Path.Combine(inputFolder, "Sample2.txt"),
            Path.Combine(inputFolder, "Sample3.txt")
        };

        // Iterate over each file, read the code, and generate a Postnet barcode image
        foreach (string file in files)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            // Read and trim the postal code from the file
            string codeText = File.ReadAllText(file).Trim();
            if (string.IsNullOrEmpty(codeText))
            {
                Console.WriteLine($"Empty code text in file: {file}");
                continue;
            }

            try
            {
                // Initialize the barcode generator for Postnet with the read code
                using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, codeText))
                {
                    // Set the X-dimension (module width) in pixels
                    generator.Parameters.Barcode.XDimension.Pixels = 3f;
                    // Determine the output PNG file path
                    string outputPath = Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(file) + ".png");
                    // Save the generated barcode image
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                    Console.WriteLine($"Generated barcode for '{codeText}' -> {outputPath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating barcode for file '{file}': {ex.Message}");
            }
        }

        // List all generated PNG files for verification
        Console.WriteLine("Generated barcode files:");
        foreach (string img in Directory.GetFiles(outputFolder, "*.png"))
        {
            Console.WriteLine(img);
        }
    }
}