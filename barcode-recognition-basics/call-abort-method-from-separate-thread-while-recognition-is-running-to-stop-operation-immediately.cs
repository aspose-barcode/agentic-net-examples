// Title: Abort barcode recognition from another thread
// Description: Demonstrates aborting an ongoing barcode recognition operation using BarCodeReader.Abort from a separate thread.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, showcasing how to control long‑running barcode scanning tasks. It uses the BarCodeReader class to decode QR codes and the Abort method to stop processing instantly. Developers often need to cancel recognition in responsive UI scenarios or when a timeout occurs, making this pattern essential for robust multithreaded applications.
// Prompt: Call Abort method from a separate thread while recognition is running to stop the operation immediately.
// Tags: barcode recognition, abort, multithreading, aspose.barcode, qr, c#

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR barcode, starts recognition on a separate thread,
/// and aborts the operation from the main thread using <see cref="BarCodeReader.Abort"/>.
/// </summary>
class Program
{
    /// <summary>
    /// Method executed on a background thread to perform barcode recognition.
    /// It iterates through detected barcodes until the operation is aborted.
    /// </summary>
    /// <param name="readerObj">An instance of <see cref="BarCodeReader"/> passed as an object.</param>
    private static void ThreadRecognize(object readerObj)
    {
        var reader = (BarCodeReader)readerObj;
        try
        {
            // Enumerate all detected barcodes; this loop runs until Abort is called.
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected: {result.CodeTypeName} - {result.CodeText}");
            }
        }
        catch (Exception ex)
        {
            // Abort throws an exception; capture it to indicate the operation was stopped.
            Console.WriteLine($"Recognition stopped: {ex.Message}");
        }
    }

    /// <summary>
    /// Entry point of the program. Generates a QR code image, starts recognition on a separate thread,
    /// aborts the recognition after a short delay, and cleans up resources.
    /// </summary>
    static void Main()
    {
        // Generate a temporary QR barcode image.
        string tempDir = Path.GetTempPath();
        string imagePath = Path.Combine(tempDir, "sample_qr.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose"))
        {
            generator.Save(imagePath);
        }

        // Verify that the image was created successfully.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Initialize a BarCodeReader for the generated QR image.
        using (var reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            // Launch the recognition process on a separate thread.
            Thread recognizeThread = new Thread(ThreadRecognize);
            recognizeThread.Start(reader);

            // Allow the recognition to run briefly before aborting.
            Task.Delay(200).Wait();

            Console.WriteLine("Calling Abort...");
            // Abort the ongoing recognition operation.
            reader.Abort();

            // Wait for the background thread to finish handling the abort.
            recognizeThread.Join();
            Console.WriteLine("Recognition thread finished.");
        }

        Console.WriteLine("Program completed.");
    }
}