using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates reading a Swiss Post Parcel barcode from a BMP image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Reads and decodes a Swiss Post Parcel barcode from a specified image file.
    /// </summary>
    static void Main()
    {
        // Path to the BMP image containing the Swiss Post Parcel barcode
        const string imagePath = "SwissPostParcel.bmp";

        // Verify that the image file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Initialize a barcode reader for the Swiss Post Parcel symbology
        using (var reader = new BarCodeReader(imagePath, DecodeType.SwissPostParcel))
        {
            // Enable checksum validation to ensure the checksum is checked/corrected
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            // Perform the barcode recognition
            var results = reader.ReadBarCodes();

            // Iterate through all detected barcodes and output their details
            foreach (var result in results)
            {
                Console.WriteLine($"Barcode Type : {result.CodeTypeName}");
                Console.WriteLine($"Decoded Text : {result.CodeText}");
            }

            // If no barcodes were found, inform the user
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
            }
        }
    }
}