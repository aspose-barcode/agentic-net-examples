// Title: Encode Unicode characters in DataMatrix barcode with UTF‑8 ECI and verify decoding
// Description: Demonstrates generating a DataMatrix barcode that contains Unicode characters using UTF‑8 ECI encoding, saving it as PNG, and reading it back to confirm the encoded text matches the original.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator with DataMatrix symbology, configure ECI encoding to UTF‑8, and employ BarCodeReader for decoding. Developers working with international text, multi‑script barcodes, or needing reliable round‑trip verification can use these APIs to ensure correct encoding and decoding of Unicode data.
// Prompt: Encode Unicode characters in DataMatrix barcode using UTF‑8 ECI encoding and verify the output.
// Tags: datamatrix,unicode,utf-8,eci,barcode generation,barcode recognition,csharp,aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates encoding Unicode text into a DataMatrix barcode using UTF‑8 ECI encoding,
/// saving the image, and verifying the decoded result matches the original text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, reads it back, and cleans up.
    /// </summary>
    static void Main()
    {
        // Sample Unicode text containing characters from different scripts
        string originalText = "犬Right狗 🌟";

        // Path for the generated barcode image (temporary folder)
        string imagePath = Path.Combine(Path.GetTempPath(), "datamatrix_utf8.png");

        // Generate DataMatrix barcode with UTF‑8 ECI encoding
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, originalText))
        {
            // Set DataMatrix to use ECI mode and specify UTF‑8 as the encoding
            generator.Parameters.Barcode.DataMatrix.EncodeMode = DataMatrixEncodeMode.ECI;
            generator.Parameters.Barcode.DataMatrix.ECIEncoding = ECIEncodings.UTF8;

            // Save the barcode image as PNG
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image file was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read and decode the DataMatrix barcode from the saved image
        using (var reader = new BarCodeReader(imagePath, DecodeType.DataMatrix))
        {
            // Enable automatic detection of the encoding used in the barcode
            reader.BarcodeSettings.DetectEncoding = true;

            bool found = false;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine("Decoded Text: " + result.CodeText);
                if (result.CodeText == originalText)
                {
                    Console.WriteLine("Verification succeeded: decoded text matches original.");
                    found = true;
                }
                else
                {
                    Console.WriteLine("Verification failed: decoded text does not match original.");
                }
            }

            if (!found)
            {
                Console.WriteLine("No DataMatrix barcode was detected in the image.");
            }
        }

        // Clean up the temporary image file
        try
        {
            File.Delete(imagePath);
        }
        catch
        {
            // Ignored – file may be in use or deletion may fail on some platforms
        }
    }
}