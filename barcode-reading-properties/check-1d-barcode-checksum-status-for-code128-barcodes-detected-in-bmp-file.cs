// Title: Check 1D barcode checksum status for Code128 barcodes in BMP
// Description: Demonstrates how to read a Code128 barcode from a BMP image and retrieve its checksum value using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating the use of BarCodeReader with DecodeType.Code128 and enabling ChecksumValidation. Developers often need to verify checksum status for 1D symbologies such as Code128 when processing scanned images, ensuring data integrity in inventory, shipping, or point‑of‑sale applications.
// Prompt: Check 1D barcode checksum status for Code128 barcodes detected in a BMP file.
// Tags: code128, checksum, barcode recognition, bmp, aspose.barcode, 1d

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates (if needed) a BMP image containing a Code128 barcode,
/// reads the barcode using Aspose.BarCode, and displays its checksum status.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        const string imagePath = "code128.bmp";

        // Ensure a sample BMP exists; generate one if missing.
        if (!File.Exists(imagePath))
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ABC123"))
            {
                // Save the generated barcode as a BMP image.
                generator.Save(imagePath, BarCodeImageFormat.Bmp);
                Console.WriteLine($"Generated sample barcode image: {imagePath}");
            }
        }

        // Verify the file exists before attempting to read it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: File not found - {imagePath}");
            return;
        }

        // Open the BMP file and read Code128 barcodes.
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Enable checksum validation so the checksum value is evaluated.
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            // Iterate through all detected barcodes.
            foreach (var result in reader.ReadBarCodes())
            {
                // Output basic barcode information.
                Console.WriteLine($"Detected Barcode Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text: {result.CodeText}");

                // Retrieve the checksum for 1D barcodes (if available).
                string checksum = result.Extended?.OneD?.CheckSum;
                if (!string.IsNullOrEmpty(checksum))
                {
                    Console.WriteLine($"Checksum: {checksum}");
                }
                else
                {
                    Console.WriteLine("Checksum: not available for this barcode.");
                }

                Console.WriteLine(); // Blank line between results.
            }
        }
    }
}