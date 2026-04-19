using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Simulated request payload
        int width = 300;
        int height = 150;
        string unit = "Pixels";

        try
        {
            byte[] imageBytes = GenerateBarcode(width, height, unit);
            Console.WriteLine($"Generated barcode image bytes: {imageBytes.Length}");
            // In a real API you would write the bytes to the HTTP response.
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Generates a Code128 barcode with the specified dimensions and returns the PNG image bytes.
    /// </summary>
    /// <param name="width">Image width value.</param>
    /// <param name="height">Image height value.</param>
    /// <param name="unit">Unit name: Pixels, Inches, Millimeters, or Point.</param>
    /// <returns>PNG image as byte array.</returns>
    static byte[] GenerateBarcode(int width, int height, string unit)
    {
        if (width <= 0) throw new ArgumentOutOfRangeException(nameof(width));
        if (height <= 0) throw new ArgumentOutOfRangeException(nameof(height));
        if (string.IsNullOrWhiteSpace(unit)) throw new ArgumentException("Unit must be provided.", nameof(unit));

        // Create generator for Code128 barcode
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Sample codetext
            generator.CodeText = "123456";

            // Use interpolation mode so ImageWidth/ImageHeight control size
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Apply requested dimensions using the appropriate unit member
            switch (unit.Trim().ToLowerInvariant())
            {
                case "pixels":
                    generator.Parameters.ImageWidth.Pixels = width;
                    generator.Parameters.ImageHeight.Pixels = height;
                    break;
                case "inches":
                    generator.Parameters.ImageWidth.Inches = width;
                    generator.Parameters.ImageHeight.Inches = height;
                    break;
                case "millimeters":
                    generator.Parameters.ImageWidth.Millimeters = width;
                    generator.Parameters.ImageHeight.Millimeters = height;
                    break;
                case "point":
                case "points":
                    generator.Parameters.ImageWidth.Point = width;
                    generator.Parameters.ImageHeight.Point = height;
                    break;
                default:
                    throw new ArgumentException($"Unsupported unit '{unit}'.", nameof(unit));
            }

            // Save to memory stream as PNG
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                return ms.ToArray();
            }
        }
    }
}