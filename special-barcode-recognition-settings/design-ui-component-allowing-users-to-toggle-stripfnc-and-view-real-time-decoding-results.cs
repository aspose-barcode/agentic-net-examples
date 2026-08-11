// Title: Toggle StripFNC while decoding a GS1‑128 barcode
// Description: Generates a GS1‑128 barcode containing FNC1 characters, then decodes it twice—once preserving and once stripping the FNC characters—to illustrate the effect of the StripFNC setting.
// Category-Description: This example belongs to the Aspose.BarCode decoding settings category. It demonstrates how to use BarcodeGenerator, BarCodeReader, and BarcodeSettings to control the StripFNC option, a common requirement when processing GS1 symbologies such as Code128. Developers often need to toggle this setting to obtain raw data or human‑readable output, making it essential for inventory, shipping, and retail applications.
// Prompt: Design a UI component allowing users to toggle StripFNC and view real‑time decoding results.
// Tags: gs1-128, stripfnc, barcode decoding, aspose.barcode, code128, barcode generation, c#

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a GS1‑128 barcode with FNC1 characters and decoding it
/// with the <c>StripFNC</c> option toggled on and off.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, then decodes it twice
    /// to show the impact of the <c>StripFNC</c> setting.
    /// </summary>
    static void Main()
    {
        // Sample GS1‑128 data containing FNC1 characters (application identifiers)
        const string barcodeData = "(02)04006664241007(37)1(400)7019590754";

        // Create an in‑memory stream to hold the generated barcode image
        using (var imageStream = new MemoryStream())
        {
            // Generate the barcode image and write it to the stream
            using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, barcodeData))
            {
                // Save the barcode as PNG into the memory stream
                generator.Save(imageStream, BarCodeImageFormat.Png);
                // Reset the stream position so it can be read from the beginning
                imageStream.Position = 0;
            }

            // Decode the barcode without stripping FNC characters
            DecodeAndPrint(imageStream, stripFnc: false);

            // Reset the stream position for the second decoding pass
            imageStream.Position = 0;

            // Decode the barcode with FNC characters stripped
            DecodeAndPrint(imageStream, stripFnc: true);
        }
    }

    /// <summary>
    /// Decodes the barcode from the provided stream and prints the result to the console.
    /// </summary>
    /// <param name="stream">Stream containing the barcode image.</param>
    /// <param name="stripFnc">If <c>true</c>, FNC characters are stripped from the decoded text.</param>
    private static void DecodeAndPrint(Stream stream, bool stripFnc)
    {
        // Initialize a reader for Code128 barcodes using the supplied image stream
        using (var reader = new BarCodeReader(stream, DecodeType.Code128))
        {
            // Apply the StripFNC setting based on the caller's request
            reader.BarcodeSettings.StripFNC = stripFnc;

            // Perform the decoding operation
            BarCodeResult[] results = reader.ReadBarCodes();

            Console.WriteLine($"--- Decoding with StripFNC = {stripFnc} ---");
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
                return;
            }

            // Iterate through all detected barcodes and output their details
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"Type   : {result.CodeTypeName}");
                Console.WriteLine($"Text   : {result.CodeText}");
                Console.WriteLine($"Angle  : {result.Region.Angle}");
                Console.WriteLine($"Region : X={result.Region.Rectangle.X}, Y={result.Region.Rectangle.Y}, " +
                                  $"Width={result.Region.Rectangle.Width}, Height={result.Region.Rectangle.Height}");
                Console.WriteLine();
            }
        }
    }
}