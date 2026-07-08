// Title: Batch generate barcodes from XML configurations
// Description: Demonstrates how to read barcode settings from XML files, generate images, and handle errors.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator.ImportFromXml to create barcodes from configuration files. Typical use cases include bulk barcode creation for inventory, shipping, or labeling systems where settings are stored in XML. Developers often need to process multiple files, manage output directories, and log processing issues.
// Prompt: Batch process a directory of XML configuration files, generating corresponding barcode images and logging any errors encountered.
// Tags: barcode generation, xml configuration, batch processing, png output, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides a console application that batch‑processes XML barcode configuration files,
/// generates PNG images for each configuration, and logs any errors encountered during the process.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Accepts optional command‑line arguments for input and output directories,
    /// processes up to a safety‑capped number of XML files, and creates corresponding barcode images.
    /// </summary>
    /// <param name="args">
    /// args[0] – optional path to the input directory containing XML files (default: "BarcodesConfig").
    /// args[1] – optional path to the output directory for generated PNG images (default: "BarcodesOutput").
    /// </param>
    static void Main(string[] args)
    {
        // Resolve input directory (first argument or default)
        string inputDir = args.Length > 0 ? args[0] : "BarcodesConfig";

        // Resolve output directory (second argument or default)
        string outputDir = args.Length > 1 ? args[1] : "BarcodesOutput";

        // Verify that the input directory exists
        if (!Directory.Exists(inputDir))
        {
            Console.WriteLine($"Input directory does not exist: {inputDir}");
            return;
        }

        // Ensure the output directory exists
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Retrieve all XML configuration files from the input directory
        string[] xmlFiles = Directory.GetFiles(inputDir, "*.xml");

        // Safety cap to avoid processing an unexpectedly large number of files
        const int maxFiles = 5;
        int processed = 0;

        // Iterate over each XML file until the safety cap is reached
        foreach (string xmlPath in xmlFiles)
        {
            if (processed >= maxFiles)
                break;

            try
            {
                // Load barcode generator settings from the XML configuration
                using (BarcodeGenerator generator = BarcodeGenerator.ImportFromXml(xmlPath))
                {
                    // Build the output image path (same base name, .png extension)
                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(xmlPath);
                    string outputPath = Path.Combine(outputDir, fileNameWithoutExt + ".png");

                    // Save the generated barcode image to the output directory
                    generator.Save(outputPath);
                    Console.WriteLine($"Generated barcode: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur while processing the current XML file
                Console.WriteLine($"Error processing '{xmlPath}': {ex.Message}");
            }

            processed++;
        }

        Console.WriteLine("Batch processing completed.");
    }
}