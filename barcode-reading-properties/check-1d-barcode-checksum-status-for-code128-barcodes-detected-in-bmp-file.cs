using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates how to read Code128 barcodes from a BMP image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Reads a BMP image, detects Code128 barcodes, and displays their details.
    /// </summary>
    static void Main()
    {
        // Path to the BMP image containing Code128 barcodes.
        const string imagePath = "barcode.bmp";

        // Verify that the file exists before attempting to read it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Initialize a BarCodeReader that scans only for Code128 barcodes.
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Enable checksum validation during barcode recognition.
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            // Iterate through all detected barcodes in the image.
            foreach (var result in reader.ReadBarCodes())
            {
                // Output the type of the detected barcode (e.g., Code128).
                Console.WriteLine($"Detected barcode type: {result.CodeTypeName}");

                // Output the decoded text of the barcode.
                Console.WriteLine($"Code text: {result.CodeText}");

                // For 1D barcodes, retrieve the checksum value from the extended data.
                // If the checksum string is empty, the checksum could not be determined.
                string checksum = result.Extended.OneD.CheckSum;
                if (!string.IsNullOrEmpty(checksum))
                {
                    Console.WriteLine($"Checksum: {checksum} (valid)");
                }
                else
                {
                    Console.WriteLine("Checksum: not available or invalid");
                }

                Console.WriteLine(); // Blank line between results for readability.
            }
        }
    }
}