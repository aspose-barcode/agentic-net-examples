using System;
using System.IO;
using System.Text;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode;

/// <summary>
/// Demonstrates encoding and decoding binary data using DotCode barcode in Binary mode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a DotCode barcode from binary data, reads it back, and verifies integrity.
    /// </summary>
    static void Main()
    {
        // Sample binary data to encode
        byte[] originalData = new byte[] { 0x01, 0x02, 0xFF, 0x00, 0xAB, 0x7E, 0x20 };

        // Path for the generated barcode image (temporary folder)
        string imagePath = Path.Combine(Path.GetTempPath(), "dotcode_binary.png");

        // -------------------------------------------------
        // Generate DotCode barcode in Binary mode
        // -------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.DotCode))
        {
            // Set raw byte data as the code text (binary payload)
            generator.SetCodeText(originalData);

            // Configure the barcode to use Binary encoding mode
            generator.Parameters.Barcode.DotCode.DotCodeEncodeMode = DotCodeEncodeMode.Binary;

            // Save the generated barcode image to the temporary path
            generator.Save(imagePath);
        }

        // -------------------------------------------------
        // Verify the barcode by reading it back
        // -------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.DotCode))
        {
            // Read all barcodes found in the image
            var results = reader.ReadBarCodes();

            // If no barcode is detected, report and exit
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
                return;
            }

            // Assume the first result corresponds to the barcode we generated
            var decodedText = results[0].CodeText ?? string.Empty;

            // Convert the decoded string back to bytes using ISO-8859-1 encoding
            // (ISO-8859-1 provides a one-to-one mapping for byte values 0‑255)
            byte[] decodedData = Encoding.GetEncoding("ISO-8859-1").GetBytes(decodedText);

            // Compare original and decoded byte arrays for equality
            bool isEqual = originalData.Length == decodedData.Length;
            if (isEqual)
            {
                for (int i = 0; i < originalData.Length; i++)
                {
                    if (originalData[i] != decodedData[i])
                    {
                        isEqual = false;
                        break;
                    }
                }
            }

            // Output verification result
            Console.WriteLine(isEqual
                ? "Success: Decoded data matches original binary data."
                : "Failure: Decoded data does not match original binary data.");
        }

        // -------------------------------------------------
        // Clean up the temporary image file
        // -------------------------------------------------
        if (File.Exists(imagePath))
        {
            try
            {
                File.Delete(imagePath);
            }
            catch
            {
                // Ignore any cleanup errors (e.g., file in use)
            }
        }
    }
}