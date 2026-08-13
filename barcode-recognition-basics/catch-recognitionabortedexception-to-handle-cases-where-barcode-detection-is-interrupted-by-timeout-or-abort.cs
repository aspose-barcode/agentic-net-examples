// Title: Barcode recognition with timeout handling
// Description: Demonstrates generating a Code128 barcode, setting a very low timeout, and catching RecognitionAbortedException when detection is aborted.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator, BarCodeReader, and DecodeType classes to create a barcode image in memory and attempt to read it with a custom timeout. Developers often need to handle recognition timeouts or aborts, making exception handling for RecognitionAbortedException essential for robust barcode scanning solutions.
// Prompt: Catch RecognitionAbortedException to handle cases where barcode detection is interrupted by timeout or abort.
// Tags: barcode, code128, timeout, recognitionabortedexception, generation, recognition, aspnet, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates a Code128 barcode, attempts to read it with an extremely low timeout,
/// and demonstrates handling of <see cref="RecognitionAbortedException"/> when the
/// recognition process is aborted (e.g., due to timeout).
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes barcode generation, sets a short timeout,
    /// and reads the barcode while handling possible abort scenarios.
    /// </summary>
    static void Main()
    {
        // Create a barcode generator for Code128 with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Generate the barcode image in memory
            using (Bitmap barcodeImage = generator.GenerateBarCodeImage())
            {
                // Initialize a barcode reader for the generated image, targeting Code128 symbology
                using (var reader = new BarCodeReader(barcodeImage, DecodeType.Code128))
                {
                    // Configure an extremely low timeout (1 ms) to force an abort condition
                    reader.Timeout = 1; // milliseconds

                    try
                    {
                        // Attempt to read barcodes; may throw RecognitionAbortedException
                        foreach (BarCodeResult result in reader.ReadBarCodes())
                        {
                            Console.WriteLine($"Detected Type: {result.CodeType}");
                            Console.WriteLine($"Detected Text: {result.CodeText}");
                        }
                    }
                    catch (RecognitionAbortedException ex)
                    {
                        // Handle recognition abort (e.g., timeout) gracefully
                        Console.WriteLine("Barcode recognition was aborted:");
                        Console.WriteLine(ex.Message);
                    }
                    catch (Exception ex)
                    {
                        // Fallback for any other unexpected errors
                        Console.WriteLine("An unexpected error occurred:");
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
    }
}