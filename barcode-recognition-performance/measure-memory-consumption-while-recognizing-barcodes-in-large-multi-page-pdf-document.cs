using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main(string[] args)
    {
        // Determine PDF file path (argument or default)
        string pdfPath = args.Length > 0 ? args[0] : "sample.pdf";

        // Verify that the file exists
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Helper to get current process memory (in MB)
        long GetMemoryMb()
        {
            Process proc = Process.GetCurrentProcess();
            proc.Refresh();
            return proc.WorkingSet64 / (1024 * 1024);
        }

        Console.WriteLine($"Memory before creating reader: {GetMemoryMb()} MB");

        // Create BarCodeReader for the PDF document
        using (var reader = new BarCodeReader(pdfPath))
        {
            // Optional: set a timeout to avoid hangs on large files
            reader.Timeout = 30000; // 30 seconds

            // Optional: use a quality preset for faster processing
            reader.QualitySettings = QualitySettings.HighPerformance;

            Console.WriteLine($"Memory after creating reader: {GetMemoryMb()} MB");

            // Perform barcode recognition on all pages
            BarCodeResult[] results = reader.ReadBarCodes();

            Console.WriteLine($"Recognized {results.Length} barcode(s).");
            foreach (var result in results)
            {
                Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }

            Console.WriteLine($"Memory after reading barcodes: {GetMemoryMb()} MB");
        }

        // Reader disposed here
        Console.WriteLine($"Memory after disposing reader: {GetMemoryMb()} MB");
    }
}