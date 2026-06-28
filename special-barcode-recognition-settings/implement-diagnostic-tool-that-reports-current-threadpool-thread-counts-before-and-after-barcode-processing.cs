using System;
using System.IO;
using System.Threading;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode;

/// <summary>
/// Demonstrates barcode generation, reading, and ThreadPool usage reporting.
/// </summary>
class Program
{
    /// <summary>
    /// Writes the current ThreadPool usage statistics to the console.
    /// </summary>
    /// <param name="stage">A label indicating the point in execution (e.g., "Before processing").</param>
    static void ReportThreadPool(string stage)
    {
        // Retrieve maximum thread counts for worker and I/O threads.
        ThreadPool.GetMaxThreads(out int maxWorker, out int maxIO);
        // Retrieve currently available thread counts.
        ThreadPool.GetAvailableThreads(out int availWorker, out int availIO);
        // Calculate used threads by subtracting available from maximum.
        int usedWorker = maxWorker - availWorker;
        int usedIO = maxIO - availIO;

        // Output the usage information.
        Console.WriteLine($"{stage} - ThreadPool usage:");
        Console.WriteLine($"  Worker threads: used {usedWorker} / max {maxWorker}");
        Console.WriteLine($"  IO threads:     used {usedIO} / max {maxIO}");
    }

    /// <summary>
    /// Application entry point. Generates a barcode, reads it back, reports ThreadPool usage,
    /// and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Report ThreadPool status before any processing.
        ReportThreadPool("Before processing");

        // Define a temporary file path for the barcode image.
        string tempFile = Path.Combine(Path.GetTempPath(), "sample_barcode.png");

        // -------------------------------------------------
        // Generate a barcode image and save it to the file.
        // -------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(tempFile);
        }

        // -------------------------------------------------
        // Read the barcode from the generated image.
        // -------------------------------------------------
        using (var reader = new BarCodeReader(tempFile, DecodeType.Code128))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected barcode type: {result.CodeTypeName}");
                Console.WriteLine($"Detected code text: {result.CodeText}");
            }
        }

        // Report ThreadPool status after processing.
        ReportThreadPool("After processing");

        // -------------------------------------------------
        // Clean up the temporary file if it exists.
        // -------------------------------------------------
        if (File.Exists(tempFile))
        {
            try
            {
                File.Delete(tempFile);
            }
            catch
            {
                // Ignore any cleanup errors to avoid crashing the program.
            }
        }
    }
}