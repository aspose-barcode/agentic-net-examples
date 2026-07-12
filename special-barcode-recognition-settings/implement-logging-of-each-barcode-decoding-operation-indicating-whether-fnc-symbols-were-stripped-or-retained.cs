// Title: GS1 Code128 barcode generation and decoding with optional FNC stripping
// Description: Demonstrates creating a GS1 Code128 barcode image and decoding it twice—once preserving FNC symbols and once stripping them.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for image creation and BarCodeReader for decoding, highlighting the StripFNC setting. Developers often need to control FNC character handling when working with GS1 symbologies, making this pattern common in inventory and logistics applications.
// Prompt: Implement logging of each barcode decoding operation, indicating whether FNC symbols were stripped or retained.
// Tags: gs1code128, barcode, generation, decoding, fnc, stripfnc, png, aspnet.barcode, barcodegenerator, barcodereader

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a GS1 Code128 barcode image and decoding it with and without stripping FNC characters.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode if needed and performs two decoding runs, logging the results.
    /// </summary>
    static void Main()
    {
        // Path for the sample barcode image
        const string imagePath = "sample_gs1code128.png";

        // Sample GS1 Code128 text containing FNC characters (represented by parentheses)
        const string codeText = "(02)04006664241007(37)1(400)7019590754";

        // Generate the barcode image if it does not exist
        if (!File.Exists(imagePath))
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, codeText))
            {
                // Save the generated barcode to a PNG file
                generator.Save(imagePath);
                Console.WriteLine($"Barcode image generated: {imagePath}");
            }
        }
        else
        {
            Console.WriteLine($"Using existing barcode image: {imagePath}");
        }

        // Perform two decoding runs: one without stripping FNC, one with stripping FNC
        bool[] stripFncOptions = new[] { false, true };

        foreach (bool stripFnc in stripFncOptions)
        {
            using (var reader = new BarCodeReader(imagePath, DecodeType.GS1Code128))
            {
                // Configure whether FNC characters should be stripped from the decoded text
                reader.BarcodeSettings.StripFNC = stripFnc;

                // Read all barcodes from the image
                foreach (var result in reader.ReadBarCodes())
                {
                    // Log the decoding operation, indicating the StripFNC setting and decoded data
                    Console.WriteLine($"StripFNC = {stripFnc} | Detected Type: {result.CodeTypeName} | CodeText: {result.CodeText}");
                }
            }
        }
    }
}