using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating QR code images using Aspose.BarCode and tracks memory usage.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a batch of QR codes, saves them to a temporary folder,
    /// and reports memory usage before, during, and after the process.
    /// </summary>
    static void Main()
    {
        // Prepare output folder in the system's temporary directory.
        string outputFolder = Path.Combine(Path.GetTempPath(), "AsposeBarCodeQR");
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Sample QR code texts (small batch for safe execution).
        string[] qrTexts = new string[]
        {
            "Sample QR 1",
            "Sample QR 2",
            "Sample QR 3",
            "Sample QR 4",
            "Sample QR 5"
        };

        // Get a reference to the current process to monitor memory usage.
        Process currentProcess = Process.GetCurrentProcess();

        // Report memory usage before starting the batch.
        Console.WriteLine("Memory usage before batch: {0} MB", BytesToMegabytes(currentProcess.PrivateMemorySize64));

        // Iterate over each text, generate a QR code, and save it to a file.
        for (int i = 0; i < qrTexts.Length; i++)
        {
            string text = qrTexts[i];
            string filePath = Path.Combine(outputFolder, $"qr_{i + 1}.png");

            // Generate QR code and save to file.
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, text))
            {
                // Optional: set high error correction level for better resilience.
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;
                generator.Save(filePath);
            }

            // Capture and display memory usage after each generation.
            long memoryBytes = currentProcess.PrivateMemorySize64;
            Console.WriteLine("Generated QR {0}: Memory = {1} MB", i + 1, BytesToMegabytes(memoryBytes));
        }

        // Report memory usage after completing the batch.
        Console.WriteLine("Memory usage after batch: {0} MB", BytesToMegabytes(currentProcess.PrivateMemorySize64));
        Console.WriteLine("QR code images saved to: " + outputFolder);
    }

    /// <summary>
    /// Converts a byte value to megabytes, rounded to two decimal places.
    /// </summary>
    /// <param name="bytes">The size in bytes.</param>
    /// <returns>The size in megabytes.</returns>
    static double BytesToMegabytes(long bytes)
    {
        return Math.Round((double)bytes / (1024 * 1024), 2);
    }
}