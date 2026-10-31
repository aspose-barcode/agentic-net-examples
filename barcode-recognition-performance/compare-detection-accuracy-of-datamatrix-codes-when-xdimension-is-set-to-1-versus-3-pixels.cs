// Title: Compare DataMatrix XDimension 1px vs 3px detection accuracy
// Description: Generates two DataMatrix barcodes with XDimension set to 1 and 3 pixels, then reads them to compare detection success and reading quality.
// Category-Description: This example demonstrates Aspose.BarCode generation and recognition workflows. It uses BarcodeGenerator to create barcodes and BarCodeReader to decode them, showcasing typical scenarios such as quality assessment, parameter tuning, and automated verification of barcode readability. Developers working with barcode imaging often need to experiment with rendering settings to optimize scanner performance.
// Prompt: Compare detection accuracy of DataMatrix codes when XDimension is set to 1 versus 3 pixels.
// Tags: datamatrix, xdimension, detection, accuracy, generation, recognition, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how XDimension affects DataMatrix detection by generating two barcodes
/// (1 px and 3 px) and evaluating their readability using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcodes, evaluates detection quality,
    /// and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the generated images
        string tempDir = Path.Combine(Path.GetTempPath(), "DataMatrixXDimTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Common barcode content
        string codeText = "ASPOSE123";

        // Paths for the two test images
        string file1 = Path.Combine(tempDir, "datamatrix_1.png");
        string file3 = Path.Combine(tempDir, "datamatrix_3.png");

        // Generate DataMatrix with XDimension = 1 pixel
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 1f;
            generator.Save(file1, BarCodeImageFormat.Png);
        }

        // Generate DataMatrix with XDimension = 3 pixels
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 3f;
            generator.Save(file3, BarCodeImageFormat.Png);
        }

        // Local function to read a barcode image and report detection results
        void Evaluate(string path, string label)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine($"{label}: File not found.");
                return;
            }

            using (var reader = new BarCodeReader(path, DecodeType.DataMatrix))
            {
                BarCodeResult[] results = reader.ReadBarCodes();
                if (results.Length > 0 && results[0].CodeText == codeText)
                {
                    Console.WriteLine($"{label}: Detected successfully. ReadingQuality={results[0].ReadingQuality}");
                }
                else
                {
                    Console.WriteLine($"{label}: Detection failed or mismatched text.");
                }
            }
        }

        // Evaluate both generated images
        Evaluate(file1, "XDimension=1px");
        Evaluate(file3, "XDimension=3px");

        // Cleanup temporary files and directory
        try
        {
            File.Delete(file1);
            File.Delete(file3);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Suppress any cleanup errors
        }
    }
}