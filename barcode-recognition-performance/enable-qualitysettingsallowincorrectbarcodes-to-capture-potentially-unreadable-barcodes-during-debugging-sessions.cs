using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Create a barcode image (Code128) with valid data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            generator.Save("barcode.png");
        }

        // Read the barcode with AllowIncorrectBarcodes enabled.
        using (var reader = new BarCodeReader("barcode.png", DecodeType.Code128))
        {
            // Use a preset quality setting and enable the flag.
            reader.QualitySettings = QualitySettings.NormalQuality;
            reader.QualitySettings.AllowIncorrectBarcodes = true;

            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Decoded text: {result.CodeText}");
            }
        }
    }
}