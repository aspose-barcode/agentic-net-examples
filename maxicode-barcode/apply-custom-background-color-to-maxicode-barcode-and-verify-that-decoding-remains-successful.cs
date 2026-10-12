// Title: Apply custom background color to a MaxiCode barcode and verify decoding
// Description: Demonstrates how to generate a MaxiCode barcode with a custom background color, save it as PNG, and confirm that the barcode can be decoded correctly.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating MaxiCode symbols, setting visual parameters such as BackColor, and employing BarCodeReader with DecodeType.MaxiCode to read complex codetext. Developers working with high‑density 2‑D barcodes can use these APIs to customize appearance while ensuring scan reliability.
// Prompt: Apply a custom background color to a MaxiCode barcode and verify that decoding remains successful.
// Tags: maxicode, background color, barcode generation, barcode decoding, aspose.barcode, png, complexcodetext

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a MaxiCode barcode with a custom background color,
/// saving it to a PNG file, and decoding it to verify successful reading.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the temporary directory.
        string outputPath = Path.Combine(Path.GetTempPath(), "maxicode.png");

        // Generate a MaxiCode barcode with a custom background color.
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "Sample MaxiCode"))
        {
            // Set the background color to LightBlue.
            generator.Parameters.BackColor = Aspose.Drawing.Color.LightBlue;
            // Save the barcode image as PNG.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was created successfully.
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to generate barcode image.");
            return;
        }

        // Decode the generated barcode and verify decoding.
        bool decodeSuccess = false;
        using (var reader = new BarCodeReader(outputPath, DecodeType.MaxiCode))
        {
            // Read all barcodes found in the image.
            BarCodeResult[] results = reader.ReadBarCodes();
            foreach (var result in results)
            {
                // Attempt to decode complex MaxiCode codetext.
                var complexCodetext = ComplexCodetextReader.TryDecodeMaxiCode(result.Extended.MaxiCode.Mode, result.CodeText);
                if (complexCodetext != null)
                {
                    decodeSuccess = true;
                    Console.WriteLine($"Decoded complex codetext type: {complexCodetext.GetType().Name}");
                    Console.WriteLine($"CodeText: {result.CodeText}");
                }
            }
        }

        // Output the overall decoding result.
        Console.WriteLine($"Decoding {(decodeSuccess ? "succeeded" : "failed")}.");
    }
}