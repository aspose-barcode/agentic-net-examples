using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Prepare a simple barcode image and save it to a temporary file
        string tempPath = Path.Combine(Path.GetTempPath(), "abort_test.png");
        try
        {
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                generator.Save(tempPath, BarCodeImageFormat.Png);
            }

            if (!File.Exists(tempPath))
            {
                Console.WriteLine("Failed to create barcode image.");
                return;
            }

            bool abortCaught = false;
            Stopwatch totalWatch = Stopwatch.StartNew();

            // Initialize the reader with a long timeout (10 seconds)
            using (BarCodeReader reader = new BarCodeReader(tempPath, DecodeType.Code128))
            {
                reader.Timeout = 10000; // milliseconds

                // Start a task that will abort the recognition after a short delay (200 ms)
                Task abortTask = Task.Run(async () =>
                {
                    await Task.Delay(200);
                    reader.Abort();
                });

                try
                {
                    // This call blocks until recognition finishes or is aborted
                    BarCodeResult[] results = reader.ReadBarCodes();

                    // If we reach here without an exception, the abort did not work as expected
                    Console.WriteLine($"Recognition completed normally, found {results.Length} barcode(s).");
                }
                catch (RecognitionAbortedException ex)
                {
                    abortCaught = true;
                    Console.WriteLine($"Recognition aborted after {ex.ExecutionTime} ms (exception caught).");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected exception: {ex.GetType().Name} - {ex.Message}");
                }
            }

            totalWatch.Stop();

            // Output test verification results
            Console.WriteLine($"Abort caught: {abortCaught}");
            Console.WriteLine($"Total elapsed time: {totalWatch.ElapsedMilliseconds} ms (should be << timeout)");
        }
        finally
        {
            // Clean up the temporary file
            if (File.Exists(tempPath))
            {
                try { File.Delete(tempPath); } catch { /* ignore cleanup errors */ }
            }
        }
    }
}