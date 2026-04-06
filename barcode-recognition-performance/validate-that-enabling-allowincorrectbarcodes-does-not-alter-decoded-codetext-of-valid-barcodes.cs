using System;
using System.IO;
using System.Linq;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Path for temporary barcode image
        string imagePath = "temp_barcode.png";

        // Generate a valid EAN13 barcode and save it
        using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, "1234567890128"))
        {
            generator.Save(imagePath);
        }

        // Read barcode with default settings
        string defaultCodeText;
        using (var reader = new BarCodeReader(imagePath, DecodeType.EAN13))
        {
            var result = reader.ReadBarCodes().FirstOrDefault();
            defaultCodeText = result?.CodeText;
        }

        // Read barcode with AllowIncorrectBarcodes enabled
        string allowIncorrectCodeText;
        using (var reader = new BarCodeReader(imagePath, DecodeType.EAN13))
        {
            // Enable recognition of barcodes with incorrect checksum or values
            reader.QualitySettings.AllowIncorrectBarcodes = true;
            var result = reader.ReadBarCodes().FirstOrDefault();
            allowIncorrectCodeText = result?.CodeText;
        }

        // Validate that both decoded texts are identical
        bool isEqual = string.Equals(defaultCodeText, allowIncorrectCodeText, StringComparison.Ordinal);
        Console.WriteLine($"Default decoding CodeText: {defaultCodeText}");
        Console.WriteLine($"AllowIncorrectBarcodes decoding CodeText: {allowIncorrectCodeText}");
        Console.WriteLine($"CodeText unchanged: {isEqual}");

        // Clean up temporary file
        if (File.Exists(imagePath))
        {
            File.Delete(imagePath);
        }
    }
}