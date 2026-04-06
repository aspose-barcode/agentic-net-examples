using System;
using System.IO;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // DPI values to test
        float[] dpis = new float[] { 96f, 150f, 300f };
        string[] fileNames = new string[dpis.Length];
        long[] sizes = new long[dpis.Length];

        // Generate a barcode for each DPI
        for (int i = 0; i < dpis.Length; i++)
        {
            string fileName = $"barcode_{(int)dpis[i]}dpi.png";
            fileNames[i] = fileName;

            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Set the resolution (dpi)
                generator.Parameters.Resolution = dpis[i];
                // Save as PNG
                generator.Save(fileName, BarCodeImageFormat.Png);
            }

            // Record file size
            sizes[i] = new FileInfo(fileName).Length;
        }

        // Display sizes
        Console.WriteLine("Barcode file sizes at different DPI:");
        for (int i = 0; i < dpis.Length; i++)
        {
            Console.WriteLine($"{fileNames[i]} : {sizes[i]} bytes");
        }

        // Simple pairwise comparison
        Console.WriteLine("\nSize comparison:");
        for (int i = 0; i < dpis.Length; i++)
        {
            for (int j = i + 1; j < dpis.Length; j++)
            {
                string largerFile = sizes[i] > sizes[j] ? fileNames[i] : fileNames[j];
                string smallerFile = sizes[i] > sizes[j] ? fileNames[j] : fileNames[i];
                long largerSize = Math.Max(sizes[i], sizes[j]);
                long smallerSize = Math.Min(sizes[i], sizes[j]);
                Console.WriteLine($"{largerFile} ({largerSize} bytes) is larger than {smallerFile} ({smallerSize} bytes).");
            }
        }
    }
}