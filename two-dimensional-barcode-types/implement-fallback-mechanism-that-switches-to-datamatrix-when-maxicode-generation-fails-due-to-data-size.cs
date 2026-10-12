// Title: Barcode generation with fallback from MaxiCode to DataMatrix
// Description: Demonstrates generating a MaxiCode barcode and automatically falling back to a DataMatrix barcode when the data exceeds MaxiCode capacity.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use the BarcodeGenerator class with different EncodeTypes, handle exceptions, and implement a fallback strategy. Developers often need to choose appropriate symbologies based on data size and format constraints; this snippet shows typical use cases for MaxiCode and DataMatrix, including setting XDimension and saving PNG images.
// Prompt: Implement fallback mechanism that switches to DataMatrix when MaxiCode generation fails due to data size.
// Tags: barcode, fallback, maximcode, datamatrix, generation, png, aspose.barcode, barcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates a fallback mechanism that attempts to generate a MaxiCode barcode
/// and, if that fails (e.g., due to data size limits), switches to generating a DataMatrix barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates sample data, attempts MaxiCode generation,
    /// and falls back to DataMatrix when necessary.
    /// </summary>
    static void Main()
    {
        // Create sample data that exceeds MaxiCode capacity (more than 60 bytes)
        string data = new string('A', 200);

        // Prepare an output directory in the temporary folder
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeFallbackDemo");
        Directory.CreateDirectory(outputDir);

        // Define file paths for the two possible barcode images
        string maxiCodePath = Path.Combine(outputDir, "maxicode.png");
        string dataMatrixPath = Path.Combine(outputDir, "datamatrix.png");

        Console.WriteLine("Attempting to generate MaxiCode...");

        // Try to generate MaxiCode; if it fails, fallback to DataMatrix
        bool maxiSuccess = TryGenerateMaxiCode(data, maxiCodePath);

        if (maxiSuccess)
        {
            Console.WriteLine($"MaxiCode generated successfully: {maxiCodePath}");
        }
        else
        {
            Console.WriteLine("MaxiCode generation failed. Falling back to DataMatrix...");

            bool dmSuccess = TryGenerateDataMatrix(data, dataMatrixPath);
            if (dmSuccess)
            {
                Console.WriteLine($"DataMatrix generated successfully: {dataMatrixPath}");
            }
            else
            {
                Console.WriteLine("DataMatrix generation also failed.");
            }
        }
    }

    /// <summary>
    /// Attempts to generate a MaxiCode barcode and save it as a PNG file.
    /// </summary>
    /// <param name="codeText">The text to encode.</param>
    /// <param name="filePath">The full path where the PNG will be saved.</param>
    /// <returns>True if generation succeeds; otherwise, false.</returns>
    static bool TryGenerateMaxiCode(string codeText, string filePath)
    {
        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, codeText))
            {
                // Optional: increase XDimension for better visibility
                generator.Parameters.Barcode.XDimension.Pixels = 10f;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"MaxiCode generation error: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Attempts to generate a DataMatrix barcode and save it as a PNG file.
    /// </summary>
    /// <param name="codeText">The text to encode.</param>
    /// <param name="filePath">The full path where the PNG will be saved.</param>
    /// <returns>True if generation succeeds; otherwise, false.</returns>
    static bool TryGenerateDataMatrix(string codeText, string filePath)
    {
        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
            {
                // Let the library choose the appropriate version automatically
                generator.Parameters.Barcode.XDimension.Pixels = 5f;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"DataMatrix generation error: {ex.Message}");
            return false;
        }
    }
}