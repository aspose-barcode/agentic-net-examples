// Title: Abort Barcode Recognition Example
// Description: Demonstrates how to abort a barcode recognition operation using Aspose.BarCode and verify it stops within a time limit.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, showcasing the use of BarCodeReader, its Timeout property, and the Abort method. Developers often need to stop long‑running recognition tasks programmatically, especially in UI or service scenarios where responsiveness is critical. The code illustrates typical patterns for threading, exception handling, and performance measurement when working with barcode recognition APIs.
// Prompt: Create unit tests that verify Abort method successfully stops recognition within a specified time frame.
// Tags: barcode, symbology, abort, recognition, unit-test, aspose.barcode, threading, performance

using System;
using System.IO;
using System.Diagnostics;
using System.Threading;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Contains the entry point and demonstration of aborting a barcode recognition operation.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point. Executes the abort recognition test.
    /// </summary>
    static void Main()
    {
        TestAbortRecognition();
    }

    static void TestAbortRecognition()
    {
        // Create a unique temporary folder for the test files
        string tempDir = Path.Combine(Path.GetTempPath(), "AbortTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "code.png");

        // Generate a simple Code128 barcode image and save it to the temporary folder
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "TestAbort"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Initialize the barcode reader with a long timeout to prevent automatic abort
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            reader.Timeout = 10000; // 10 seconds

            Exception readException = null;
            Stopwatch sw = new Stopwatch();

            // Start recognition on a separate thread so we can abort it from the main thread
            Thread readThread = new Thread(() =>
            {
                try
                {
                    sw.Start();
                    var results = reader.ReadBarCodes(); // Blocking call
                    sw.Stop(); // Should not reach here if abort works
                }
                catch (RecognitionAbortedException ex)
                {
                    readException = ex;
                    sw.Stop();
                }
                catch (Exception ex)
                {
                    readException = ex;
                    sw.Stop();
                }
            });
            readThread.Start();

            // Give the reader a short moment to begin processing
            Thread.Sleep(100);

            // Request abort of the ongoing recognition operation
            reader.Abort();

            // Wait for the recognition thread to finish
            readThread.Join();

            // Verify that the abort was raised and completed within a reasonable time (<= 1 second)
            bool passed = readException is RecognitionAbortedException && sw.ElapsedMilliseconds <= 1000;
            Console.WriteLine(passed
                ? "PASS: Abort stopped recognition within time."
                : "FAIL: Abort did not work as expected.");

            // Output any unexpected exception details
            if (readException != null && !(readException is RecognitionAbortedException))
            {
                Console.WriteLine($"Unexpected exception: {readException.GetType().Name} - {readException.Message}");
            }

            Console.WriteLine($"Elapsed ms: {sw.ElapsedMilliseconds}");
        }

        // Clean up temporary files and folder
        try
        {
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignored – cleanup failure should not affect test outcome
        }
    }
}