using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Valid ASCII data – should be encoded without error.
        EncodeAndSave("ABC123", "maxicode_binary_ascii.png");

        // Unicode data – Binary mode does not support Unicode, expect an exception.
        EncodeAndSave("テスト", "maxicode_binary_unicode.png");
    }

    static void EncodeAndSave(string text, string outputPath)
    {
        try
        {
            // Create a MaxiCode generator.
            using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode))
            {
                // Set the code text.
                generator.CodeText = text;

                // Switch to Binary mode (throws if Unicode characters are present).
                generator.Parameters.Barcode.MaxiCode.MaxiCodeEncodeMode = MaxiCodeEncodeMode.Binary;

                // Save the barcode image to a memory stream.
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    File.WriteAllBytes(outputPath, ms.ToArray());
                }

                Console.WriteLine($"Barcode saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            // Handle the exception that occurs when Unicode characters are not allowed.
            Console.WriteLine($"Failed to encode '{text}' in Binary mode: {ex.Message}");
        }
    }
}