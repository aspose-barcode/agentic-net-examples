using System;
using System.IO;
using System.ComponentModel;
using System.Threading;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.Common;

class Program
{
    static void Main()
    {
        // Sample image files representing video frames.
        string[] frameFiles = { "frame1.png", "frame2.png", "frame3.png" };

        // Event to wait for background worker completion.
        using (var completedEvent = new ManualResetEvent(false))
        {
            // BackgroundWorker implements IDisposable, use full using statement.
            using (var worker = new BackgroundWorker())
            {
                // Configure what the worker does.
                worker.DoWork += (sender, args) =>
                {
                    // Enable use of all processor cores for each BarCodeReader call.
                    BarCodeReader.ProcessorSettings.UseAllCores = true;
                    // Optionally limit cores (uncomment if needed).
                    // BarCodeReader.ProcessorSettings.UseAllCores = false;
                    // BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Math.Max(1, Environment.ProcessorCount / 2);

                    foreach (string filePath in frameFiles)
                    {
                        if (!File.Exists(filePath))
                        {
                            Console.WriteLine($"File not found: {filePath}");
                            continue;
                        }

                        // BarCodeReader is IDisposable.
                        using (var reader = new BarCodeReader(filePath))
                        {
                            // Use high‑performance preset for faster recognition.
                            reader.QualitySettings = QualitySettings.HighPerformance;

                            // Read all barcodes in the current frame.
                            foreach (var result in reader.ReadBarCodes())
                            {
                                Console.WriteLine($"Frame: {Path.GetFileName(filePath)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                            }
                        }
                    }
                };

                // Signal completion (or error) to the main thread.
                worker.RunWorkerCompleted += (sender, args) =>
                {
                    if (args.Error != null)
                    {
                        Console.WriteLine($"Error during processing: {args.Error.Message}");
                    }
                    completedEvent.Set();
                };

                // Start processing.
                worker.RunWorkerAsync();

                // Wait until the worker finishes (no infinite loop).
                completedEvent.WaitOne();
            }
        }
    }
}