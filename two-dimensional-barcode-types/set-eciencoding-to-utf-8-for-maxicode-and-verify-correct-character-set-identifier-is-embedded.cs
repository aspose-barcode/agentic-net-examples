// Title: Set ECI Encoding to UTF-8 for MaxiCode and verify embedded character set
// Description: Demonstrates generating a MaxiCode barcode with UTF‑8 ECI encoding and reading it back to confirm the correct character set identifier is embedded.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on ECI (Extended Channel Interpretation) handling for Unicode data. It shows how to configure the MaxiCode symbology using the BarcodeGenerator class, set the EncodeMode to ECI, specify the ECIEncodings.UTF8 value, and then validate the result with BarCodeReader. Developers working with international text, multi‑language barcodes, or needing precise character set control will find this pattern useful.
// Prompt: Set ECIEncoding to UTF‑8 for MaxiCode and verify the correct character set identifier is embedded.
// Tags: maxicode, eci, utf-8, barcode generation, barcode recognition, aspnet, aspose.barcode, unicode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a MaxiCode barcode with UTF‑8 ECI encoding and verifying it by reading the barcode back.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, reads it back, and validates the encoded text.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary folder and file path for the barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "MaxiCodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "maxicode.png");

        // Unicode text containing characters outside the ASCII range
        string codeText = "犬Right狗";

        // Generate a MaxiCode barcode using ECI mode with UTF‑8 encoding
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, codeText))
        {
            // Set the MaxiCode specific encode mode to ECI (Extended Channel Interpretation)
            generator.Parameters.Barcode.MaxiCode.EncodeMode = MaxiCodeEncodeMode.ECI;
            // Specify UTF‑8 as the character set for the ECI segment
            generator.Parameters.Barcode.MaxiCode.ECIEncoding = ECIEncodings.UTF8;
            // Save the generated barcode as a PNG image
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to generate barcode image.");
            return;
        }

        // Read the barcode back and compare the decoded text with the original
        using (var reader = new BarCodeReader(barcodePath, DecodeType.MaxiCode))
        {
            bool found = false;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Decoded CodeText: {result.CodeText}");
                if (result.CodeText == codeText)
                {
                    Console.WriteLine("Verification succeeded: decoded text matches original.");
                    found = true;
                }
            }
            if (!found)
            {
                Console.WriteLine("Verification failed: decoded text does not match original.");
            }
        }

        // Clean up temporary files (optional)
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore any errors that occur during cleanup
        }
    }
}