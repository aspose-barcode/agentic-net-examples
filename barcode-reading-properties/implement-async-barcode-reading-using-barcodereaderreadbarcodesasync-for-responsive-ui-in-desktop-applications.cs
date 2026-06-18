using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode, saving it to a temporary file,
/// reading it back, and outputting the decoded information.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode, reads it, and displays results.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static async Task Main(string[] args)
    {
        // Define a temporary file path for the barcode image.
        string tempFile = Path.Combine(Path.GetTempPath(), "sample_barcode.png");

        // Generate a Code128 barcode with the value "1234567890" and save it to the temp file.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(tempFile);
        }

        // Ensure the barcode image was successfully created.
        if (!File.Exists(tempFile))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Load the saved image into a Bitmap for decoding.
        using (var bitmap = new Bitmap(tempFile))
        {
            // Create a reader capable of decoding all supported barcode types.
            using (var reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
            {
                // Perform the decoding on a background thread to avoid blocking.
                BarCodeResult[] results = await Task.Run(() => reader.ReadBarCodes());

                // Iterate through all detected barcodes and display their type and value.
                foreach (var result in results)
                {
                    Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                    Console.WriteLine($"Decoded Text: {result.CodeText}");
                }
            }
        }

        // Attempt to delete the temporary file; ignore any exceptions.
        try
        {
            File.Delete(tempFile);
        }
        catch
        {
            // Suppress any errors that occur during cleanup.
        }
    }
}