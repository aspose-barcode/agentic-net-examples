using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Prepare a temporary file path for the barcode image
        string barcodePath = Path.Combine(Path.GetTempPath(), "useMinimalXDimension_test.png");

        // Generate a Code128 barcode with known text and save it to the file
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            // Ensure the image is saved in PNG format
            generator.Save(barcodePath);
        }

        // Read the barcode using UseMinimalXDimension mode
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            // Configure recognition to use the minimal X dimension mode
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            // Set the minimal X dimension (in pixels) that matches the generated barcode
            reader.QualitySettings.MinimalXDimension = 2;

            // Perform recognition
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Recognized CodeText: {result.CodeText}");
                // Simple verification
                if (result.CodeText == "Test123")
                {
                    Console.WriteLine("Recognition succeeded with UseMinimalXDimension.");
                }
                else
                {
                    Console.WriteLine("Recognition failed or returned unexpected text.");
                }
            }
        }

        // Clean up the temporary file
        try
        {
            File.Delete(barcodePath);
        }
        catch
        {
            // Ignored
        }
    }
}