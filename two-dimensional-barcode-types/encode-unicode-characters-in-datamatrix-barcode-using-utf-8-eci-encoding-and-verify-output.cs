// Title: Encode Unicode characters in DataMatrix barcode using UTF‑8 ECI and verify the result
// Description: Demonstrates how to generate a DataMatrix barcode that contains Unicode text, apply UTF‑8 ECI encoding, save it as PNG, and then read it back to confirm the encoded data matches the original.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating DataMatrix symbols with ECI (Extended Channel Interpretation) support, and BarCodeReader for decoding. Developers working with internationalized data, QR/DataMatrix symbologies, or needing reliable round‑trip verification will find these APIs essential for handling Unicode content in barcodes.
// Prompt: Encode Unicode characters in DataMatrix barcode using UTF‑8 ECI encoding and verify the output.
// Tags: datamatrix, eci, utf-8, barcode, generation, recognition, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Provides a simple console demonstration of generating a DataMatrix barcode with Unicode text,
/// applying UTF‑8 ECI encoding, saving the image, and verifying the content by reading it back.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes barcode creation, saving, reading, and cleanup.
    /// </summary>
    static void Main()
    {
        // Define the Unicode text to encode.
        string unicodeText = "Aspose常に先を行";

        // Create a unique temporary folder for the generated image.
        string tempFolder = Path.Combine(Path.GetTempPath(), "DataMatrixDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string outputFile = Path.Combine(tempFolder, "datamatrix.png");

        // ------------------------------------------------------------
        // Generate a DataMatrix barcode with UTF‑8 ECI encoding.
        // ------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DataMatrix, unicodeText))
        {
            // Set the module size (pixel dimension) for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 8f;

            // Enable ECI encoding mode and specify UTF‑8 as the character set.
            generator.Parameters.Barcode.DataMatrix.EncodeMode = DataMatrixEncodeMode.ECI;
            generator.Parameters.Barcode.DataMatrix.ECIEncoding = ECIEncodings.UTF8;

            // Save the barcode image as PNG.
            generator.Save(outputFile, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Verify the barcode by decoding it back to text.
        // ------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DataMatrix, unicodeText))
        {
            // Generate the barcode image in memory and pass it to the reader.
            using (BarCodeReader reader = new BarCodeReader(generator.GenerateBarCodeImage(), DecodeType.DataMatrix))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Decoded CodeText: {result.CodeText}");
                    if (result.CodeText == unicodeText)
                    {
                        Console.WriteLine("Verification succeeded: decoded text matches original.");
                    }
                    else
                    {
                        Console.WriteLine("Verification failed: decoded text does not match original.");
                    }
                }
            }
        }

        // ------------------------------------------------------------
        // Clean up temporary files (optional).
        // ------------------------------------------------------------
        try
        {
            if (File.Exists(outputFile))
                File.Delete(outputFile);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder);
        }
        catch
        {
            // Suppress any exceptions during cleanup to avoid breaking the demo.
        }
    }
}