using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        try
        {
            TestMaxiCodeAspectRatioEffect();
            Console.WriteLine("All tests passed.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Test failed: {ex.Message}");
        }
    }

    static void Assert(bool condition, string message)
    {
        if (!condition)
            throw new Exception(message);
    }

    static void TestMaxiCodeAspectRatioEffect()
    {
        string tempDir = Path.Combine(Path.GetTempPath(), "MaxiCodeAspectRatioTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Generate barcode with default aspect ratio (1.0)
        int widthDefault, heightDefault;
        string pathDefault = Path.Combine(tempDir, "maxicode_aspect1.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "TestAspect"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 15;
            generator.Parameters.Barcode.MaxiCode.AspectRatio = 1.0f;
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                widthDefault = bitmap.Width;
                heightDefault = bitmap.Height;
                bitmap.Save(pathDefault, ImageFormat.Png);
            }
        }

        // Generate barcode with reduced aspect ratio (0.5)
        int widthReduced, heightReduced;
        string pathReduced = Path.Combine(tempDir, "maxicode_aspect0_5.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "TestAspect"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 15;
            generator.Parameters.Barcode.MaxiCode.AspectRatio = 0.5f;
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                widthReduced = bitmap.Width;
                heightReduced = bitmap.Height;
                bitmap.Save(pathReduced, ImageFormat.Png);
            }
        }

        // Verify that dimensions differ as expected
        Assert(widthDefault == widthReduced, "Width should remain the same when only aspect ratio changes.");
        Assert(heightDefault != heightReduced, "Height should change when aspect ratio changes.");
        // For aspect ratio 0.5, height should be greater than default (since width/height ratio is smaller)
        Assert(heightReduced > heightDefault, "Height with aspect ratio 0.5 should be greater than height with aspect ratio 1.0.");

        // Clean up generated files (optional)
        try { File.Delete(pathDefault); } catch { }
        try { File.Delete(pathReduced); } catch { }
        try { Directory.Delete(tempDir); } catch { }
    }
}