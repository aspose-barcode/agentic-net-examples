using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating an EAN13 barcode, saving it to memory and a temporary file,
/// then reading it back with checksum validation using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, reads it from a stream and a file, and compares the results.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // 1. Prepare a valid EAN13 code (13 digits, includes checksum)
        // --------------------------------------------------------------------
        string ean13Code = "1234567890128"; // valid EAN13 with checksum

        // --------------------------------------------------------------------
        // 2. Generate the barcode image and store it in both a MemoryStream
        //    and a temporary file for later reading.
        // --------------------------------------------------------------------
        byte[] imageBytes;
        string tempFilePath = Path.Combine(Path.GetTempPath(), "sample_ean13.png");

        using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, ean13Code))
        {
            // Save the barcode to a MemoryStream
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                imageBytes = ms.ToArray(); // capture the byte array for later use
            }

            // Also save the barcode to a temporary file (file‑based read)
            generator.Save(tempFilePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // 3. Local function: read barcode from a byte array (MemoryStream) with
        //    checksum validation enabled.
        // --------------------------------------------------------------------
        string ReadFromStream(byte[] bytes)
        {
            using (var ms = new MemoryStream(bytes))
            using (var reader = new BarCodeReader(ms, DecodeType.EAN13))
            {
                // Enable checksum validation to ensure the code is correct
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                // Return the first decoded barcode text, or null if none found
                foreach (var result in reader.ReadBarCodes())
                {
                    return result.CodeText;
                }

                return null;
            }
        }

        // --------------------------------------------------------------------
        // 4. Local function: read barcode from a file with checksum validation.
        // --------------------------------------------------------------------
        string ReadFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                return null;
            }

            using (var reader = new BarCodeReader(filePath, DecodeType.EAN13))
            {
                // Enable checksum validation
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                // Return the first decoded barcode text, or null if none found
                foreach (var result in reader.ReadBarCodes())
                {
                    return result.CodeText;
                }

                return null;
            }
        }

        // --------------------------------------------------------------------
        // 5. Perform the reads from both sources
        // --------------------------------------------------------------------
        string codeFromStream = ReadFromStream(imageBytes);
        string codeFromFile = ReadFromFile(tempFilePath);

        // --------------------------------------------------------------------
        // 6. Output the results and verify that both reads match
        // --------------------------------------------------------------------
        Console.WriteLine($"Code read from MemoryStream: {(codeFromStream ?? "None")}");
        Console.WriteLine($"Code read from file: {(codeFromFile ?? "None")}");

        if (codeFromStream != null && codeFromStream == codeFromFile)
        {
            Console.WriteLine("Checksum validation results match between stream and file reads.");
        }
        else
        {
            Console.WriteLine("Mismatch in checksum validation results.");
        }

        // --------------------------------------------------------------------
        // 7. Clean up the temporary file
        // --------------------------------------------------------------------
        try
        {
            if (File.Exists(tempFilePath))
                File.Delete(tempFilePath);
        }
        catch
        {
            // Ignore any cleanup errors (e.g., file in use)
        }
    }
}