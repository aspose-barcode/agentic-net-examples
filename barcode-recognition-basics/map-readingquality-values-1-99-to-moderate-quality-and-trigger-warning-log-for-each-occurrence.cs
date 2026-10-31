// Title: Map ReadingQuality to Moderate Quality with Warning Logging
// Description: Demonstrates generating a Code128 barcode, reading it, and logging a warning when the reading quality falls within the moderate range (1‑99).
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for decoding, and QualitySettings for assessing reading quality. Developers often need to evaluate barcode scan quality and log issues, making this pattern useful for quality control and diagnostic logging in scanning applications.
// Prompt: Map ReadingQuality values 1‑99 to moderate quality and trigger a warning log for each occurrence.
// Tags: code128, generation, recognition, quality-assessment, png, warnings-log, barcodegenerator, barcodereader, qualitysettings

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a Code128 barcode, reads it back,
/// evaluates the reading quality, and logs a warning for moderate quality values.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, recognition,
    /// quality assessment, and warning logging.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder to store generated files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for the barcode image and the warning log
        string barcodePath = Path.Combine(tempFolder, "sample.png");
        string logPath = Path.Combine(tempFolder, "warnings.log");

        // Generate a simple Code128 barcode and save it as PNG
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Initialize a barcode reader for the generated image using Code128 decode type
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            // Apply the default quality preset (normal quality)
            reader.QualitySettings = QualitySettings.NormalQuality;

            // Read all barcodes found in the image
            BarCodeResult[] results = reader.ReadBarCodes();
            foreach (BarCodeResult result in results)
            {
                double quality = result.ReadingQuality;
                string message = $"CodeType:{result.CodeTypeName} CodeText:{result.CodeText} ReadingQuality:{quality}";

                // Check if the reading quality falls within the moderate range (1‑99)
                if (quality >= 1 && quality <= 99)
                {
                    string warning = $"Warning: Moderate quality detected (ReadingQuality:{quality}) for barcode type {result.CodeTypeName}";
                    Console.WriteLine(warning);
                    // Append the warning to the log file
                    File.AppendAllText(logPath, warning + Environment.NewLine);
                }
                else
                {
                    // Output normal reading information
                    Console.WriteLine(message);
                }
            }
        }

        // Cleanup: optionally delete temporary files (commented out to allow inspection)
        // Directory.Delete(tempFolder, true);
    }
}