// Title: GS1 Composite Barcode Generation from Folder Files
// Description: Demonstrates generating GS1 Composite barcodes for each new text file in a folder and saving them as PNG images.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator with EncodeTypes.GS1CompositeBar. It covers setting linear and 2D component types, configuring GS1 encoding, and saving barcode images. Developers working on inventory, logistics, or product labeling often need to create GS1 Composite barcodes programmatically for batch processing.
// Prompt: Develop a Windows service that monitors a folder and generates GS1 Composite barcodes for new files.
// Tags: gs1 composite, barcode generation, png output, aspose.barcode, windows service

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that processes text files in an input folder,
/// generates GS1 Composite barcodes for each file, and saves the images to an output folder.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Sets up directories, creates sample files,
    /// generates barcodes for each .txt file, and saves them as PNG images.
    /// </summary>
    static void Main()
    {
        // Define input and output directories relative to the current folder
        string inputDir = Path.Combine(Directory.GetCurrentDirectory(), "Input");
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");

        // Ensure directories exist
        if (!Directory.Exists(inputDir))
        {
            Directory.CreateDirectory(inputDir);
        }
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Create a few sample text files if the input folder is empty
        string[] sampleFiles = { "Sample1.txt", "Sample2.txt", "Sample3.txt" };
        foreach (string fileName in sampleFiles)
        {
            string filePath = Path.Combine(inputDir, fileName);
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, $"Content of {fileName}");
            }
        }

        // Process each .txt file in the input folder
        string[] txtFiles = Directory.GetFiles(inputDir, "*.txt");
        foreach (string txtFile in txtFiles)
        {
            // Use the file name (without extension) as the serial number in AI (21)
            string serial = Path.GetFileNameWithoutExtension(txtFile);
            // Build GS1 Composite codetext: linear part (01) and 2D part (21) separated by '|'
            string codetext = $"(01)03212345678906|({21}){serial}";

            // Generate the barcode
            using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codetext))
            {
                // Set linear component to GS1Code128 and 2D component to CC_A (MicroPDF417)
                generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
                generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

                // Optional: enforce GS1 encoding for the 2D component
                generator.Parameters.Barcode.GS1CompositeBar.AllowOnlyGS1Encoding = true;

                // Set dimensions
                generator.Parameters.Barcode.XDimension.Pixels = 3f;
                generator.Parameters.Barcode.BarHeight.Pixels = 100f;

                // Save the barcode image as PNG in the output folder
                string outputFileName = Path.Combine(outputDir, $"{serial}.png");
                generator.Save(outputFileName);
                Console.WriteLine($"Generated barcode for '{txtFile}' -> '{outputFileName}'");
            }
        }

        // Indicate completion
        Console.WriteLine("Barcode generation completed.");
    }
}