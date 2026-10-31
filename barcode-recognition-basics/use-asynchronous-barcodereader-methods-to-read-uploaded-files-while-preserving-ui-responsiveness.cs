// Title: Asynchronous barcode reading from generated image
// Description: Demonstrates generating a barcode image and reading it asynchronously to keep the UI responsive.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator to create a barcode, BarCodeReader for decoding, and asynchronous patterns (Task.Run) to perform recognition without blocking the UI thread. Developers often need to process uploaded images in web or desktop apps while maintaining responsiveness, and this snippet illustrates typical API usage for such scenarios.
// Prompt: Use asynchronous BarCodeReader methods to read uploaded files while preserving UI responsiveness.
// Tags: barcode, asynchronous, reader, generation, code128, png, aspnet, aspnetcore, aspose.barcode

using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates asynchronous barcode reading using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode image, reads it asynchronously, and cleans up temporary files.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    static async Task Main(string[] args)
    {
        // Create a temporary folder for the sample barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a sample barcode image using Code128 symbology
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify the file exists before attempting to read
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Barcode image file was not created.");
            Cleanup(tempFolder);
            return;
        }

        // Configure processor settings (optional) to utilize all CPU cores
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Environment.ProcessorCount;

        // Asynchronously read barcodes from the generated image
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
        {
            // Run the synchronous ReadBarCodes method on a background thread
            BarCodeResult[] results = await Task.Run(() => reader.ReadBarCodes());

            Console.WriteLine($"Found {results.Length} barcode(s):");
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
                Console.WriteLine($"Quality: {result.ReadingQuality}");
                var bounds = result.Region.Rectangle;
                Console.WriteLine($"Region - X:{bounds.X}, Y:{bounds.Y}, Width:{bounds.Width}, Height:{bounds.Height}, Angle:{result.Region.Angle}");
            }
        }

        // Clean up temporary files and folder
        Cleanup(tempFolder);
    }

    /// <summary>
    /// Deletes the specified folder and its contents, handling any exceptions.
    /// </summary>
    /// <param name="folderPath">Path of the folder to delete.</param>
    static void Cleanup(string folderPath)
    {
        try
        {
            if (Directory.Exists(folderPath))
            {
                Directory.Delete(folderPath, true);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cleanup failed: {ex.Message}");
        }
    }
}