using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        const string filePath = "barcode.png";

        // Generate a Code128 barcode using Interpolation mode at 150 dpi
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            generator.Parameters.Resolution = 150f;
            generator.Save(filePath);
        }

        // Read the generated barcode to verify readability
        using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
        {
            bool anyFound = false;
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}, Text: {result.CodeText}");
                anyFound = true;
            }

            if (!anyFound)
            {
                Console.WriteLine("Barcode could not be read at 150 dpi with Interpolation mode. Consider using a higher DPI.");
            }
        }
    }
}