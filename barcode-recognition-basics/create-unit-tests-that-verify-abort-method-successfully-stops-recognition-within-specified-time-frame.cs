// Title: Abort Barcode Recognition Example
// Description: Demonstrates how to abort a barcode recognition operation using Aspose.BarCode's Abort method.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, showcasing the use of BarCodeReader, BarcodeGenerator, and the Abort method to control long‑running recognition tasks. Developers often need to stop recognition after a timeout or user cancellation, and this snippet illustrates setting a timeout, running recognition asynchronously, and aborting it safely.
// Prompt: Create unit tests that verify Abort method successfully stops recognition within a specified time frame.
// Tags: code128, abort, recognition, aspose.barcode, generation, timeout

using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates aborting a barcode recognition operation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates a barcode, starts recognition asynchronously, aborts it, and reports the outcome.
    /// </summary>
    static void Main()
    {
        // Generate a barcode image in memory
        using (var imageStream = new MemoryStream())
        {
            // Create a barcode generator for Code128 with sample text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "TestAbort"))
            {
                // Save the generated barcode as PNG into the memory stream
                generator.Save(imageStream, BarCodeImageFormat.Png);
                // Reset stream position for reading
                imageStream.Position = 0;
            }

            // Initialize a barcode reader with a long timeout (10 seconds)
            using (var reader = new BarCodeReader(imageStream, DecodeType.Code128))
            {
                reader.Timeout = 10000; // Timeout in milliseconds

                // Start recognition on a separate task to allow aborting
                var recognitionTask = Task.Run(() =>
                {
                    try
                    {
                        // Perform synchronous read; may be aborted
                        var results = reader.ReadBarCodes();
                        Console.WriteLine($"Recognition completed, found {results.Length} barcode(s).");
                    }
                    catch (RecognitionAbortedException ex)
                    {
                        // Expected path when abort is invoked
                        Console.WriteLine($"Recognition aborted after {ex.ExecutionTime} ms (expected).");
                    }
                    catch (Exception ex)
                    {
                        // Log any unexpected errors
                        Console.WriteLine($"Unexpected exception: {ex.GetType().Name} - {ex.Message}");
                    }
                });

                // Brief pause before aborting to ensure recognition has started
                Task.Delay(100).Wait(); // 100 ms delay
                // Request abort of the ongoing recognition
                reader.Abort();

                // Wait for the recognition task to complete
                recognitionTask.Wait();
            }
        }

        // Indicate that the abort test has finished
        Console.WriteLine("Abort test completed.");
    }
}