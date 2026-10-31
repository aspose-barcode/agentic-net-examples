// Title: Barcode generation with error handling for invalid codetext
// Description: Demonstrates creating a barcode image, handling errors when the codetext is invalid for the selected symbology.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showing how to use BarcodeGenerator, configure ThrowExceptionWhenCodeTextIncorrect, and catch exceptions during Save. Developers often need to validate codetext and handle generation failures when integrating barcode creation into automated workflows or CI pipelines.
// Prompt: Implement error handling to catch exceptions when barcode generation fails due to invalid Codetext characters.
// Tags: barcode, symbology, error handling, generation, png, aspose.barcode, code128, itf6

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeErrorHandlingDemo
{
    /// <summary>
    /// Demonstrates barcode generation with proper error handling for invalid codetext.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the demo. Generates a valid barcode and attempts to generate an invalid one,
        /// catching any exceptions that occur due to incorrect codetext.
        /// </summary>
        static void Main()
        {
            // Create a unique temporary directory for output files.
            string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(outputDir);

            // -------------------- Generate a valid barcode --------------------
            string validPath = Path.Combine(outputDir, "valid.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Valid123"))
            {
                // Save the barcode image in PNG format.
                generator.Save(validPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Valid barcode saved to: {validPath}");
            }

            // -------------------- Attempt to generate an invalid barcode --------------------
            string invalidPath = Path.Combine(outputDir, "invalid.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.ITF6, "12"))
            {
                // Instruct the generator to throw an exception if the codetext is incorrect for the symbology.
                generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;

                try
                {
                    // This call is expected to fail because "12" is not a valid ITF6 codetext.
                    generator.Save(invalidPath, BarCodeImageFormat.Png);
                    Console.WriteLine($"Invalid barcode saved to: {invalidPath}");
                }
                catch (Exception ex)
                {
                    // Handle the generation error and output a friendly message.
                    Console.WriteLine($"Error generating barcode: {ex.Message}");
                }
            }
        }
    }
}