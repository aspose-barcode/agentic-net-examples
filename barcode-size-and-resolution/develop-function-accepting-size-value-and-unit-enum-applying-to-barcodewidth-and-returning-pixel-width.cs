// Title: Set barcode image width using various units and retrieve pixel value
// Description: Demonstrates how to set the barcode image width in different measurement units (pixels, millimeters, inches, points, document units) using Aspose.BarCode and obtain the resulting pixel width.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of the BarcodeGenerator class and its Parameters.ImageWidth property. Developers often need to control barcode dimensions for printing, UI display, or PDF embedding, requiring conversion between units. The code illustrates typical unit‑setting patterns and how to read the calculated pixel width, a common requirement in barcode rendering workflows.
// Prompt: Develop function accepting size value and unit enum, applying to BarCodeWidth and returning pixel width.
// Tags: barcode, image width, unit conversion, aspose.barcode, generation, csharp

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeWidthExample
{
    /// <summary>
    /// Contains examples for setting barcode image width in various units and retrieving the pixel width.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Sets the barcode image width based on the supplied size and unit, then returns the calculated pixel width.
        /// </summary>
        /// <param name="size">The numeric value representing the desired width.</param>
        /// <param name="unit">The measurement unit to apply (pixels, millimeters, inches, points, or document units).</param>
        /// <returns>The width of the generated barcode image expressed in pixels.</returns>
        static float SetBarcodeWidth(float size, UnitEnum unit)
        {
            // Create a temporary barcode generator; the actual barcode text is irrelevant for width calculation.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
            {
                // Apply the requested unit to the ImageWidth property.
                switch (unit)
                {
                    case UnitEnum.Pixels:
                        generator.Parameters.ImageWidth.Pixels = size;
                        break;
                    case UnitEnum.Millimeters:
                        generator.Parameters.ImageWidth.Millimeters = size;
                        break;
                    case UnitEnum.Inches:
                        generator.Parameters.ImageWidth.Inches = size;
                        break;
                    case UnitEnum.Points:
                        generator.Parameters.ImageWidth.Point = size;
                        break;
                    case UnitEnum.Document:
                        generator.Parameters.ImageWidth.Document = size;
                        break;
                    default:
                        throw new ArgumentException("Unsupported unit.");
                }

                // Return the width converted to pixels.
                return generator.Parameters.ImageWidth.Pixels;
            }
        }

        /// <summary>
        /// Entry point of the example. Demonstrates setting barcode width in different units and printing the resulting pixel values.
        /// </summary>
        /// <param name="args">Command‑line arguments (not used).</param>
        static void Main(string[] args)
        {
            // Set width using pixels.
            float widthPixels = SetBarcodeWidth(3f, UnitEnum.Pixels);
            Console.WriteLine($"Width set as 3 pixels => {widthPixels} pixels");

            // Set width using millimeters.
            float widthMillimeters = SetBarcodeWidth(2f, UnitEnum.Millimeters);
            Console.WriteLine($"Width set as 2 millimeters => {widthMillimeters} pixels");

            // Set width using inches.
            float widthInches = SetBarcodeWidth(0.5f, UnitEnum.Inches);
            Console.WriteLine($"Width set as 0.5 inches => {widthInches} pixels");

            // Set width using points.
            float widthPoints = SetBarcodeWidth(12f, UnitEnum.Points);
            Console.WriteLine($"Width set as 12 points => {widthPoints} pixels");

            // Set width using document units.
            float widthDocument = SetBarcodeWidth(4f, UnitEnum.Document);
            Console.WriteLine($"Width set as 4 document units => {widthDocument} pixels");
        }
    }

    /// <summary>
    /// Enumeration of supported measurement units for barcode image width.
    /// </summary>
    enum UnitEnum
    {
        Pixels,
        Millimeters,
        Inches,
        Points,
        Document
    }
}