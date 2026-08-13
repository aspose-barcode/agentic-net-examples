// Title: Access PDF417 Extended Parameters to Determine Linkage
// Description: Demonstrates how to set and read the IsLinked property of a PDF417 barcode using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode PDF417 barcode manipulation category, showcasing the use of BarcodeGenerator, BarCodeReader, and extended PDF417 parameters. Developers often need to control and verify segment linking for multi‑segment PDF417 codes in document processing and scanning solutions.
// Prompt: Access PDF417 extended parameters to check if the barcode is linked to another segment.
// Tags: pdf417, extended-parameters, islinked, barcode-generation, barcode-recognition, aspnet, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that creates a PDF417 barcode with the IsLinked flag set,
/// saves it as an image, and then reads the barcode to verify the flag using
/// Aspose.BarCode's extended PDF417 parameters.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a PDF417 barcode, saves it,
    /// and reads back the IsLinked property from the extended parameters.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "pdf417.png";

        // Create a PDF417 barcode generator with sample text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Pdf417, "Sample PDF417 Text"))
        {
            // Enable the IsLinked flag to indicate this barcode is linked to another segment.
            generator.Parameters.Barcode.Pdf417.IsLinked = true;

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was successfully created.
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to create the barcode image.");
            return;
        }

        // Initialize a barcode reader for PDF417 type to read the saved image.
        using (BarCodeReader reader = new BarCodeReader(outputPath, DecodeType.Pdf417))
        {
            // Iterate through all detected barcode results.
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Retrieve the IsLinked flag from the extended PDF417 parameters.
                bool isLinked = result.Extended.Pdf417.IsLinked;

                // Output the flag value to the console.
                Console.WriteLine($"IsLinked: {isLinked}");
            }
        }
    }
}