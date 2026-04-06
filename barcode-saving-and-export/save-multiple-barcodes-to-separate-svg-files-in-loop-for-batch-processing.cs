using System;
using System.IO;
using Aspose.BarCode.Generation;
namespace BatchBarcodeSvg
{
    class Program
    {
        static void Main()
        {
            // Ensure the output directory exists
            string outputDir = "Barcodes";
            if (!Directory.Exists(outputDir))
                Directory.CreateDirectory(outputDir);

            // Sample list of code texts to encode
            string[] codes = { "ABC123", "987654321", "HelloWorld", "20230605", "SampleCode" };

            for (int i = 0; i < codes.Length; i++)
            {
                string code = codes[i];
                string filePath = Path.Combine(outputDir, $"barcode_{i + 1}.svg");

                try
                {
                    // Create a barcode generator for Code128 symbology
                    using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
                    {
                        // Set the text to encode
                        generator.CodeText = code;

                        // Save the barcode as an SVG file
                        generator.Save(filePath, BarCodeImageFormat.Svg);
                    }

                    Console.WriteLine($"Saved barcode '{code}' to '{filePath}'.");
                }
                catch (Exception ex)
                {
                    // Log any generation errors
                    Console.WriteLine($"Failed to generate barcode for '{code}': {ex.Message}");
                }
            }
        }
    }
}