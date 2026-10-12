// Title: Generate QR Code batch with cancellation token support
// Description: Demonstrates creating multiple QR Code barcodes using Aspose.BarCode and shows how to cancel a long-running batch operation via a CancellationToken.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.QR, configure QR error correction, and save images in PNG format. Typical use cases include batch creation of QR codes for inventory, tickets, or marketing materials where operations may need to be aborted gracefully. Developers often need to manage long-running barcode generation tasks and implement cancellation patterns using CancellationToken.
// Prompt: Generate QR Code barcode and implement cancellation token support for long batch operations.
// Tags: qr, barcode, generation, cancellation token, batch processing, aspose.barcode, png

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a batch of QR Code barcodes and demonstrates cancellation support.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Sets up the output directory, starts a cancellation timer,
    /// and invokes the QR batch generation method.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Create a unique temporary folder for the generated QR code images.
        string outputFolder = Path.Combine(Path.GetTempPath(), "QrBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        Console.WriteLine($"Output folder: {outputFolder}");

        // Use a CancellationTokenSource to allow the batch operation to be cancelled.
        using (CancellationTokenSource cts = new CancellationTokenSource())
        {
            // Schedule cancellation after 2 seconds to demonstrate the feature.
            Task.Run(() =>
            {
                Thread.Sleep(2000);
                Console.WriteLine("Cancellation requested.");
                cts.Cancel();
            });

            // Generate the QR codes, passing the cancellation token.
            GenerateQrBatch(outputFolder, cts.Token);
        }

        Console.WriteLine("Program completed.");
    }

    /// <summary>
    /// Generates up to ten QR Code images from a predefined set of sample texts.
    /// The method respects the provided <see cref="CancellationToken"/> and stops
    /// processing if cancellation is requested.
    /// </summary>
    /// <param name="folderPath">Directory where PNG files will be saved.</param>
    /// <param name="token">Token used to observe cancellation requests.</param>
    static void GenerateQrBatch(string folderPath, CancellationToken token)
    {
        // Sample data to encode into QR codes.
        string[] sampleTexts = new string[]
        {
            "First QR",
            "Second QR",
            "Third QR",
            "Fourth QR",
            "Fifth QR",
            "Sixth QR"
        };

        // Limit the number of generated barcodes to the smaller of the sample count or 10.
        int maxCount = Math.Min(sampleTexts.Length, 10);
        for (int i = 0; i < maxCount; i++)
        {
            // Check for cancellation before processing each item.
            if (token.IsCancellationRequested)
            {
                Console.WriteLine($"Operation cancelled before generating item {i + 1}.");
                break;
            }

            string text = sampleTexts[i];
            string filePath = Path.Combine(folderPath, $"qr_{i + 1}.png");

            // Create a QR Code generator with the specified text.
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, text))
            {
                // Configure visual properties: module size and error correction level.
                generator.Parameters.Barcode.XDimension.Pixels = 4f;
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

                // Save the generated QR code as a PNG image.
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            Console.WriteLine($"Generated QR code {i + 1}: {filePath}");
        }
    }
}