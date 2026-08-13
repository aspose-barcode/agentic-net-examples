// Title: DataMatrix XDimension detection accuracy comparison
// Description: Demonstrates how changing the XDimension of a DataMatrix barcode (1 vs 3 pixels) affects detection reliability.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, showcasing the use of BarcodeGenerator for creating DataMatrix symbols and BarCodeReader for decoding them. Developers often need to fine‑tune XDimension to balance image size and scanner accuracy, especially in high‑density applications.
// Prompt: Compare detection accuracy of DataMatrix codes when XDimension is set to 1 versus 3 pixels.
// Tags: datamatrix, xdimension, detection, accuracy, generation, recognition, aspnet, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates two DataMatrix barcodes with different XDimension values
/// and evaluates their detection accuracy using Aspose.BarCode's recognition engine.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcodes, then reads and reports detection results.
    /// </summary>
    static void Main()
    {
        const string data = "SampleData";
        const string fileX1 = "datamatrix_x1.png";
        const string fileX3 = "datamatrix_x3.png";

        // Generate DataMatrix barcode with XDimension = 1 pixel
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, data))
        {
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;
            generator.Parameters.Barcode.XDimension.Point = 1f; // set module size to 1 pixel
            generator.Save(fileX1, BarCodeImageFormat.Png);
        }

        // Generate DataMatrix barcode with XDimension = 3 pixels
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, data))
        {
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;
            generator.Parameters.Barcode.XDimension.Point = 3f; // set module size to 3 pixels
            generator.Save(fileX3, BarCodeImageFormat.Png);
        }

        // Read and evaluate the barcode generated with XDimension = 1 pixel
        ReadAndReport(fileX1, XDimensionMode.Small);

        // Read and evaluate the barcode generated with XDimension = 3 pixels
        ReadAndReport(fileX3, XDimensionMode.Large);
    }

    /// <summary>
    /// Reads a barcode image, applies appropriate XDimension settings, and prints detection details.
    /// </summary>
    /// <param name="imagePath">Path to the barcode image file.</param>
    /// <param name="xDimMode">Expected XDimension mode (Small for 1 px, Large for 3 px).</param>
    static void ReadAndReport(string imagePath, XDimensionMode xDimMode)
    {
        // Verify that the image file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Initialize the barcode reader for DataMatrix symbology
        using (var reader = new BarCodeReader(imagePath, DecodeType.DataMatrix))
        {
            // Use high‑quality settings to improve detection reliability
            reader.QualitySettings = QualitySettings.HighQuality;

            // Adjust the reader's XDimension mode to match the generated barcode's module size
            reader.QualitySettings.XDimension = xDimMode;

            // Iterate through all detected barcodes (should be one in this example)
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"{imagePath} | XDimensionMode={xDimMode}");
                Console.WriteLine($"  CodeText        : {result.CodeText}");
                Console.WriteLine($"  Confidence      : {result.Confidence}");
                Console.WriteLine($"  ReadingQuality  : {result.ReadingQuality}");
            }
        }
    }
}