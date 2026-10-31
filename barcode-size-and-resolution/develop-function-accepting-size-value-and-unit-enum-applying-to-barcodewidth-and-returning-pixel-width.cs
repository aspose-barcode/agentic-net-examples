// Title: Barcode Width Conversion Example
// Description: Demonstrates how to set a barcode image width using different measurement units and retrieve the resulting pixel width.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of the BarcodeGenerator class and its Parameters.ImageWidth property. It illustrates typical scenarios where developers need to specify barcode dimensions in pixels, millimeters, inches, points, or document units and obtain the equivalent pixel value for rendering or further processing. Ideal for developers working on dynamic barcode sizing in web, desktop, or printing applications.
// Prompt: Develop function accepting size value and unit enum, applying to BarCodeWidth and returning pixel width.
// Tags: barcode, size conversion, imagewidth, aspose.barcode, csharp, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeSizeExample
{
    /// <summary>
    /// Enumeration of supported measurement units for barcode image width.
    /// </summary>
    enum UnitEnum
    {
        Pixels,
        Millimeters,
        Inches,
        Point,
        Document
    }

    /// <summary>
    /// Provides examples for converting barcode image width specifications across units using Aspose.BarCode.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Calculates the pixel width of a barcode image based on a size value and a measurement unit.
        /// </summary>
        /// <param name="sizeValue">The numeric size to apply.</param>
        /// <param name="unit">The unit of measurement.</param>
        /// <returns>The width in pixels after conversion.</returns>
        static float GetPixelWidth(float sizeValue, UnitEnum unit)
        {
            // Create a simple barcode generator; the actual code text is irrelevant for size calculation.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
            {
                // Ensure default resolution (96 DPI) for predictable conversion.
                generator.Parameters.Resolution = 96f;

                // Apply the size to ImageWidth using the specified unit.
                switch (unit)
                {
                    case UnitEnum.Pixels:
                        generator.Parameters.ImageWidth.Pixels = sizeValue;
                        break;
                    case UnitEnum.Millimeters:
                        generator.Parameters.ImageWidth.Millimeters = sizeValue;
                        break;
                    case UnitEnum.Inches:
                        generator.Parameters.ImageWidth.Inches = sizeValue;
                        break;
                    case UnitEnum.Point:
                        generator.Parameters.ImageWidth.Point = sizeValue;
                        break;
                    case UnitEnum.Document:
                        generator.Parameters.ImageWidth.Document = sizeValue;
                        break;
                    default:
                        throw new ArgumentException("Unsupported unit.", nameof(unit));
                }

                // Retrieve and return the calculated pixel width.
                return generator.Parameters.ImageWidth.Pixels;
            }
        }

        /// <summary>
        /// Entry point of the example. Demonstrates width conversion for each supported unit.
        /// </summary>
        static void Main()
        {
            float size = 10f;

            // Convert from pixels to pixels (identity conversion).
            float pixels = GetPixelWidth(size, UnitEnum.Pixels);
            Console.WriteLine($"Size {size} Pixels => {pixels} pixels");

            // Convert from millimeters to pixels.
            float millimeters = GetPixelWidth(size, UnitEnum.Millimeters);
            Console.WriteLine($"Size {size} Millimeters => {millimeters} pixels");

            // Convert from inches to pixels.
            float inches = GetPixelWidth(size, UnitEnum.Inches);
            Console.WriteLine($"Size {size} Inches => {inches} pixels");

            // Convert from points to pixels.
            float points = GetPixelWidth(size, UnitEnum.Point);
            Console.WriteLine($"Size {size} Points => {points} pixels");

            // Convert from document units to pixels.
            float document = GetPixelWidth(size, UnitEnum.Document);
            Console.WriteLine($"Size {size} Document units => {document} pixels");
        }
    }
}