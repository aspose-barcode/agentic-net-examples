using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Define barcode data and file path
        string codeText = "Test123";
        string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

        // Generate barcode image
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Optional: set XDimension for clearer image
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Save(imagePath);
        }

        // Verify that the image was created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Baseline recognition without toggling UseMinimalXDimension
        bool baselineSuccess = false;
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                if (result.CodeText == codeText)
                {
                    baselineSuccess = true;
                    Console.WriteLine("Baseline recognition succeeded.");
                }
            }
        }

        // Recognition with UseMinimalXDimension mode
        bool minimalSuccess = false;
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Set recognition mode to use MinimalXDimension
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            // Define the minimal X dimension (in pixels)
            reader.QualitySettings.MinimalXDimension = 2;
            foreach (var result in reader.ReadBarCodes())
            {
                if (result.CodeText == codeText)
                {
                    minimalSuccess = true;
                    Console.WriteLine("UseMinimalXDimension recognition succeeded.");
                }
            }
        }

        // Final test result
        if (baselineSuccess && minimalSuccess)
        {
            Console.WriteLine("Integration test passed: barcode recognized correctly in both modes.");
        }
        else
        {
            Console.WriteLine("Integration test failed.");
        }
    }
}