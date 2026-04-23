using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Sample data: generate a Dutch KIX barcode and obtain its image as a byte array
        byte[] barcodeBytes;
        using (var generator = new BarcodeGenerator(EncodeTypes.DutchKIX, "1234567890"))
        {
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                barcodeBytes = ms.ToArray();
            }
        }

        // Decode the barcode from the byte array
        try
        {
            using (var ms = new MemoryStream(barcodeBytes))
            {
                using (var bitmap = new Bitmap(ms))
                {
                    using (var reader = new BarCodeReader(bitmap, DecodeType.DutchKIX))
                    {
                        var results = reader.ReadBarCodes();
                        if (results.Length == 0)
                        {
                            Console.WriteLine("No Dutch KIX barcode detected.");
                        }
                        else
                        {
                            foreach (var result in results)
                            {
                                Console.WriteLine($"Decoded Type: {result.CodeTypeName}");
                                Console.WriteLine($"Decoded Text: {result.CodeText}");
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Handle any format or processing exceptions
            Console.WriteLine($"Error during barcode decoding: {ex.Message}");
        }
    }
}