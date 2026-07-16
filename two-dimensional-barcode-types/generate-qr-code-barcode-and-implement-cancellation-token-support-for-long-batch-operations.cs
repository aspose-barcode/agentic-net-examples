// Title: Generate QR Code batch with cancellation support
// Description: Demonstrates creating multiple QR Code barcodes and handling cancellation for long-running batch operations.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and QRErrorLevel classes. It illustrates typical batch processing scenarios where developers need to generate many barcodes efficiently while providing responsive cancellation handling via CancellationToken.
// Prompt: Generate QR Code barcode and implement cancellation token support for long batch operations.
// Tags: qr code, barcode generation, batch processing, cancellation token, aspose.barcode, encode types, qrcode

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides an entry point for generating a batch of QR Code barcodes with cancellation support.
/// </summary>
class Program
{
    /// <summary>
    /// Asynchronously generates QR Code images for a set of texts, respecting a cancellation token.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static async Task Main(string[] args)
    {
        // Create a CancellationTokenSource that will trigger cancellation after a short delay.
        using (var cts = new CancellationTokenSource())
        {
            // Cancel after 5 seconds to simulate a timeout scenario.
            _ = Task.Delay(TimeSpan.FromSeconds(5)).ContinueWith(_ => cts.Cancel());

            // Sample texts to encode into QR codes.
            string[] sampleTexts = new[] { "Hello", "World", "Aspose", "Barcode", "QR" };

            // Generate the QR codes batch, passing the cancellation token.
            await GenerateQrBatchAsync(sampleTexts, cts.Token);
        }

        // Inform the user that batch processing has completed (or was cancelled).
        Console.WriteLine("Batch processing finished.");
    }

    /// <summary>
    /// Generates QR Code images for each provided text string, checking for cancellation between items.
    /// </summary>
    /// <param name="texts">Array of strings to encode as QR codes.</param>
    /// <param name="token">Cancellation token to observe.</param>
    private static async Task GenerateQrBatchAsync(string[] texts, CancellationToken token)
    {
        // Iterate over each text item sequentially.
        for (int i = 0; i < texts.Length; i++)
        {
            // Throw if cancellation has been requested before processing the next item.
            token.ThrowIfCancellationRequested();

            string codeText = texts[i];
            string fileName = $"qr_{i + 1}.png";

            // Create a BarcodeGenerator for QR encoding with the current text.
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
            {
                // Configure high error correction level for better resilience.
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

                // Set image size using interpolation mode for smoother scaling.
                generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 300f;

                // Save the generated QR code image to a PNG file.
                generator.Save(fileName);
            }

            // Output the location of the saved QR code file.
            Console.WriteLine($"Saved QR code '{codeText}' to {Path.GetFullPath(fileName)}");

            // Yield control to allow other awaiting tasks to run without blocking the thread.
            await Task.Yield();
        }
    }
}