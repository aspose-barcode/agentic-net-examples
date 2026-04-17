using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

namespace BarcodeRecognitionRetry
{
    class Program
    {
        static void Main(string[] args)
        {
            // Path to the image containing barcodes. Use a default sample if not provided.
            string imagePath = args.Length > 0 ? args[0] : "sample.png";

            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"File not found: {imagePath}");
                return;
            }

            // Try to recognize barcodes with up to 3 retries.
            RecognizeBarcodesWithRetry(imagePath, maxRetries: 3);
        }

        private static void RecognizeBarcodesWithRetry(string imagePath, int maxRetries)
        {
            int attempt = 0;
            int timeoutMs = 2000; // initial timeout

            while (attempt <= maxRetries)
            {
                try
                {
                    // Create a BarCodeReader for the image.
                    using (BarCodeReader reader = new BarCodeReader(imagePath))
                    {
                        // Set the timeout for this attempt.
                        reader.Timeout = timeoutMs;

                        // Perform recognition.
                        BarCodeResult[] results = reader.ReadBarCodes();

                        // Output results.
                        if (results.Length == 0)
                        {
                            Console.WriteLine("No barcodes detected.");
                        }
                        else
                        {
                            foreach (BarCodeResult result in results)
                            {
                                Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
                            }
                        }
                    }

                    // Successful recognition, exit the retry loop.
                    break;
                }
                catch (RecognitionAbortedException ex)
                {
                    // Recognition aborted due to timeout. Retry if attempts remain.
                    attempt++;
                    Console.WriteLine($"Recognition aborted (attempt {attempt}/{maxRetries}). Execution time: {ex.ExecutionTime} ms.");

                    if (attempt > maxRetries)
                    {
                        Console.WriteLine("Maximum retry attempts reached. Giving up.");
                        break;
                    }

                    // Optionally increase timeout for the next attempt.
                    timeoutMs += 2000;
                }
                catch (BarCodeRecognitionException ex)
                {
                    // Other recognition errors are reported and the process stops.
                    Console.WriteLine($"Recognition error: {ex.Message}");
                    break;
                }
                catch (Exception ex)
                {
                    // Unexpected errors.
                    Console.WriteLine($"Unexpected error: {ex.Message}");
                    break;
                }
            }
        }
    }
}