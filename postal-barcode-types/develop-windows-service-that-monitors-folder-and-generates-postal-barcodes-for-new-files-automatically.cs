// Title: Windows Service Simulation for Automatic Postal Barcode Generation
// Description: Demonstrates monitoring a folder and generating Postnet barcodes for new files using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode file‑processing and barcode generation category. It showcases the BarcodeGenerator class with EncodeTypes.Postnet, folder handling, and image output—common tasks for developers automating postal barcode creation in batch or service scenarios.
// Prompt: Develop a Windows service that monitors a folder and generates postal barcodes for new files automatically.
// Tags: postnet, postal barcode, barcode generation, file monitoring, aspose.barcode, image output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Simulates a Windows service that watches a directory and creates a Postnet barcode
/// for each newly added file. The example focuses on folder preparation, file handling,
/// and barcode generation using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the simulation. Sets up input/output folders, ensures a sample file,
    /// and generates a Postnet barcode based on the file name.
    /// </summary>
    static void Main()
    {
        // Define input and output directories relative to the current working directory
        string inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "InputFiles");
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");

        // Ensure the input folder exists; create it if missing
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
        }

        // Ensure the output folder exists; create it if missing
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Seed a sample file when the input folder is empty to demonstrate processing
        string[] existingFiles = Directory.GetFiles(inputFolder);
        if (existingFiles.Length == 0)
        {
            string samplePath = Path.Combine(inputFolder, "Sample.txt");
            File.WriteAllText(samplePath, "Sample content for postal barcode");
            existingFiles = new[] { samplePath };
        }

        // Simulate monitoring by processing the first file found in the input folder
        string fileToProcess = existingFiles[0];
        if (!File.Exists(fileToProcess))
        {
            Console.WriteLine($"File not found: {fileToProcess}");
            return;
        }

        // Use the file name (without extension) as the barcode text (e.g., "Sample")
        string codeText = Path.GetFileNameWithoutExtension(fileToProcess);

        // Generate a Postnet (postal) barcode for the extracted code text
        using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, codeText))
        {
            // Optional: customize barcode appearance (black bars on white background)
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Construct the output image path and save the barcode as PNG
            string outputPath = Path.Combine(outputFolder, $"{codeText}_Postnet.png");
            generator.Save(outputPath);
            Console.WriteLine($"Barcode generated: {outputPath}");
        }
    }
}