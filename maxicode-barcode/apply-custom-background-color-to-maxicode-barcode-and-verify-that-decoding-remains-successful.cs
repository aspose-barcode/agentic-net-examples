// Title: Custom Background Color for MaxiCode Barcode
// Description: Demonstrates applying a custom background color to a MaxiCode barcode and confirming that it can still be decoded correctly.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, focusing on MaxiCode symbology. It showcases the use of ComplexBarcodeGenerator to create a MaxiCode with custom visual settings, and BarCodeReader with ComplexCodetextReader to decode the generated image. Developers working with shipping, logistics, or inventory systems often need to customize barcode appearance while ensuring reliable scanning.
// Prompt: Apply a custom background color to a MaxiCode barcode and verify that decoding remains successful.
// Tags: maxicode, background color, barcode generation, barcode recognition, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Generates a MaxiCode barcode with a custom background color,
/// saves it as an image, and verifies that the barcode can be decoded successfully.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a MaxiCode with a light‑yellow background,
    /// writes it to a PNG file, and then reads the file back to confirm decoding.
    /// </summary>
    static void Main()
    {
        // Define the output file name.
        string outputPath = "maxicode.png";

        // Prepare MaxiCode codetext (Mode 2 example) with postal code, country code, and service category.
        var maxiCode = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",   // 9‑digit US postal code
            CountryCode = 56,           // USA
            ServiceCategory = 999       // Example service category
        };

        // Add a second message to the MaxiCode.
        var secondMessage = new MaxiCodeStandardSecondMessage
        {
            Message = "Sample MaxiCode"
        };
        maxiCode.SecondMessage = secondMessage;

        // Generate the MaxiCode barcode with a custom background color.
        using (var complexGenerator = new ComplexBarcodeGenerator(maxiCode))
        {
            // Set background to light yellow (RGB 255,255,224).
            complexGenerator.Parameters.BackColor = Aspose.Drawing.Color.FromArgb(255, 255, 224);

            // Create the barcode image.
            using (var bitmap = complexGenerator.GenerateBarCodeImage())
            {
                // Save the image as PNG.
                bitmap.Save(outputPath, Aspose.Drawing.Imaging.ImageFormat.Png);
            }
        }

        // Verify that the image file was created.
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read and decode the generated MaxiCode barcode.
        using (var reader = new BarCodeReader(outputPath, DecodeType.MaxiCode))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                // Decode the raw codetext using the appropriate MaxiCode mode.
                var decoded = ComplexCodetextReader.TryDecodeMaxiCode(
                    result.Extended.MaxiCode.MaxiCodeMode,
                    result.CodeText);

                // Check if decoding produced the expected Mode 2 codetext.
                if (decoded is MaxiCodeCodetextMode2 decodedMode2)
                {
                    Console.WriteLine("Decoding successful:");
                    Console.WriteLine($"Postal Code: {decodedMode2.PostalCode}");
                    Console.WriteLine($"Country Code: {decodedMode2.CountryCode}");
                    Console.WriteLine($"Service Category: {decodedMode2.ServiceCategory}");

                    // Output the second message if present.
                    if (decodedMode2.SecondMessage is MaxiCodeStandardSecondMessage stdMsg)
                    {
                        Console.WriteLine($"Message: {stdMsg.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Decoding failed or unexpected codetext type.");
                }
            }
        }
    }
}