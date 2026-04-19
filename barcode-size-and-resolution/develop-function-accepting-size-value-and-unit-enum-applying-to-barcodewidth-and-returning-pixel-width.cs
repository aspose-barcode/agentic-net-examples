using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace AsposeBarcodeWidthDemo
{
    enum WidthUnit
    {
        Pixels,
        Inches,
        Millimeters,
        Points,
        Document
    }

    class Program
    {
        static float SetBarCodeWidth(float size, WidthUnit unit)
        {
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
            {
                switch (unit)
                {
                    case WidthUnit.Pixels:
                        generator.Parameters.ImageWidth.Pixels = size;
                        break;
                    case WidthUnit.Inches:
                        generator.Parameters.ImageWidth.Inches = size;
                        break;
                    case WidthUnit.Millimeters:
                        generator.Parameters.ImageWidth.Millimeters = size;
                        break;
                    case WidthUnit.Points:
                        generator.Parameters.ImageWidth.Point = size;
                        break;
                    case WidthUnit.Document:
                        generator.Parameters.ImageWidth.Document = size;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(unit), "Unsupported width unit.");
                }

                return generator.Parameters.ImageWidth.Pixels;
            }
        }

        static void Main()
        {
            float pixelWidth = SetBarCodeWidth(200f, WidthUnit.Pixels);
            Console.WriteLine($"BarCode width in pixels: {pixelWidth}");

            pixelWidth = SetBarCodeWidth(2f, WidthUnit.Inches);
            Console.WriteLine($"BarCode width for 2 inches (pixels): {pixelWidth}");
        }
    }
}