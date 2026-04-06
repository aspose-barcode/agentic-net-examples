using System;
using System.IO;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Simulate a request with width=300, height=150, unit="pt"
        byte[] imageBytes = GenerateBarcode(300, 150, "pt");
        Console.WriteLine($"Generated barcode image size: {imageBytes.Length} bytes");
    }

    // Generates a Code128 barcode with specified dimensions and returns PNG bytes.
    static byte[] GenerateBarcode(int width, int height, string unit)
    {
        if (width <= 0)
            throw new ArgumentOutOfRangeException(nameof(width), "Width must be positive.");
        if (height <= 0)
            throw new ArgumentOutOfRangeException(nameof(height), "Height must be positive.");
        if (string.IsNullOrWhiteSpace(unit))
            throw new ArgumentException("Unit must be provided.", nameof(unit));

        // Create the barcode generator for Code128 with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample"))
        {
            // Set image dimensions based on the requested unit.
            switch (unit.Trim().ToLowerInvariant())
            {
                case "pt":
                case "point":
                case "points":
                    generator.Parameters.ImageWidth.Point = (float)width;
                    generator.Parameters.ImageHeight.Point = (float)height;
                    break;
                case "px":
                case "pixel":
                case "pixels":
                    generator.Parameters.ImageWidth.Pixels = (float)width;
                    generator.Parameters.ImageHeight.Pixels = (float)height;
                    break;
                case "in":
                case "inch":
                case "inches":
                    generator.Parameters.ImageWidth.Inches = (float)width;
                    generator.Parameters.ImageHeight.Inches = (float)height;
                    break;
                case "mm":
                case "millimeter":
                case "millimeters":
                    generator.Parameters.ImageWidth.Millimeters = (float)width;
                    generator.Parameters.ImageHeight.Millimeters = (float)height;
                    break;
                default:
                    throw new ArgumentException($"Unsupported unit '{unit}'. Supported units: pt, px, in, mm.", nameof(unit));
            }

            // Save the barcode to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                return ms.ToArray();
            }
        }
    }
}