// Title: Read Code 11 barcode with checksum validation
// Description: Demonstrates reading a Code 11 barcode image while enforcing checksum verification using Aspose.BarCode.
// Prompt: Read a single Code 11 barcode image after enabling obligatory checksum verification with ChecksumValidation.On.
// Tags: code11, read, checksum, console, barcodereader, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates (if needed) and reads a Code 11 barcode image
/// with checksum validation enabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode image if missing, then reads it with checksum verification.
    /// </summary>
    static void Main()
    {
        // Define the path for the sample barcode image
        const string imagePath = "code11.png";

        // ------------------------------------------------------------
        // Generate a Code 11 barcode image if it does not already exist
        // ------------------------------------------------------------
        if (!File.Exists(imagePath))
        {
            // Create a barcode generator for Code 11 with sample data
            using (var generator = new BarcodeGenerator(EncodeTypes.Code11, "1234567890"))
            {
                // Save the generated barcode to the specified file
                generator.Save(imagePath);
            }
        }

        // ------------------------------------------------------------
        // Verify that the image now exists before attempting to read it
        // ------------------------------------------------------------
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // ------------------------------------------------------------
        // Read the barcode with checksum validation enabled
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code11))
        {
            // Enable obligatory checksum verification
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            // Iterate through all detected barcodes (should be only one)
            foreach (var result in reader.ReadBarCodes())
            {
                // Output the raw decoded text
                Console.WriteLine($"CodeText: {result.CodeText}");

                // Output checksum and value without checksum (OneD extended info)
                Console.WriteLine($"Checksum: {result.Extended.OneD.CheckSum}");
                Console.WriteLine($"Value (without checksum): {result.Extended.OneD.Value}");
            }
        }
    }
}