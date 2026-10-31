// Title: Set ECIEncoding to UTF-8 for MaxiCode and verify embedded character set
// Description: Demonstrates how to generate a MaxiCode barcode with UTF‑8 ECI encoding and then read it back to confirm the correct character set identifier is embedded.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on MaxiCode symbology. It showcases the use of BarcodeGenerator, MaxiCode parameters, and BarCodeReader to handle ECI (Extended Channel Interpretation) encoding. Developers working with international text in MaxiCode can use this pattern to ensure proper character set handling across generation and decoding.
// Prompt: Set ECIEncoding to UTF‑8 for MaxiCode and verify the correct character set identifier is embedded.
// Tags: maxicode, eciencoding, utf-8, barcode generation, barcode recognition, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a MaxiCode barcode with UTF‑8 ECI encoding and verifying the embedded character set by decoding the image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a MaxiCode image, saves it, and reads it back to display the decoded text.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory to store the generated image
        string tempDir = Path.Combine(Path.GetTempPath(), "MaxiCodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string imagePath = Path.Combine(tempDir, "maxicode.png");

        // Generate a MaxiCode barcode containing Unicode characters and set ECI encoding to UTF-8
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "犬Right狗"))
        {
            // Apply UTF-8 ECI encoding to the MaxiCode parameters
            generator.Parameters.Barcode.MaxiCode.ECIEncoding = ECIEncodings.UTF8;

            // Save the barcode as a PNG image
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify the generated barcode by decoding it and outputting the result
        if (File.Exists(imagePath))
        {
            using (var reader = new BarCodeReader(imagePath, DecodeType.MaxiCode))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Decoded CodeText: {result.CodeText}");
                }
            }
        }
        else
        {
            Console.WriteLine("Failed to generate the MaxiCode image.");
        }
    }
}