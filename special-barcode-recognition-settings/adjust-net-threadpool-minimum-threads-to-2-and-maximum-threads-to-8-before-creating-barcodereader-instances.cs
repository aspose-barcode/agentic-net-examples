using System;
using System.IO;
using System.Threading;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a barcode, storing it in memory, and reading it back using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Configures thread pool, creates a barcode image in memory, and reads it back.
    /// </summary>
    static void Main()
    {
        // Configure the thread pool to have a minimum of 2 worker and I/O threads,
        // and a maximum of 8 worker and I/O threads.
        ThreadPool.SetMinThreads(2, 2);
        ThreadPool.SetMaxThreads(8, 8);

        // Use a memory stream to hold the generated barcode image.
        using (var ms = new MemoryStream())
        {
            // Create a barcode generator for Code128 with the data "123456".
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
            {
                // Save the generated barcode as a PNG image into the memory stream.
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Reset the stream position to the beginning before reading.
            ms.Position = 0;

            // Initialize a barcode reader that can decode all supported barcode types,
            // using the memory stream that contains the PNG image.
            using (var reader = new BarCodeReader(ms, DecodeType.AllSupportedTypes))
            {
                // Iterate through all detected barcodes and output their type and text.
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
                }
            }
        }
    }
}