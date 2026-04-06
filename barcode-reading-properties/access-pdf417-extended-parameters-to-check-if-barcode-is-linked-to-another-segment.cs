using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        const string filePath = "pdf417.png";

        // Create a PDF417 barcode with the linkage flag set
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "ABCDE123456789012345678"))
        {
            // Enable the linked mode (codeword 918)
            generator.Parameters.Barcode.Pdf417.IsLinked = true;
            generator.Save(filePath);
        }

        // Read the barcode and access the extended PDF417 parameters
        using (var reader = new BarCodeReader(filePath, DecodeType.Pdf417))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                bool isLinked = result.Extended.Pdf417.IsLinked;
                Console.WriteLine($"Barcode Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text: {result.CodeText}");
                Console.WriteLine($"IsLinked: {isLinked}");
            }
        }
    }
}