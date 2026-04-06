using System;
using System.Threading.Tasks;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static async Task Main(string[] args)
    {
        // Generate a sample barcode image.
        const string barcodeFile = "sample.png";
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = "12345";
            generator.Save(barcodeFile);
        }

        // Asynchronously read the barcode from the generated image.
        var results = await ReadBarcodesAsync(barcodeFile);

        // Output the detection results.
        foreach (var result in results)
        {
            Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
        }
    }

    private static Task<BarCodeResult[]> ReadBarcodesAsync(string imagePath)
    {
        return Task.Run(() =>
        {
            using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
            {
                // Optional: set a timeout to avoid long processing.
                reader.Timeout = 5000;
                return reader.ReadBarCodes();
            }
        });
    }
}