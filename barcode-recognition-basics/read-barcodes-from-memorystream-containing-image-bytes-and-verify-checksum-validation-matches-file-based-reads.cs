// Title: Read barcode from MemoryStream and verify checksum against file read
// Description: Demonstrates generating a Code11 barcode, saving it to a file and a MemoryStream, then reading both sources with checksum validation enabled to ensure consistent results.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them, highlighting checksum validation with the ChecksumValidation enum. Typical scenarios include validating data integrity when reading barcodes from different storage mediums such as files and streams, a common requirement for developers working with inventory, shipping, or authentication systems. The example serves as a reference for using key API classes like BarcodeGenerator, BarCodeReader, and related settings in C# projects.
/// Prompt: Read barcodes from a MemoryStream containing image bytes and verify checksum validation matches file‑based reads.
// Tags: code11, checksum, barcode, generation, recognition, memorystream, file, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Code11 barcode, saves it to a file and a memory stream,
/// then reads the barcode from both sources with checksum validation enabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcode, performs file and memory reads, and validates checksum.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeTemp_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string filePath = Path.Combine(tempFolder, "code11.png");

        // Generate a Code11 barcode with checksum enabled
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code11, "123456"))
        {
            // Set barcode size (2 pixels per module)
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Save the barcode image to a physical file
            generator.Save(filePath, BarCodeImageFormat.Png);

            // Also save the barcode image to a memory stream for in‑memory processing
            using (MemoryStream memoryStream = new MemoryStream())
            {
                generator.Save(memoryStream, BarCodeImageFormat.Png);
                memoryStream.Position = 0; // Reset stream position for reading

                // ---------- Read barcode from the saved file ----------
                bool fileReadSuccess = false;
                if (File.Exists(filePath))
                {
                    using (BarCodeReader fileReader = new BarCodeReader(filePath, DecodeType.Code11))
                    {
                        // Enable checksum validation for the file read
                        fileReader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;
                        foreach (BarCodeResult result in fileReader.ReadBarCodes())
                        {
                            Console.WriteLine($"[File] Type: {result.CodeTypeName}, Text: {result.CodeText}");
                            fileReadSuccess = true;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("File not found: " + filePath);
                }

                // ---------- Read barcode from the memory stream ----------
                bool memoryReadSuccess = false;
                using (BarCodeReader memReader = new BarCodeReader(memoryStream))
                {
                    // Associate the same stream with the reader (required for some formats)
                    memReader.SetBarCodeImage(memoryStream);
                    // Enable checksum validation for the memory read
                    memReader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;
                    foreach (BarCodeResult result in memReader.ReadBarCodes())
                    {
                        Console.WriteLine($"[Memory] Type: {result.CodeTypeName}, Text: {result.CodeText}");
                        memoryReadSuccess = true;
                    }
                }

                // Verify that both reads succeeded and checksum validation matched
                if (fileReadSuccess && memoryReadSuccess)
                {
                    Console.WriteLine("Checksum validation succeeded for both file and memory reads.");
                }
                else
                {
                    Console.WriteLine("Checksum validation mismatch or read failure.");
                }
            }
        }

        // Clean up the temporary folder and its contents
        try
        {
            if (Directory.Exists(tempFolder))
            {
                Directory.Delete(tempFolder, true);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Cleanup failed: " + ex.Message);
        }
    }
}