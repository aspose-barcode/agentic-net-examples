using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.Generation; // for EncodeTypes and CodabarSymbol
using Aspose.Drawing.Imaging; // for BarCodeImageFormat

class Program
{
    static void Main()
    {
        // Number of barcodes to generate (safe sample size)
        const int barcodeCount = 5;
        // Sample codetext (numeric part of Codabar)
        const string codeText = "123456";

        // Ensure output directory exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "CodabarOutputs");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        for (int i = 0; i < barcodeCount; i++)
        {
            // Alternate start/stop symbols: A for even index, B for odd index
            CodabarSymbol startStopSymbol = (i % 2 == 0) ? CodabarSymbol.A : CodabarSymbol.B;

            // Create a new barcode generator for Codabar with the numeric codetext
            using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, codeText))
            {
                // Set start and stop symbols
                generator.Parameters.Barcode.Codabar.StartSymbol = startStopSymbol;
                generator.Parameters.Barcode.Codabar.StopSymbol = startStopSymbol;

                // Build output file name
                string fileName = $"codabar_{i + 1}_{startStopSymbol}.jpeg";
                string outputPath = Path.Combine(outputDir, fileName);

                // Save the barcode as JPEG
                generator.Save(outputPath, BarCodeImageFormat.Jpeg);
                Console.WriteLine($"Saved {outputPath}");
            }
        }

        Console.WriteLine("Barcode generation completed.");
    }
}