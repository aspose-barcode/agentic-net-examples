using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static async Task Main(string[] args)
    {
        // Generate a barcode image
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = "1234567890";
            generator.Save("barcode.png");
        }

        // Synchronous barcode reading
        var syncStopwatch = Stopwatch.StartNew();
        using (var reader = new BarCodeReader("barcode.png", DecodeType.Code128))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Sync read: {result.CodeText}");
            }
        }
        syncStopwatch.Stop();
        Console.WriteLine($"Synchronous read time: {syncStopwatch.ElapsedMilliseconds} ms");

        // Asynchronous barcode reading using Task.Run
        var asyncStopwatch = Stopwatch.StartNew();
        BarCodeResult[] asyncResults = await Task.Run(() =>
        {
            using (var reader = new BarCodeReader("barcode.png", DecodeType.Code128))
            {
                return reader.ReadBarCodes();
            }
        });
        asyncStopwatch.Stop();

        foreach (var result in asyncResults)
        {
            Console.WriteLine($"Async read: {result.CodeText}");
        }
        Console.WriteLine($"Asynchronous read time: {asyncStopwatch.ElapsedMilliseconds} ms");

        // Compare latency
        long reduction = syncStopwatch.ElapsedMilliseconds - asyncStopwatch.ElapsedMilliseconds;
        Console.WriteLine($"Latency reduction: {reduction} ms");
    }
}