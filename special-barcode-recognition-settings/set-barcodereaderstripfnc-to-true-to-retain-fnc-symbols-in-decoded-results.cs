// Title: Retaining FNC Symbols in GS1-128 Barcode Decoding with Aspose.BarCode
// Description: This example generates a GS1-128 barcode containing FNC characters, then reads it while preserving those FNC symbols in the decoded text.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition for GS1-128 symbology. It showcases the use of BarcodeGenerator to create barcodes and BarCodeReader to decode them, highlighting how to configure BarcodeSettings (StripFNC) to retain functional characters. Ideal for developers needing precise GS1 data handling, such as inventory or logistics applications.
// Prompt: Set BarCodeReader.StripFNC to true to retain FNC symbols in decoded results.
// Tags: gs1-128, fnc, barcode, encoding, decoding, aspose.barcode, barcodegenerator, barcodereader

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates how to retain FNC symbols when reading a GS1-128 barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode with FNC characters, saves it, and reads it back preserving FNC symbols.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define the full path for the output barcode image.
        string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

        // Generate a GS1-128 barcode that includes FNC characters in its data string.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1Code128, "(02)04006664241007(37)1(400)7019590754"))
        {
            // Save the generated barcode image to the specified path.
            generator.Save(imagePath);
        }

        // Verify that the barcode image was successfully created.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create the barcode image.");
            return;
        }

        // Initialize a reader for the saved image, specifying the GS1-128 decode type.
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.GS1Code128))
        {
            // Configure the reader to retain FNC symbols in the decoded result.
            reader.BarcodeSettings.StripFNC = true;

            // Iterate through all detected barcodes and output their type and decoded text.
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"BarCode Type: {result.CodeTypeName}");
                Console.WriteLine($"BarCode CodeText: {result.CodeText}");
            }
        }
    }
}