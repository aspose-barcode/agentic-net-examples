using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Sample list of code texts for batch generation.
        string[] codeTexts = new string[]
        {
            "123456789012",   // Valid Code128 (even length, checksum will be calculated)
            "ABCDEF",         // Valid Code128 (no checksum needed)
            "12345",          // Invalid for Code128 if checksum enforcement is on
            "9876543210",     // Another valid example
            "INVALID!@#"      // Invalid characters, will cause an exception
        };

        // Output folder for generated barcode images.
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        foreach (string text in codeTexts)
        {
            try
            {
                // Create a barcode generator for Code128 with the current code text.
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, text))
                {
                    // Enable checksum calculation.
                    generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
                    // Throw exception if the code text is incorrect (e.g., checksum error).
                    generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;

                    // Save the barcode image.
                    string filePath = Path.Combine(outputFolder, $"{text}.png");
                    generator.Save(filePath);
                    Console.WriteLine($"Generated barcode for \"{text}\" at \"{filePath}\"");
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur during generation, including checksum errors.
                Console.WriteLine($"Error generating barcode for \"{text}\": {ex.Message}");
            }
        }

        Console.WriteLine("Batch barcode generation completed.");
    }
}