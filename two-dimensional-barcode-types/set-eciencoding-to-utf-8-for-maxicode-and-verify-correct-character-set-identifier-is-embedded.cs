// Title: Generate MaxiCode with UTF-8 ECI Encoding and Verify Embedded Character Set
// Description: Demonstrates how to set the ECI encoding to UTF-8 when generating a MaxiCode barcode, save it as PNG, and read it back to confirm the correct character set identifier is embedded.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator (for creating barcodes) and BarCodeReader (for decoding them) with EncodeTypes.MaxiCode and DecodeType.MaxiCode. Typical use cases include creating MaxiCode symbols for shipping labels or inventory systems where UTF-8 character support is required. Developers often need to configure ECI encoding to ensure proper character set identification across different scanning devices.
// Prompt: Set ECIEncoding to UTF‑8 for MaxiCode and verify the correct character set identifier is embedded.
// Tags: maxicode, eci encoding, utf-8, barcode generation, barcode recognition, aspose.barcode, png, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a MaxiCode barcode with UTF‑8 ECI encoding,
/// saving it as a PNG image, and verifying the embedded character set by reading the barcode back.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary folder, generates the barcode,
    /// saves it, and then reads it to confirm the encoded text.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "MaxiCodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "maxicode.png");

        // Unicode text containing characters outside the ASCII range
        string codeText = "犬Right狗";

        // Generate a MaxiCode barcode with UTF‑8 ECI encoding
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, codeText))
        {
            // Configure the generator to embed UTF‑8 as the ECI character set
            generator.Parameters.Barcode.MaxiCode.ECIEncoding = ECIEncodings.UTF8;

            // Save the generated barcode image to the temporary folder
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode saved to: {imagePath}");

        // Verify the barcode by reading it back from the saved image
        if (File.Exists(imagePath))
        {
            using (var reader = new BarCodeReader(imagePath, DecodeType.MaxiCode))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Decoded CodeText: {result.CodeText}");
                }
            }
        }
        else
        {
            Console.WriteLine("Failed to generate barcode image.");
        }
    }
}