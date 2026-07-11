// Title: Validate ProcessorSettings.MaxAdditionalAllowedThreads against system limits
// Description: Demonstrates how to ensure the MaxAdditionalAllowedThreads setting does not exceed the machine's logical processor count, adjusting it if necessary.
// Category-Description: This example belongs to the Aspose.BarCode threading configuration category, illustrating the use of BarCodeReader.ProcessorSettings to control parallel processing. Developers often need to limit additional threads to avoid oversubscription of CPU resources, especially in high‑throughput scanning scenarios. The snippet shows retrieving system processor count, defining safe bounds, and applying validated values.
// Prompt: Write a validation routine ensuring ProcessorSettings.MaxAdditionalAllowedThreads does not exceed system limits.
// Tags: barcode, threading, validation, processorsettings, aspose.barcode

using System;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Provides an example of validating and setting the maximum number of additional threads
/// allowed for Aspose.BarCode's <see cref="BarCodeReader.ProcessorSettings"/> based on system limits.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Retrieves the logical processor count, validates a desired
    /// thread count against a safe upper bound, and applies the validated value to the processor settings.
    /// </summary>
    static void Main()
    {
        // Retrieve the number of logical processors available on the current machine.
        int processorCount = Environment.ProcessorCount;

        // Example desired value for additional threads (could be sourced from configuration or arguments).
        // Here we intentionally set it to three times the processor count to demonstrate the validation.
        int desiredAdditionalThreads = processorCount * 3;

        // Define a safe upper bound for additional threads (e.g., twice the core count).
        int maxAllowed = processorCount * 2;

        // Ensure the requested thread count is not negative.
        if (desiredAdditionalThreads < 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(desiredAdditionalThreads),
                "MaxAdditionalAllowedThreads cannot be negative.");
        }

        // If the requested value exceeds the safe limit, adjust it down to the maximum allowed.
        if (desiredAdditionalThreads > maxAllowed)
        {
            Console.WriteLine(
                $"Requested MaxAdditionalAllowedThreads ({desiredAdditionalThreads}) exceeds system limit ({maxAllowed}). Adjusting to limit.");
            desiredAdditionalThreads = maxAllowed;
        }

        // Apply the validated (and possibly adjusted) thread count to the Aspose.BarCode processor settings.
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = desiredAdditionalThreads;

        // Output the final setting for verification.
        Console.WriteLine(
            $"ProcessorSettings.MaxAdditionalAllowedThreads is set to {BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads}.");
    }
}