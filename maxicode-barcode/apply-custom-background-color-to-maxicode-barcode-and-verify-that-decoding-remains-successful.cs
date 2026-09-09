// Title: Custom Background Color for MaxiCode Barcode with Decoding Verification
// Description: Demonstrates how to generate a MaxiCode barcode with a custom background color and then decode it to ensure the barcode remains readable.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing how to customize barcode appearance using BarcodeGenerator and verify readability with BarCodeReader. It highlights key API classes such as BarcodeGenerator, BarCodeReader, and ComplexCodetextReader, which developers commonly use for creating, styling, and decoding complex symbologies like MaxiCode in .NET applications.
// Prompt: Apply a custom background color to a MaxiCode barcode and verify that decoding remains successful.
// Tags: maxicode, barcode generation, barcode decoding, background color, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a MaxiCode barcode with a custom background color and verifying successful decoding.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a temporary directory, generates the barcode image, and decodes it to confirm readability.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempDir = Path.Combine(Path.GetTempPath(), "MaxiCodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define the output image path and the text to encode
        string imagePath = Path.Combine(tempDir, "maxicode.png");
        string codeText = "Sample MaxiCode";

        // Generate MaxiCode barcode with a custom background color
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, codeText))
        {
            // Set background to LightBlue and barcode bars to Black
            generator.Parameters.BackColor = Color.LightBlue;
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save the generated barcode as a PNG image
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode saved to: {imagePath}");

        // Verify that the image file was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to generate barcode image.");
            return;
        }

        // Decode the generated barcode using the MaxiCode decoder
        using (var reader = new BarCodeReader(imagePath, DecodeType.MaxiCode))
        {
            var results = reader.ReadBarCodes();

            // Determine if decoding succeeded and the code text is not empty
            bool success = results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText);
            Console.WriteLine($"Decoding success: {success}");

            if (success)
            {
                // Iterate through all decoded results
                foreach (var result in results)
                {
                    Console.WriteLine($"CodeText: {result.CodeText}");

                    try
                    {
                        // Retrieve MaxiCode mode from extended information
                        var mode = result.Extended.MaxiCode.Mode;

                        // Attempt to decode complex codetext using the appropriate mode
                        var complex = ComplexCodetextReader.TryDecodeMaxiCode(mode, result.CodeText);
                        if (complex != null)
                        {
                            Console.WriteLine($"Complex codetext type: {complex.GetType().Name}");
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log any errors that occur during complex decoding
                        Console.WriteLine($"Complex decode error: {ex.Message}");
                    }
                }
            }
        }
    }
}