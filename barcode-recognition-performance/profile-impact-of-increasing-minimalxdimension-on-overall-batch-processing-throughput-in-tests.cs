using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Prepare a folder for sample barcodes
        string folder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }

        // Generate a small batch of barcodes (5 items)
        int batchSize = 5;
        for (int i = 1; i <= batchSize; i++)
        {
            string codeText = $"Test{i:D3}";
            string filePath = Path.Combine(folder, $"barcode_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Optional: set a fixed XDimension for generation
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Save(filePath);
            }
        }

        // Define MinimalXDimension values to test
        int[] minimalDimensions = new int[] { 1, 2, 3, 4, 5 };

        // Process each MinimalXDimension setting and measure throughput
        foreach (int minDim in minimalDimensions)
        {
            Stopwatch sw = Stopwatch.StartNew();

            // Read all generated barcodes with the current MinimalXDimension setting
            foreach (string file in Directory.GetFiles(folder, "*.png"))
            {
                using (var reader = new BarCodeReader(file, DecodeType.Code128))
                {
                    // Configure quality settings for this run
                    reader.QualitySettings = QualitySettings.NormalQuality;
                    reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                    reader.QualitySettings.MinimalXDimension = minDim;

                    // Perform recognition (results are not used further)
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        // No output needed; just ensure reading occurs
                    }
                }
            }

            sw.Stop();
            Console.WriteLine($"MinimalXDimension = {minDim}: Processed {batchSize} barcodes in {sw.ElapsedMilliseconds} ms");
        }
    }
}