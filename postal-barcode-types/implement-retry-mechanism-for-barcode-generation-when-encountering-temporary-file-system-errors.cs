using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        const string outputPath = "barcode.png";
        const int maxAttempts = 3;
        int attempt = 0;
        bool success = false;

        while (attempt < maxAttempts && !success)
        {
            attempt++;
            try
            {
                // Create barcode generator for Code128 with sample text
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
                {
                    // Example of setting a parameter (optional)
                    generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;

                    // Save the barcode image to file
                    generator.Save(outputPath);
                }

                Console.WriteLine($"Barcode generated successfully on attempt {attempt}.");
                success = true;
            }
            catch (IOException ioEx)
            {
                // Temporary file system error, retry
                Console.WriteLine($"IO error on attempt {attempt}: {ioEx.Message}");
                if (attempt >= maxAttempts)
                {
                    Console.WriteLine("Maximum retry attempts reached. Operation failed.");
                }
            }
            catch (BarCodeException bcEx)
            {
                // Barcode generation specific error, do not retry
                Console.WriteLine($"Barcode generation error: {bcEx.Message}");
                break;
            }
            catch (Exception ex)
            {
                // Unexpected error, do not retry
                Console.WriteLine($"Unexpected error: {ex.Message}");
                break;
            }
        }
    }
}