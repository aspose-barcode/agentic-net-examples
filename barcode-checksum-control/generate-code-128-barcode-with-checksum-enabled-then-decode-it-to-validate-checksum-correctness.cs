using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a Code128 barcode with checksum enabled,
/// saving it to a file, and then reading it back while validating the checksum.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode image, then reads and validates it.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string imagePath = "code128.png";

        // ------------------------------------------------------------
        // Generate a Code128 barcode with checksum enabled.
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Enable checksum (required for Code128) and display it in the human‑readable text.
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            generator.Parameters.Barcode.ChecksumAlwaysShow = true;

            // Save the generated barcode image to the specified file.
            generator.Save(imagePath);
        }

        // ------------------------------------------------------------
        // Decode the generated barcode and validate its checksum.
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Instruct the reader to perform checksum validation during recognition.
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            // Iterate through all detected barcodes (there should be only one in this case).
            foreach (var result in reader.ReadBarCodes())
            {
                // Output the decoded text to the console.
                Console.WriteLine($"Decoded Text: {result.CodeText}");

                // For Code128 the checksum is always validated; additional parameters can be inspected if needed.
            }
        }
    }
}