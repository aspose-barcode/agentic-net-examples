using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation, recognition, and aborting the recognition process using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode, attempts to recognize it, aborts the recognition,
    /// and reports the outcome.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static async Task Main(string[] args)
    {
        // Create an in‑memory stream to hold the generated barcode image.
        using (var ms = new MemoryStream())
        {
            // Generate a Code128 barcode with the text "TestAbort" and save it as PNG into the memory stream.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "TestAbort"))
            {
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Reset stream position so it can be read from the beginning.
            ms.Position = 0;

            // Load the PNG image from the memory stream into a Bitmap for recognition.
            using (var bitmap = new Bitmap(ms))
            {
                // Initialize a barcode reader that can decode all supported barcode types.
                using (var reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
                {
                    // Start the recognition process on a background thread.
                    var recognitionTask = Task.Run(() =>
                    {
                        try
                        {
                            // Attempt to read barcodes; may throw if aborted.
                            return reader.ReadBarCodes();
                        }
                        catch (RecognitionAbortedException)
                        {
                            // Expected exception when Abort is called; return an empty result set.
                            return Array.Empty<BarCodeResult>();
                        }
                    });

                    // Allow the recognition task a brief moment to start before aborting.
                    await Task.Delay(100);
                    reader.Abort(); // Signal the reader to abort the ongoing recognition.

                    // Await the completion of the recognition task (either aborted or finished).
                    var results = await recognitionTask;

                    // Evaluate the results: an empty array indicates the abort was successful.
                    if (results.Length == 0)
                    {
                        Console.WriteLine("Recognition was aborted successfully.");
                    }
                    else
                    {
                        Console.WriteLine($"Recognition completed with {results.Length} result(s).");
                        // Output details of each recognized barcode.
                        foreach (var result in results)
                        {
                            Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
                        }
                    }
                }
            }
        }
    }
}