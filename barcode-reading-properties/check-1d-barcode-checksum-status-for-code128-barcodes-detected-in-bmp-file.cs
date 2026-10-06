// Title: Check 1D barcode checksum status for Code128 barcodes in a BMP image
// Description: Demonstrates how to read a BMP file, detect Code128 barcodes, and retrieve checksum information using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating the use of BarCodeReader, DecodeType, and checksum validation settings. Developers commonly need to verify barcode integrity, extract extended OneD parameters, and handle missing data when processing scanned images.
// Prompt: Check 1D barcode checksum status for Code128 barcodes detected in a BMP file.
// Tags: code128, checksum, oned, barcode, recognition, bmp, aspose.barcode, decode

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that reads a BMP image, detects Code128 barcodes,
/// and outputs checksum validation details.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Accepts an optional image path argument, validates the file,
    /// configures checksum validation, and prints barcode type, text, and checksum data.
    /// </summary>
    static void Main(string[] args)
    {
        // Determine image path: use first argument if provided, otherwise default to "code128.bmp"
        string imagePath = args.Length > 0 ? args[0] : "code128.bmp";

        // Verify that the specified file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Initialize the barcode reader for Code128 symbology on the given image
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Enable checksum verification for symbologies that require it
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            // Iterate through all detected barcodes in the image
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Output basic barcode information
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");

                // Access extended OneD parameters to retrieve checksum details, if available
                if (result.Extended != null && result.Extended.OneD != null)
                {
                    Console.WriteLine($"Value: {result.Extended.OneD.Value}");
                    Console.WriteLine($"CheckSum: {result.Extended.OneD.CheckSum}");
                }
                else
                {
                    // Inform the user when extended OneD data is not present
                    Console.WriteLine("OneD extended data not available.");
                }

                Console.WriteLine(); // Separate entries for readability
            }
        }
    }
}