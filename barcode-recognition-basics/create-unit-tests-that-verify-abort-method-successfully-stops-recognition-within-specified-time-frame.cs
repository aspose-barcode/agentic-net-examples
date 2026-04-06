using System;
using System.IO;
using System.Threading;
using System.Diagnostics;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    // Shared data for the recognition thread
    private static BarCodeResult[] _results;
    private static Exception _exception;

    // Thread method that performs barcode recognition
    private static void Recognize(object obj)
    {
        var reader = (BarCodeReader)obj;
        try
        {
            _results = reader.ReadBarCodes();
        }
        catch (Exception ex)
        {
            // Capture any exception (including RecognitionAbortedException)
            _exception = ex;
        }
    }

    static void Main()
    {
        // Create a large bitmap and draw several barcodes onto it to make recognition take noticeable time
        const int cellSize = 200;
        const int rows = 5;
        const int cols = 5;
        int width = cellSize * cols;
        int height = cellSize * rows;

        using (var largeBmp = new Bitmap(width, height))
        using (var graphics = Graphics.FromImage(largeBmp))
        {
            graphics.Clear(Color.White);

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    string text = $"CODE{r}{c}";
                    using (var generator = new BarcodeGenerator(EncodeTypes.Code128, text))
                    {
                        using (var ms = new MemoryStream())
                        {
                            generator.Save(ms, BarCodeImageFormat.Png);
                            ms.Position = 0;
                            using (var barcodeBmp = new Bitmap(ms))
                            {
                                int x = c * cellSize;
                                int y = r * cellSize;
                                graphics.DrawImage(barcodeBmp, x, y, cellSize, cellSize);
                            }
                        }
                    }
                }
            }

            // Initialize the reader with the generated image and the desired decode type
            using (var reader = new BarCodeReader(largeBmp, DecodeType.Code128))
            {
                // Start recognition in a separate thread
                Thread recognizerThread = new Thread(Recognize);
                recognizerThread.Start(reader);

                // Give the recognizer a short moment to start (without using Thread.Sleep)
                Task.Delay(100).Wait();

                // Abort the recognition
                Stopwatch abortStopwatch = Stopwatch.StartNew();
                reader.Abort();
                abortStopwatch.Stop();

                // Wait for the thread to finish
                recognizerThread.Join();

                // Verify that abort was effective
                bool abortExceptionCaught = _exception is RecognitionAbortedException;
                bool resultsEmpty = _results == null || _results.Length == 0;

                Console.WriteLine("Abort call duration (ms): " + abortStopwatch.ElapsedMilliseconds);
                Console.WriteLine("Exception caught: " + (_exception?.GetType().Name ?? "none"));
                Console.WriteLine("Results count: " + (_results?.Length ?? 0));

                // Simple test assertions
                if (abortExceptionCaught || resultsEmpty)
                {
                    Console.WriteLine("Test Passed: Abort successfully stopped recognition.");
                }
                else
                {
                    Console.WriteLine("Test Failed: Recognition was not aborted as expected.");
                }
            }
        }
    }
}