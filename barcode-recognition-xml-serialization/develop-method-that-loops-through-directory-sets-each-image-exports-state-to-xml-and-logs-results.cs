// Title: Batch barcode generation, image export to PNG, and XML state logging
// Description: Demonstrates how to iterate over a directory of images, generate new barcodes, save them as PNG files, export the generator state to XML, and log the processing results.
// Category-Description: This example belongs to the Aspose.BarCode generation and export category, showcasing the use of BarcodeGenerator, encoding settings, image saving, and ExportToXml. Typical use cases include batch processing of barcode images, automated report generation, and state persistence for later reuse. Developers often need to loop through files, customize barcode appearance, and maintain logs of operations.
// Prompt: Develop a method that loops through a directory, sets each image, exports state to XML, and logs results.
// Tags: code128, barcode generation, png, xml, logging, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch processing of barcode images: generating new barcodes, saving PNG output,
/// exporting generator state to XML, and logging the results.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates sample barcodes, processes each file, and records the outcome.
    /// </summary>
    static void Main()
    {
        // Create a unique working directory for input and output files
        string workRoot = Path.Combine(Path.GetTempPath(), "BarcodeBatch_" + Guid.NewGuid().ToString("N"));
        string inputDir = Path.Combine(workRoot, "Input");
        string outputDir = Path.Combine(workRoot, "Output");
        Directory.CreateDirectory(inputDir);
        Directory.CreateDirectory(outputDir);

        // Seed a few sample barcode images into the input folder
        string[] sampleTexts = { "Sample001", "Sample002", "Sample003", "Sample004", "Sample005" };
        for (int i = 0; i < sampleTexts.Length; i++)
        {
            string codeText = sampleTexts[i];
            string inputPath = Path.Combine(inputDir, $"barcode_{i + 1}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;
                generator.Save(inputPath, BarCodeImageFormat.Png);
            }
        }

        // Prepare a log file to capture processing details
        string logPath = Path.Combine(outputDir, "process.log");
        File.WriteAllText(logPath, $"Processing started at {DateTime.Now}{Environment.NewLine}");

        // Process each PNG image in the input folder
        string[] files = Directory.GetFiles(inputDir, "*.png");
        foreach (string filePath in files)
        {
            try
            {
                // Derive a new code text from the original file name (without extension)
                string fileName = Path.GetFileNameWithoutExtension(filePath);
                string newCodeText = $"Processed_{fileName}";

                // Create a new barcode generator for the derived text
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, newCodeText))
                {
                    generator.Parameters.Barcode.XDimension.Point = 2f;
                    generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.DarkBlue;
                    generator.Parameters.BackColor = Aspose.Drawing.Color.LightYellow;

                    // Save the new barcode image to the output directory
                    string outputImagePath = Path.Combine(outputDir, $"{fileName}_out.png");
                    generator.Save(outputImagePath, BarCodeImageFormat.Png);

                    // Export the generator state to an XML file
                    string xmlPath = Path.Combine(outputDir, $"{fileName}.xml");
                    generator.ExportToXml(xmlPath);

                    // Log successful processing
                    string logEntry = $"SUCCESS: Processed '{filePath}' -> '{outputImagePath}', XML saved to '{xmlPath}'{Environment.NewLine}";
                    File.AppendAllText(logPath, logEntry);
                    Console.WriteLine(logEntry.Trim());
                }
            }
            catch (Exception ex)
            {
                // Log any errors but continue processing remaining files
                string errorEntry = $"ERROR: Failed to process '{filePath}'. Exception: {ex.Message}{Environment.NewLine}";
                File.AppendAllText(logPath, errorEntry);
                Console.WriteLine(errorEntry.Trim());
            }
        }

        // Write final log entry indicating completion
        File.AppendAllText(logPath, $"Processing completed at {DateTime.Now}{Environment.NewLine}");
        Console.WriteLine("All done. Log written to: " + logPath);
    }
}