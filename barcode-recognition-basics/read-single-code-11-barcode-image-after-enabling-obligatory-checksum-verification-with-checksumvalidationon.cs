// Title: Read Code 11 barcode with checksum validation
// Description: Demonstrates reading a Code 11 barcode image while enforcing checksum verification using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition and generation category. It showcases the use of BarcodeGenerator to create a Code 11 image and BarCodeReader with BarcodeSettings to validate mandatory checksums. Developers commonly need to generate barcodes for testing and then read them with strict checksum enforcement to ensure data integrity in logistics, inventory, and manufacturing systems.
// Prompt: Read a single Code 11 barcode image after enabling obligatory checksum verification with ChecksumValidation.On.
// Tags: code11, barcode, read, checksum, aspose.barcode, generation, recognition

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates (if needed) and reads a Code 11 barcode image
/// with mandatory checksum validation enabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code 11 barcode image if missing, then reads it
    /// while enforcing checksum verification.
    /// </summary>
    static void Main()
    {
        // Path to the barcode image file
        string imagePath = "code11.png";

        // Generate a Code 11 barcode image if it does not already exist
        if (!File.Exists(imagePath))
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code11, "1234567890"))
            {
                // Save the generated barcode to a PNG file
                generator.Save(imagePath);
                Console.WriteLine($"Generated barcode image: {imagePath}");
            }
        }

        // Ensure the image file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: Barcode image file not found at '{imagePath}'.");
            return;
        }

        // Create a BarCodeReader for Code 11 with checksum validation enabled
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code11))
        {
            // Enable obligatory checksum verification
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            // Iterate through all detected barcodes in the image
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine("Detected Code 11 barcode:");
                Console.WriteLine($"  CodeText: {result.CodeText}");

                // If extended OneD data is available, display the checksum value
                if (result.Extended?.OneD != null)
                {
                    Console.WriteLine($"  CheckSum: {result.Extended.OneD.CheckSum}");
                }
            }
        }
    }
}