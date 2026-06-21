using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a barcode, saving it to memory, and then recognizing it
/// using Aspose.BarCode with different quality settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a Code128 barcode, reads it with custom and default quality settings,
    /// and outputs detection results and timing information to the console.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Generate a simple barcode image in memory (PNG format)
        // ------------------------------------------------------------
        byte[] barcodeBytes;
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set image resolution to 300 DPI for higher quality
            generator.Parameters.Resolution = 300f;

            // Save the generated barcode to a memory stream
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                // Convert the stream to a byte array for later use
                barcodeBytes = ms.ToArray();
            }
        }

        // ------------------------------------------------------------
        // 2. Load the barcode image from memory for recognition
        // ------------------------------------------------------------
        using (var imageStream = new MemoryStream(barcodeBytes))
        using (var bitmap = new Bitmap(imageStream))
        {
            // --------------------------------------------------------
            // 2a. Read with custom QualitySettings (allow incorrect barcodes)
            // --------------------------------------------------------
            var stopwatch = new Stopwatch();

            using (var reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
            {
                // Enable reading of barcodes that may not meet strict quality criteria
                reader.QualitySettings.AllowIncorrectBarcodes = true;

                // Measure the time taken to read barcodes
                stopwatch.Start();
                var results = reader.ReadBarCodes();
                stopwatch.Stop();

                // Output timing information
                Console.WriteLine($"Custom QualitySettings (AllowIncorrectBarcodes=true) read time: {stopwatch.ElapsedMilliseconds} ms");

                // Output each detected barcode's type and text
                foreach (var result in results)
                {
                    Console.WriteLine($"Detected: {result.CodeTypeName} - {result.CodeText}");
                }
            }

            // --------------------------------------------------------
            // 2b. Read with default QualitySettings (new reader instance)
            // --------------------------------------------------------
            using (var readerDefault = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
            {
                // No custom settings; defaults are used

                // Restart the stopwatch for a fresh measurement
                stopwatch.Restart();
                var results = readerDefault.ReadBarCodes();
                stopwatch.Stop();

                // Output timing information for default settings
                Console.WriteLine($"Default QualitySettings read time: {stopwatch.ElapsedMilliseconds} ms");

                // Output each detected barcode's type and text
                foreach (var result in results)
                {
                    Console.WriteLine($"Detected: {result.CodeTypeName} - {result.CodeText}");
                }
            }
        }
    }
}