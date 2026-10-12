// Title: Batch processing of XML barcode configurations to generate images
// Description: Demonstrates how to read barcode configuration XML files from a directory, generate corresponding barcode images, and log the processing results.
// Category-Description: This example belongs to the Aspose.BarCode batch processing category, showcasing how to import barcode settings from XML, generate images, and export configurations. It uses BarcodeGenerator.ImportFromXml, BarcodeGenerator.Save, and related parameter classes. Typical use cases include automated generation of multiple barcodes from predefined configurations, useful for inventory, shipping, or marketing workflows where developers need to process many barcode definitions efficiently.
// Prompt: Batch process a directory of XML configuration files, generating corresponding barcode images and logging any errors encountered.
// Tags: barcode, batch processing, xml, generation, aspose.barcode, png, logging

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides a console application that batch‑processes XML barcode configuration files,
/// generates PNG images for each configuration, and records the operation in a log file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Accepts an optional folder path argument; if omitted,
    /// a temporary folder with sample XML configurations is created and processed.
    /// </summary>
    /// <param name="args">Command‑line arguments; the first argument may specify the input folder.</param>
    static void Main(string[] args)
    {
        // Determine the input folder: use argument if valid, otherwise create a temporary folder with samples.
        string inputFolder;
        if (args.Length > 0 && Directory.Exists(args[0]))
        {
            inputFolder = args[0];
        }
        else
        {
            inputFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(inputFolder);
            GenerateSampleXmlConfigs(inputFolder);
        }

        // Prepare the output folder for generated barcode images.
        string outputFolder = Path.Combine(inputFolder, "Output");
        Directory.CreateDirectory(outputFolder);

        // Initialize the processing log.
        string logPath = Path.Combine(inputFolder, "process_log.txt");
        File.WriteAllText(logPath, $"Processing started at {DateTime.Now}{Environment.NewLine}");

        // Collect all XML configuration files in the input folder.
        List<string> xmlFiles = new List<string>();
        foreach (string file in Directory.GetFiles(inputFolder, "*.xml"))
        {
            xmlFiles.Add(file);
        }

        // Process each XML file: import settings, generate image, and log the result.
        foreach (string xmlFile in xmlFiles)
        {
            try
            {
                using (BarcodeGenerator generator = BarcodeGenerator.ImportFromXml(xmlFile))
                {
                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(xmlFile);
                    string imagePath = Path.Combine(outputFolder, fileNameWithoutExt + ".png");
                    generator.Save(imagePath, BarCodeImageFormat.Png);
                    File.AppendAllText(logPath, $"Generated: {imagePath}{Environment.NewLine}");
                }
            }
            catch (Exception ex)
            {
                // Log any errors encountered while processing the current XML file.
                File.AppendAllText(logPath, $"Error processing {xmlFile}: {ex.Message}{Environment.NewLine}");
            }
        }

        // Finalize the log with a completion timestamp.
        File.AppendAllText(logPath, $"Processing completed at {DateTime.Now}{Environment.NewLine}");
        Console.WriteLine($"Batch processing completed. See log at: {logPath}");
    }

    /// <summary>
    /// Generates a set of sample barcode configuration XML files in the specified folder.
    /// These samples include Code128, QR, and DataMatrix barcodes.
    /// </summary>
    /// <param name="folder">The folder where sample XML files will be saved.</param>
    static void GenerateSampleXmlConfigs(string folder)
    {
        // Sample Code128 configuration.
        using (BarcodeGenerator gen1 = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            string path1 = Path.Combine(folder, "code128.xml");
            gen1.ExportToXml(path1);
        }

        // Sample QR configuration with error correction level M.
        using (BarcodeGenerator gen2 = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            gen2.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;
            string path2 = Path.Combine(folder, "qr.xml");
            gen2.ExportToXml(path2);
        }

        // Sample DataMatrix configuration with a specific version.
        using (BarcodeGenerator gen3 = new BarcodeGenerator(EncodeTypes.DataMatrix, "DM12345"))
        {
            gen3.Parameters.Barcode.DataMatrix.Version = DataMatrixVersion.ECC200_10x10;
            string path3 = Path.Combine(folder, "datamatrix.xml");
            gen3.ExportToXml(path3);
        }
    }
}