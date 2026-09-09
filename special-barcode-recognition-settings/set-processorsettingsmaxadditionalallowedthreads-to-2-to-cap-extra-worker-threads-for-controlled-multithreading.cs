// Title: Cap Additional Worker Threads for Barcode Processing
// Description: Demonstrates how to limit the number of extra threads used by Aspose.BarCode's processor settings while generating and reading a QR code.
// Category-Description: This example belongs to the Aspose.BarCode multithreading and performance tuning category. It shows how to configure ProcessorSettings, generate a QR code with BarcodeGenerator, and read it using BarCodeReader. Developers working with high‑throughput barcode scanning often need to control thread usage to avoid resource contention.
// Prompt: Set ProcessorSettings.MaxAdditionalAllowedThreads to 2 to cap extra worker threads for controlled multithreading.
// Tags: qr code, multithreading, processor settings, aspose.barcode, barcode generation, barcode recognition, performance

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that caps additional worker threads for barcode processing,
/// generates a QR code, reads it back, and cleans up temporary files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Configure the processor to allow a maximum of 2 additional threads for barcode reading.
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = 2;
        Console.WriteLine($"ProcessorSettings.MaxAdditionalAllowedThreads = {BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads}");

        // Create a unique temporary folder to store the generated barcode image.
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodeFile = Path.Combine(tempFolder, "sample.png");

        // Generate a QR code image with the specified text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose"))
        {
            // Set the module size (X dimension) for the QR code.
            generator.Parameters.Barcode.XDimension.Point = 2f;
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Save the generated QR code as a PNG file.
                bitmap.Save(barcodeFile, ImageFormat.Png);
            }
        }

        // Read the generated QR code from the file.
        using (BarCodeReader reader = new BarCodeReader(barcodeFile, DecodeType.QR))
        {
            Stopwatch sw = Stopwatch.StartNew(); // Start timing the read operation.
            BarCodeResult[] results = reader.ReadBarCodes();
            sw.Stop(); // Stop timing.

            Console.WriteLine($"Read {results.Length} barcode(s) in {sw.ElapsedMilliseconds} ms");
            foreach (BarCodeResult result in results)
            {
                // Output the type and decoded text of each barcode found.
                Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }
        }

        // Attempt to delete the temporary files and folder; ignore any errors.
        try
        {
            File.Delete(barcodeFile);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program exit.
        }
    }
}