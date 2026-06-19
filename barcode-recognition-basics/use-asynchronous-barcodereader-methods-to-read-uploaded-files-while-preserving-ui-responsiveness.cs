using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates asynchronous barcode generation and recognition using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a sample barcode image if missing,
    /// then reads and displays any barcodes found in the image asynchronously.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static async Task Main(string[] args)
    {
        // Define the path for the sample barcode image.
        string imagePath = "sample.png";

        // Generate a barcode image if it does not already exist.
        if (!File.Exists(imagePath))
        {
            // Create a barcode generator for Code128 with the text "AsyncDemo".
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "AsyncDemo"))
            {
                // Save the generated barcode to the specified file.
                generator.Save(imagePath);
            }
        }

        // Verify the file exists before attempting to read it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Asynchronously read barcodes from the image to avoid blocking the UI thread.
        BarCodeResult[] results = await Task.Run(() =>
        {
            // Initialize a barcode reader that supports all barcode types.
            using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
            {
                // Read and return all barcodes found in the image.
                return reader.ReadBarCodes();
            }
        });

        // Output the detected barcode information to the console.
        foreach (var result in results)
        {
            Console.WriteLine($"BarCode Type: {result.CodeTypeName}");
            Console.WriteLine($"BarCode Text: {result.CodeText}");
        }
    }
}