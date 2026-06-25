using System;
using System.IO;
using System.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generation and verification of a Codabar barcode with Mod16 checksum using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a Codabar barcode, saves it as an image, and validates the checksum.
    /// </summary>
    static void Main()
    {
        // -------------------- Prepare barcode data --------------------
        // Data part of the Codabar (without start/stop characters)
        string data = "123456";

        // Start and stop characters required by Codabar specification
        string start = "A";
        string stop = "B";

        // Full codetext including start/stop characters
        string fullCode = start + data + stop;

        // Path where the generated barcode image will be saved
        string outputPath = "codabar_mod16.png";

        // -------------------- Generate barcode with Mod16 checksum --------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, fullCode))
        {
            // Enable checksum generation for the barcode
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Specify that the Mod16 checksum algorithm should be used
            generator.Parameters.Barcode.Codabar.ChecksumMode = CodabarChecksumMode.Mod16;

            // Save the generated barcode image to the specified file
            generator.Save(outputPath);
        }

        // -------------------- Compute expected checksum --------------------
        // Expected checksum: sum of numeric data characters modulo 16
        int sum = data.Sum(ch => ch - '0');
        int expectedChecksumValue = sum % 16;
        string expectedChecksum = expectedChecksumValue.ToString();

        // Verify that the image file was created successfully
        if (!File.Exists(outputPath))
        {
            Console.WriteLine($"Failed to generate barcode image at {outputPath}");
            return;
        }

        // -------------------- Read and validate barcode --------------------
        using (var reader = new BarCodeReader(outputPath, DecodeType.Codabar))
        {
            // Ensure checksum validation is performed during recognition
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            // Read all barcodes found in the image
            var results = reader.ReadBarCodes();

            // If no barcodes were detected, report and exit
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
                return;
            }

            // Iterate through each detected barcode (typically only one)
            foreach (var result in results)
            {
                // Retrieve the checksum reported by the reader
                string detectedChecksum = result.Extended.OneD.CheckSum;

                // Output both detected and expected checksum values
                Console.WriteLine($"Detected checksum: {detectedChecksum}");
                Console.WriteLine($"Expected checksum: {expectedChecksum}");

                // Compare detected checksum with the expected value
                if (detectedChecksum == expectedChecksum)
                {
                    Console.WriteLine("Checksum verification succeeded.");
                }
                else
                {
                    Console.WriteLine("Checksum verification failed.");
                }
            }
        }
    }
}