// Title: Barcode decoding with fallback StripFNC setting
// Description: Demonstrates decoding a Code128 barcode and retrying with StripFNC enabled if the first attempt fails.
// Category-Description: This example belongs to Aspose.BarCode recognition operations, showcasing the use of BarCodeReader, BarcodeSettings, and DecodeType to read barcodes from images. Developers often need to handle Function Code (FNC) characters that may be present in Code128 symbols; toggling the StripFNC property provides a fallback mechanism for reliable decoding. The snippet serves as a reference for implementing robust barcode reading in .NET applications.
// Prompt: Implement a fallback mechanism that retries decoding with StripFNC true if initial attempt with false fails.
// Tags: code128, decoding, stripfnc, fallback, barcodereader, aspose.barcode, .net

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation and recognition with a fallback StripFNC setting.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode, attempts to decode it, and retries with StripFNC enabled if needed.
    /// </summary>
    static void Main()
    {
        // Generate a Code128 barcode image in memory.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ABC123"))
        {
            using (var ms = new MemoryStream())
            {
                // Save the generated barcode to the memory stream as PNG.
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading.

                // Load the image from the stream into a Bitmap for recognition.
                using (var bitmap = new Bitmap(ms))
                {
                    // Create a reader that supports all barcode types.
                    using (var reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
                    {
                        // First attempt: do not strip Function Code (FNC) characters.
                        reader.BarcodeSettings.StripFNC = false;
                        var results = reader.ReadBarCodes();

                        // Check if decoding succeeded.
                        if (results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText))
                        {
                            Console.WriteLine("Decoded with StripFNC = false: " + results[0].CodeText);
                        }
                        else
                        {
                            // Fallback: enable StripFNC and retry decoding.
                            reader.BarcodeSettings.StripFNC = true;
                            var retryResults = reader.ReadBarCodes();

                            if (retryResults.Length > 0 && !string.IsNullOrEmpty(retryResults[0].CodeText))
                            {
                                Console.WriteLine("Decoded with StripFNC = true: " + retryResults[0].CodeText);
                            }
                            else
                            {
                                Console.WriteLine("Failed to decode the barcode.");
                            }
                        }
                    }
                }
            }
        }
    }
}