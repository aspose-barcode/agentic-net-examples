using System;
using System.Diagnostics;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Generate a sample barcode image in memory
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            using (Bitmap barcodeImage = generator.GenerateBarCodeImage())
            {
                // Prepare CPU and time measurement
                Process currentProcess = Process.GetCurrentProcess();
                TimeSpan cpuStart = currentProcess.TotalProcessorTime;
                Stopwatch sw = Stopwatch.StartNew();

                // Recognize the barcode
                using (var reader = new BarCodeReader(barcodeImage, DecodeType.Code128))
                {
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                        Console.WriteLine($"Detected Text: {result.CodeText}");
                    }
                }

                // Stop measurements
                sw.Stop();
                TimeSpan cpuEnd = currentProcess.TotalProcessorTime;

                // Log recognition time and CPU usage
                Console.WriteLine($"Recognition elapsed time: {sw.ElapsedMilliseconds} ms");
                Console.WriteLine($"CPU time used: {(cpuEnd - cpuStart).TotalMilliseconds} ms");
            }
        }
    }
}