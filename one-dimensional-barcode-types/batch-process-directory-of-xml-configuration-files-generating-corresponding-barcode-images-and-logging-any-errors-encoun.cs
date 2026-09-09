// Title: Batch generate barcode images from XML configuration files
// Description: Demonstrates importing barcode settings from XML files, generating PNG images, and logging any errors encountered during processing.
// Category-Description: This example belongs to the Aspose.BarCode batch processing category, showcasing the use of BarcodeGenerator.ImportFromXml and ExportToXml for bulk barcode creation. Developers often need to automate barcode generation from predefined configurations, handling multiple symbologies and output formats while capturing processing issues. The code illustrates typical patterns for directory traversal, image saving, and error logging using Aspose.BarCode classes.
// Prompt: Batch process a directory of XML configuration files, generating corresponding barcode images and logging any errors encountered.
// Tags: barcode symbology, batch processing, xml import, image generation, error logging, aspose.barcode, c#

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides a console application that creates sample barcode XML configurations,
/// imports them, generates PNG images, and logs any processing errors.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Executes the batch processing workflow.
    /// </summary>
    static void Main()
    {
        // Create dedicated temporary folders for XML configs and output images
        string inputFolder = Path.Combine(Path.GetTempPath(), "BarcodesXml_" + Guid.NewGuid().ToString("N"));
        string outputFolder = Path.Combine(Path.GetTempPath(), "BarcodesImg_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(inputFolder);
        Directory.CreateDirectory(outputFolder);

        // Generate sample barcode XML configuration files in the input folder
        GenerateSampleXmlConfigs(inputFolder);

        // Retrieve all XML configuration files from the input folder
        List<string> xmlFiles = new List<string>(Directory.GetFiles(inputFolder, "*.xml"));

        // Process each XML configuration file: import, generate image, and handle errors
        foreach (string xmlPath in xmlFiles)
        {
            try
            {
                // Import barcode settings from the XML file
                using (BarcodeGenerator generator = BarcodeGenerator.ImportFromXml(xmlPath))
                {
                    // Determine output image path (same name, PNG extension)
                    string imageFileName = Path.GetFileNameWithoutExtension(xmlPath) + ".png";
                    string imagePath = Path.Combine(outputFolder, imageFileName);

                    // Save the generated barcode image as PNG
                    generator.Save(imagePath, BarCodeImageFormat.Png);
                    Console.WriteLine($"Generated barcode image: {imagePath}");
                }
            }
            catch (Exception ex)
            {
                // Log the error message to console and to an error log file
                string message = $"Error processing '{xmlPath}': {ex.Message}";
                Console.WriteLine(message);
                string logPath = Path.Combine(outputFolder, "error.log");
                File.AppendAllText(logPath, message + Environment.NewLine);
            }
        }

        Console.WriteLine("Batch processing completed.");
    }

    /// <summary>
    /// Generates sample barcode configuration XML files for Code128, QR, and DataMatrix symbologies.
    /// </summary>
    /// <param name="folder">The directory where the XML files will be saved.</param>
    static void GenerateSampleXmlConfigs(string folder)
    {
        // Sample 1: Code128
        string xml1 = Path.Combine(folder, "Code128.xml");
        using (var gen = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            gen.Parameters.Barcode.XDimension.Point = 2f;
            gen.ExportToXml(xml1);
        }

        // Sample 2: QR
        string xml2 = Path.Combine(folder, "QR.xml");
        using (var gen = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            gen.Parameters.Barcode.XDimension.Point = 3f;
            gen.ExportToXml(xml2);
        }

        // Sample 3: DataMatrix
        string xml3 = Path.Combine(folder, "DataMatrix.xml");
        using (var gen = new BarcodeGenerator(EncodeTypes.DataMatrix, "DM12345"))
        {
            gen.Parameters.Barcode.XDimension.Point = 2f;
            gen.ExportToXml(xml3);
        }
    }
}