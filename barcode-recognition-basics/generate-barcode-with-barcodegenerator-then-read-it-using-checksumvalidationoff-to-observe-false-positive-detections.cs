using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code39 barcode, then reading it with checksum validation
/// turned off and on using Aspose.BarCode library.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode image, then reads it twice to show the effect of checksum validation.
    /// </summary>
    static void Main()
    {
        const string imagePath = "barcode.png";

        // ------------------------------------------------------------
        // Generate a Code39 barcode and save it to a file.
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, "ABC123"))
        {
            // No special checksum settings; Code39 checksum is optional.
            generator.Save(imagePath);
        }

        // ------------------------------------------------------------
        // Read the barcode with checksum validation turned OFF.
        // This allows the reader to return results even if the checksum is missing or incorrect.
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code39))
        {
            // Disable checksum validation to allow possible false positives.
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Off;

            // Iterate through all detected barcodes.
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine("Read with ChecksumValidation.Off");
                Console.WriteLine($"Code Text : {result.CodeText}");
                // For Code39, checksum may be empty; display if available.
                Console.WriteLine($"Checksum  : {result.Extended.OneD.CheckSum}");
                Console.WriteLine($"Confidence: {result.Confidence}");
                Console.WriteLine();
            }
        }

        // ------------------------------------------------------------
        // For comparison, read the same barcode with checksum validation ON.
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code39))
        {
            // Enable checksum validation; only barcodes with a valid checksum will be returned.
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            // Iterate through all detected barcodes.
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine("Read with ChecksumValidation.On");
                Console.WriteLine($"Code Text : {result.CodeText}");
                Console.WriteLine($"Checksum  : {result.Extended.OneD.CheckSum}");
                Console.WriteLine($"Confidence: {result.Confidence}");
                Console.WriteLine();
            }
        }
    }
}