using System;
using System.Diagnostics;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Prepare barcode data
        const string codeText = "1234567890";
        const string outputFile = "barcode.png";

        // Start measuring time
        Stopwatch stopwatch = Stopwatch.StartNew();

        // Generate barcode inside a using block to ensure proper disposal
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = codeText;
            generator.Save(outputFile);
        }

        // Stop timer
        stopwatch.Stop();

        // Log duration to the application log (console used here)
        Console.WriteLine($"Barcode generation completed in {stopwatch.ElapsedMilliseconds} ms. Saved to '{outputFile}'.");
    }
}