// Title: Code128 checksum verification in BMP image
// Description: Demonstrates how to read Code128 barcodes from a BMP file and display their checksum status.
// Prompt: Check 1D barcode checksum status for Code128 barcodes detected in a BMP file.
// Tags: code128, checksum, barcode, bmp, aspose.barcode, console

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that reads Code128 barcodes from a BMP image and reports checksum information.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Loads the image, reads barcodes, and prints type, text, and checksum status.
    /// </summary>
    static void Main()
    {
        // Path to the BMP image containing barcodes
        string imagePath = "barcode.bmp";

        // Verify that the file exists before attempting to load it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Load the image as a bitmap (ensures proper disposal with using)
        using (Bitmap bitmap = new Bitmap(imagePath))
        {
            // Initialize the barcode reader for Code128 symbology
            using (BarCodeReader reader = new BarCodeReader(bitmap, DecodeType.Code128))
            {
                // Enable checksum validation (optional, ensures checksum is checked)
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                // Iterate through all detected barcodes in the image
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    // Output the detected barcode type (e.g., Code128)
                    Console.WriteLine($"Detected Barcode Type: {result.CodeTypeName}");

                    // Output the decoded text of the barcode
                    Console.WriteLine($"Code Text: {result.CodeText}");

                    // Retrieve checksum from extended parameters (if available)
                    string checksum = result.Extended.OneD?.CheckSum;
                    if (!string.IsNullOrEmpty(checksum))
                    {
                        Console.WriteLine($"Checksum: {checksum}");
                    }
                    else
                    {
                        Console.WriteLine("Checksum: not available");
                    }

                    // Add a blank line for readability between results
                    Console.WriteLine();
                }
            }
        }
    }
}