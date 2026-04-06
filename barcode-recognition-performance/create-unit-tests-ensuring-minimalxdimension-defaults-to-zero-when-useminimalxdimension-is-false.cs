using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        string tempFile = Path.Combine(Path.GetTempPath(), "test_barcode.png");

        // Generate a simple barcode and save it to a temporary file
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = "12345";
            generator.Save(tempFile);
        }

        // Read the barcode and verify MinimalXDimension defaults to zero when UseMinimalXDimension is not set
        using (var reader = new BarCodeReader(tempFile, DecodeType.Code128))
        {
            // Ensure the XDimension mode is not UseMinimalXDimension
            if (reader.QualitySettings.XDimension == XDimensionMode.UseMinimalXDimension)
            {
                Console.WriteLine("Test Failed: XDimension mode is unexpectedly set to UseMinimalXDimension.");
            }
            else
            {
                float minimal = reader.QualitySettings.MinimalXDimension;
                if (Math.Abs(minimal) < 0.0001f)
                {
                    Console.WriteLine("Test Passed: MinimalXDimension defaults to zero when UseMinimalXDimension is false.");
                }
                else
                {
                    Console.WriteLine($"Test Failed: MinimalXDimension is {minimal}, expected 0.");
                }
            }
        }

        // Clean up temporary file
        try
        {
            if (File.Exists(tempFile))
                File.Delete(tempFile);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}