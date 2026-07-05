// Title: DataMatrix XDimension Comparison
// Description: Demonstrates generating DataMatrix barcodes with XDimension set to 1 and 3 pixels and compares detection accuracy.
// Prompt: Compare detection accuracy of DataMatrix codes when XDimension is set to 1 versus 3 pixels.
// Tags: datamatrix, xdimension, detection, accuracy, barcode, generation, recognition, aspnet, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating DataMatrix barcodes with different XDimension values and compares their detection accuracy.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates two DataMatrix images with XDimension 1px and 3px, then reads them back to report detection results.
    /// </summary>
    static void Main()
    {
        // Sample data to encode
        const string data = "Aspose.DataMatrix.Test";

        // Paths for generated images
        const string pathX1 = "datamatrix_x1.png";
        const string pathX3 = "datamatrix_x3.png";

        // Create DataMatrix with XDimension = 1 pixel
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, data))
        {
            // Disable auto-sizing to keep XDimension exact
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;
            generator.Parameters.Barcode.XDimension.Point = 1f;
            generator.Save(pathX1, BarCodeImageFormat.Png);
        }

        // Create DataMatrix with XDimension = 3 pixels
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, data))
        {
            // Disable auto-sizing to keep XDimension exact
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;
            generator.Parameters.Barcode.XDimension.Point = 3f;
            generator.Save(pathX3, BarCodeImageFormat.Png);
        }

        // Local function to read a barcode image and report detection details
        void ReadAndReport(string imagePath, string label)
        {
            // Verify that the image file exists before attempting to read
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"{label}: Image file not found.");
                return;
            }

            // Initialize the reader for DataMatrix barcodes
            using (var reader = new BarCodeReader(imagePath, DecodeType.DataMatrix))
            {
                // Use high quality settings for reliable detection
                reader.QualitySettings = QualitySettings.HighQuality;

                // Attempt to read barcodes from the image
                var results = reader.ReadBarCodes();
                if (results.Length == 0)
                {
                    Console.WriteLine($"{label}: No barcode detected.");
                    return;
                }

                // Evaluate the first detected barcode
                var result = results[0];
                bool isCorrect = string.Equals(result.CodeText, data, StringComparison.Ordinal);
                Console.WriteLine($"{label}: Detected CodeText = \"{result.CodeText}\" (Correct: {isCorrect})");
                Console.WriteLine($"{label}: Confidence = {result.Confidence}");
                Console.WriteLine($"{label}: ReadingQuality = {result.ReadingQuality}");
            }
        }

        // Read and compare both generated images
        ReadAndReport(pathX1, "XDimension=1px");
        ReadAndReport(pathX3, "XDimension=3px");
    }
}