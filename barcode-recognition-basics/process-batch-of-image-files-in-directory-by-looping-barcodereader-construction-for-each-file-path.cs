// Title: Batch Barcode Reading from Directory
// Description: Demonstrates reading barcodes from multiple image files in a folder by creating a BarCodeReader for each file.
// Prompt: Process a batch of image files in a directory by looping BarCodeReader construction for each file path.
// Tags: barcode, batch processing, image, aspose, barcodereader, console

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that scans a set of image files for barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Loops through a limited number of image files in a directory,
    /// creates a <see cref="BarCodeReader"/> for each, and prints detected barcode information.
    /// </summary>
    /// <param name="args">
    /// Optional command‑line argument specifying the directory path containing barcode images.
    /// If omitted, the program defaults to a folder named "Barcodes".
    /// </param>
    static void Main(string[] args)
    {
        // Determine the directory containing barcode images.
        // Use the first command‑line argument if provided; otherwise default to "Barcodes".
        string directoryPath = args.Length > 0 ? args[0] : "Barcodes";

        // Verify that the directory exists before proceeding.
        if (!Directory.Exists(directoryPath))
        {
            Console.WriteLine($"Directory not found: {directoryPath}");
            return;
        }

        // Retrieve all files in the directory (any extension).
        string[] files = Directory.GetFiles(directoryPath);

        // Limit processing to a safe sample size (up to 5 files) to avoid long runtimes.
        int maxFiles = Math.Min(5, files.Length);

        // Iterate over each selected file.
        for (int i = 0; i < maxFiles; i++)
        {
            string filePath = files[i];

            // Ensure the file still exists (it could have been removed after the initial listing).
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            // Create a BarCodeReader for the current image file.
            using (BarCodeReader reader = new BarCodeReader(filePath))
            {
                // Read all barcodes detected in the image.
                foreach (var result in reader.ReadBarCodes())
                {
                    // Output file name, barcode type, and decoded text.
                    Console.WriteLine($"File: {Path.GetFileName(filePath)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                }
            }
        }
    }
}