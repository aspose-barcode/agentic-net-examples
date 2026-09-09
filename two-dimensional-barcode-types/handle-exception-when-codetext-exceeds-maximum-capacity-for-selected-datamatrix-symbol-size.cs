// Title: Handle DataMatrix CodeText Capacity Exception
// Description: Demonstrates how to catch an exception when the CodeText exceeds the maximum capacity of a selected DataMatrix symbol size.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on DataMatrix symbology. It shows how to configure the BarcodeGenerator, select a specific DataMatrix version, enable validation, and handle errors caused by oversized code text. Developers working with barcode creation often need to ensure the data fits the chosen symbol and gracefully handle validation failures.
// Prompt: Handle exception when CodeText exceeds maximum capacity for the selected DataMatrix symbol size.
// Tags: datamatrix, exception-handling, png, barcodegenerator, parameters

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates handling of an exception when the CodeText exceeds the capacity of a selected DataMatrix symbol.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates a DataMatrix barcode with an oversized CodeText and catches the resulting exception.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the temporary directory
        string outputPath = Path.Combine(Path.GetTempPath(), "DataMatrixExceeded.png");

        // Initialize a BarcodeGenerator for DataMatrix with an initially valid short code text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "ABC"))
        {
            // Choose a small DataMatrix version (10x10) which has limited data capacity
            generator.Parameters.Barcode.DataMatrix.Version = DataMatrixVersion.ECC200_10x10;

            // Instruct the generator to throw an exception if the CodeText does not fit the selected symbol
            generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;

            // Assign a CodeText that is deliberately longer than the selected symbol can accommodate
            generator.CodeText = "This text is definitely longer than the capacity of a 10x10 DataMatrix symbol";

            try
            {
                // Attempt to generate the barcode image and save it as PNG
                generator.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Barcode generated successfully and saved to: {outputPath}");
            }
            catch (Exception ex)
            {
                // Output the caught exception details to the console
                Console.WriteLine("Exception caught while generating DataMatrix barcode:");
                Console.WriteLine(ex.Message);
            }
        }
    }
}