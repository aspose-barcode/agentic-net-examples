// Title: Han Xin Barcode Generation with Oversized Data Exception Handling
// Description: Demonstrates generating a Han Xin barcode and handling the exception thrown when the input data exceeds the symbol's maximum capacity.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on Han Xin symbology. It showcases the use of BarcodeGenerator, EncodeTypes, and HanXinErrorLevel classes to create a barcode, and illustrates proper exception handling for data size constraints. Developers working with 2D barcodes can refer to this pattern for validating input length and managing BarCodeException errors.
// Prompt: Implement exception handling for data exceeding maximum capacity of selected Han Xin symbol size.
// Tags: hanxin,barcode,generation,exception-handling,aspnet,aspose.barcode,2d,capacity,validation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Han Xin barcode and demonstrates handling of data that exceeds the symbol's capacity.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a Han Xin barcode with oversized data and captures any capacity-related exceptions.
    /// </summary>
    static void Main()
    {
        // Prepare sample data that intentionally exceeds the maximum capacity of any Han Xin symbol size.
        string oversizedData = new string('A', 2000);

        // Define the output file path in the system's temporary directory.
        string outputPath = Path.Combine(Path.GetTempPath(), "HanXin_Oversized.png");

        // Initialize the barcode generator for Han Xin symbology with the oversized data.
        using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, oversizedData))
        {
            // Optionally set the error correction level (L1-L4). Here we use level L2.
            generator.Parameters.Barcode.HanXin.ErrorLevel = HanXinErrorLevel.L2;

            try
            {
                // Attempt to save the barcode image. This call will throw if the data does not fit the selected symbol size.
                generator.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Barcode generated successfully: {outputPath}");
            }
            catch (BarCodeException ex)
            {
                // Specific handling for data exceeding the maximum capacity of the Han Xin symbol.
                Console.WriteLine("Failed to generate Han Xin barcode: data exceeds maximum capacity.");
                Console.WriteLine($"Error details: {ex.Message}");
            }
            catch (Exception ex)
            {
                // General fallback for any other unexpected errors during barcode generation.
                Console.WriteLine("An unexpected error occurred while generating the barcode.");
                Console.WriteLine($"Error details: {ex.Message}");
            }
        }
    }
}