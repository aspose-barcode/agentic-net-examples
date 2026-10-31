using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        string outputPath = Path.Combine(Path.GetTempPath(), "barcode.png");
        string codeText = "12345678";
        int maxAttempts = 3;

        GenerateBarcodeWithRetry(outputPath, codeText, maxAttempts);
    }

    static void GenerateBarcodeWithRetry(string outputPath, string codeText, int maxAttempts)
    {
        for (int attempt = 1; attempt <= maxAttempts; attempt++)
        {
            try
            {
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
                {
                    // Optional: configure parameters here if needed
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                }

                Console.WriteLine($"Barcode saved successfully to '{outputPath}' on attempt {attempt}.");
                break;
            }
            catch (IOException ioEx)
            {
                Console.WriteLine($"IO exception on attempt {attempt}: {ioEx.Message}");
                if (attempt == maxAttempts)
                {
                    Console.WriteLine("Maximum attempts reached. Barcode generation failed.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected exception on attempt {attempt}: {ex.Message}");
                // Do not retry on non-IO exceptions
                break;
            }
        }
    }
}