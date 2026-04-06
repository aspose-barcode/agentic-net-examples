using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Generate a sample barcode image in memory
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = "1234567890";
            using (var memoryStream = new MemoryStream())
            {
                generator.Save(memoryStream, BarCodeImageFormat.Png);
                memoryStream.Position = 0;

                // Read the barcode with quality settings for large XDimension (6 pixels)
                using (var reader = new BarCodeReader(memoryStream, DecodeType.Code128))
                {
                    // Start from a normal quality preset
                    reader.QualitySettings = QualitySettings.NormalQuality;
                    // Use minimal XDimension mode and set the minimal size to 6 pixels
                    reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                    reader.QualitySettings.MinimalXDimension = 6;

                    foreach (var result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"Detected barcode: Type={result.CodeTypeName}, Text={result.CodeText}");
                    }
                }
            }
        }
    }
}