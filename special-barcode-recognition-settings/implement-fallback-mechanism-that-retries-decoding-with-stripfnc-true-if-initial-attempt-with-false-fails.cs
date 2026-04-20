using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "sample.png");

        // Ensure barcode image exists
        if (!File.Exists(imagePath))
        {
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1Code128, "(02)04006664241007(37)1(400)7019590754"))
            {
                generator.Save(imagePath);
            }
        }

        // First attempt without stripping FNC characters
        BarCodeResult[] results = DecodeBarcode(imagePath, stripFnc: false);

        // If no results, retry with StripFNC enabled
        if (results == null || results.Length == 0)
        {
            Console.WriteLine("No barcode detected without StripFNC. Retrying with StripFNC enabled...");
            results = DecodeBarcode(imagePath, stripFnc: true);
        }

        // Output results
        if (results != null && results.Length > 0)
        {
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"Code Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text: {result.CodeText}");
            }
        }
        else
        {
            Console.WriteLine("Barcode could not be decoded.");
        }
    }

    private static BarCodeResult[] DecodeBarcode(string filePath, bool stripFnc)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"File not found: {filePath}");
            return null;
        }

        using (BarCodeReader reader = new BarCodeReader(filePath, DecodeType.Code128))
        {
            reader.BarcodeSettings.StripFNC = stripFnc;
            return reader.ReadBarCodes();
        }
    }
}