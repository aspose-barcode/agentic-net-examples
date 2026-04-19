using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "barcode.png";
        const int maxRetries = 3;
        int attempt = 0;
        bool saved = false;

        while (attempt < maxRetries && !saved)
        {
            attempt++;
            try
            {
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
                {
                    // Example of setting a barcode property
                    generator.Parameters.Barcode.BarColor = Color.Black;
                    // Attempt to save the barcode image
                    generator.Save(outputPath);
                }

                saved = true;
                Console.WriteLine($"Barcode saved successfully on attempt {attempt}.");
            }
            catch (IOException ioEx)
            {
                Console.WriteLine($"IO exception on attempt {attempt}: {ioEx.Message}");
                if (attempt >= maxRetries)
                {
                    Console.WriteLine("Failed to save barcode after maximum retries.");
                }
            }
            catch (BarCodeException bcEx)
            {
                Console.WriteLine($"Barcode generation exception: {bcEx.Message}");
                // No point in retrying if barcode generation itself fails
                break;
            }
        }
    }
}