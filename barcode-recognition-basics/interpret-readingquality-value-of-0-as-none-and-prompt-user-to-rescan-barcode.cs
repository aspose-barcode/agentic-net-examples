using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        const string filePath = "barcode.png";

        // Create a Code128 barcode and save it to a file
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            generator.Save(filePath);
        }

        // Read the saved barcode image
        using (var reader = new BarCodeReader(filePath, DecodeType.Code128))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"BarCode Type: {result.CodeTypeName}");
                Console.WriteLine($"BarCode CodeText: {result.CodeText}");
                Console.WriteLine($"BarCode Confidence: {result.Confidence}");
                Console.WriteLine($"BarCode ReadingQuality: {result.ReadingQuality}");

                // Interpret ReadingQuality == 0 as none and prompt for rescan
                if (result.ReadingQuality == 0.0)
                {
                    Console.WriteLine("Reading quality is none. Please rescan the barcode.");
                }
                else
                {
                    Console.WriteLine($"Reading quality is {result.ReadingQuality}%.");
                }
            }
        }
    }
}