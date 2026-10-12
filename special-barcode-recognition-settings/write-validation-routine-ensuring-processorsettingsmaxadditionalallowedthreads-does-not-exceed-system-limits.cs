// Title: Validate MaxAdditionalAllowedThreads Setting for Aspose.BarCode Processor
// Description: Demonstrates how to ensure the ProcessorSettings.MaxAdditionalAllowedThreads value does not exceed the system's thread pool limits, preventing runtime errors.
// Category-Description: This example belongs to the Aspose.BarCode threading and performance configuration category. It shows how to query the .NET ThreadPool, compare against Aspose.BarCode's ProcessorSettings, and safely adjust the MaxAdditionalAllowedThreads property. Developers working with high‑throughput barcode scanning often need to tune thread usage to match hardware capabilities while staying within system constraints.
// Prompt: Write a validation routine ensuring ProcessorSettings.MaxAdditionalAllowedThreads does not exceed system limits.
// Tags: barcode, threading, validation, aspose.barcode, processorsettings, maxadditionalallowedthreads

using System;
using System.Threading;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Provides a sample program that validates and sets the maximum additional allowed threads for Aspose.BarCode processing.
/// </summary>
class Program
{
    /// <summary>
    /// Validates the desired thread count against the system's ThreadPool limits and applies it to BarCodeReader.ProcessorSettings.
    /// </summary>
    /// <param name="desiredValue">The requested number of additional threads.</param>
    static void ValidateMaxAdditionalAllowedThreads(int desiredValue)
    {
        // Ensure the requested value is non‑negative.
        if (desiredValue < 0)
            throw new ArgumentOutOfRangeException(nameof(desiredValue), "Value cannot be negative.");

        // Retrieve the maximum number of worker threads the ThreadPool can support.
        ThreadPool.GetMaxThreads(out int maxWorkerThreads, out int _);
        int limit = maxWorkerThreads;

        // Start with the desired value; adjust if it exceeds the system limit.
        int finalValue = desiredValue;
        if (desiredValue > limit)
        {
            Console.WriteLine($"Desired MaxAdditionalAllowedThreads ({desiredValue}) exceeds system max worker threads ({limit}). Adjusting to limit.");
            finalValue = limit;
        }

        // Apply the validated thread count to the Aspose.BarCode processor settings.
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = finalValue;
        Console.WriteLine($"ProcessorSettings.MaxAdditionalAllowedThreads set to {BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads}.");
    }

    /// <summary>
    /// Entry point of the sample. Calculates a sample thread count and invokes the validation routine.
    /// </summary>
    static void Main()
    {
        // Example value that may be higher than the system's allowed worker threads.
        int sampleValue = Environment.ProcessorCount * 4;
        Console.WriteLine($"Attempting to set MaxAdditionalAllowedThreads to {sampleValue}.");
        ValidateMaxAdditionalAllowedThreads(sampleValue);
    }
}