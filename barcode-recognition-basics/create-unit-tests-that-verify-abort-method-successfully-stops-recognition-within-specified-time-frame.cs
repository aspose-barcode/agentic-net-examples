// Title: Demonstrate aborting barcode recognition using Aspose.BarCode
// Description: Shows how to generate a Code128 barcode, start a recognition task, abort it, and verify that the abort was detected.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, illustrating the use of BarCodeReader, the Abort method, and handling RecognitionAbortedException. Developers working with barcode scanning often need to cancel long‑running recognition operations, for instance when a user cancels an action or a timeout occurs. The snippet demonstrates typical patterns for initiating recognition, aborting it, and cleaning up resources, which are common in unit‑test scenarios and interactive applications.
// Prompt: Create unit tests that verify Abort method successfully stops recognition within a specified time frame.
// Tags: barcode symbology, recognition, abort, aspose.barcode, code128, unit-test, exception handling, async

using System;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program demonstrating aborting barcode recognition with Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Executes the abort demonstration.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the test files
        string tempDir = Path.Combine(Path.GetTempPath(), "AbortTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string imagePath = Path.Combine(tempDir, "barcode.png");

        // Generate a simple Code128 barcode image
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Variable to capture whether the abort was detected
        bool abortDetected = false;

        // Open a reader for the generated image and start recognition in a separate task
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            var readTask = Task.Run(() =>
            {
                try
                {
                    // Attempt to read barcodes (this call will be aborted)
                    var results = reader.ReadBarCodes();
                }
                catch (RecognitionAbortedException)
                {
                    // Expected exception when abort is successful
                    abortDetected = true;
                }
                catch (Exception ex)
                {
                    // Log any unexpected exceptions
                    Console.WriteLine("Unexpected exception: " + ex.Message);
                }
            });

            // Small bounded wait before aborting (ensures the read operation has started)
            Stopwatch sw = Stopwatch.StartNew();
            while (sw.ElapsedMilliseconds < 10) { }

            // Abort the recognition process
            reader.Abort();

            // Wait for the reading task to complete
            readTask.Wait();
        }

        // Output the result of the abort verification
        Console.WriteLine(abortDetected ? "Abort succeeded" : "Abort failed");

        // Clean up temporary files and directory
        try
        {
            if (File.Exists(imagePath))
                File.Delete(imagePath);
            if (Directory.Exists(tempDir))
                Directory.Delete(tempDir);
        }
        catch
        {
            // Suppress any cleanup exceptions
        }
    }
}