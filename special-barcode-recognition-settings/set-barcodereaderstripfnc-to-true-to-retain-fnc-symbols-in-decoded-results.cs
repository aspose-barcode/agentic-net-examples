// Title: Retain FNC Symbols Using BarCodeReader.StripFNC
// Description: Demonstrates how to generate a GS1 Code128 barcode containing FNC characters, then read it with StripFNC set to true so the FNC symbols are kept in the decoded text.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating the use of BarCodeReader and BarcodeGenerator for handling GS1 Code128 symbology. It shows how to configure BarcodeSettings.StripFNC to control whether function characters (FNC) are stripped or retained during decoding—common when processing GS1 data streams that include application identifiers. Developers often need to preserve FNC symbols to maintain data integrity in supply‑chain and inventory systems.
// Prompt: Set BarCodeReader.StripFNC to true to retain FNC symbols in decoded results.
// Tags: barcode symbology, gs1, code128, stripfnc, barcode generation, barcode recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a GS1 Code128 barcode containing FNC characters,
/// then reads it back with <c>StripFNC</c> enabled to retain those characters in the result.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, saves it to disk,
    /// and reads it back while preserving FNC symbols.
    /// </summary>
    static void Main()
    {
        // Path where the barcode image will be saved
        string imagePath = "sample_barcode.png";

        // Generate a GS1 Code128 barcode that includes FNC characters (e.g., application identifiers)
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, "(02)04006664241007(37)1(400)7019590754"))
        {
            // Save the generated barcode as a PNG file
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Ensure the barcode image was successfully created before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Failed to create barcode image at '{imagePath}'.");
            return;
        }

        // Initialize a reader for Code128 barcodes and configure it to retain FNC symbols
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Set StripFNC to true so function characters are NOT stripped from the decoded text
            reader.BarcodeSettings.StripFNC = true;

            // Iterate through all detected barcodes in the image
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Decoded Text: {result.CodeText}");
            }
        }
    }
}