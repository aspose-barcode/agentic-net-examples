using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Path to a sample image used to simulate video frames.
        string sampleImagePath = "frame.jpg";

        // Validate the sample image exists.
        if (!File.Exists(sampleImagePath))
        {
            Console.WriteLine($"Sample image file not found: {sampleImagePath}");
            return;
        }

        // Number of frames to process (simulating 5 seconds of video at 1 fps).
        int totalFrames = 5;

        long totalProcessingMs = 0;
        int processedFrames = 0;

        for (int i = 0; i < totalFrames; i++)
        {
            // Simulate extracting a frame from the video by loading the same image.
            using (Bitmap frame = new Bitmap(sampleImagePath))
            {
                Stopwatch stopwatch = Stopwatch.StartNew();

                try
                {
                    using (BarCodeReader reader = new BarCodeReader())
                    {
                        // Set a timeout to avoid hanging on large images.
                        reader.Timeout = 5000;

                        // Use high‑performance mode for faster processing.
                        reader.QualitySettings = QualitySettings.HighPerformance;

                        // Provide the bitmap to the reader.
                        reader.SetBarCodeImage(frame);

                        // Perform barcode recognition.
                        BarCodeResult[] results = reader.ReadBarCodes();

                        foreach (BarCodeResult result in results)
                        {
                            Console.WriteLine($"Frame {i + 1}: Type={result.CodeTypeName}, Text={result.CodeText}");
                        }
                    }
                }
                catch (RecognitionAbortedException ex)
                {
                    Console.WriteLine($"Recognition aborted on frame {i + 1}: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing frame {i + 1}: {ex.Message}");
                }

                stopwatch.Stop();
                totalProcessingMs += stopwatch.ElapsedMilliseconds;
                processedFrames++;
            }
        }

        if (processedFrames > 0)
        {
            double averageMs = (double)totalProcessingMs / processedFrames;
            Console.WriteLine($"Average processing time per frame: {averageMs:F2} ms");
        }
        else
        {
            Console.WriteLine("No frames were processed.");
        }
    }
}