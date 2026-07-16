// Title: Encode Unicode characters in DataMatrix barcode with UTF‑8 ECI
// Description: Demonstrates encoding Unicode text into a DataMatrix barcode using UTF‑8 ECI encoding and verifies the result by decoding the generated image.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating DataMatrix barcodes with specific ECI settings and BarCodeReader for extracting encoded data. Developers often need to handle Unicode content, select appropriate ECI encodings, and validate barcode integrity in automated workflows.
// Prompt: Encode Unicode characters in DataMatrix barcode using UTF‑8 ECI encoding and verify the output.
// Tags: datamatrix, unicode, eci, encoding, generation, recognition, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that creates a DataMatrix barcode containing Unicode characters,
/// applies UTF‑8 ECI encoding, saves the image, and then verifies the content by decoding it.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, saves it, and validates the encoded text.
    /// </summary>
    static void Main()
    {
        // Define the Unicode text to encode and the output image file name.
        string originalText = "犬Right狗";
        string imagePath = "datamatrix.png";

        // --------------------------------------------------------------------
        // Generate a DataMatrix barcode with UTF‑8 ECI encoding.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, originalText))
        {
            // Set the ECI (Extended Channel Interpretation) to UTF‑8.
            generator.Parameters.Barcode.DataMatrix.ECIEncoding = ECIEncodings.UTF8;

            // Save the generated barcode image to the specified path.
            generator.Save(imagePath);
        }

        // --------------------------------------------------------------------
        // Verify that the barcode image was created successfully.
        // --------------------------------------------------------------------
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Barcode image was not created.");
            return;
        }

        // --------------------------------------------------------------------
        // Decode the barcode from the saved image and compare with the original text.
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.DataMatrix))
        {
            bool anyFound = false;

            // Iterate through all detected barcodes (should be only one).
            foreach (var result in reader.ReadBarCodes())
            {
                anyFound = true;
                string decodedText = result.CodeText;
                Console.WriteLine($"Decoded text: {decodedText}");

                // Check if the decoded text matches the original Unicode string.
                if (decodedText == originalText)
                {
                    Console.WriteLine("Verification succeeded: decoded text matches original.");
                }
                else
                {
                    Console.WriteLine("Verification failed: decoded text does not match original.");
                }
            }

            // If no barcodes were detected, inform the user.
            if (!anyFound)
            {
                Console.WriteLine("No DataMatrix barcode detected in the image.");
            }
        }
    }
}