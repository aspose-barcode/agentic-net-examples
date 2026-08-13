// Title: Decode Swiss Post Parcel barcode from BMP and verify checksum
// Description: Demonstrates generating a Swiss Post Parcel international barcode, saving it as a BMP image, decoding it with checksum validation, and confirming any checksum correction.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating SwissPostParcel barcodes, BarCodeReader for decoding, and the ChecksumValidation feature to ensure data integrity. Developers working with postal symbologies often need to generate barcodes, read them from images, and validate checksums, making this pattern common in logistics and mailing applications.
// Prompt: Decode a Swiss Post Parcel international barcode from a BMP image and verify checksum correction.
// Tags: swisspostparcel, barcode, generation, recognition, checksum, bmp, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating, saving, decoding, and checksum validation of a Swiss Post Parcel barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, decodes it with checksum validation, and reports results.
    /// </summary>
    static void Main()
    {
        // Define a temporary file path for the generated BMP image
        string imagePath = Path.Combine(Path.GetTempPath(), "SwissPostParcel.bmp");

        // Sample code text for a Swiss Post Parcel (international) barcode
        string originalCodeText = "1234567890123";

        // Generate a Swiss Post Parcel barcode and save it as a BMP file
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, originalCodeText))
        {
            // Persist the barcode image to the temporary location
            generator.Save(imagePath, BarCodeImageFormat.Bmp);
        }

        // Verify that the image file was successfully created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create the barcode image.");
            return;
        }

        // Decode the barcode from the BMP image with checksum validation enabled
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.SwissPostParcel))
        {
            // Force checksum validation; Aspose.BarCode will correct the code text if needed
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            // Iterate through all detected barcodes in the image
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Decoded CodeText: {result.CodeText}");

                // Compare the decoded text with the original to determine if correction occurred
                if (result.CodeText == originalCodeText)
                {
                    Console.WriteLine("Checksum is valid (no correction needed).");
                }
                else
                {
                    Console.WriteLine($"Checksum corrected. Original: {originalCodeText}, Corrected: {result.CodeText}");
                }

                // If extended data is available, display the value without checksum and the checksum itself
                if (result.Extended?.OneD != null)
                {
                    Console.WriteLine($"Extracted Value (without checksum): {result.Extended.OneD.Value}");
                    Console.WriteLine($"Extracted Checksum: {result.Extended.OneD.CheckSum}");
                }
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