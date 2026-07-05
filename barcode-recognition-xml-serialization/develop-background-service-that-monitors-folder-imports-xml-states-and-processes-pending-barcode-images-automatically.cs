// Title: Automatic barcode generation from XML definitions
// Description: Demonstrates a console background service that watches a folder, imports barcode settings from XML files, generates PNG images, and archives processed XML.
// Prompt: Develop a background service that monitors a folder, imports XML states, and processes pending barcode images automatically.
// Tags: barcode, generation, xml, png, file-io, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Entry point for the barcode generation service.
/// </summary>
class Program
{
    /// <summary>
    /// Main method processes XML barcode definitions, generates images, and moves processed files.
    /// </summary>
    /// <param name="args">Command‑line arguments: [0] input folder, [1] output folder.</param>
    static void Main(string[] args)
    {
        // Determine input folder (first argument) or use default "Input"
        string inputFolder = args.Length > 0 ? args[0] : "Input";

        // Determine output folder for generated images (second argument) or use default "Output"
        string outputFolder = args.Length > 1 ? args[1] : "Output";

        // Folder where processed XML files will be moved after successful handling
        string processedFolder = Path.Combine(inputFolder, "Processed");

        // Validate that the input folder exists; abort if it does not
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input directory '{inputFolder}' does not exist.");
            return;
        }

        // Ensure the output and processed folders exist (create if necessary)
        Directory.CreateDirectory(outputFolder);
        Directory.CreateDirectory(processedFolder);

        // Retrieve all XML files in the input folder
        string[] xmlFiles = Directory.GetFiles(inputFolder, "*.xml");
        if (xmlFiles.Length == 0)
        {
            Console.WriteLine("No XML files found to process.");
            return;
        }

        // Process each XML file individually
        foreach (string xmlPath in xmlFiles)
        {
            try
            {
                // Import barcode settings from the XML file using Aspose.BarCode
                using (BarcodeGenerator generator = BarcodeGenerator.ImportFromXml(xmlPath))
                {
                    // If import fails, log and continue with next file
                    if (generator == null)
                    {
                        Console.WriteLine($"Failed to import barcode from '{xmlPath}'.");
                        continue;
                    }

                    // Construct output image path (same base name as XML, but with .png extension)
                    string imageFileName = Path.GetFileNameWithoutExtension(xmlPath) + ".png";
                    string imagePath = Path.Combine(outputFolder, imageFileName);

                    // Save the generated barcode image to the output folder
                    generator.Save(imagePath);
                    Console.WriteLine($"Generated barcode saved to '{imagePath}'.");
                }

                // After successful generation, move the processed XML to the "Processed" subfolder
                string destXmlPath = Path.Combine(processedFolder, Path.GetFileName(xmlPath));
                File.Move(xmlPath, destXmlPath);
            }
            catch (Exception ex)
            {
                // Log any errors that occur during processing of the current XML file
                Console.WriteLine($"Error processing '{xmlPath}': {ex.Message}");
            }
        }
    }
}