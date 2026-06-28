using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generation and recognition of a Postnet barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a Postnet barcode, saves it to a memory stream, and then reads it back
    /// while validating the checksum.
    /// </summary>
    static void Main()
    {
        // Sample Postnet code (ZIP+4 + checksum). Example: "12345678"
        const string postnetCode = "12345678";

        // Create a memory stream to hold the generated barcode image
        using (var ms = new MemoryStream())
        {
            // Initialize the barcode generator for Postnet encoding with the sample code
            using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, postnetCode))
            {
                // Save the generated barcode as a PNG image into the memory stream
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Reset the stream position to the beginning so it can be read
            ms.Position = 0;

            // Initialize a barcode reader to decode Postnet barcodes from the memory stream
            using (var reader = new BarCodeReader(ms, DecodeType.Postnet))
            {
                // Enable checksum validation during the recognition process
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                // Perform the barcode recognition
                var results = reader.ReadBarCodes();

                // If no barcodes were detected, inform the user
                if (results.Length == 0)
                {
                    Console.WriteLine("No Postnet barcode detected.");
                }
                else
                {
                    // Iterate through each detected barcode result
                    foreach (var result in results)
                    {
                        // Output the decoded text of the barcode
                        Console.WriteLine($"Detected CodeText: {result.CodeText}");

                        // For 1D barcodes the checksum value is available in the extended parameters (OneD.CheckSum)
                        // If the checksum is invalid, the result will be empty or null.
                        var checksum = result.Extended?.OneD?.CheckSum;
                        if (!string.IsNullOrEmpty(checksum))
                        {
                            Console.WriteLine($"Checksum from barcode: {checksum}");
                            Console.WriteLine("Checksum validation: SUCCESS");
                        }
                        else
                        {
                            Console.WriteLine("Checksum validation: FAILED or not applicable");
                        }
                    }
                }
            }
        }
    }
}