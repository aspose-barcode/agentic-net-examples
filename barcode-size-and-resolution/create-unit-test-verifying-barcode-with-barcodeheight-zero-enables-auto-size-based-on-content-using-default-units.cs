using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code128 barcode, applying auto‑size mode,
/// and handling the exception thrown when an unsupported BarHeight is set.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, attempts an invalid BarHeight setting,
    /// applies auto‑size, and validates the resulting image dimensions.
    /// </summary>
    static void Main()
    {
        try
        {
            // Create a barcode generator for Code128 with the text "Test123"
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
            {
                // Attempt to set BarHeight to zero (this will throw ArgumentException)
                generator.Parameters.Barcode.BarHeight.Point = 0f;

                // Enable auto‑size mode (Interpolation) – the correct way to auto‑size
                generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

                // Save the generated barcode to a memory stream in PNG format
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    ms.Position = 0; // Reset stream position for reading

                    // Load the image from the memory stream
                    using (var bitmap = new Bitmap(ms))
                    {
                        int width = bitmap.Width;
                        int height = bitmap.Height;
                        Console.WriteLine($"Generated barcode size: {width}x{height}");

                        // Verify that the image dimensions are valid (greater than zero)
                        if (width > 0 && height > 0)
                        {
                            Console.WriteLine("PASS: Auto‑size applied based on content.");
                        }
                        else
                        {
                            Console.WriteLine("FAIL: Image dimensions are invalid.");
                        }
                    }
                }
            }
        }
        catch (ArgumentException ex)
        {
            // Expected when BarHeight is set to zero
            Console.WriteLine($"Caught expected exception: {ex.Message}");
            Console.WriteLine("NOTE: Setting BarHeight to zero is not supported; use AutoSizeMode for auto‑sizing.");
        }
        catch (Exception ex)
        {
            // Handle any other unexpected errors
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}