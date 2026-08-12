// Title: Encode MaxiCode in Binary Mode with Unicode Exception Handling
// Description: Demonstrates generating a MaxiCode barcode in Binary encoding mode and handling errors when Unicode characters are present.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on MaxiCode symbology. It showcases the use of BarcodeGenerator, EncodeTypes, and MaxiCodeEncodeMode to create barcodes, a common task for developers integrating shipping or logistics solutions. Typical use cases include encoding package data in MaxiCode for automated sorting, where binary mode is required and Unicode validation is essential.
// Prompt: Encode MaxiCode data in Binary mode and handle exceptions for Unicode characters.
// Tags: maxicode, binary encoding, unicode exception, barcode generation, aspose.barcode, png output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a MaxiCode barcode in Binary mode and demonstrates exception handling for unsupported Unicode characters.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, saves it to a temporary PNG file, and reports success or errors.
    /// </summary>
    static void Main()
    {
        // Sample code text containing Unicode characters (emoji) that are not allowed in Binary mode.
        string codeText = "TestUnicode😀";

        // Prepare output file path in the temporary folder.
        string outputPath = Path.Combine(Path.GetTempPath(), "maxicode_binary.png");

        // Create the barcode generator for MaxiCode with the provided text.
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, codeText))
        {
            // Set the encoding mode to Binary, which does not support Unicode characters.
            generator.Parameters.Barcode.MaxiCode.EncodeMode = MaxiCodeEncodeMode.Binary;

            try
            {
                // Attempt to save the barcode image to the specified path.
                generator.Save(outputPath);
                Console.WriteLine($"Barcode saved successfully to: {outputPath}");
            }
            catch (InvalidCodeException ex)
            {
                // Handle the exception thrown when Unicode characters are present in Binary mode.
                Console.WriteLine("Failed to encode in Binary mode due to unsupported Unicode characters.");
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                // General exception handling for any unexpected errors.
                Console.WriteLine("An unexpected error occurred:");
                Console.WriteLine(ex.ToString());
            }
        }
    }
}