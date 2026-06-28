using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generation of DataBar Omnidirectional barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a set of barcodes and saves them as JPEG files.
    /// </summary>
    static void Main()
    {
        // Determine the output directory path relative to the current working directory.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");

        // Create the output directory if it does not already exist.
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Sample code texts to be encoded as DataBar Omnidirectional barcodes.
        string[] codeTexts = new string[]
        {
            "(01)12345678901231",
            "(01)98765432109876",
            "(01)55555555555555",
            "(01)11111111111111",
            "(01)22222222222222"
        };

        // Iterate over each code text, generate a barcode, and save it to a file.
        for (int i = 0; i < codeTexts.Length; i++)
        {
            // Current code text to encode.
            string codeText = codeTexts[i];

            // Construct the output file path (e.g., databar_1.jpg, databar_2.jpg, ...).
            string filePath = Path.Combine(outputDir, $"databar_{i + 1}.jpg");

            // BarcodeGenerator implements IDisposable; use a using block to ensure proper disposal.
            using (var generator = new BarcodeGenerator(EncodeTypes.DatabarOmniDirectional, codeText))
            {
                // Disable automatic sizing so we can set a custom bar height.
                generator.Parameters.AutoSizeMode = AutoSizeMode.None;

                // Set the bar height to 50 points (pixels).
                generator.Parameters.Barcode.BarHeight.Point = 50f;

                // Save the generated barcode image as a JPEG file.
                generator.Save(filePath, BarCodeImageFormat.Jpeg);
            }

            // Inform the user that the barcode has been saved.
            Console.WriteLine($"Saved barcode {i + 1} to {filePath}");
        }
    }
}