using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        int failedTests = 0;

        // Test: MinimalXDimension should be zero when XDimension mode is not UseMinimalXDimension
        try
        {
            // Generate a simple barcode image in memory
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
            {
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    ms.Position = 0;

                    // Read the barcode with specific quality settings
                    using (var reader = new BarCodeReader(ms, DecodeType.Code128))
                    {
                        // Set XDimension mode to Normal (not UseMinimalXDimension)
                        reader.QualitySettings.XDimension = XDimensionMode.Normal;

                        // Verify that MinimalXDimension defaults to zero
                        if (reader.QualitySettings.MinimalXDimension != 0f)
                        {
                            Console.WriteLine("FAILED: MinimalXDimension is not zero when XDimension is not UseMinimalXDimension.");
                            failedTests++;
                        }
                        else
                        {
                            Console.WriteLine("PASSED: MinimalXDimension defaults to zero when XDimension is not UseMinimalXDimension.");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"FAILED: Exception occurred during test - {ex.Message}");
            failedTests++;
        }

        // Summary
        if (failedTests == 0)
        {
            Console.WriteLine("All tests passed.");
        }
        else
        {
            Console.WriteLine($"FAILED: {failedTests} test(s) failed.");
        }
    }
}