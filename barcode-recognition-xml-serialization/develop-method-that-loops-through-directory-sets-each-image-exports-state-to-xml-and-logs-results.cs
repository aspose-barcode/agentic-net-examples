// Title: Batch barcode generation, recognition, and XML export
// Description: Demonstrates generating multiple barcode images, reading each image, exporting the recognition state to XML, and logging the results.
// Category-Description: Shows a typical Aspose.BarCode workflow for batch processing: using BarcodeGenerator to create barcodes, BarCodeReader to recognize them, and ExportToXml to obtain detailed recognition data. Useful for developers who need to automate barcode creation, validation, and state persistence across large image sets.
// Prompt: Develop a method that loops through a directory, sets each image, exports state to XML, and logs results.
// Tags: barcode, generation, recognition, xml, file-io, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that creates sample barcode images, reads them, exports recognition state to XML,
/// and writes a processing log. Demonstrates a typical batch workflow with Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Sets up a temporary folder, generates sample barcodes, and processes them.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for this run
        string tempFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample barcode images and collect their file paths
        List<string> imageFiles = GenerateSampleBarcodes(tempFolder);

        // Process each image: load, export recognition state to XML, and log outcomes
        ProcessBarcodes(tempFolder, imageFiles);
    }

    /// <summary>
    /// Generates a set of sample barcode images using various symbologies.
    /// </summary>
    /// <param name="folder">Folder where the images will be saved.</param>
    /// <returns>List of full file paths to the generated images.</returns>
    static List<string> GenerateSampleBarcodes(string folder)
    {
        var files = new List<string>();

        // Define sample data: (symbology, encoded text, output file name)
        var samples = new (BaseEncodeType encode, string text, string name)[]
        {
            (EncodeTypes.Code128, "ABC123", "code128.png"),
            (EncodeTypes.QR, "https://example.com", "qr.png"),
            (EncodeTypes.DataMatrix, "DM12345", "datamatrix.png"),
            (EncodeTypes.Pdf417, "PDF417 Sample", "pdf417.png"),
            (EncodeTypes.Aztec, "AztecSample", "aztec.png")
        };

        // Iterate over each sample, generate the barcode, and save as PNG
        foreach (var (encode, text, name) in samples)
        {
            string filePath = Path.Combine(folder, name);
            using (var generator = new BarcodeGenerator(encode, text))
            {
                // Simple configuration: set X-dimension for better readability
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            files.Add(filePath);
        }

        return files;
    }

    /// <summary>
    /// Reads each barcode image, exports the recognition state to an XML file, and logs the process.
    /// </summary>
    /// <param name="folder">Base folder for log and XML output.</param>
    /// <param name="imageFiles">List of barcode image file paths to process.</param>
    static void ProcessBarcodes(string folder, List<string> imageFiles)
    {
        // Initialize log file
        string logPath = Path.Combine(folder, "process_log.txt");
        File.WriteAllText(logPath, $"Processing started at {DateTime.Now}{Environment.NewLine}");

        // Process each image file
        foreach (string imagePath in imageFiles)
        {
            if (!File.Exists(imagePath))
            {
                AppendLog(logPath, $"File not found: {imagePath}");
                continue;
            }

            // Determine XML output path based on image file name
            string xmlPath = Path.ChangeExtension(imagePath, ".xml");
            try
            {
                using (var reader = new BarCodeReader())
                {
                    // Load the image for recognition
                    reader.SetBarCodeImage(imagePath);

                    // Export detailed recognition state to XML
                    reader.ExportToXml(xmlPath);
                }
                AppendLog(logPath, $"Successfully processed: {Path.GetFileName(imagePath)} -> {Path.GetFileName(xmlPath)}");
            }
            catch (ArgumentException ex)
            {
                // Handles image loading failures or unsupported formats
                AppendLog(logPath, $"Failed to process {Path.GetFileName(imagePath)}: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Catch-all for unexpected errors
                AppendLog(logPath, $"Unexpected error for {Path.GetFileName(imagePath)}: {ex.Message}");
            }
        }

        // Finalize log
        AppendLog(logPath, $"Processing completed at {DateTime.Now}");
        Console.WriteLine($"Log written to: {logPath}");
    }

    /// <summary>
    /// Appends a timestamped message to the specified log file.
    /// </summary>
    /// <param name="logFile">Path to the log file.</param>
    /// <param name="message">Message to append.</param>
    static void AppendLog(string logFile, string message)
    {
        File.AppendAllText(logFile, $"{DateTime.Now}: {message}{Environment.NewLine}");
    }
}