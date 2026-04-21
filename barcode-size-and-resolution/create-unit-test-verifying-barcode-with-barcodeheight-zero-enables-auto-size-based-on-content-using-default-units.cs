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
        int passed = 0;
        int failed = 0;

        // Test 1: Setting BarHeight to zero should throw ArgumentException
        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                // Attempt to set BarHeight to zero (invalid per API)
                generator.Parameters.Barcode.BarHeight.Point = 0f;
                // If no exception, the test fails
                Console.WriteLine("FAILED: Setting BarHeight to zero did not throw.");
                failed++;
            }
        }
        catch (ArgumentException)
        {
            Console.WriteLine("PASSED: Setting BarHeight to zero threw ArgumentException as expected.");
            passed++;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"FAILED: Unexpected exception type: {ex.GetType().Name}");
            failed++;
        }

        // Test 2: AutoSizeMode.Interpolation should respect ImageWidth/ImageHeight
        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
            {
                // Enable interpolation auto‑size
                generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

                // Define target size using default units (points)
                generator.Parameters.ImageWidth.Point = 200f;
                generator.Parameters.ImageHeight.Point = 100f;

                // Generate bitmap
                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    // Verify dimensions match the requested size
                    if (bitmap.Width == 200 && bitmap.Height == 100)
                    {
                        Console.WriteLine("PASSED: AutoSizeMode.Interpolation produced expected image size.");
                        passed++;
                    }
                    else
                    {
                        Console.WriteLine($"FAILED: Image size mismatch. Expected 200x100, got {bitmap.Width}x{bitmap.Height}.");
                        failed++;
                    }

                    // Optionally save the image for manual inspection
                    string tempPath = Path.Combine(Path.GetTempPath(), "autosize_test.png");
                    bitmap.Save(tempPath, ImageFormat.Png);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"FAILED: Exception during AutoSizeMode test: {ex.Message}");
            failed++;
        }

        // Summary
        Console.WriteLine($"TEST SUMMARY: {passed} passed, {failed} failed.");
    }
}