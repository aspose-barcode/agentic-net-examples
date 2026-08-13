// Title: Barcode generation, state export, and decoding batch processing
// Description: Demonstrates creating barcode images, exporting generator state to XML, then reading back each image to decode and log results.
// Category-Description: This example belongs to the Aspose.BarCode batch processing category, showcasing how to use BarcodeGenerator for image creation, ExportToXml for persisting generator settings, and BarCodeReader for decoding. Typical use cases include automated barcode workflows, bulk processing, and state persistence for later reuse. Developers often need to generate, store, and later validate barcodes in large volumes.
// Prompt: Develop a method that loops through a directory, sets each image, exports state to XML, and logs results.
// Tags: barcode generation, barcode decoding, xml export, code128, aspose.barcode, batch processing

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch creation of Code128 barcodes, exporting generator state to XML,
/// and decoding each generated image while logging the process.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcodes, exports their state, decodes them,
    /// and writes detailed logs to a file.
    /// </summary>
    static void Main()
    {
        // Define the working directory for barcode images and logs.
        string workDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(workDir))
        {
            Directory.CreateDirectory(workDir);
        }

        // Initialize a simple log file with a start timestamp.
        string logPath = Path.Combine(workDir, "process.log");
        File.WriteAllText(logPath, $"Process started at {DateTime.Now}{Environment.NewLine}");

        // --------------------------------------------------------------------
        // Generate sample barcode images and export their generator state to XML.
        // --------------------------------------------------------------------
        for (int i = 1; i <= 5; i++)
        {
            string codeText = $"Sample{i}";
            string imagePath = Path.Combine(workDir, $"barcode{i}.png");
            string xmlPath = Path.Combine(workDir, $"barcode{i}.xml");

            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Configure generator properties.
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Parameters.Barcode.BarHeight.Point = 40f;
                generator.Parameters.Barcode.FilledBars = false;
                generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false;
                generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Helvetica";
                generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 10f;
                generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;

                // Save the barcode image to disk.
                generator.Save(imagePath);

                // Export the current generator configuration to an XML file.
                generator.ExportToXml(xmlPath);
            }

            // Log the successful generation and export.
            Log(logPath, $"Generated barcode {i}: {imagePath}, state exported to {xmlPath}");
        }

        // --------------------------------------------------------------------
        // Decode each generated barcode image and log the results.
        // --------------------------------------------------------------------
        string[] imageFiles = Directory.GetFiles(workDir, "*.png");
        foreach (string imgFile in imageFiles)
        {
            if (!File.Exists(imgFile))
            {
                Log(logPath, $"File not found: {imgFile}");
                continue;
            }

            try
            {
                using (var reader = new BarCodeReader(imgFile, DecodeType.Code128))
                {
                    bool found = false;
                    foreach (var result in reader.ReadBarCodes())
                    {
                        // Log each decoded barcode's type and text.
                        Log(logPath, $"Decoded from {Path.GetFileName(imgFile)}: Type={result.CodeTypeName}, Text={result.CodeText}");
                        found = true;
                    }

                    if (!found)
                    {
                        // No barcode detected in the current image.
                        Log(logPath, $"No barcode detected in {Path.GetFileName(imgFile)}");
                    }
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur during decoding.
                Log(logPath, $"Error processing {Path.GetFileName(imgFile)}: {ex.Message}");
            }
        }

        // Final log entry indicating completion.
        Log(logPath, $"Process completed at {DateTime.Now}");
    }

    /// <summary>
    /// Writes a message to both the console and the specified log file with a timestamp.
    /// </summary>
    /// <param name="logFile">Path to the log file.</param>
    /// <param name="message">Message to log.</param>
    static void Log(string logFile, string message)
    {
        Console.WriteLine(message);
        File.AppendAllText(logFile, $"{DateTime.Now}: {message}{Environment.NewLine}");
    }
}