using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

namespace BarcodeChecksumDemo
{
    class Program
    {
        static void Main()
        {
            // Create a sample EAN13 barcode (includes checksum digit)
            const string codeText = "1234567890128";

            // Generate barcode image and store it in a memory stream
            using (var memoryStream = new MemoryStream())
            {
                using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, codeText))
                {
                    // Save to memory stream in PNG format
                    generator.Save(memoryStream, BarCodeImageFormat.Png);
                }

                // Also save the same image to a temporary file for file‑based reading
                string tempFilePath = Path.Combine(Path.GetTempPath(), "barcode_demo.png");
                try
                {
                    // Write the memory stream contents to the file
                    using (var fileStream = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write))
                    {
                        memoryStream.Position = 0;
                        memoryStream.CopyTo(fileStream);
                    }

                    // Verify that the file was created
                    if (!File.Exists(tempFilePath))
                    {
                        Console.WriteLine("Failed to create temporary barcode file.");
                        return;
                    }

                    // Function to read barcode and return checksum string
                    string ReadChecksumFromFile(bool enableChecksumValidation)
                    {
                        using (var reader = new BarCodeReader(tempFilePath, DecodeType.EAN13))
                        {
                            reader.BarcodeSettings.ChecksumValidation = enableChecksumValidation
                                ? ChecksumValidation.On
                                : ChecksumValidation.Off;

                            foreach (var result in reader.ReadBarCodes())
                            {
                                return result.Extended.OneD.CheckSum;
                            }
                        }
                        return null;
                    }

                    string ReadChecksumFromMemory(bool enableChecksumValidation)
                    {
                        // Reset stream position before each read
                        memoryStream.Position = 0;
                        using (var reader = new BarCodeReader(memoryStream, DecodeType.EAN13))
                        {
                            reader.BarcodeSettings.ChecksumValidation = enableChecksumValidation
                                ? ChecksumValidation.On
                                : ChecksumValidation.Off;

                            foreach (var result in reader.ReadBarCodes())
                            {
                                return result.Extended.OneD.CheckSum;
                            }
                        }
                        return null;
                    }

                    // Read with checksum validation enabled
                    string fileChecksumOn = ReadChecksumFromFile(true);
                    string memChecksumOn = ReadChecksumFromMemory(true);

                    // Read with checksum validation disabled
                    string fileChecksumOff = ReadChecksumFromFile(false);
                    string memChecksumOff = ReadChecksumFromMemory(false);

                    // Output comparison results
                    Console.WriteLine("Checksum Validation: ON");
                    Console.WriteLine($"File  checksum: {fileChecksumOn ?? "null"}");
                    Console.WriteLine($"Memory checksum: {memChecksumOn ?? "null"}");
                    Console.WriteLine($"Match: {string.Equals(fileChecksumOn, memChecksumOn, StringComparison.Ordinal)}");
                    Console.WriteLine();

                    Console.WriteLine("Checksum Validation: OFF");
                    Console.WriteLine($"File  checksum: {fileChecksumOff ?? "null"}");
                    Console.WriteLine($"Memory checksum: {memChecksumOff ?? "null"}");
                    Console.WriteLine($"Match: {string.Equals(fileChecksumOff, memChecksumOff, StringComparison.Ordinal)}");
                }
                finally
                {
                    // Clean up temporary file
                    if (File.Exists(tempFilePath))
                    {
                        try { File.Delete(tempFilePath); } catch { /* ignore cleanup errors */ }
                    }
                }
            }
        }
    }
}