using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

class Program
{
    static async Task Main(string[] args)
    {
        // Prepare MaxiCode codetext for Mode 4 with a simple message
        var maxiCodeCodetext = new MaxiCodeStandardCodetext
        {
            Mode = MaxiCodeMode.Mode4,
            Message = "Test message"
        };

        // Generate and save the barcode image asynchronously
        using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            // Save to PNG file without blocking the calling thread
            await Task.Run(() => generator.Save("maxicode.png", BarCodeImageFormat.Png));
        }

        Console.WriteLine("MaxiCode image generated: maxicode.png");
    }
}