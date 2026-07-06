// Title: Barcode width conversion example
// Description: Demonstrates how to set barcode image width using different measurement units and retrieve the resulting pixel width.
// Category-Description: This example belongs to the Aspose.BarCode image sizing category, illustrating the use of BarcodeGenerator, ImageWidth, and unit conversion properties. Developers often need to define barcode dimensions in pixels, inches, millimeters, or points to fit layout requirements, and this snippet shows the typical approach for such operations.
// Prompt: Develop function accepting size value and unit enum, applying to BarCodeWidth and returning pixel width.
// Tags: barcode, width, unit conversion, image sizing, aspnet, aspose.barcode, code128

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeWidthExample
{
    // Supported units for setting barcode width
    enum WidthUnit
    {
        Pixels,
        Inches,
        Millimeters,
        Points
    }

    /// <summary>
    /// Demonstrates setting barcode width using various units and retrieving pixel width.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Sets the barcode image width according to the supplied size and unit,
        /// then returns the calculated width in pixels.
        /// </summary>
        /// <param name="size">The numeric size value.</param>
        /// <param name="unit">The measurement unit for the size.</param>
        /// <returns>Width of the barcode image in whole pixels.</returns>
        static int SetBarcodeWidth(float size, WidthUnit unit)
        {
            // Use Code128 as a simple symbology for the example
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                // Assign the width based on the selected unit
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
                    default:
                        throw new ArgumentOutOfRangeException(nameof(unit), "Unsupported width unit.");
                }

                // Return the width expressed in pixels (rounded down to integer)
                return (int)generator.Parameters.ImageWidth.Pixels;
            }
        }

        /// <summary>
        /// Entry point demonstrating SetBarcodeWidth with different units.
        /// </summary>
        static void Main()
        {
            // Example usage: width specified in pixels
            int widthPx1 = SetBarcodeWidth(200f, WidthUnit.Pixels);
            Console.WriteLine($"Width set to 200 pixels => {widthPx1} pixels");

            // Example usage: width specified in inches
            int widthPx2 = SetBarcodeWidth(2.5f, WidthUnit.Inches);
            Console.WriteLine($"Width set to 2.5 inches => {widthPx2} pixels");

            // Example usage: width specified in millimeters
            int widthPx3 = SetBarcodeWidth(50f, WidthUnit.Millimeters);
            Console.WriteLine($"Width set to 50 millimeters => {widthPx3} pixels");

            // Example usage: width specified in points
            int widthPx4 = SetBarcodeWidth(72f, WidthUnit.Points);
            Console.WriteLine($"Width set to 72 points => {widthPx4} pixels");
        }
    }
}