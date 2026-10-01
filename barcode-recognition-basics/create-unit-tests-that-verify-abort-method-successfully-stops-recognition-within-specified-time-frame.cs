// Title: Abort Barcode Recognition Test
// Description: Demonstrates how to abort a barcode recognition operation using Aspose.BarCode and verifies it completes within a defined time.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, showcasing the use of BarCodeReader, its Abort method, and timeout handling. Developers often need to stop long-running barcode scans in responsive applications or unit tests; this snippet illustrates typical patterns for aborting and measuring recognition duration.
// Prompt: Create unit tests that verify Abort method successfully stops recognition within a specified time frame.
// Tags: barcode recognition, abort, timeout, aspose.barcode, code128, unit test, performance

using System;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates aborting a barcode recognition operation and validates its timing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates a sample barcode, runs the abort test, and reports the outcome.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for test files
        string tempFolder = Path.Combine(Path.GetTempPath(), "AbortTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate a sample barcode image
        string barcodePath = Path.Combine(tempFolder, "sample.png");
        GenerateSampleBarcode(barcodePath);

        // Run the abort test
        bool testResult = RunAbortTest(barcodePath, expectedMaxDurationMs: 1000);

        // Report result
        if (testResult)
        {
            Console.WriteLine("PASSED: Abort stopped recognition within the expected time.");
        }
        else
        {
            Console.WriteLine("FAILED: Abort did not stop recognition as expected.");
        }

        // Clean up temporary files
        try { Directory.Delete(tempFolder, true); } catch { /* ignore cleanup errors */ }
    }

    // Generates a simple Code128 barcode and saves it to the specified path
    private static void GenerateSampleBarcode(string path)
    {
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(path, BarCodeImageFormat.Png);
        }
    }

    // Starts recognition, aborts it after a short delay, and verifies it stops quickly
    private static bool RunAbortTest(string imagePath, int expectedMaxDurationMs)
    {
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Test image not found.");
            return false;
        }

        bool abortCaught = false;
        long elapsedMs = 0;

        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Set a long timeout so that abort is the only way to stop early
            reader.Timeout = 10000; // 10 seconds

            // Task that performs the reading
            Task readTask = Task.Run(() =>
            {
                Stopwatch sw = Stopwatch.StartNew();
                try
                {
                    // This call blocks until reading finishes, timeout expires, or abort is invoked
                    BarCodeResult[] results = reader.ReadBarCodes();
                    // If we get results before abort, treat as failure for this test
                    abortCaught = false;
                }
                catch (RecognitionAbortedException)
                {
                    // Expected when abort is triggered
                    abortCaught = true;
                }
                catch (Exception ex)
                {
                    // Any other exception is unexpected
                    Console.WriteLine($"Unexpected exception: {ex.GetType().Name} - {ex.Message}");
                    abortCaught = false;
                }
                finally
                {
                    sw.Stop();
                    elapsedMs = sw.ElapsedMilliseconds;
                }
            });

            // Wait a short period before aborting
            Thread.Sleep(200); // 200 ms
            reader.Abort();

            // Wait for the reading task to complete (with a safety timeout)
            if (!readTask.Wait(5000))
            {
                Console.WriteLine("Read task did not complete in expected time.");
                return false;
            }
        }

        // Verify that abort was caught and that the operation finished quickly
        bool durationOk = elapsedMs <= expectedMaxDurationMs;
        Console.WriteLine($"Abort caught: {abortCaught}, Elapsed ms: {elapsedMs}, Duration OK: {durationOk}");
        return abortCaught && durationOk;
    }
}