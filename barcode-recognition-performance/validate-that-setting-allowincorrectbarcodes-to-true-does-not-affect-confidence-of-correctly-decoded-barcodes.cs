using System;
using System.Linq;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Path for temporary barcode image
        string imagePath = "temp_barcode.png";

        // Generate a correct Code128 barcode and save it
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(imagePath);
        }

        // Read barcode with default settings
        BarCodeConfidence confidenceDefault = BarCodeConfidence.None;
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            var result = reader.ReadBarCodes().FirstOrDefault();
            if (result != null)
                confidenceDefault = result.Confidence;
        }

        // Read barcode with AllowIncorrectBarcodes set to true
        BarCodeConfidence confidenceAllowIncorrect = BarCodeConfidence.None;
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Enable recognition of incorrect barcodes (should not affect correct ones)
            reader.QualitySettings.AllowIncorrectBarcodes = true;

            var result = reader.ReadBarCodes().FirstOrDefault();
            if (result != null)
                confidenceAllowIncorrect = result.Confidence;
        }

        // Output the confidence values
        Console.WriteLine($"Confidence with default settings: {confidenceDefault}");
        Console.WriteLine($"Confidence with AllowIncorrectBarcodes=true: {confidenceAllowIncorrect}");

        // Validate that both confidences are equal
        if (confidenceDefault == confidenceAllowIncorrect)
        {
            Console.WriteLine("Test passed: AllowIncorrectBarcodes does not affect confidence of correctly decoded barcodes.");
        }
        else
        {
            Console.WriteLine("Test failed: Confidence values differ.");
        }
    }
}