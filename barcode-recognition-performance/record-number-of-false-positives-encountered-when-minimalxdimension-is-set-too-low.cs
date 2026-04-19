using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        const string imagePath = "barcode.png";
        const string codeText = "Test12345";

        // Generate a barcode image
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set a reasonable XDimension for generation
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Save(imagePath);
        }

        // Verify the image was created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Failed to create barcode image at '{imagePath}'.");
            return;
        }

        int falsePositiveCount = 0;

        // Read the barcode with a low MinimalXDimension setting
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Use the mode that relies on MinimalXDimension
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;

            // Optionally set a very low MinimalXDimension (if the API provides it)
            // reader.QualitySettings.MinimalXDimension = 0.5f; // Uncomment if available

            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Count results with no confidence as false positives
                if (result.Confidence == BarCodeConfidence.None)
                {
                    falsePositiveCount++;
                }
            }
        }

        Console.WriteLine($"False positives detected with low MinimalXDimension: {falsePositiveCount}");
    }
}