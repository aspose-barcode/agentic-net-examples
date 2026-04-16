using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

class Program
{
    static void Main()
    {
        // Sample list of codetext strings to be encoded as MaxiCode barcodes.
        string[] codetexts = new string[]
        {
            "Sample message 1",
            "Another test message",
            "MaxiCode demo text",
            "Hello World!",
            "Aspose.BarCode"
        };

        // Output directory (current folder). Ensure it exists.
        string outputDir = Directory.GetCurrentDirectory();

        for (int i = 0; i < codetexts.Length; i++)
        {
            // Create a standard MaxiCode codetext for Mode 4 (data only).
            var maxiCodeCodetext = new MaxiCodeStandardCodetext
            {
                Mode = MaxiCodeMode.Mode4,
                Message = codetexts[i]
            };

            // Generate the barcode image using ComplexBarcodeGenerator.
            using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
            {
                generator.GenerateBarCodeImage();

                // Save the image to a PNG file via a MemoryStream.
                using (var memoryStream = new MemoryStream())
                {
                    generator.Save(memoryStream, BarCodeImageFormat.Png);
                    string filePath = Path.Combine(outputDir, $"maxicode_{i + 1}.png");
                    File.WriteAllBytes(filePath, memoryStream.ToArray());
                    Console.WriteLine($"Saved: {filePath}");
                }
            }
        }
    }
}