using System;
using System.Collections.Generic;
using System.Threading;

namespace BarcodeThreadPoolHelper
{
    /// <summary>
    /// Demonstrates configuring the ThreadPool based on a list of barcode files
    /// and simulating processing of each file.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the application.
        /// </summary>
        static void Main()
        {
            // Define a sample list of barcode file paths (replace with real paths as needed)
            var barcodeFiles = new List<string>
            {
                "barcode1.png",
                "barcode2.png",
                "barcode3.png"
            };

            // Configure the ThreadPool's minimum worker threads based on the number of files
            ConfigureThreadPoolMinThreads(barcodeFiles.Count);

            // Iterate over each file and simulate processing (real barcode logic would go here)
            foreach (var file in barcodeFiles)
            {
                // Output which file is being processed and on which thread
                Console.WriteLine($"Processing {file} on thread {Thread.CurrentThread.ManagedThreadId}");
                // Placeholder for barcode generation/recognition logic
            }

            // Indicate that all files have been processed
            Console.WriteLine("All barcode files processed.");
        }

        /// <summary>
        /// Sets the ThreadPool minimum worker threads to at least the number of barcode files.
        /// </summary>
        /// <param name="barcodeFileCount">Number of barcode files to be processed.</param>
        static void ConfigureThreadPoolMinThreads(int barcodeFileCount)
        {
            // Validate input to ensure a non‑negative file count
            if (barcodeFileCount < 0)
                throw new ArgumentOutOfRangeException(nameof(barcodeFileCount), "File count cannot be negative.");

            // Retrieve the current minimum thread settings from the ThreadPool
            ThreadPool.GetMinThreads(out int currentWorkerThreads, out int completionPortThreads);

            // Determine the desired number of worker threads (at least as many as files)
            int desiredWorkerThreads = Math.Max(currentWorkerThreads, barcodeFileCount);

            // Apply the new minimum settings; completion port threads remain unchanged
            bool result = ThreadPool.SetMinThreads(desiredWorkerThreads, completionPortThreads);

            // Report the outcome of the ThreadPool configuration
            Console.WriteLine($"ThreadPool minimum worker threads set to {desiredWorkerThreads}: {(result ? "Success" : "Failed")}");
        }
    }
}