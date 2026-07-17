// Title: Han Xin Barcode Generation with Capacity Exception Handling
// Description: Demonstrates generating a Han Xin barcode and handling cases where the input data exceeds the symbol's maximum capacity.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on Han Xin (Chinese Postal) symbology. It showcases the use of BarcodeGenerator, EncodeTypes, and HanXin parameters to create a barcode image, while illustrating typical developer needs such as automatic version selection, error correction configuration, and robust exception handling for data overflow scenarios. Ideal for developers searching for barcode generation patterns, error handling techniques, and Han Xin specific API usage.
/// Prompt: Implement exception handling for data exceeding maximum capacity of selected Han Xin symbol size.
/// Tags: barcode, hansin, exception-handling, generation, png, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Provides an example of generating a Han Xin barcode and handling data capacity overflow exceptions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a Han Xin barcode and writes the result to a PNG file.
    /// </summary>
    static void Main()
    {
        // Path where the generated barcode image will be saved
        const string outputPath = "HanXinBarcode.png";

        // Create a long string that is likely to exceed the capacity of the automatically selected Han Xin version
        string longCodeText = new string('A', 5000); // Adjust length to trigger capacity overflow

        // Initialize the barcode generator for Han Xin symbology with the provided text
        using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, longCodeText))
        {
            // Set a high error correction level (optional, improves readability at the cost of capacity)
            generator.Parameters.Barcode.HanXin.ErrorLevel = HanXinErrorLevel.L4;

            // Allow the library to automatically select the appropriate Han Xin version based on data length
            generator.Parameters.Barcode.HanXin.Version = HanXinVersion.Auto;

            try
            {
                // Attempt to generate and save the barcode image as PNG
                generator.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Barcode generated successfully: {outputPath}");
            }
            catch (BarCodeException ex)
            {
                // Specific handling when the input data exceeds the maximum capacity of the selected symbol size
                Console.WriteLine("Error: The provided data exceeds the maximum capacity of the selected Han Xin symbol size.");
                Console.WriteLine($"Exception message: {ex.Message}");
            }
            catch (Exception ex)
            {
                // General fallback for any other unexpected errors during barcode generation
                Console.WriteLine("An unexpected error occurred while generating the barcode.");
                Console.WriteLine($"Exception message: {ex.Message}");
            }
        }
    }
}