using System;
using System.Diagnostics;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    // Path for temporary barcode image
    private const string BarcodeFile = "barcode.png";

    static void Main()
    {
        // Generate a simple barcode image to be used for scanning
        GenerateBarcodeImage();

        const int scanCount = 500; // number of simulated scans

        // Measure performance with AllowIncorrectBarcodes = false
        var resultWithout = PerformScans(allowIncorrect: false, iterations: scanCount);
        Console.WriteLine($"AllowIncorrectBarcodes = false: Elapsed = {resultWithout.ElapsedMilliseconds} ms, CPU = {resultWithout.CpuMilliseconds} ms");

        // Measure performance with AllowIncorrectBarcodes = true
        var resultWith = PerformScans(allowIncorrect: true, iterations: scanCount);
        Console.WriteLine($"AllowIncorrectBarcodes = true : Elapsed = {resultWith.ElapsedMilliseconds} ms, CPU = {resultWith.CpuMilliseconds} ms");
    }

    // Generates a Code128 barcode and saves it to a file
    private static void GenerateBarcodeImage()
    {
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            generator.Save(BarcodeFile);
        }
    }

    // Performs a number of scans and returns timing information
    private static (long ElapsedMilliseconds, long CpuMilliseconds) PerformScans(bool allowIncorrect, int iterations)
    {
        var process = Process.GetCurrentProcess();
        var cpuStart = process.TotalProcessorTime;
        var stopwatch = Stopwatch.StartNew();

        for (int i = 0; i < iterations; i++)
        {
            using (var reader = new BarCodeReader(BarcodeFile, DecodeType.Code128))
            {
                // Use NormalQuality preset as base
                reader.QualitySettings = QualitySettings.NormalQuality;
                // Toggle AllowIncorrectBarcodes according to the test case
                reader.QualitySettings.AllowIncorrectBarcodes = allowIncorrect;

                // Perform the actual read (results are ignored for profiling)
                foreach (var result in reader.ReadBarCodes())
                {
                    // No operation; just iterate to ensure the read is executed
                }
            }
        }

        stopwatch.Stop();
        var cpuEnd = process.TotalProcessorTime;
        long cpuMs = (long)(cpuEnd - cpuStart).TotalMilliseconds;
        return (stopwatch.ElapsedMilliseconds, cpuMs);
    }
}