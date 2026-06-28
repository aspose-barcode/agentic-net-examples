using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating barcode images from XML configuration files using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Processes XML barcode configurations and generates PNG images.
    /// </summary>
    static void Main()
    {
        // Define the input folder that should contain XML barcode configuration files.
        string inputFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BarcodesConfig");
        if (!Directory.Exists(inputFolder))
        {
            // Create the folder if it does not exist.
            Directory.CreateDirectory(inputFolder);
        }

        // Retrieve all XML files in the input folder.
        string[] xmlFiles = Directory.GetFiles(inputFolder, "*.xml");
        if (xmlFiles.Length == 0)
        {
            // No XML files found – create a sample configuration to demonstrate functionality.
            string samplePath = Path.Combine(inputFolder, "sample.xml");
            using (var sampleGenerator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                // Export the sample generator settings to an XML file.
                sampleGenerator.ExportToXml(samplePath);
            }

            // Use the newly created sample file for further processing.
            xmlFiles = new[] { samplePath };
            Console.WriteLine("Created sample XML configuration at: " + samplePath);
        }

        // Create a timestamped output folder where generated barcode images will be saved.
        string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        string outputFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BarcodesOutput", timestamp);
        Directory.CreateDirectory(outputFolder);

        // Iterate over each XML configuration file and generate the corresponding barcode image.
        foreach (string xmlFile in xmlFiles)
        {
            try
            {
                // Import generator settings from the XML file.
                using (var generator = BarcodeGenerator.ImportFromXml(xmlFile))
                {
                    // Build the output image path: same base name as the XML file, PNG format.
                    string imageName = Path.GetFileNameWithoutExtension(xmlFile) + ".png";
                    string imagePath = Path.Combine(outputFolder, imageName);

                    // Save the generated barcode image to the output folder.
                    generator.Save(imagePath);
                    Console.WriteLine($"Generated barcode saved to: {imagePath}");
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur while processing a specific XML file.
                Console.WriteLine($"Error processing '{xmlFile}': {ex.Message}");
            }
        }

        // Indicate that the barcode generation process has finished.
        Console.WriteLine("Barcode generation completed.");
    }
}