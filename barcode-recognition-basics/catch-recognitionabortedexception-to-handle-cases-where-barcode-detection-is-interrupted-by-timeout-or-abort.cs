// Title: Demonstrate handling RecognitionAbortedException during barcode recognition with timeout
// Description: This example generates a Code128 barcode, then attempts to read it using an intentionally short timeout to trigger a RecognitionAbortedException, showing how to catch and handle the exception.
// Category-Description: This sample belongs to the Aspose.BarCode barcode recognition category, illustrating the use of BarCodeReader, setting a timeout, and handling recognition abort scenarios. Developers working with barcode scanning often need to manage timeouts, aborts, and exceptions to ensure robust applications. The example showcases key API classes such as BarcodeGenerator, BarCodeReader, and RecognitionAbortedException.
// Prompt: Catch RecognitionAbortedException to handle cases where barcode detection is interrupted by timeout or abort.
// Tags: barcode, code128, recognitionabortedexception, timeout, exception-handling, generation, reading, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a barcode, attempting to read it with a short timeout, and handling <see cref="RecognitionAbortedException"/>.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the sample. Generates a barcode image, reads it with a forced timeout, and catches any <see cref="RecognitionAbortedException"/>.
    /// </summary>
    static void Main()
    {
        // Create a temporary directory for the sample files
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define the full path for the generated barcode image
        string barcodePath = Path.Combine(tempDir, "sample.png");

        // Generate a simple Code128 barcode image and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify the file was created before attempting to read it
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Attempt to read the barcode with a very short timeout to trigger abort
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            // Set timeout to 1 millisecond (likely to cause a timeout)
            reader.Timeout = 1;

            try
            {
                // Read all barcodes from the image
                var results = reader.ReadBarCodes();
                foreach (var result in results)
                {
                    Console.WriteLine($"CodeText: {result.CodeText}");
                    Console.WriteLine($"CodeType: {result.CodeTypeName}");
                }
            }
            catch (RecognitionAbortedException ex)
            {
                // Handle the case where recognition was aborted due to timeout or external abort
                Console.WriteLine($"Recognition aborted after {ex.ExecutionTime} ms: {ex.Message}");
            }
        }

        // Clean up temporary files
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program exit
        }
    }
}