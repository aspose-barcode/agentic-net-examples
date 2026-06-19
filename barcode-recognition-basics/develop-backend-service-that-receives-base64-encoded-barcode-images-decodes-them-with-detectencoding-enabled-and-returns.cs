using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a barcode, converting it to a Base64 string,
/// decoding it back to an image, and reading the barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a Code128 barcode, encodes it to Base64,
    /// decodes it, and reads the barcode.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Step 1: Generate a sample barcode image (Code128) and obtain its Base64 representation.
        // ------------------------------------------------------------
        string base64Image;
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "HelloWorld"))
        {
            // Create an in‑memory stream to hold the PNG image.
            using (var ms = new MemoryStream())
            {
                // Save the generated barcode to the memory stream in PNG format.
                generator.Save(ms, BarCodeImageFormat.Png);

                // Convert the stream contents to a byte array.
                byte[] imageBytes = ms.ToArray();

                // Encode the byte array as a Base64 string.
                base64Image = Convert.ToBase64String(imageBytes);
            }
        }

        // ------------------------------------------------------------
        // Step 2: Decode the Base64 string back to image bytes.
        // ------------------------------------------------------------
        byte[] decodedBytes = Convert.FromBase64String(base64Image);

        // ------------------------------------------------------------
        // Step 3: Use BarCodeReader with DetectEncoding enabled to read the barcode.
        // ------------------------------------------------------------
        using (var imageStream = new MemoryStream(decodedBytes))
        {
            // Initialize the reader to detect all supported barcode types.
            using (var reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
            {
                // Enable automatic detection of text encoding for the barcode.
                reader.BarcodeSettings.DetectEncoding = true;

                // Read all barcodes found in the image.
                var results = reader.ReadBarCodes();

                // Output decoded text for each barcode.
                foreach (var result in results)
                {
                    Console.WriteLine("Decoded Text: " + result.CodeText);
                }
            }
        }
    }
}