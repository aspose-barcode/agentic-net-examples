// Title: Generate QR Code batch with cancellation token support
// Description: Demonstrates creating a set of QR Code images and reading them back, using CancellationToken to allow graceful abort of long-running batch operations.
// Category-Description: This example belongs to the Aspose.BarCode batch processing category, showcasing the BarcodeGenerator for QR Code creation and BarCodeReader for QR Code recognition. It illustrates typical use cases such as bulk barcode generation, automated scanning, and cancellation handling in long-running tasks. Developers often need to generate many barcodes, process them in parallel, and provide responsive cancellation to improve application robustness.
// Prompt: Generate QR Code barcode and implement cancellation token support for long batch operations.
// Tags: qr code, barcode generation, barcode recognition, cancellation token, batch processing, png, aspose.barcode

using System;
using System.IO;
using System.Threading;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates QR Code batch generation and reading with cancellation token support using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a temporary folder, generates QR Code images, reads them back, and cleans up, while handling cancellation.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the batch operation
        string batchFolder = Path.Combine(Path.GetTempPath(), "QrBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);
        Console.WriteLine($"Batch folder: {batchFolder}");

        // Set up a cancellation token that will cancel after 2 seconds
        using (var cts = new CancellationTokenSource())
        {
            cts.CancelAfter(TimeSpan.FromSeconds(2));
            try
            {
                // Generate QR Code images; operation can be cancelled via the token
                GenerateQrBatch(batchFolder, 5, cts.Token);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Batch generation was cancelled.");
            }
        }

        // Read back the generated barcodes (if any) respecting cancellation
        Console.WriteLine("Starting batch read...");
        using (var ctsRead = new CancellationTokenSource())
        {
            // Cancel after 3 seconds to demonstrate abort during read
            ctsRead.CancelAfter(TimeSpan.FromSeconds(3));
            ReadQrBatch(batchFolder, ctsRead.Token);
        }

        // Clean up the temporary folder
        try
        {
            Directory.Delete(batchFolder, true);
            Console.WriteLine("Temporary folder deleted.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to delete temporary folder: {ex.Message}");
        }
    }

    /// <summary>
    /// Generates a batch of QR Code images in the specified folder.
    /// </summary>
    /// <param name="folderPath">Destination folder for generated images.</param>
    /// <param name="count">Number of QR Code images to create.</param>
    /// <param name="token">Cancellation token to abort the operation.</param>
    static void GenerateQrBatch(string folderPath, int count, CancellationToken token)
    {
        for (int i = 0; i < count; i++)
        {
            // Throw if cancellation has been requested
            token.ThrowIfCancellationRequested();

            string codeText = $"QR Code {i + 1}";
            string filePath = Path.Combine(folderPath, $"qr_{i + 1}.png");

            // Create a QR Code generator with the desired text
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
            {
                // Set module size (pixel dimension of a single QR element)
                generator.Parameters.Barcode.XDimension.Pixels = 4f;
                // Set error correction level to Medium
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;
                // Save the generated barcode as PNG
                generator.Save(filePath, BarCodeImageFormat.Png);
                Console.WriteLine($"Generated: {filePath}");
            }
        }
    }

    /// <summary>
    /// Reads QR Code images from the specified folder, respecting cancellation.
    /// </summary>
    /// <param name="folderPath">Folder containing QR Code images.</param>
    /// <param name="token">Cancellation token to abort the read operation.</param>
    static void ReadQrBatch(string folderPath, CancellationToken token)
    {
        // List of files we know were created
        var files = new string[]
        {
            Path.Combine(folderPath, "qr_1.png"),
            Path.Combine(folderPath, "qr_2.png"),
            Path.Combine(folderPath, "qr_3.png"),
            Path.Combine(folderPath, "qr_4.png"),
            Path.Combine(folderPath, "qr_5.png")
        };

        foreach (var file in files)
        {
            // Throw if cancellation has been requested
            token.ThrowIfCancellationRequested();

            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            try
            {
                // Initialize a QR Code reader for the current file
                using (var reader = new BarCodeReader(file, DecodeType.QR))
                {
                    // Optional: set a short timeout (ms) to avoid long waits
                    reader.Timeout = 5000;
                    var results = reader.ReadBarCodes();
                    foreach (var result in results)
                    {
                        Console.WriteLine($"Read from {Path.GetFileName(file)}: {result.CodeText}");
                    }
                }
            }
            catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
            {
                // Skip files that cannot be loaded as images
                Console.WriteLine($"Skipping unreadable file: {file}");
            }
            catch (Exception ex)
            {
                // Log any other errors encountered during reading
                Console.WriteLine($"Error reading {file}: {ex.Message}");
            }
        }
    }
}