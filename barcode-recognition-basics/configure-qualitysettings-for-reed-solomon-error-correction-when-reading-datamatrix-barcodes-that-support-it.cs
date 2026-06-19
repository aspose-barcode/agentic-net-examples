using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a DataMatrix barcode, storing it in memory,
/// and then recognizing it using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a DataMatrix barcode, saves it to a memory stream,
    /// loads it into a bitmap, and reads the barcode using a high‑quality
    /// recognition configuration.
    /// </summary>
    static void Main()
    {
        // Define the text to encode in the DataMatrix barcode.
        const string codeText = "HelloWorld";

        // Create a barcode generator for DataMatrix with the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            // Save the generated barcode to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position to the beginning for reading.

                // Load the PNG image from the memory stream into a Bitmap object.
                using (var bitmap = new Bitmap(ms))
                {
                    // Initialize a barcode reader for the bitmap, limiting decoding to DataMatrix.
                    using (var reader = new BarCodeReader(bitmap, DecodeType.DataMatrix))
                    {
                        // Set the reader to use the highest quality preset for robust decoding.
                        reader.QualitySettings = QualitySettings.MaxQuality;

                        // Enable fast deconvolution to improve detection of damaged codes.
                        reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;

                        // Iterate through all detected barcodes and output their details.
                        foreach (var result in reader.ReadBarCodes())
                        {
                            Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                            Console.WriteLine($"Decoded Text: {result.CodeText}");
                            Console.WriteLine($"Confidence: {result.Confidence}");
                            Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
                        }
                    }
                }
            }
        }
    }
}