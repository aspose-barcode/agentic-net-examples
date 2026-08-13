// Title: Batch decode Dutch KIX (DotCode) barcodes from a folder and log failures
// Description: Demonstrates generating sample Dutch KIX (DotCode) barcode images, then batch decoding them from a directory that simulates a cloud storage container, while recording any decoding failures.
// Category-Description: This example belongs to the Aspose.BarCode barcode processing category, focusing on batch recognition of specific symbologies. It showcases the use of BarcodeGenerator for image creation, BarCodeReader with DecodeType.DutchKIX for recognition, and handling of results and errors. Developers often need to process large sets of barcode images from storage, extract data, and log problematic files for further analysis.
// Prompt: Perform batch decoding of Dutch KIX barcodes from a cloud storage container and log failures.
// Tags: dotcode, dutchkix, batch-decoding, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates batch decoding of Dutch KIX (DotCode) barcodes from a simulated cloud storage container,
/// including generation of sample images and logging of any decoding failures.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates sample barcode images, decodes them, and logs failures.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Folder that represents the cloud storage container.
        string inputFolder = "InputBarcodes";
        string logFile = "failures.log";

        // Ensure a clean log file before starting.
        if (File.Exists(logFile))
            File.Delete(logFile);

        // Create the input folder if it does not exist.
        if (!Directory.Exists(inputFolder))
            Directory.CreateDirectory(inputFolder);

        // -----------------------------------------------------------------
        // Generate a few sample Dutch KIX (DotCode) barcode images.
        // In a real scenario these images would be downloaded from cloud storage.
        // -----------------------------------------------------------------
        string[] sampleTexts = { "123456", "ABCDEF", "9876543210" };
        for (int i = 0; i < sampleTexts.Length; i++)
        {
            string filePath = Path.Combine(inputFolder, $"sample_{i + 1}.png");
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DotCode, sampleTexts[i]))
            {
                // Optional: set visual parameters for better readability.
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Save the generated barcode as a PNG image.
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // -----------------------------------------------------------------
        // Batch decode all PNG images in the folder as Dutch KIX barcodes.
        // -----------------------------------------------------------------
        string[] imageFiles = Directory.GetFiles(inputFolder, "*.png");
        foreach (string imagePath in imageFiles)
        {
            try
            {
                // Use the DutchKIX decode type to recognize the specific symbology.
                using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.DutchKIX))
                {
                    BarCodeResult[] results = reader.ReadBarCodes();

                    if (results.Length == 0)
                    {
                        // No barcode detected – log the failure.
                        LogFailure(logFile, imagePath, "No barcode detected.");
                        continue;
                    }

                    foreach (BarCodeResult result in results)
                    {
                        if (string.IsNullOrEmpty(result.CodeText))
                        {
                            // Barcode detected but the text is empty – log the failure.
                            LogFailure(logFile, imagePath, "Detected barcode but CodeText is empty.");
                        }
                        else
                        {
                            // Successful decode – output details to the console.
                            Console.WriteLine($"File: {Path.GetFileName(imagePath)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Unexpected exception – log the failure with the exception message.
                LogFailure(logFile, imagePath, $"Exception: {ex.Message}");
            }
        }

        Console.WriteLine("Batch decoding completed.");
    }

    // Helper method to append failure information to the log file and echo it to the console.
    static void LogFailure(string logPath, string imagePath, string message)
    {
        string logEntry = $"[FAIL] File: {Path.GetFileName(imagePath)} - {message}{Environment.NewLine}";
        File.AppendAllText(logPath, logEntry);
        Console.WriteLine(logEntry.TrimEnd());
    }
}