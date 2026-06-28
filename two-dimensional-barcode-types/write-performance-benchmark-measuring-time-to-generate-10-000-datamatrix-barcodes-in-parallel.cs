using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates parallel generation of DataMatrix barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a set number of barcodes in parallel and measures execution time.
    /// </summary>
    static void Main()
    {
        // Number of barcodes to generate; kept small for sample runner constraints
        const int barcodeCount = 10;

        // Stopwatch to measure total generation time
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        // Parallel loop to generate barcodes concurrently
        Parallel.For(0, barcodeCount, i =>
        {
            // Create a new barcode generator for each iteration to avoid shared state
            using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, $"Data{i:D4}"))
            {
                // Use a memory stream to hold the generated PNG image (no disk I/O)
                using (var ms = new MemoryStream())
                {
                    // Save the barcode image into the memory stream
                    generator.Save(ms, BarCodeImageFormat.Png);

                    // Reset stream position to the beginning for any subsequent read operations
                    ms.Position = 0;
                } // MemoryStream disposed here
            } // BarcodeGenerator disposed here
        });

        // Stop timing after all parallel tasks complete
        stopwatch.Stop();

        // Output results to console
        Console.WriteLine($"Generated {barcodeCount} DataMatrix barcodes in parallel.");
        Console.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds} ms");
    }
}