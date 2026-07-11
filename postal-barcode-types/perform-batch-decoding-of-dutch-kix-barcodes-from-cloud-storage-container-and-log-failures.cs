// Title: Batch decode Dutch KIX barcodes from local folder and log failures
// Description: Demonstrates generating sample Dutch KIX barcodes, decoding them in bulk, and writing any failures to a log file.
// Category-Description: This example belongs to the Aspose.BarCode batch processing category, showcasing how to use BarcodeGenerator to create barcodes and BarCodeReader with DecodeType.DutchKIX to read them. Typical use cases include automated verification of large barcode sets stored in cloud or local containers, where developers need to handle success and failure reporting. The snippet highlights key classes such as BarcodeGenerator, BarCodeReader, and common parameters for image handling.
// Prompt: Perform batch decoding of Dutch KIX barcodes from a cloud storage container and log failures.
// Tags: dutch kix, decoding, batch, log, aspose.barcode, barcodegenerator, barcodeReader, png

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates batch decoding of Dutch KIX barcodes and logging failures.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes if missing, decodes each PNG, and records any failures.
    /// </summary>
    static void Main()
    {
        // Define the folder that will hold sample barcode images.
        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(folderPath))
        {
            // Create the folder when it does not exist.
            Directory.CreateDirectory(folderPath);
        }

        // Sample Dutch KIX code texts (5 items for safety).
        string[] sampleCodes = new[]
        {
            "1234567890",
            "0987654321",
            "1122334455",
            "5566778899",
            "0001112223"
        };

        // Generate sample barcode images (if they do not already exist).
        for (int i = 0; i < sampleCodes.Length; i++)
        {
            string filePath = Path.Combine(folderPath, $"sample_{i + 1}.png");
            if (!File.Exists(filePath))
            {
                // EncodeTypes.DutchKIX is assumed to exist in the Aspose.BarCode library.
                using (var generator = new BarcodeGenerator(EncodeTypes.DutchKIX, sampleCodes[i]))
                {
                    // Optional visual settings.
                    generator.Parameters.Barcode.BarColor = Color.Black;
                    generator.Parameters.BackColor = Color.White;
                    generator.Parameters.ImageWidth.Point = 300f;
                    generator.Parameters.ImageHeight.Point = 150f;

                    // Save the generated barcode as PNG.
                    generator.Save(filePath, BarCodeImageFormat.Png);
                }
            }
        }

        // Path to the log file that will capture any decoding failures.
        string logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "decode_log.txt");
        using (var logWriter = new StreamWriter(logFilePath, false))
        {
            // Retrieve all PNG images from the folder.
            string[] imageFiles = Directory.GetFiles(folderPath, "*.png");
            foreach (string imagePath in imageFiles)
            {
                try
                {
                    // Initialize a reader for Dutch KIX barcodes.
                    using (var reader = new BarCodeReader(imagePath, DecodeType.DutchKIX))
                    {
                        bool decoded = false;

                        // Iterate through all detected barcodes in the image.
                        foreach (var result in reader.ReadBarCodes())
                        {
                            if (!string.IsNullOrEmpty(result.CodeText))
                            {
                                Console.WriteLine($"SUCCESS: File '{Path.GetFileName(imagePath)}' decoded as '{result.CodeText}'.");
                                decoded = true;
                            }
                        }

                        // If no barcode was decoded, log the failure.
                        if (!decoded)
                        {
                            string message = $"FAILURE: No Dutch KIX barcode detected in file '{Path.GetFileName(imagePath)}'.";
                            Console.WriteLine(message);
                            logWriter.WriteLine(message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log any exceptions that occur during processing.
                    string errorMsg = $"ERROR: Exception processing file '{Path.GetFileName(imagePath)}' - {ex.Message}";
                    Console.WriteLine(errorMsg);
                    logWriter.WriteLine(errorMsg);
                }
            }
        }

        Console.WriteLine("Batch decoding completed. See 'decode_log.txt' for any failures.");
    }
}