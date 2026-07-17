// Title: Encode MaxiCode in Binary mode with Unicode exception handling
// Description: Demonstrates generating a MaxiCode barcode in Binary mode using ASCII data and shows how to catch errors when Unicode characters are supplied.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on MaxiCode symbology. It illustrates using BarcodeGenerator, setting MaxiCodeEncodeMode, and handling encoding exceptions—common tasks for developers creating shipping labels or inventory tags where MaxiCode is required.
// Prompt: Encode MaxiCode data in Binary mode and handle exceptions for Unicode characters.
// Tags: maxicode, binary mode, unicode handling, barcode generation, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a MaxiCode barcode in Binary mode,
/// saves a valid barcode, and demonstrates exception handling when
/// Unicode characters are used in the code text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Path for the successfully generated barcode image
        string outputPath = "maxicode_binary.png";

        // ------------------------------------------------------------
        // Generate a valid MaxiCode barcode in Binary mode using ASCII data
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode))
        {
            // Configure the generator to use Binary encoding mode
            generator.Parameters.Barcode.MaxiCode.MaxiCodeEncodeMode = MaxiCodeEncodeMode.Binary;

            // Set ASCII-only code text (valid for Binary mode)
            generator.CodeText = "ABC123";

            // Save the barcode image as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
            Console.WriteLine($"Barcode saved to {Path.GetFullPath(outputPath)}");
        }

        // ------------------------------------------------------------
        // Attempt to generate a MaxiCode barcode with Unicode characters
        // in Binary mode, which should raise an exception
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode))
        {
            // Set Binary encoding mode again
            generator.Parameters.Barcode.MaxiCode.MaxiCodeEncodeMode = MaxiCodeEncodeMode.Binary;

            try
            {
                // This code text contains a Unicode character (é) not allowed in Binary mode
                generator.CodeText = "ABCé123";

                // If no exception occurs (unlikely), save the image
                generator.Save("maxicode_unicode.png");
                Console.WriteLine("Unicode barcode saved (unexpected).");
            }
            catch (Exception ex)
            {
                // Expected outcome: an exception is thrown due to the Unicode character
                Console.WriteLine("Failed to encode Unicode characters in Binary mode:");
                Console.WriteLine(ex.Message);
            }
        }
    }
}