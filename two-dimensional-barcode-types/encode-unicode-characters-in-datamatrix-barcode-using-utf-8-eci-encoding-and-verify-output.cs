// Title: Encode Unicode characters in DataMatrix barcode with UTF-8 ECI
// Description: Demonstrates generating a DataMatrix barcode containing Unicode characters using UTF‑8 ECI encoding and then reading it back to verify the content.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator with EncodeTypes.DataMatrix, configure ECI encoding via DataMatrixEncodeMode.ECI and ECIEncodings.UTF8, and then employ BarCodeReader to decode the image. Developers working with international text, QR/DataMatrix symbologies, or needing reliable round‑trip verification will find these APIs useful.
// Prompt: Encode Unicode characters in DataMatrix barcode using UTF‑8 ECI encoding and verify the output.
// Tags: datamatrix, eci, utf-8, unicode, barcode generation, barcode recognition, aspnet, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates a DataMatrix barcode with Unicode characters using UTF‑8 ECI encoding,
/// saves it as a PNG image, and then reads the image back to verify the encoded text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode creation, saving, and verification.
    /// </summary>
    static void Main()
    {
        // Define the Unicode text to encode, including emoji and CJK characters.
        string text = "Unicode 🚀 漢字";

        // Create a unique temporary folder to store the generated barcode image.
        string tempFolder = Path.Combine(Path.GetTempPath(), "DataMatrixDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "datamatrix.png");

        // Generate the DataMatrix barcode with UTF‑8 ECI encoding.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, text))
        {
            // Set the module size (pixel dimension) for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 8f;

            // Enable ECI encoding mode for DataMatrix.
            generator.Parameters.Barcode.DataMatrix.EncodeMode = DataMatrixEncodeMode.ECI;

            // Specify UTF‑8 as the ECI encoding.
            generator.Parameters.Barcode.DataMatrix.ECIEncoding = ECIEncodings.UTF8;

            // Save the barcode image as PNG.
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Read the saved barcode image and verify that the decoded text matches the original.
        using (var reader = new BarCodeReader(imagePath, DecodeType.DataMatrix))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Decoded CodeText: {result.CodeText}");
                if (result.CodeText == text)
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
}