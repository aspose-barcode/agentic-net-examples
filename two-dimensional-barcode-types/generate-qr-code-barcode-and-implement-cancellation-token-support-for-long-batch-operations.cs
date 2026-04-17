using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static async Task Main(string[] args)
    {
        // Prepare a small batch of sample texts.
        List<string> samples = new List<string>
        {
            "Sample 1",
            "Sample 2",
            "Sample 3",
            "Sample 4",
            "Sample 5"
        };

        // Output folder.
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "QrOutput");
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        using (CancellationTokenSource cts = new CancellationTokenSource())
        {
            // Cancel after a short delay to demonstrate cancellation support.
            Task cancelTask = Task.Delay(500).ContinueWith(_ => cts.Cancel());

            try
            {
                await GenerateQrBatchAsync(samples, outputFolder, cts.Token);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Batch operation was cancelled.");
            }

            await cancelTask; // Ensure the cancel task completes.
        }

        Console.WriteLine("Program finished.");
    }

    private static async Task GenerateQrBatchAsync(List<string> texts, string folder, CancellationToken token)
    {
        int index = 0;
        foreach (string text in texts)
        {
            token.ThrowIfCancellationRequested();

            string filePath = Path.Combine(folder, $"qr_{index + 1}.png");

            using (var generator = new BarcodeGenerator(EncodeTypes.QR))
            {
                // Set the data to encode.
                generator.CodeText = text;

                // Set high error correction level.
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

                // Save the QR code image.
                generator.Save(filePath);
            }

            Console.WriteLine($"Generated QR code {index + 1}: {filePath}");

            index++;

            // Simulate asynchronous work without blocking.
            await Task.Yield();
        }
    }
}