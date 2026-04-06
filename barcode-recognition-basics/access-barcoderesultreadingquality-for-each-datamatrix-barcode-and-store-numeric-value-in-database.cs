using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        const string imagePath = "datamatrix.png";

        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "SampleText"))
        {
            generator.Save(imagePath);
        }

        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.DataMatrix))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                if (result.CodeTypeName.Equals("DataMatrix", StringComparison.OrdinalIgnoreCase))
                {
                    double quality = result.ReadingQuality;
                    Console.WriteLine($"DataMatrix ReadingQuality: {quality}");
                }
            }
        }
    }
}