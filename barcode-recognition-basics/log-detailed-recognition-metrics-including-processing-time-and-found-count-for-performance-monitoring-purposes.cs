using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Path for temporary barcode image
        string imagePath = Path.Combine(Path.GetTempPath(), "temp_barcode.png");

        // Generate a barcode and save it to a file
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(imagePath);
        }

        // Measure recognition time
        var stopwatch = new Stopwatch();
        int foundCount = 0;

        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            stopwatch.Start();
            // Perform recognition
            BarCodeResult[] results = reader.ReadBarCodes();
            stopwatch.Stop();

            foundCount = results.Length;

            // Output each recognized barcode
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }
        }

        // Log detailed metrics
        Console.WriteLine($"Recognition Time (ms): {stopwatch.ElapsedMilliseconds}");
        Console.WriteLine($"Barcodes Found: {foundCount}");

        // Clean up temporary file
        try
        {
            File.Delete(imagePath);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}