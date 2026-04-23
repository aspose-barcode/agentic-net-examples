using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        string[] serviceCodes = new string[]
        {
            "01",
            "02",
            "03",
            "04",
            "05"
        };

        string outputDir = "SwissPostBarcodes";
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        for (int i = 0; i < serviceCodes.Length; i++)
        {
            string code = serviceCodes[i];
            string filePath = Path.Combine(outputDir, $"SwissPost_{code}.svg");

            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code39, code))
            {
                generator.Parameters.Resolution = 300;
                generator.Save(filePath, BarCodeImageFormat.Svg);
            }
        }

        Console.WriteLine("Swiss Post Parcel additional service barcodes have been generated.");
    }
}