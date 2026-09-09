// Title: Barcode Generation, Recognition, and Logging Example
// Description: Demonstrates generating a Code128 barcode image, reading it with BarCodeReader, exporting the reader state to XML, and logging the file paths used.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing the use of BarcodeGenerator for creating barcodes, BarCodeReader for image recognition, and ExportToXml for persisting recognition state. Developers often need to generate barcodes, validate them programmatically, and keep audit logs of file operations; this snippet illustrates typical API classes and workflow for such tasks.
// Prompt: Design a logging mechanism that records the file path used in SetBarCodeImage alongside the exported XML state.
// Tags: barcode, generation, recognition, logging, xml, code128, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode creation, recognition, XML export, and logging of file paths.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, reads it, exports state to XML, and logs file locations.
    /// </summary>
    static void Main()
    {
        // -----------------------------------------------------------------
        // Prepare a unique temporary working directory for all generated files.
        // -----------------------------------------------------------------
        string workDir = Path.Combine(Path.GetTempPath(), "BarcodeLogDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workDir);

        // Define file paths for the barcode image, XML export, and log file.
        string imagePath = Path.Combine(workDir, "sample_barcode.png");
        string readerXmlPath = Path.Combine(workDir, "reader_state.xml");
        string logPath = Path.Combine(workDir, "log.txt");

        // -----------------------------------------------------------------
        // Generate a Code128 barcode image and save it as PNG.
        // -----------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            // Set barcode module size (X-dimension) to 2 points.
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was successfully created.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // -----------------------------------------------------------------
        // Initialize BarCodeReader, load the generated image, and export its state.
        // -----------------------------------------------------------------
        using (var reader = new BarCodeReader())
        {
            // Record the image path used in SetBarCodeImage for later logging.
            reader.SetBarCodeImage(imagePath);
            // Export the recognition state to an XML file.
            reader.ExportToXml(readerXmlPath);
        }

        // Verify that the XML export succeeded.
        if (!File.Exists(readerXmlPath))
        {
            Console.WriteLine("Failed to export reader state to XML.");
            return;
        }

        // -----------------------------------------------------------------
        // Log the file paths used during SetBarCodeImage and ExportToXml.
        // -----------------------------------------------------------------
        string logContent = $"SetBarCodeImage path: {imagePath}{Environment.NewLine}" +
                            $"Exported XML path: {readerXmlPath}{Environment.NewLine}";
        File.AppendAllText(logPath, logContent);

        // Output the locations of the generated files for verification.
        Console.WriteLine("Barcode image saved to: " + imagePath);
        Console.WriteLine("Reader state XML saved to: " + readerXmlPath);
        Console.WriteLine("Log written to: " + logPath);
    }
}