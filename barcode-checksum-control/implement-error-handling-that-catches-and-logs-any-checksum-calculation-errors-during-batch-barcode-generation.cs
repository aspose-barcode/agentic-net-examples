using System;
using System.IO;
using Aspose.BarCode.Generation;
class Program
{
    static void Main()
    {
        // Sample list of code texts to generate barcodes for
        string[] codeTexts = new[]
        {
            "123456789012",   // Missing checksum for EAN13 (will be calculated)
            "1234567890128",  // Correct EAN13 with checksum
            "ABC123",         // Code128 example
            "INVALID!!"       // Intentionally invalid to trigger an error
        };

        // Output folder for generated barcode images
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        int index = 1;
        foreach (string code in codeTexts)
        {
            string filePath = Path.Combine(outputFolder, $"barcode_{index}.png");
            try
            {
                // Create a barcode generator for Code128 (generic 1D type)
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
                {
                    // Set the code text
                    generator.CodeText = code;

                    // Enable checksum calculation
                    generator.Parameters.Barcode.IsChecksumEnabled = Aspose.BarCode.Generation.EnableChecksum.Yes;

                    // Throw exception when the code text is incorrect (e.g., invalid characters)
                    generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;

                    // Save the barcode image
                    generator.Save(filePath);
                }

                Console.WriteLine($"Successfully generated barcode #{index}: {filePath}");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during generation, including checksum issues
                Console.WriteLine($"Error generating barcode #{index} for code '{code}': {ex.Message}");
            }

            index++;
        }
    }
}