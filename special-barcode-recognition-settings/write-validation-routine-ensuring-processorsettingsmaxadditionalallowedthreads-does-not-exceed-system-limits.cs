using System;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates validation and configuration of the maximum number of additional threads
/// used by Aspose.BarCode's barcode processing engine.
/// </summary>
class Program
{
    /// <summary>
    /// Validates and sets the maximum number of additional threads for barcode processing.
    /// </summary>
    /// <param name="requestedThreads">The desired number of additional threads.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="requestedThreads"/> is negative or exceeds the system limit.
    /// </exception>
    static void SetMaxAdditionalAllowedThreads(int requestedThreads)
    {
        // Calculate a reasonable system limit (e.g., 4 times the logical processor count).
        int systemLimit = Environment.ProcessorCount * 4;

        // Ensure the requested thread count is not negative.
        if (requestedThreads < 0)
            throw new ArgumentOutOfRangeException(
                nameof(requestedThreads),
                "Thread count cannot be negative.");

        // Ensure the requested thread count does not exceed the calculated system limit.
        if (requestedThreads > systemLimit)
            throw new ArgumentOutOfRangeException(
                nameof(requestedThreads),
                $"Thread count exceeds system limit of {systemLimit} threads.");

        // Apply the validated value to the Aspose.BarCode processor settings.
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = requestedThreads;

        // Inform the user of the successful configuration.
        Console.WriteLine($"ProcessorSettings.MaxAdditionalAllowedThreads set to {requestedThreads}");
    }

    /// <summary>
    /// Entry point demonstrating thread count validation.
    /// </summary>
    static void Main()
    {
        // Attempt to set a valid thread count and handle any validation errors.
        try
        {
            SetMaxAdditionalAllowedThreads(8);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Validation error: {ex.Message}");
        }

        // Attempt to set an invalid thread count (exceeds limit) and handle the error.
        try
        {
            SetMaxAdditionalAllowedThreads(1000);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Validation error: {ex.Message}");
        }
    }
}