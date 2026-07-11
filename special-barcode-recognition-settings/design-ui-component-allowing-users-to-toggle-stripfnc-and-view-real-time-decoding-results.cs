// Title: Toggle StripFNC on GS1-128 barcode decoding
// Description: Demonstrates generating a GS1‑128 barcode, then decoding it with and without stripping FNC characters to show the effect on the extracted text.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the BarcodeGenerator and BarCodeReader classes, focusing on the StripFNC setting used when decoding GS1‑128 (Code128) barcodes. Developers often need to control whether Function characters are retained or removed during decoding to meet GS1 data formatting requirements.
// Prompt: Design a UI component allowing users to toggle StripFNC and view real‑time decoding results.
// Tags: gs1-128, stripfnc, barcode generation, barcode recognition, code128, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates a GS1‑128 barcode, then reads it twice: once preserving FNC characters
/// and once stripping them, illustrating the impact of the <c>StripFNC</c> setting.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a barcode image in memory, then decodes it
    /// with different <c>StripFNC</c> configurations.
    /// </summary>
    static void Main()
    {
        // Sample GS1‑128 barcode text containing FNC characters
        const string barcodeText = "(02)04006664241007(37)1(400)7019590754";

        // Generate the barcode image into a memory stream (PNG format)
        using (var imageStream = new MemoryStream())
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, barcodeText))
            {
                // Save the generated barcode as PNG to the stream
                generator.Save(imageStream, BarCodeImageFormat.Png);
            }

            // Reset stream position so it can be read from the beginning
            imageStream.Position = 0;

            // Local function that reads the barcode with a specified StripFNC value
            void ReadAndDisplay(bool stripFnc)
            {
                // Ensure the stream is positioned at the start before creating a bitmap
                imageStream.Position = 0;

                // Load the image from the stream into a bitmap object
                using (var bitmap = new Bitmap(imageStream))
                {
                    // Initialize a reader for Code128 (covers GS1‑128)
                    using (var reader = new BarCodeReader(bitmap, DecodeType.Code128))
                    {
                        // Apply the StripFNC setting (true = remove FNC characters)
                        reader.BarcodeSettings.StripFNC = stripFnc;

                        // Perform barcode recognition and output results
                        foreach (var result in reader.ReadBarCodes())
                        {
                            Console.WriteLine($"StripFNC = {stripFnc}");
                            Console.WriteLine($"  Type : {result.CodeTypeName}");
                            Console.WriteLine($"  Text : {result.CodeText}");
                        }
                    }
                }
            }

            // Decode and display results without stripping FNC characters
            ReadAndDisplay(false);

            // Decode and display results with FNC characters stripped
            ReadAndDisplay(true);
        }
    }
}