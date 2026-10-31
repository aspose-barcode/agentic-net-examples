// Title: Encode Unicode characters in DataMatrix barcode with UTF‑8 ECI encoding
// Description: Demonstrates generating a DataMatrix barcode that contains Unicode text using UTF‑8 ECI encoding and then reading it back to verify the decoded value.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, focusing on DataMatrix symbology with ECI support. It showcases the use of BarcodeGenerator, BarcodeParameters, and BarCodeReader classes to create, save, and decode barcodes containing Unicode characters. Developers often need to embed international text in barcodes and verify correct encoding, making this pattern common in multilingual applications.
// Prompt: Encode Unicode characters in DataMatrix barcode using UTF‑8 ECI encoding and verify the output.
// Tags: datamatrix, eci, utf-8, barcode generation, barcode recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a DataMatrix barcode with Unicode text using UTF‑8 ECI encoding,
/// saves it as PNG, and reads it back to verify the decoded content.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, saves it, reads it, and prints the decoded text.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory to store the generated image
        string tempDir = Path.Combine(Path.GetTempPath(), "DataMatrixDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string imagePath = Path.Combine(tempDir, "datamatrix.png");

        // Unicode text to encode in the barcode
        string codeText = "こんにちは世界";

        // Initialize the barcode generator for DataMatrix with the specified text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            // Set visual parameters: module size (X dimension) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 8f;

            // Enable ECI (Extended Channel Interpretation) mode for Unicode support
            generator.Parameters.Barcode.DataMatrix.EncodeMode = DataMatrixEncodeMode.ECI;
            generator.Parameters.Barcode.DataMatrix.ECIEncoding = ECIEncodings.UTF8;

            // Save the generated barcode image to a PNG file
            generator.Save(imagePath, BarCodeImageFormat.Png);

            // Generate the barcode image in memory for immediate recognition
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Initialize a reader configured for DataMatrix decoding
                using (BarCodeReader reader = new BarCodeReader(bitmap, DecodeType.DataMatrix))
                {
                    // Read all barcodes found in the image
                    BarCodeResult[] results = reader.ReadBarCodes();

                    if (results.Length == 0)
                    {
                        Console.WriteLine("No barcode detected.");
                    }
                    else
                    {
                        // Output each decoded result to the console
                        foreach (BarCodeResult result in results)
                        {
                            Console.WriteLine($"Decoded CodeText: {result.CodeText}");
                        }
                    }
                }
            }
        }

        // Optional cleanup: delete the temporary directory and its contents
        // Directory.Delete(tempDir, true);
    }
}