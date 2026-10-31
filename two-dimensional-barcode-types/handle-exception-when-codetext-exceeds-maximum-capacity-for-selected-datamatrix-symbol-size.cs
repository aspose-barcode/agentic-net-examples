// Title: DataMatrix barcode generation with exception handling for oversized CodeText
// Description: Demonstrates how to catch an exception when the provided CodeText exceeds the capacity of a selected DataMatrix symbol size and fallback to a larger symbol.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on DataMatrix symbology. It showcases the use of BarcodeGenerator, EncodeTypes, BarCodeImageFormat, and DataMatrixVersion to create barcodes, while handling capacity limits that developers often encounter when encoding large amounts of data. Typical scenarios include generating DataMatrix codes for inventory, packaging, or tracking where the data size may vary.
// Prompt: Handle exception when CodeText exceeds maximum capacity for the selected DataMatrix symbol size.
// Tags: datamatrix, barcode, exception handling, aspose.barcodes, generation, png, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a DataMatrix barcode and handling the case where the
/// supplied CodeText is too large for the chosen symbol size. If the first attempt
/// fails, the code falls back to a larger DataMatrix version.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a DataMatrix barcode, catches capacity
    /// exceptions, and retries with a larger symbol size.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary output directory.
        string outputDir = Path.Combine(Path.GetTempPath(), "DataMatrixDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "datamatrix.png");

        // Create a CodeText that exceeds the capacity of a small DataMatrix symbol (200 characters).
        string longCodeText = new string('A', 200);

        // Attempt to generate the barcode using a small 10x10 DataMatrix version.
        try
        {
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DataMatrix, longCodeText))
            {
                // Enable exception throwing when the CodeText does not fit the selected symbol.
                generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;
                // Set the specific DataMatrix version (10x10).
                generator.Parameters.Barcode.DataMatrix.Version = DataMatrixVersion.ECC200_10x10;
                // Save the generated barcode as a PNG file.
                generator.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine("Barcode generated successfully with version ECC200_10x10.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed with ECC200_10x10: {ex.Message}");

            // Fallback: try generating with a larger 32x32 DataMatrix version.
            try
            {
                using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DataMatrix, longCodeText))
                {
                    generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;
                    generator.Parameters.Barcode.DataMatrix.Version = DataMatrixVersion.ECC200_32x32;
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                    Console.WriteLine("Barcode generated successfully with version ECC200_32x32.");
                }
            }
            catch (Exception ex2)
            {
                Console.WriteLine($"Failed with ECC200_32x32 as well: {ex2.Message}");
            }
        }
    }
}