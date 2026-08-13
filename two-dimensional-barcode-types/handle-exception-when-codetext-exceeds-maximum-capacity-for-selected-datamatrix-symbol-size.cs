// Title: Handle DataMatrix capacity overflow with exception handling
// Description: Demonstrates generating a DataMatrix barcode, detecting when the supplied CodeText exceeds the capacity of a selected symbol size, and falling back to automatic size selection.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on DataMatrix symbology. It showcases the use of BarcodeGenerator, EncodeTypes, and DataMatrixVersion classes to control symbol size, handle capacity limits, and implement fallback logic. Developers often need to generate DataMatrix codes of a specific size or automatically select the optimal size while gracefully handling overflow errors.
// Prompt: Handle exception when CodeText exceeds maximum capacity for the selected DataMatrix symbol size.
// Tags: datamatrix, barcode, exception handling, capacity, autosize, aspose.barcode, generation, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates handling of capacity overflow when generating a DataMatrix barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Attempts to generate a DataMatrix barcode with a fixed small version, catches capacity overflow, and retries with automatic sizing.
    /// </summary>
    static void Main()
    {
        // Create a code text that exceeds the capacity of a small 10x10 DataMatrix symbol.
        string longCodeText = new string('A', 200);

        // Choose a small DataMatrix version to intentionally cause overflow.
        DataMatrixVersion smallVersion = DataMatrixVersion.ECC200_10x10;

        // Define output file paths in the system's temporary directory.
        string tempDir = Path.GetTempPath();
        string outputPath = Path.Combine(tempDir, "DataMatrix_Small.png");
        string fallbackPath = Path.Combine(tempDir, "DataMatrix_Auto.png");

        Console.WriteLine("Attempting to generate DataMatrix with fixed size {0}...", smallVersion);

        // Try generating the barcode with the small version.
        bool success = GenerateDataMatrix(longCodeText, smallVersion, outputPath);

        if (!success)
        {
            // If generation fails due to capacity limits, retry with automatic version selection.
            Console.WriteLine("Generation failed due to code text exceeding symbol capacity.");
            Console.WriteLine("Retrying with automatic size selection (Version.Auto)...");
            GenerateDataMatrix(longCodeText, DataMatrixVersion.Auto, fallbackPath);
        }

        Console.WriteLine("Execution completed.");
    }

    /// <summary>
    /// Generates a DataMatrix barcode with the specified version.
    /// Returns true if generation succeeds; false if an exception occurs due to capacity limits.
    /// </summary>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="version">The DataMatrix version (symbol size) to use.</param>
    /// <param name="outputFile">The file path where the barcode image will be saved.</param>
    /// <returns>True if the barcode is generated successfully; otherwise, false.</returns>
    static bool GenerateDataMatrix(string codeText, DataMatrixVersion version, string outputFile)
    {
        try
        {
            // Initialize the barcode generator for DataMatrix encoding.
            using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
            {
                // Set the desired DataMatrix version (symbol size).
                generator.Parameters.Barcode.DataMatrix.Version = version;

                // Configure optional padding around the barcode.
                generator.Parameters.Barcode.Padding.Left.Point = 5f;
                generator.Parameters.Barcode.Padding.Top.Point = 5f;
                generator.Parameters.Barcode.Padding.Right.Point = 5f;
                generator.Parameters.Barcode.Padding.Bottom.Point = 5f;

                // Save the generated barcode as a PNG image.
                generator.Save(outputFile, BarCodeImageFormat.Png);
                Console.WriteLine("Barcode saved to: " + outputFile);
                return true;
            }
        }
        catch (Exception ex)
        {
            // Capture any exception (e.g., capacity overflow) and report the error.
            Console.WriteLine("Error: " + ex.Message);
            return false;
        }
    }
}