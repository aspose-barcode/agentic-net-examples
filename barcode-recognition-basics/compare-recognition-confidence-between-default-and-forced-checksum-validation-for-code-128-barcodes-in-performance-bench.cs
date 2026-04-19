using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Sample Code128 texts
        string[] samples = { "123456", "ABCDEF", "9876543210", "Test123", "Code128Test" };
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        Console.WriteLine("Generating barcodes and comparing confidence levels:");
        foreach (string text in samples)
        {
            string filePath = Path.Combine(outputDir, $"code128_{text}.png");

            // Generate barcode image
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, text))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Read with default checksum validation
            BarCodeConfidence defaultConfidence = BarCodeConfidence.None;
            using (BarCodeReader reader = new BarCodeReader(filePath, DecodeType.Code128))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    defaultConfidence = result.Confidence;
                    break; // only one barcode expected
                }
            }

            // Read with forced checksum validation (On)
            BarCodeConfidence forcedConfidence = BarCodeConfidence.None;
            using (BarCodeReader reader = new BarCodeReader(filePath, DecodeType.Code128))
            {
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    forcedConfidence = result.Confidence;
                    break;
                }
            }

            Console.WriteLine($"Text: {text}");
            Console.WriteLine($"  Default ChecksumValidation Confidence: {defaultConfidence}");
            Console.WriteLine($"  Forced ChecksumValidation (On) Confidence: {forcedConfidence}");
            Console.WriteLine();
        }

        // Cleanup generated files (optional)
        // Directory.Delete(outputDir, true);
    }
}