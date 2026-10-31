// Title: Scheduled barcode reader state export to XML
// Description: Demonstrates a simple scheduled job that generates barcodes, reads them, and exports the reader state to XML for audit logging.
// Category-Description: This example belongs to the Aspose.BarCode processing and audit logging category. It shows how to use BarcodeGenerator to create barcodes, BarCodeReader to decode them, and the ExportToXml method to capture the reader's internal state. Developers often need to log barcode processing details for compliance or debugging, and this pattern illustrates typical use of the Aspose.BarCode API classes such as BarcodeGenerator, BarCodeReader, and related settings.
// Prompt: Create a scheduled job that periodically exports reader state to XML for audit logging of processed barcodes.
// Tags: barcode generation, barcode reading, xml export, audit logging, code128, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates a scheduled job that generates Code128 barcodes, reads them,
/// and exports the reader state to XML for audit logging.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcodes, reads them, and logs reader state.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the job
        string jobFolder = Path.Combine(Path.GetTempPath(), "BarcodeJob_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(jobFolder);
        Console.WriteLine($"Job folder: {jobFolder}");

        // Number of iterations to simulate periodic execution
        int iterations = 3;

        // Simulate a scheduled job by looping through a fixed number of iterations
        for (int i = 1; i <= iterations; i++)
        {
            // Prepare file paths for the barcode image and the corresponding XML export
            string codeText = $"Sample{i}";
            string imagePath = Path.Combine(jobFolder, $"barcode{i}.png");
            string xmlPath = Path.Combine(jobFolder, $"readerState{i}.xml");

            // Generate a barcode image using Code128 symbology
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                generator.Save(imagePath, BarCodeImageFormat.Png);
                Console.WriteLine($"Generated barcode image: {imagePath}");
            }

            // Verify the image file exists before attempting to read it
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"Image file not found: {imagePath}");
                continue;
            }

            // Read the barcode and export the reader's internal state to XML
            using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Code128))
            {
                // Example setting: strip FNC characters from the decoded result
                reader.BarcodeSettings.StripFNC = true;

                // Perform recognition and obtain results
                BarCodeResult[] results = reader.ReadBarCodes();
                Console.WriteLine($"Iteration {i}: Barcodes read: {results.Length}");

                // Export the reader's state to XML for audit logging
                reader.ExportToXml(xmlPath);
                Console.WriteLine($"Exported reader state to XML: {xmlPath}");
            }
        }

        Console.WriteLine("Job completed.");
    }
}