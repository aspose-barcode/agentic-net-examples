using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    // Asynchronous method that generates a barcode image and returns the PNG bytes.
    static async Task<byte[]> GenerateBarcodeAsync(string codeText, BaseEncodeType encodeType)
    {
        // Run the synchronous generation on a background thread to avoid blocking.
        return await Task.Run(() =>
        {
            // BarcodeGenerator implements IDisposable, so use a full using block.
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // MemoryStream also implements IDisposable.
                using (var memoryStream = new MemoryStream())
                {
                    // Save the barcode image to the stream in PNG format.
                    generator.Save(memoryStream, BarCodeImageFormat.Png);
                    // Return the underlying byte array.
                    return memoryStream.ToArray();
                }
            }
        });
    }

    // Entry point of the console application.
    static async Task Main(string[] args)
    {
        // Use a sample code text if none is provided via command‑line arguments.
        string codeText = args.Length > 0 ? args[0] : "123ABC";

        // Choose a barcode symbology; Code128 is used here.
        BaseEncodeType encodeType = EncodeTypes.Code128;

        // Generate the barcode asynchronously.
        byte[] pngBytes = await GenerateBarcodeAsync(codeText, encodeType);

        // Output the size of the generated PNG data.
        Console.WriteLine($"Generated barcode PNG byte array length: {pngBytes.Length}");

        // Optionally write the PNG to a file for verification.
        File.WriteAllBytes("barcode.png", pngBytes);
    }
}