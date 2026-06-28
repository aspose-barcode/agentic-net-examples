using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a DataMatrix barcode with Unicode (Japanese + emoji) content,
/// saving it as a PNG image, and then reading it back to verify the encoded text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, saves it, and verifies it by decoding.
    /// </summary>
    static void Main()
    {
        // Unicode text to encode (Japanese greeting + Earth emoji)
        string unicodeText = "こんにちは世界 🌍";

        // Destination file path for the generated barcode image
        string imagePath = "datamatrix.png";

        // ------------------------------------------------------------
        // Generate a DataMatrix barcode with UTF-8 ECI encoding
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, unicodeText))
        {
            // Specify that the barcode should use UTF-8 encoding (ECI)
            generator.Parameters.Barcode.DataMatrix.ECIEncoding = ECIEncodings.UTF8;

            // Save the generated barcode as a PNG file
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Verify that the barcode image was created successfully
        // ------------------------------------------------------------
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: Barcode image not found at '{imagePath}'.");
            return;
        }

        // ------------------------------------------------------------
        // Read and decode the barcode from the saved image
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.DataMatrix))
        {
            // Attempt to read all barcodes present in the image
            var results = reader.ReadBarCodes();

            // If no barcodes were detected, inform the user
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
                return;
            }

            // Iterate through each detected barcode (should be only one in this case)
            foreach (var result in results)
            {
                // Output the decoded text
                Console.WriteLine($"Decoded CodeText: {result.CodeText}");

                // Compare the decoded text with the original Unicode string
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
}