// Title: Demonstrate aborting barcode recognition with Aspose.BarCode
// Description: Shows how to abort a long-running barcode recognition task and verify it stops within a time limit.
// Prompt: Create unit tests that verify Abort method successfully stops recognition within a specified time frame.
// Tags: barcode, symbology, code128, abort, recognition, unit-test, aspose.barcode

using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a large barcode, starts recognition,
/// aborts it, and checks that the abort completes within a specified time frame.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the program. Executes the abort verification logic.
    /// </summary>
    static void Main()
    {
        // Generate a large barcode image to ensure recognition takes noticeable time.
        const string longText = "1234567890123456789012345678901234567890";
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, longText))
        {
            // Use interpolation mode and set a large image size.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            generator.Parameters.ImageWidth.Point = 2000f;
            generator.Parameters.ImageHeight.Point = 2000f;

            using (Bitmap barcodeImage = generator.GenerateBarCodeImage())
            {
                // Prepare the reader.
                using (var reader = new BarCodeReader())
                {
                    // Assign the image to the reader.
                    reader.SetBarCodeImage(barcodeImage);
                    // Set a generous timeout (will be ignored because we abort).
                    reader.Timeout = 10000;

                    // Measure the time taken for the recognition task.
                    var stopwatch = Stopwatch.StartNew();

                    // Run recognition in a separate task.
                    var recognitionTask = Task.Run(() =>
                    {
                        try
                        {
                            // Attempt to read barcodes.
                            var results = reader.ReadBarCodes();
                            // Return the number of detected barcodes (should be 0 if aborted early).
                            return results.Length;
                        }
                        catch (RecognitionAbortedException)
                        {
                            // Expected when abort is successful.
                            return -1;
                        }
                    });

                    // Immediately request abort.
                    reader.Abort();

                    // Wait for the task to finish.
                    recognitionTask.Wait();

                    stopwatch.Stop();

                    // Determine if abort stopped recognition quickly (within 2 seconds).
                    bool isFast = stopwatch.Elapsed.TotalMilliseconds < 2000;
                    bool isAborted = recognitionTask.Result == -1 || recognitionTask.Result == 0;

                    if (isFast && isAborted)
                    {
                        Console.WriteLine("Test Passed: Abort stopped recognition within the expected time frame.");
                    }
                    else
                    {
                        Console.WriteLine("Test Failed:");
                        Console.WriteLine($"  Elapsed time (ms): {stopwatch.Elapsed.TotalMilliseconds}");
                        Console.WriteLine($"  Recognition result count: {recognitionTask.Result}");
                        Console.WriteLine("  Expected abort to stop quickly.");
                    }
                }
            }
        }
    }
}