using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    // Returns the computed checksum character for a Code 39 string.
    // If the checksum cannot be determined, throws an exception.
    static char ComputeCode39Checksum(string codeText)
    {
        if (string.IsNullOrEmpty(codeText))
            throw new ArgumentException("Code text must not be null or empty.", nameof(codeText));

        // Enable checksum calculation during generation.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, codeText))
        {
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Render to a memory stream (no file I/O required).
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0;

                // Read the barcode back to obtain the checksum value.
                using (var reader = new BarCodeReader(ms, DecodeType.Code39))
                {
                    // Ensure checksum validation is performed.
                    reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        // The checksum for 1D barcodes is provided as a string.
                        string checksum = result.Extended.OneD.CheckSum;
                        if (!string.IsNullOrEmpty(checksum) && checksum.Length == 1)
                            return checksum[0];
                        else
                            throw new InvalidOperationException("Checksum could not be retrieved.");
                    }

                    throw new InvalidOperationException("No barcode detected in the generated image.");
                }
            }
        }
    }

    static void Main()
    {
        // Example usage.
        string input = "HELLO";
        try
        {
            char checksumChar = ComputeCode39Checksum(input);
            Console.WriteLine($"Input: {input}");
            Console.WriteLine($"Computed Code 39 checksum character: {checksumChar}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}