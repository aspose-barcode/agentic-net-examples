using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main(string[] args)
    {
        // Determine image path (argument or default)
        string imagePath = args.Length > 0 ? args[0] : "highres.png";

        // Verify that the file exists
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Image file not found: " + imagePath);
            return;
        }

        // List to hold recognition results
        List<BarCodeResult> results = new List<BarCodeResult>();

        // Measure recognition time
        using (BarCodeReader reader = new BarCodeReader(imagePath))
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                results.Add(result);
            }

            stopwatch.Stop();
            Console.WriteLine($"Recognition time: {stopwatch.ElapsedMilliseconds} ms");
        }

        // Output recognized barcodes
        foreach (BarCodeResult result in results)
        {
            Console.WriteLine("BarCode Type: " + result.CodeTypeName);
            Console.WriteLine("BarCode CodeText: " + result.CodeText);
        }
    }
}