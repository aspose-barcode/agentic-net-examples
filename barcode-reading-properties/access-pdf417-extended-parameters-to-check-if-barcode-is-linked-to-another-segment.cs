// Title: PDF417 barcode generation and linked segment detection
// Description: Demonstrates generating a PDF417 barcode with the IsLinked flag and reading the flag via extended parameters.
// Prompt: Access PDF417 extended parameters to check if the barcode is linked to another segment.
// Tags: pdf417, barcode, generation, recognition, extended-parameters, islinked

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a PDF417 barcode with the IsLinked flag and reads the flag using extended parameters.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a PDF417 barcode, saves it, and reads back the IsLinked property.
    /// </summary>
    static void Main()
    {
        // Define the text to encode in the barcode
        const string codeText = "Sample PDF417 Text";

        // Determine the output file path for the generated barcode image
        string outputPath = Path.Combine(Environment.CurrentDirectory, "pdf417.png");

        // Generate a PDF417 barcode and set the IsLinked flag to true
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, codeText))
        {
            // Enable linked mode for PDF417
            generator.Parameters.Barcode.Pdf417.IsLinked = true;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image file was successfully created
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Initialize a reader to decode the PDF417 barcode from the saved image
        using (var reader = new BarCodeReader(outputPath, DecodeType.Pdf417))
        {
            // Iterate through all detected barcode results
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Output the decoded text
                Console.WriteLine($"CodeText: {result.CodeText}");

                // Output the IsLinked flag from the extended PDF417 parameters
                Console.WriteLine($"IsLinked: {result.Extended.Pdf417.IsLinked}");
            }
        }
    }
}