using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating DataMatrix barcodes with different XDimension values
/// and recognizing them using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates two DataMatrix images with different XDimension settings,
    /// then attempts to recognize and report the results.
    /// </summary>
    static void Main()
    {
        // Sample data to encode
        const string codeText = "DataMatrixTest";

        // Determine temporary folder for image files
        string tempPath = Path.GetTempPath();
        string file1 = Path.Combine(tempPath, "datamatrix_x1.png");
        string file3 = Path.Combine(tempPath, "datamatrix_x3.png");

        // Generate DataMatrix with XDimension = 1 pixel
        GenerateDataMatrix(codeText, 1f, file1);

        // Generate DataMatrix with XDimension = 3 pixels
        GenerateDataMatrix(codeText, 3f, file3);

        // Recognize and report results for the 1‑pixel image
        Console.WriteLine("XDimension = 1 pixel:");
        RecognizeAndReport(file1);

        // Recognize and report results for the 3‑pixel image
        Console.WriteLine("XDimension = 3 pixels:");
        RecognizeAndReport(file3);
    }

    /// <summary>
    /// Generates a DataMatrix barcode image with a specified XDimension.
    /// </summary>
    /// <param name="text">The text to encode in the barcode.</param>
    /// <param name="xDimPixels">The XDimension value in pixels.</param>
    /// <param name="outputPath">The file path where the image will be saved.</param>
    static void GenerateDataMatrix(string text, float xDimPixels, string outputPath)
    {
        // Initialize the barcode generator for DataMatrix encoding
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, text))
        {
            // Disable automatic sizing to apply custom XDimension
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Set the XDimension (module size) in points (1 point = 1 pixel for this example)
            generator.Parameters.Barcode.XDimension.Point = xDimPixels;

            // Save the generated barcode image to the specified path
            generator.Save(outputPath);
        }
    }

    /// <summary>
    /// Reads a barcode image and reports whether a DataMatrix code was detected.
    /// </summary>
    /// <param name="imagePath">The path to the barcode image file.</param>
    static void RecognizeAndReport(string imagePath)
    {
        // Verify that the image file exists before attempting recognition
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image not found: {imagePath}");
            return;
        }

        // Initialize the barcode reader for DataMatrix decoding
        using (var reader = new BarCodeReader(imagePath, DecodeType.DataMatrix))
        {
            // Read all barcodes found in the image
            var results = reader.ReadBarCodes();

            // Report detection results
            if (results.Length > 0)
            {
                foreach (var result in results)
                {
                    Console.WriteLine($"Detected: Yes, CodeText = {result.CodeText}");
                }
            }
            else
            {
                Console.WriteLine("Detected: No");
            }
        }
    }
}