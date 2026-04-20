using System;
using System.Threading;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Determine the maximum number of worker threads allowed by the ThreadPool
        ThreadPool.GetMaxThreads(out int maxWorkerThreads, out int maxCompletionPortThreads);

        // Example requested value for additional threads
        int requestedAdditionalThreads = Environment.ProcessorCount * 3; // sample value

        try
        {
            ValidateMaxAdditionalAllowedThreads(requestedAdditionalThreads, maxWorkerThreads);
            // Apply the validated setting
            BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = requestedAdditionalThreads;
            Console.WriteLine($"ProcessorSettings.MaxAdditionalAllowedThreads set to {requestedAdditionalThreads}.");
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        // Display current setting
        int currentSetting = BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads;
        Console.WriteLine($"Current MaxAdditionalAllowedThreads: {currentSetting}");
        Console.WriteLine($"System max worker threads: {maxWorkerThreads}");
    }

    static void ValidateMaxAdditionalAllowedThreads(int requested, int systemMaxWorkerThreads)
    {
        if (requested < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(requested), "Requested thread count cannot be negative.");
        }

        // Ensure the requested additional threads do not exceed the system's max worker threads
        if (requested > systemMaxWorkerThreads)
        {
            throw new ArgumentOutOfRangeException(
                nameof(requested),
                $"Requested thread count ({requested}) exceeds the system's maximum worker threads ({systemMaxWorkerThreads}).");
        }
    }
}