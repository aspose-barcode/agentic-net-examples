// Title: Automatic barcode size adjustment based on CodeText length
// Description: Demonstrates how to generate Code128 barcodes where the image dimensions automatically adapt to the length of the supplied CodeText.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and AutoSizeMode for dynamic sizing. Developers often need to create barcodes of varying lengths without manually calculating dimensions, and this pattern shows typical usage for generating PNG images in batch.
// Prompt: Enable automatic size adjustment so dimensions adapt based on the length of CodeText.
// Tags: barcode, code128, autosize, dynamic sizing, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates automatic size adjustment for Code128 barcodes based on the length of the CodeText.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a set of barcodes with varying text lengths, saving each as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define a collection of sample code texts with different lengths
        string[] codeTexts = new[]
        {
            "A1",
            "ABC123",
            "LongerCodeTextExample12345",
            "EvenLongerCodeTextExampleThatExceedsTypicalLengths1234567890"
        };

        // Ensure the output directory exists before saving images
        string outputDir = "Barcodes";
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Iterate over each sample text and generate a corresponding barcode
        foreach (var text in codeTexts)
        {
            // Initialize the generator for Code128, which supports alphanumeric strings
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, text))
            {
                // Set AutoSizeMode to None to keep the default automatic sizing behavior explicit
                generator.Parameters.AutoSizeMode = AutoSizeMode.None;

                // Optionally reduce the X-dimension to keep the overall image size reasonable
                generator.Parameters.Barcode.XDimension.Point = 2f;

                // Build a filename that reflects the length of the code text
                string fileName = Path.Combine(outputDir, $"barcode_{text.Length}.png");

                // Save the generated barcode as a PNG image
                generator.Save(fileName, BarCodeImageFormat.Png);

                // Output a simple status message to the console
                Console.WriteLine($"Generated barcode for text length {text.Length}: {fileName}");
            }
        }
    }
}