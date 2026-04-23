using System;
using System.Threading;

namespace BarcodeThreadPoolHelper
{
    class Program
    {
        static void Main()
        {
            // Sample list of barcode files to process (using dummy file names)
            string[] barcodeFiles = new string[]
            {
                "barcode1.png",
                "barcode2.png",
                "barcode3.png",
                "barcode4.png",
                "barcode5.png"
            };

            // Configure the ThreadPool based on the number of files
            ConfigureThreadPool(barcodeFiles.Length);

            // Display the configured minimum worker threads
            ThreadPool.GetMinThreads(out int workerThreads, out int completionPortThreads);
            Console.WriteLine($"ThreadPool minimum worker threads set to: {workerThreads}");
            Console.WriteLine($"ThreadPool minimum completion port threads remain: {completionPortThreads}");
        }

        /// <summary>
        /// Adjusts the ThreadPool minimum worker threads based on the number of barcode files to process.
        /// </summary>
        /// <param name="fileCount">Number of barcode files that will be processed.</param>
        static void ConfigureThreadPool(int fileCount)
        {
            if (fileCount < 0)
                throw new ArgumentOutOfRangeException(nameof(fileCount), "File count cannot be negative.");

            // Retrieve current minimum thread settings
            ThreadPool.GetMinThreads(out int currentWorkerThreads, out int currentCompletionPortThreads);

            // Determine a desired minimum number of worker threads.
            // Use the greater of:
            //   - Existing minimum
            //   - Twice the number of logical processors (a common heuristic)
            //   - The number of files to process (ensuring at least one thread per file)
            int desiredWorkerThreads = Math.Max(currentWorkerThreads,
                                        Math.Max(Environment.ProcessorCount * 2, fileCount));

            // Apply the new minimum worker thread count while keeping the completion port threads unchanged
            ThreadPool.SetMinThreads(desiredWorkerThreads, currentCompletionPortThreads);
        }
    }
}