using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

namespace BarcodeWidthExample
{
    // Supported units for setting the barcode width
    enum SizeUnit
    {
        Point,
        Pixel,
        Inch,
        Millimeter
    }

    /// <summary>
    /// Demonstrates how to set barcode width using different measurement units
    /// and retrieve the resulting pixel width.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the application. Calls <see cref="GetBarCodePixelWidth"/>
        /// with various units and prints the resulting pixel widths.
        /// </summary>
        static void Main()
        {
            // Sample calls demonstrating different units
            Console.WriteLine("Width (100 points)  = " + GetBarCodePixelWidth(100f, SizeUnit.Point) + " px");
            Console.WriteLine("Width (200 pixels) = " + GetBarCodePixelWidth(200f, SizeUnit.Pixel) + " px");
            Console.WriteLine("Width (2 inches)   = " + GetBarCodePixelWidth(2f, SizeUnit.Inch) + " px");
            Console.WriteLine("Width (50 mm)      = " + GetBarCodePixelWidth(50f, SizeUnit.Millimeter) + " px");
        }

        /// <summary>
        /// Creates a barcode, applies the requested width, and returns the resulting pixel width.
        /// </summary>
        /// <param name="sizeValue">The numeric value of the width.</param>
        /// <param name="unit">The measurement unit for the width.</param>
        /// <returns>Pixel width of the generated barcode image.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="sizeValue"/> is not greater than zero.</exception>
        /// <exception cref="ArgumentException">Thrown when an unsupported <paramref name="unit"/> is provided.</exception>
        static int GetBarCodePixelWidth(float sizeValue, SizeUnit unit)
        {
            // Validate input
            if (sizeValue <= 0f)
                throw new ArgumentOutOfRangeException(nameof(sizeValue), "Size value must be greater than zero.");

            // Use Code128 as a simple 1D barcode for the example
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample"))
            {
                // Use interpolation mode so that ImageWidth controls the final size
                generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

                // Apply the width in the requested unit
                switch (unit)
                {
                    case SizeUnit.Point:
                        generator.Parameters.ImageWidth.Point = sizeValue;
                        break;
                    case SizeUnit.Pixel:
                        generator.Parameters.ImageWidth.Pixels = sizeValue;
                        break;
                    case SizeUnit.Inch:
                        generator.Parameters.ImageWidth.Inches = sizeValue;
                        break;
                    case SizeUnit.Millimeter:
                        generator.Parameters.ImageWidth.Millimeters = sizeValue;
                        break;
                    default:
                        throw new ArgumentException("Unsupported unit.", nameof(unit));
                }

                // Save to a memory stream and load the image to read its pixel dimensions
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    ms.Position = 0;
                    using (var bitmap = (Bitmap)Image.FromStream(ms))
                    {
                        return bitmap.Width; // Pixel width of the generated barcode image
                    }
                }
            }
        }
    }
}