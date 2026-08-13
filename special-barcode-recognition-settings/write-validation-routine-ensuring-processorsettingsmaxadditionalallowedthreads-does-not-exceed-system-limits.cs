// Title: Validate MaxAdditionalAllowedThreads Setting for Aspose.BarCode
// Description: Demonstrates how to validate and safely set the MaxAdditionalAllowedThreads property of Aspose.BarCode's ProcessorSettings, ensuring it stays within system limits.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category, illustrating how to work with processor settings to control multithreading. It showcases the BarCodeReader class and its ProcessorSettings, a common requirement when optimizing barcode recognition performance on multi‑core systems. Developers often need to validate thread counts to avoid exceeding hardware capabilities while maximizing throughput.
// Prompt: Write a validation routine ensuring ProcessorSettings.MaxAdditionalAllowedThreads does not exceed system limits.
// Tags: barcode, validation, configuration, barcodereader, processorsettings

using System;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.Common;

/// <summary>
/// Provides a console example that validates and applies a thread count limit
/// to <see cref="BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads"/>.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Demonstrates validation with both an exceeding
    /// and a valid thread count, handling any validation errors gracefully.
    /// </summary>
    static void Main()
    {
        // Calculate sample values: one that exceeds the safe limit, one that is within the limit.
        int exceedingValue = Environment.ProcessorCount * 3; // Intentionally too high.
        int validValue = Environment.ProcessorCount * 2;     // Within the safe range.

        // Attempt to set the exceeding value and capture validation failure.
        Console.WriteLine("Attempting to set exceeding value:");
        try
        {
            ValidateMaxAdditionalAllowedThreads(exceedingValue);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Validation failed: {ex.Message}");
        }

        Console.WriteLine();

        // Attempt to set a valid value and confirm successful application.
        Console.WriteLine("Attempting to set valid value:");
        try
        {
            ValidateMaxAdditionalAllowedThreads(validValue);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Validation failed: {ex.Message}");
        }
    }

    /// <summary>
    /// Validates that the requested number of additional threads does not exceed a safe system limit.
    /// The safe limit is defined as twice the number of logical processors.
    /// </summary>
    /// <param name="requestedThreads">The number of additional threads to set.</param>
    static void ValidateMaxAdditionalAllowedThreads(int requestedThreads)
    {
        // Define a safe maximum based on the current environment.
        int safeMaximum = Environment.ProcessorCount * 2;

        // Guard against negative thread counts.
        if (requestedThreads < 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(requestedThreads),
                "Thread count cannot be negative.");
        }

        // Guard against values that exceed the calculated safe maximum.
        if (requestedThreads > safeMaximum)
        {
            throw new ArgumentOutOfRangeException(
                nameof(requestedThreads),
                $"Requested threads ({requestedThreads}) exceed the safe maximum ({safeMaximum}).");
        }

        // Apply the validated value to Aspose.BarCode processor settings.
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = requestedThreads;
        Console.WriteLine($"ProcessorSettings.MaxAdditionalAllowedThreads successfully set to {requestedThreads}.");
    }
}