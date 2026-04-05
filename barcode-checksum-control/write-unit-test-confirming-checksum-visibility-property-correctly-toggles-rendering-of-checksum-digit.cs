using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        string originalText = "12345";
        string tempPath1 = Path.Combine(Path.GetTempPath(), "barcode_no_show.png");
        string tempPath2 = Path.Combine(Path.GetTempPath(), "barcode_show.png");

        // Generate barcode without showing checksum
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, originalText))
        {
            generator.Parameters.Barcode.IsChecksumEnabled = Aspose.BarCode.Generation.EnableChecksum.Yes;
            generator.Parameters.Barcode.ChecksumAlwaysShow = false;
            generator.Save(tempPath1);
        }

        // Generate barcode with checksum visible
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, originalText))
        {
            generator.Parameters.Barcode.IsChecksumEnabled = Aspose.BarCode.Generation.EnableChecksum.Yes;
            generator.Parameters.Barcode.ChecksumAlwaysShow = true;
            generator.Save(tempPath2);
        }

        // Decode first barcode
        string codeTextNoShow;
        using (var reader = new BarCodeReader(tempPath1, DecodeType.Code128))
        {
            var result = reader.ReadBarCodes()[0];
            codeTextNoShow = result.CodeText;
        }

        // Decode second barcode
        string codeTextShow;
        using (var reader = new BarCodeReader(tempPath2, DecodeType.Code128))
        {
            var result = reader.ReadBarCodes()[0];
            codeTextShow = result.CodeText;
        }

        // Verify that checksum visibility toggles the rendered text
        bool testPassed = codeTextNoShow == originalText &&
                          codeTextShow.StartsWith(originalText) &&
                          codeTextShow.Length > originalText.Length;

        Console.WriteLine($"Original text: {originalText}");
        Console.WriteLine($"Decoded without checksum shown: {codeTextNoShow}");
        Console.WriteLine($"Decoded with checksum shown: {codeTextShow}");
        Console.WriteLine($"Test {(testPassed ? "PASSED" : "FAILED")}");
    }
}