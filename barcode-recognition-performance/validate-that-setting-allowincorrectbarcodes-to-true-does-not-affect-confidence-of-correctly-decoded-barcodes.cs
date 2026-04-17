using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Prepare temporary file path
        string tempFile = Path.Combine(Path.GetTempPath(), "allowIncorrectBarcodes_test.png");

        // Generate a correct Code128 barcode
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789012"))
        {
            generator.Save(tempFile);
        }

        // Verify the file was created
        if (!File.Exists(tempFile))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read barcode with default settings
        BarCodeConfidence confidenceDefault = BarCodeConfidence.None;
        using (var reader = new BarCodeReader(tempFile, DecodeType.Code128))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                confidenceDefault = result.Confidence;
                Console.WriteLine($"Default - Confidence: {confidenceDefault}");
                break; // only need first result
            }
        }

        // Read barcode with AllowIncorrectBarcodes = true
        BarCodeConfidence confidenceAllowIncorrect = BarCodeConfidence.None;
        using (var reader = new BarCodeReader(tempFile, DecodeType.Code128))
        {
            // Enable recognition of incorrect barcodes
            reader.QualitySettings.AllowIncorrectBarcodes = true;

            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                confidenceAllowIncorrect = result.Confidence;
                Console.WriteLine($"AllowIncorrectBarcodes=true - Confidence: {confidenceAllowIncorrect}");
                break; // only need first result
            }
        }

        // Compare confidences
        if (confidenceDefault == confidenceAllowIncorrect)
        {
            Console.WriteLine("Test passed: AllowIncorrectBarcodes does not affect confidence of correctly decoded barcodes.");
        }
        else
        {
            Console.WriteLine("Test failed: Confidence values differ.");
        }

        // Clean up temporary file
        try
        {
            File.Delete(tempFile);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}