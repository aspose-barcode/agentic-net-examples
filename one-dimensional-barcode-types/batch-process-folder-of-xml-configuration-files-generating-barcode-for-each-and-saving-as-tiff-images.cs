using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates batch generation of barcodes from XML configuration files using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Processes up to 10 XML configuration files in the specified input folder,
    /// generates corresponding barcode images, and saves them as TIFF files.
    /// </summary>
    /// <param name="args">
    /// Optional command‑line argument specifying the input folder path.
    /// If omitted, defaults to "BarcodesConfig".
    /// </param>
    static void Main(string[] args)
    {
        // Determine the folder containing XML configuration files.
        string inputFolder = args.Length > 0 ? args[0] : "BarcodesConfig";

        // Verify that the input folder exists.
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Prepare output folder for generated TIFF images (subfolder named "Output").
        string outputFolder = Path.Combine(inputFolder, "Output");
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Retrieve all XML files in the input folder.
        // Limit processing to the first 10 files for safety in constrained environments.
        string[] xmlFiles = Directory.GetFiles(inputFolder, "*.xml");
        int maxFiles = Math.Min(xmlFiles.Length, 10);

        // If no XML files are found, inform the user and exit.
        if (maxFiles == 0)
        {
            Console.WriteLine("No XML configuration files found.");
            return;
        }

        // Process each XML file sequentially.
        for (int i = 0; i < maxFiles; i++)
        {
            string xmlPath = xmlFiles[i];

            // Ensure the file still exists before attempting to load it.
            if (!File.Exists(xmlPath))
            {
                Console.WriteLine($"File not found (skipped): {xmlPath}");
                continue;
            }

            try
            {
                // Load barcode settings from the XML file using Aspose.BarCode.
                using (BarcodeGenerator generator = BarcodeGenerator.ImportFromXml(xmlPath))
                {
                    // Construct the output file name with a .tiff extension.
                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(xmlPath);
                    string outputPath = Path.Combine(outputFolder, fileNameWithoutExt + ".tiff");

                    // Save the generated barcode image as TIFF.
                    generator.Save(outputPath, BarCodeImageFormat.Tiff);
                    Console.WriteLine($"Generated barcode: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                // Report any errors encountered while processing the current XML file.
                Console.WriteLine($"Error processing '{xmlPath}': {ex.Message}");
            }
        }

        // Indicate that batch processing has finished.
        Console.WriteLine("Batch processing completed.");
    }
}