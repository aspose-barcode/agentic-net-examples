// Title: DataMatrix barcode generation with exception handling for oversized code text
// Description: Demonstrates how to generate a DataMatrix barcode and catch the exception when the provided code text exceeds the maximum capacity of the selected symbol size.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on DataMatrix symbology. It showcases the use of BarcodeGenerator, EncodeTypes, and DataMatrixVersion classes to control symbol size and handle capacity limits. Developers often need to validate code text length against symbol constraints and gracefully handle overflow errors.
// Prompt: Handle exception when CodeText exceeds maximum capacity for the selected DataMatrix symbol size.
// Tags: datamatrix, exception handling, barcode generation, aspose.barcode, code text capacity

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates a DataMatrix barcode and demonstrates exception handling when the code text exceeds the selected symbol's capacity.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a DataMatrix barcode with an intentionally oversized code text,
    /// forces a small symbol size, and captures the resulting <see cref="BarCodeException"/>.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        const string outputPath = "datamatrix.png";

        // Create a code text that is intentionally too long for a small DataMatrix symbol (2000 characters).
        string longCodeText = new string('A', 2000);

        // Initialize the barcode generator with DataMatrix symbology and the oversized code text.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, longCodeText))
        {
            // Force a small symbol size (10x10) to trigger a capacity overflow.
            generator.Parameters.Barcode.DataMatrix.DataMatrixVersion = DataMatrixVersion.ECC200_10x10;

            // Enable throwing an exception when the code text does not fit the selected symbol.
            generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;

            try
            {
                // Attempt to save the barcode image; this will throw if the code text exceeds capacity.
                generator.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Barcode successfully saved to '{outputPath}'.");
            }
            catch (BarCodeException ex)
            {
                // Handle the overflow situation gracefully by outputting the error details.
                Console.WriteLine("Error generating DataMatrix barcode:");
                Console.WriteLine(ex.Message);
            }
        }
    }
}