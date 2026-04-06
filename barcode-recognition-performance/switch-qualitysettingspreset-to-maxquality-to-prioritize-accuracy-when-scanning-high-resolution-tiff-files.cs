using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Generate a temporary barcode image.
        string tempPath = "temp.png";
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            generator.Save(tempPath);
        }

        // Read the barcode from the generated image.
        using (BarCodeReader reader = new BarCodeReader(tempPath))
        {
            reader.QualitySettings = QualitySettings.MaxQuality;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Barcode Type: {result.CodeTypeName}, Code Text: {result.CodeText}");
            }
        }
    }
}