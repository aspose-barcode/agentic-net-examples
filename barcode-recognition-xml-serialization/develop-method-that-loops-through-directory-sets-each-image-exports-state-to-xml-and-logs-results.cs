// Title: Export barcode generator state to XML for images in a directory
// Description: Demonstrates looping through a folder of barcode images, creating a generator for each, exporting its configuration to XML, and logging the results.
// Prompt: Develop a method that loops through a directory, sets each image, exports state to XML, and logs results.
// Tags: barcode symbology, export, xml, file-io, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that processes barcode images, creates generators, and exports their state to XML files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Sets up input/output directories and starts processing.
    /// </summary>
    static void Main()
    {
        // Define the folder that contains barcode image files (adjust path as needed)
        string inputDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");

        // Define the folder where the exported XML files will be saved
        string outputDirectory = Path.Combine(Directory.GetCurrentDirectory(), "ExportedXml");

        // Ensure the output directory exists; create it if it does not
        if (!Directory.Exists(outputDirectory))
        {
            Directory.CreateDirectory(outputDirectory);
        }

        // Process each barcode image and export its generator state to XML
        ProcessBarcodes(inputDirectory, outputDirectory);
    }

    /// <summary>
    /// Loops through image files in <paramref name="inputDir"/>, creates a <see cref="BarcodeGenerator"/> for each,
    /// exports its configuration to an XML file in <paramref name="outputDir"/>, and logs the outcome.
    /// </summary>
    /// <param name="inputDir">Directory containing barcode image files.</param>
    /// <param name="outputDir">Directory where XML files will be saved.</param>
    static void ProcessBarcodes(string inputDir, string outputDir)
    {
        // Verify that the input directory exists before proceeding
        if (!Directory.Exists(inputDir))
        {
            Console.WriteLine($"Input directory does not exist: {inputDir}");
            return;
        }

        // Retrieve up to 5 PNG files from the input directory (as per example guidelines)
        string[] imageFiles = Directory.GetFiles(inputDir, "*.png");
        int maxItems = Math.Min(imageFiles.Length, 5);

        // Iterate over the selected image files
        for (int i = 0; i < maxItems; i++)
        {
            string imagePath = imageFiles[i];

            // Skip the file if it cannot be found (defensive check)
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"File not found, skipping: {imagePath}");
                continue;
            }

            try
            {
                // Use the file name (without extension) as the barcode's codetext for demonstration purposes
                string codeText = Path.GetFileNameWithoutExtension(imagePath);

                // Create a barcode generator with Code128 symbology and the derived codetext
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
                {
                    // Optional: customize appearance (example sets the barcode color to blue)
                    generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;

                    // Build the full path for the XML output file
                    string xmlFileName = Path.Combine(outputDir, $"{codeText}.xml");

                    // Export the generator's current state to the XML file
                    generator.ExportToXml(xmlFileName);

                    // Log successful processing
                    Console.WriteLine($"Processed '{imagePath}' -> XML saved as '{xmlFileName}'.");
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur during processing of the current file
                Console.WriteLine($"Error processing '{imagePath}': {ex.Message}");
            }
        }

        // Indicate that all processing is complete
        Console.WriteLine("Processing completed.");
    }
}