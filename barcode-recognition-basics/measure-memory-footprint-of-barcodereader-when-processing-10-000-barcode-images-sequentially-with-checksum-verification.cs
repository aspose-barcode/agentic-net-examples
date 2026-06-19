using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation and recognition while measuring memory usage.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a simple Code128 barcode image and returns its bytes.
    /// </summary>
    /// <returns>Byte array containing the PNG representation of the barcode.</returns>
    static byte[] GenerateSampleBarcode()
    {
        // Use a memory stream to hold the generated image.
        using (var ms = new MemoryStream())
        {
            // Create a barcode generator for Code128 with sample data.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Save the barcode as PNG into the memory stream.
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Return the image bytes.
            return ms.ToArray();
        }
    }

    /// <summary>
    /// Entry point. Generates a barcode, processes it multiple times, and reports memory usage.
    /// </summary>
    static void Main()
    {
        // Prepare a sample barcode image (in memory) to be reused.
        byte[] barcodeBytes = GenerateSampleBarcode();

        // Number of iterations – using a small safe count for the snippet runner.
        const int iterationCount = 10; // Replace with 10000 for real measurement.

        // Capture memory usage before processing.
        Process proc = Process.GetCurrentProcess();
        long memoryBefore = proc.PrivateMemorySize64;

        // Process the barcode image repeatedly.
        for (int i = 0; i < iterationCount; i++)
        {
            // Load the barcode bytes into a new memory stream for each iteration.
            using (var ms = new MemoryStream(barcodeBytes))
            {
                ms.Position = 0; // Ensure the stream is positioned at the start.

                // Create a bitmap from the memory stream.
                using (var bitmap = new Bitmap(ms))
                {
                    // Initialize a barcode reader that supports all barcode types.
                    using (var reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
                    {
                        // Enable checksum validation.
                        reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                        // Read barcodes (result is not used further in this demo).
                        foreach (var result in reader.ReadBarCodes())
                        {
                            // Optionally, you could inspect result.CodeText, etc.
                        }
                    }
                }
            }
        }

        // Force garbage collection to get a more accurate measurement.
        GC.Collect();
        GC.WaitForPendingFinalizers();

        // Capture memory usage after processing.
        long memoryAfter = proc.PrivateMemorySize64;
        long memoryDiff = memoryAfter - memoryBefore;

        // Output memory usage statistics.
        Console.WriteLine($"Memory before: {memoryBefore:N0} bytes");
        Console.WriteLine($"Memory after : {memoryAfter:N0} bytes");
        Console.WriteLine($"Memory increase after processing {iterationCount} images: {memoryDiff:N0} bytes");
    }
}