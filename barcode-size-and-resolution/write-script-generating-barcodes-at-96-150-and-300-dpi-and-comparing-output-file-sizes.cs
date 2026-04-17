using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Barcode content and type
        const string codeText = "1234567890";
        const string baseFileName = "barcode";

        // Resolutions to test (dpi)
        float[] resolutions = new float[] { 96f, 150f, 300f };

        // Store file sizes for comparison
        long[] fileSizes = new long[resolutions.Length];

        for (int i = 0; i < resolutions.Length; i++)
        {
            float dpi = resolutions[i];
            string fileName = $"{baseFileName}_{(int)dpi}.png";

            // Create generator with Code128 symbology
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Set desired resolution
                generator.Parameters.Resolution = dpi;

                // Save barcode image
                generator.Save(fileName, BarCodeImageFormat.Png);
            }

            // Get file size
            if (File.Exists(fileName))
            {
                fileSizes[i] = new FileInfo(fileName).Length;
            }
            else
            {
                Console.WriteLine($"Failed to create file: {fileName}");
                fileSizes[i] = -1;
            }
        }

        // Output comparison results
        Console.WriteLine("Barcode file sizes at different resolutions:");
        for (int i = 0; i < resolutions.Length; i++)
        {
            if (fileSizes[i] >= 0)
            {
                Console.WriteLine($"{(int)resolutions[i]} dpi: {fileSizes[i]} bytes");
            }
        }
    }
}