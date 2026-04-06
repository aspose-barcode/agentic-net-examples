using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Create a sample EAN13 barcode with a valid checksum.
        using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, "1234567890128"))
        {
            // Save barcode to a memory stream (PNG format).
            using (var memoryStream = new MemoryStream())
            {
                generator.Save(memoryStream, BarCodeImageFormat.Png);
                memoryStream.Position = 0; // Reset for reading.

                // Also save to a temporary file for file‑based reading.
                string tempFile = Path.Combine(Path.GetTempPath(), "barcode.png");
                generator.Save(tempFile);

                // Read from memory stream with checksum validation ON.
                using (var readerStreamOn = new BarCodeReader(memoryStream, DecodeType.EAN13))
                {
                    readerStreamOn.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;
                    var resultsOn = readerStreamOn.ReadBarCodes();
                    PrintResults("MemoryStream", "Checksum ON", resultsOn);
                }

                // Reset stream position for the next read.
                memoryStream.Position = 0;

                // Read from memory stream with checksum validation OFF.
                using (var readerStreamOff = new BarCodeReader(memoryStream, DecodeType.EAN13))
                {
                    readerStreamOff.BarcodeSettings.ChecksumValidation = ChecksumValidation.Off;
                    var resultsOff = readerStreamOff.ReadBarCodes();
                    PrintResults("MemoryStream", "Checksum OFF", resultsOff);
                }

                // Read from file with checksum validation ON.
                using (var readerFileOn = new BarCodeReader(tempFile, DecodeType.EAN13))
                {
                    readerFileOn.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;
                    var resultsOn = readerFileOn.ReadBarCodes();
                    PrintResults("File", "Checksum ON", resultsOn);
                }

                // Read from file with checksum validation OFF.
                using (var readerFileOff = new BarCodeReader(tempFile, DecodeType.EAN13))
                {
                    readerFileOff.BarcodeSettings.ChecksumValidation = ChecksumValidation.Off;
                    var resultsOff = readerFileOff.ReadBarCodes();
                    PrintResults("File", "Checksum OFF", resultsOff);
                }

                // Clean up temporary file.
                if (File.Exists(tempFile))
                {
                    File.Delete(tempFile);
                }
            }
        }
    }

    // Helper method to display barcode reading results.
    static void PrintResults(string source, string checksumMode, BarCodeResult[] results)
    {
        Console.WriteLine($"Source: {source}, {checksumMode}");
        if (results.Length == 0)
        {
            Console.WriteLine("  No barcode detected.");
            return;
        }

        foreach (var result in results)
        {
            Console.WriteLine($"  Type: {result.CodeTypeName}");
            Console.WriteLine($"  CodeText: {result.CodeText}");
            // For 1D barcodes, checksum is available via Extended.OneD.CheckSum.
            if (result.Extended?.OneD != null)
            {
                Console.WriteLine($"  Checksum: {result.Extended.OneD.CheckSum}");
            }
        }
        Console.WriteLine();
    }
}