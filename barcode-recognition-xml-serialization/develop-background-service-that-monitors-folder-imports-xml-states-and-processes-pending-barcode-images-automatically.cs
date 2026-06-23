using System;
using System.IO;
using Aspose.BarCode.Generation;

/// <summary>
/// Console application that processes XML files to generate barcode images.
/// It reads each XML file, creates a barcode using Aspose.BarCode, saves the image,
/// and moves the processed XML to a separate folder.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Scans the Input folder for XML files, generates barcode images,
    /// and moves processed XML files to the Processed folder.
    /// </summary>
    static void Main()
    {
        // Define folder paths relative to the current directory
        string inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Input");
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        string processedFolder = Path.Combine(Directory.GetCurrentDirectory(), "Processed");

        // Ensure the Output folder exists
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Ensure the Processed folder exists
        if (!Directory.Exists(processedFolder))
        {
            Directory.CreateDirectory(processedFolder);
        }

        // Verify that the Input folder exists; abort if it does not
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Retrieve all XML files in the Input folder
        string[] xmlFiles = Directory.GetFiles(inputFolder, "*.xml");
        if (xmlFiles.Length == 0)
        {
            Console.WriteLine("No XML files to process.");
            return;
        }

        // Process each XML file individually
        foreach (string xmlPath in xmlFiles)
        {
            try
            {
                // Import barcode generator settings from the XML file
                using (BarcodeGenerator generator = BarcodeGenerator.ImportFromXml(xmlPath))
                {
                    // Build the output image path (same name with .png extension)
                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(xmlPath);
                    string outputPath = Path.Combine(outputFolder, fileNameWithoutExt + ".png");

                    // Save the generated barcode image to the Output folder
                    generator.Save(outputPath);
                    Console.WriteLine($"Generated barcode saved to: {outputPath}");
                }

                // Move the processed XML file to the Processed folder
                string destXmlPath = Path.Combine(processedFolder, Path.GetFileName(xmlPath));

                // If a file with the same name already exists in Processed, delete it first
                if (File.Exists(destXmlPath))
                {
                    File.Delete(destXmlPath);
                }

                // Move the XML file
                File.Move(xmlPath, destXmlPath);
                Console.WriteLine($"Moved processed XML to: {destXmlPath}");
            }
            catch (Exception ex)
            {
                // Log any errors and continue processing remaining files
                Console.WriteLine($"Error processing '{xmlPath}': {ex.Message}");
            }
        }

        // Indicate that all processing is complete
        Console.WriteLine("Processing completed.");
    }
}