// Title: Abort barcode recognition from another thread
// Description: Demonstrates calling Abort on a BarCodeReader while recognition runs in a separate thread to stop processing immediately.
// Prompt: Call Abort method from a separate thread while recognition is running to stop the operation immediately.
// Tags: barcode, abort, multithreading, recognition, aspose.barcoderecognition

using System;
using System.IO;
using System.Threading;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program showing how to abort barcode recognition from another thread.
/// </summary>
class Program
{
    /// <summary>
    /// Runs barcode recognition in a separate thread.
    /// The method will be interrupted if <see cref="BarCodeReader.Abort"/> is called from another thread.
    /// </summary>
    /// <param name="readerObj">An instance of <see cref="BarCodeReader"/> passed as an object.</param>
    private static void ThreadRecognize(object readerObj)
    {
        // Cast the passed object back to BarCodeReader
        BarCodeReader reader = (BarCodeReader)readerObj;

        // Iterate through all detected barcodes; this loop will exit early if Abort() is invoked
        foreach (BarCodeResult result in reader.ReadBarCodes())
        {
            Console.WriteLine($"BarCode Type: {result.CodeTypeName}");
            Console.WriteLine($"BarCode Text: {result.CodeText}");
        }
    }

    /// <summary>
    /// Entry point that generates a barcode, starts recognition on a separate thread,
    /// aborts it, and waits for completion.
    /// </summary>
    static void Main()
    {
        // Generate a sample barcode image in memory (Code128 with value "123456789")
        using (MemoryStream ms = new MemoryStream())
        {
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
            {
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Reset stream position so the reader can read from the beginning
            ms.Position = 0;

            // Create a BarCodeReader for the image stream and enable all supported symbologies
            using (BarCodeReader reader = new BarCodeReader(ms, DecodeType.AllSupportedTypes))
            {
                // Set a generous timeout to give Abort() enough time to take effect
                reader.Timeout = 10000; // 10 seconds

                // Start the recognition process on a separate thread
                Thread recognizeThread = new Thread(ThreadRecognize);
                recognizeThread.Start(reader);

                // Request immediate abort from the main thread
                reader.Abort();

                // Wait for the recognition thread to finish cleanly
                recognizeThread.Join();
            }
        }

        Console.WriteLine("Recognition aborted and program completed.");
    }
}