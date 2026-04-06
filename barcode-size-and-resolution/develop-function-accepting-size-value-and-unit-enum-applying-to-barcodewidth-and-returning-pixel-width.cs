using System;
using Aspose.BarCode.Generation;

namespace BarcodeWidthExample
{
    // Supported units for setting barcode width
    enum SizeUnit
    {
        Pixels,
        Points,
        Inches,
        Millimeters
    }

    class Program
    {
        // Sets the barcode image width using the specified unit and returns the width in pixels.
        static float SetBarCodeWidth(BarcodeGenerator generator, float size, SizeUnit unit)
        {
            // Ensure the generator uses a sizing mode that respects ImageWidth.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Apply the size to the ImageWidth property using the appropriate unit member.
            switch (unit)
            {
                case SizeUnit.Pixels:
                    generator.Parameters.ImageWidth.Pixels = size;
                    break;
                case SizeUnit.Points:
                    generator.Parameters.ImageWidth.Point = size;
                    break;
                case SizeUnit.Inches:
                    generator.Parameters.ImageWidth.Inches = size;
                    break;
                case SizeUnit.Millimeters:
                    generator.Parameters.ImageWidth.Millimeters = size;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(unit), "Unsupported size unit.");
            }

            // Return the width expressed in pixels.
            return generator.Parameters.ImageWidth.Pixels;
        }

        static void Main()
        {
            // Example: set barcode width to 2 inches and obtain pixel width.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                generator.CodeText = "123456";

                float pixelWidth = SetBarCodeWidth(generator, 2f, SizeUnit.Inches);
                Console.WriteLine($"Barcode width set to 2 inches, which equals {pixelWidth} pixels.");

                // Save the barcode image to verify the result.
                generator.Save("barcode.png");
            }
        }
    }
}