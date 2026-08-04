// Title: Barcode Width Conversion Example
// Description: Demonstrates how to set a barcode image width using different measurement units and retrieve the resulting pixel width.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, AutoSizeMode, and ImageWidth properties. Developers often need to control barcode dimensions in various units (pixels, inches, millimeters, points) for printing, UI layout, or export scenarios. The snippet shows typical unit conversion and pixel retrieval, a common requirement when integrating barcodes into graphics pipelines.
// Prompt: Develop function accepting size value and unit enum, applying to BarCodeWidth and returning pixel width.
// Tags: barcode, width, unit conversion, code128, aspose.barcode, imagewidth, pixel, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeWidthExample
{
    // Supported units for setting barcode width
    enum SizeUnit
    {
        Pixels,
        Inches,
        Millimeters,
        Point
    }

    /// <summary>
    /// Contains methods that demonstrate setting barcode width in various units
    /// and obtaining the equivalent pixel width using Aspose.BarCode.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Sets the barcode image width according to the provided value and unit,
        /// then returns the calculated width in pixels.
        /// </summary>
        /// <param name="sizeValue">Numeric size value.</param>
        /// <param name="unit">Unit of measurement for the size.</param>
        /// <returns>Width of the barcode image in pixels.</returns>
        static int GetBarCodePixelWidth(float sizeValue, SizeUnit unit)
        {
            // Use Code128 as a simple symbology for the example
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                // Enable interpolation mode so ImageWidth controls the output size
                generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

                // Apply the size to the ImageWidth property using the selected unit
                switch (unit)
                {
                    case SizeUnit.Pixels:
                        generator.Parameters.ImageWidth.Pixels = sizeValue;
                        break;
                    case SizeUnit.Inches:
                        generator.Parameters.ImageWidth.Inches = sizeValue;
                        break;
                    case SizeUnit.Millimeters:
                        generator.Parameters.ImageWidth.Millimeters = sizeValue;
                        break;
                    case SizeUnit.Point:
                        generator.Parameters.ImageWidth.Point = sizeValue;
                        break;
                    default:
                        throw new ArgumentException("Unsupported size unit.", nameof(unit));
                }

                // The ImageWidth property now holds the value in all units.
                // Return the pixel representation.
                return (int)generator.Parameters.ImageWidth.Pixels;
            }
        }

        /// <summary>
        /// Entry point of the example. Calls <see cref="GetBarCodePixelWidth"/> with
        /// different units and writes the resulting pixel widths to the console.
        /// </summary>
        static void Main()
        {
            // Example usage with pixel unit
            int widthPx = GetBarCodePixelWidth(200f, SizeUnit.Pixels);
            Console.WriteLine($"Width set to 200 pixels => {widthPx} pixels");

            // Example usage with inches unit
            int widthInches = GetBarCodePixelWidth(2f, SizeUnit.Inches);
            Console.WriteLine($"Width set to 2 inches => {widthInches} pixels");

            // Example usage with millimeters unit
            int widthMm = GetBarCodePixelWidth(50f, SizeUnit.Millimeters);
            Console.WriteLine($"Width set to 50 mm => {widthMm} pixels");

            // Example usage with point unit
            int widthPt = GetBarCodePixelWidth(72f, SizeUnit.Point);
            Console.WriteLine($"Width set to 72 points => {widthPt} pixels");
        }
    }
}