using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Path for temporary barcode image
        string filePath = Path.Combine(Path.GetTempPath(), "code128.png");

        // Generate Code128 barcode with default settings
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(filePath);
        }

        // Read barcode with default checksum validation
        BarCodeConfidence defaultConfidence = BarCodeConfidence.None;
        using (var reader = new BarCodeReader(filePath, DecodeType.Code128))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                defaultConfidence = result.Confidence;
                break; // only need first result
            }
        }

        // Read barcode with forced checksum validation (ChecksumValidation.On)
        BarCodeConfidence forcedConfidence = BarCodeConfidence.None;
        using (var reader = new BarCodeReader(filePath, DecodeType.Code128))
        {
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                forcedConfidence = result.Confidence;
                break; // only need first result
            }
        }

        // Output the confidence levels
        Console.WriteLine($"Default checksum validation confidence: {defaultConfidence}");
        Console.WriteLine($"Forced checksum validation confidence: {forcedConfidence}");
    }
}