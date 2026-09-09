// Title: Demonstrate aborting barcode recognition using Aspose.BarCode
// Description: This example generates a QR barcode, runs recognition on a separate thread, aborts the operation, and verifies that the abort was caught.
// Category-Description: Shows how to use Aspose.BarCode's BarCodeReader to perform asynchronous barcode recognition and control it with the Abort method. Typical scenarios include long‑running scans where a user may cancel the operation. Developers often need to handle RecognitionAbortedException and clean up resources, making this pattern essential for responsive applications.
// Prompt: Create unit tests that verify Abort method successfully stops recognition within a specified time frame.
// Tags: qr, abort, barcode recognition, aspose.barcode, multithreading, unit-test, exception handling

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates aborting a barcode recognition operation using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a QR code, starts recognition on a background thread, aborts it,
    /// and reports whether the abort was successfully detected.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for test artifacts
        string tempFolder = Path.Combine(Path.GetTempPath(), "AbortTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate a QR barcode image and save it to the temporary folder
        string imagePath = Path.Combine(tempFolder, "qr.png");
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "TestAbort"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        bool abortCaught = false;          // Indicates whether the abort exception was caught
        Exception threadException = null;  // Captures any unexpected exception from the worker thread

        // Initialize the barcode reader for QR codes
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            // Start recognition in a separate thread to allow aborting from the main thread
            Thread readThread = new Thread(() =>
            {
                try
                {
                    // This call blocks until recognition finishes or is aborted
                    var results = reader.ReadBarCodes();
                    // If completed without abort, nothing further is required
                }
                catch (RecognitionAbortedException)
                {
                    // Expected path when Abort is invoked
                    abortCaught = true;
                }
                catch (Exception ex)
                {
                    // Capture any unexpected errors for later reporting
                    threadException = ex;
                }
            });

            readThread.Start();

            // Allow the recognition to run briefly before aborting
            Task.Delay(200).Wait();

            // Request abortion of the ongoing recognition operation
            reader.Abort();

            // Wait for the background thread to finish processing
            readThread.Join();

            // Output the result of the abort test
            Console.WriteLine(abortCaught ? "Abort succeeded" : "Abort not triggered");
            if (threadException != null)
            {
                Console.WriteLine("Unexpected error: " + threadException);
            }
        }

        // Clean up temporary files and folder; ignore any cleanup errors
        try
        {
            if (File.Exists(imagePath))
                File.Delete(imagePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Cleanup failures are non‑critical for the test outcome
        }
    }
}