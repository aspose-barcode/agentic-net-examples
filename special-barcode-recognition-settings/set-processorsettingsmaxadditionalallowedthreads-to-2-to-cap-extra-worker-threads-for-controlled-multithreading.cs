// Title: Controlling Barcode Reader Thread Pool Size
// Description: Demonstrates how to limit additional worker threads for barcode reading using Aspose.BarCode's ProcessorSettings.
// Category-Description: This example belongs to the Aspose.BarCode multithreading management category, illustrating the use of BarCodeReader.ProcessorSettings to cap extra threads. Developers working with high‑throughput barcode scanning often need to control thread usage to avoid resource exhaustion. The snippet shows generating a Code128 barcode, configuring thread limits, and reading the barcode.
// Prompt: Set ProcessorSettings.MaxAdditionalAllowedThreads to 2 to cap extra worker threads for controlled multithreading.
// Tags: code128, multithreading, png, barcodegenerator, barcodereader, processorsettings

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates setting a limit on additional worker threads for barcode reading and processing a sample Code128 barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, configures thread limits, reads the barcode, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the sample barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a Code128 barcode image and save it as PNG
        GenerateBarcode(barcodePath);

        // Limit the number of additional worker threads used by the barcode reader
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = 2;

        // Initialize the barcode reader for Code128 and read the generated image
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            var results = reader.ReadBarCodes();
            foreach (var result in results)
            {
                Console.WriteLine($"Detected: {result.CodeTypeName} - {result.CodeText}");
            }
        }

        // Attempt to delete the temporary folder and its contents
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Suppress any exceptions during cleanup to avoid breaking the flow
        }
    }

    /// <summary>
    /// Generates a Code128 barcode image with the specified text and saves it to the given path.
    /// </summary>
    /// <param name="path">Full file path where the PNG image will be saved.</param>
    static void GenerateBarcode(string path)
    {
        // Create a barcode generator for Code128 with sample data
        var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890");
        using (Bitmap bitmap = generator.GenerateBarCodeImage())
        {
            // Save the generated bitmap as a PNG file
            bitmap.Save(path, ImageFormat.Png);
        }
    }
}