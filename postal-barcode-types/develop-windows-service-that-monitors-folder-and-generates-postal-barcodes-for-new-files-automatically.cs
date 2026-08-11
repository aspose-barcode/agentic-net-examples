// Title: Generate Australia Post Barcodes for Files in a Folder
// Description: The example monitors a folder (simulated) and creates Australia Post postal barcodes for each file, saving them as PNG images.
// Category-Description: This sample belongs to the Aspose.BarCode generation category, demonstrating how to use the BarcodeGenerator class with EncodeTypes.AustraliaPost to produce postal barcodes. Typical use cases include batch processing of documents to create shipping labels or barcode‑based tracking. Developers often need to generate barcodes programmatically, configure encoding tables, and save images in common formats.
// Prompt: Develop a Windows service that monitors a folder and generates postal barcodes for new files automatically.
// Tags: australia post, barcode generation, png, barcodegenerator, encode types, folder monitoring

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating Australia Post barcodes for files in a folder.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Processes files in the input folder and creates barcode images.
    /// </summary>
    static void Main()
    {
        // Define input and output directories relative to the current working directory.
        string inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "InputFiles");
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");

        // Ensure the input folder exists; create it if missing.
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
        }

        // Ensure the output folder exists; create it if missing.
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Seed a sample file so the example can run end‑to‑end without external setup.
        string sampleFile = Path.Combine(inputFolder, "Sample.txt");
        if (!File.Exists(sampleFile))
        {
            File.WriteAllText(sampleFile, "Sample content");
        }

        // Retrieve all files present in the input folder.
        string[] files = Directory.GetFiles(inputFolder);
        foreach (string filePath in files)
        {
            // Derive a barcode file name from the original file name (without extension).
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(filePath);
            string barcodePath = Path.Combine(outputFolder, fileNameWithoutExt + ".png");

            // Generate a valid Australia Post barcode.
            // FCC = 11, DPID = 00000000, no customer info (minimum 10 characters).
            using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, "1100000000"))
            {
                // Set the encoding table to CTable (optional, shown for completeness).
                generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;

                // Save the generated barcode as a PNG image.
                generator.Save(barcodePath);
            }

            // Inform the user about the generated barcode.
            Console.WriteLine($"Generated barcode for '{Path.GetFileName(filePath)}' at '{barcodePath}'.");
        }

        // Indicate that all files have been processed.
        Console.WriteLine("Processing complete.");
    }
}