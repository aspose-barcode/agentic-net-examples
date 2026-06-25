using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation and recognition of a Swiss Post Parcel barcode,
/// including automatic checksum correction.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode from an incorrect code, lets the library auto‑correct it,
    /// then reads the barcode back and verifies the correction.
    /// </summary>
    static void Main()
    {
        // Sample incorrect code text for Swiss Post Parcel (checksum will be auto‑corrected)
        string incorrectCode = "1234567890123456789012"; // length may not match spec; generator will adjust

        // Create barcode generator for Swiss Post Parcel symbology
        BaseEncodeType symbology = EncodeTypes.SwissPostParcel;
        using (var generator = new BarcodeGenerator(symbology, incorrectCode))
        {
            // Disable exception on incorrect code to allow auto‑correction
            generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false;

            // Save generated barcode image to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading

                // Retrieve the corrected code text after generation
                string correctedCode = generator.CodeText;
                Console.WriteLine($"Corrected CodeText after generation: {correctedCode}");

                // Initialize barcode reader for Swiss Post Parcel from the memory stream
                using (var reader = new BarCodeReader(ms, DecodeType.SwissPostParcel))
                {
                    // Enable checksum validation during recognition
                    reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                    // Read all barcodes found in the image
                    var results = reader.ReadBarCodes();
                    if (results.Length == 0)
                    {
                        Console.WriteLine("No barcode detected.");
                        return;
                    }

                    // Use the first detected barcode result
                    var result = results[0];
                    Console.WriteLine($"Recognized CodeText: {result.CodeText}");

                    // Compare recognized code with the corrected code to verify auto‑correction
                    if (string.Equals(correctedCode, result.CodeText, StringComparison.Ordinal))
                    {
                        Console.WriteLine("Checksum auto‑correction verified successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Checksum auto‑correction verification failed.");
                    }
                }
            }
        }
    }
}