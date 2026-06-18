using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a Code39 barcode without a checksum and verifying it.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a Code39 barcode with checksum disabled, saves it to a file,
    /// then reads the barcode back to verify that the checksum digit is absent.
    /// </summary>
    static void Main()
    {
        // Sample Code39 data without checksum
        const string originalCodeText = "ABC123";

        // Path for the generated barcode image
        const string imagePath = "code39.png";

        // Generate Code39 barcode with checksum disabled and hidden
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, originalCodeText))
        {
            // Disable checksum generation
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;
            // Ensure checksum digit is not shown in human‑readable text
            generator.Parameters.Barcode.ChecksumAlwaysShow = false;

            // Save the barcode image to the specified path
            generator.Save(imagePath);
        }

        // Verify the barcode by reading it back from the saved image
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code39))
        {
            // Disable checksum validation to avoid false failures
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Off;

            // Iterate through all recognized barcodes (should be only one)
            foreach (var result in reader.ReadBarCodes())
            {
                // Output the recognized text
                Console.WriteLine($"Recognized CodeText: {result.CodeText}");

                // Compare with the original text to confirm checksum absence
                if (result.CodeText == originalCodeText)
                {
                    Console.WriteLine("Verification succeeded: checksum digit is absent.");
                }
                else
                {
                    Console.WriteLine("Verification failed: unexpected CodeText.");
                }
            }
        }
    }
}