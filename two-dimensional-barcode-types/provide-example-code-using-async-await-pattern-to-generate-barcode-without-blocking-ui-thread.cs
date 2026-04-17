using System;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static async Task Main(string[] args)
    {
        await GenerateBarcodeAsync();
        Console.WriteLine("Barcode generation completed.");
    }

    private static async Task GenerateBarcodeAsync()
    {
        // Run the barcode generation on a background thread to avoid blocking the caller.
        await Task.Run(() =>
        {
            // Create a BarcodeGenerator for Code128 with sample text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
            {
                // Save the generated barcode image to a file.
                generator.Save("code128.png");
            }
        });
    }
}