// Title: Set Barcode Image Width Using Various Units and Retrieve Pixel Width
// Description: Demonstrates how to assign a barcode image width in different measurement units using Aspose.BarCode and obtain the resulting pixel width.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of the BarcodeGenerator, EncodeTypes, and ImageWidth properties. Developers often need to control barcode dimensions across units such as pixels, millimeters, inches, points, or document units to fit layout requirements. The snippet shows typical usage for setting size, auto‑sizing mode, and converting to pixels, a common task when integrating barcodes into reports or UI elements.
// Prompt: Develop function accepting size value and unit enum, applying to BarCodeWidth and returning pixel width.
// Tags: barcode, size, imagewidth, generation, aspose.barcode, code128, units, pixels, millimeters, inches, point, document

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeSizeExample
{
    // Measurement units that can be applied to the barcode image width
    enum SizeUnit
    {
        Pixels,
        Millimeters,
        Inches,
        Point,
        Document
    }

    /// <summary>
    /// Example program demonstrating barcode width unit conversion.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Sets the barcode image width using the specified size value and unit,
        /// then returns the calculated width in pixels.
        /// </summary>
        /// <param name="sizeValue">Numeric value of the desired width.</param>
        /// <param name="unit">Unit of measurement for the width.</param>
        /// <returns>Width of the barcode image in pixels.</returns>
        static float GetPixelWidth(float sizeValue, SizeUnit unit)
        {
            // Create a barcode generator for Code128 with sample data
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123"))
            {
                // Use nearest auto‑size mode to let the generator adjust other dimensions
                generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;

                // Apply the requested width based on the selected unit
                switch (unit)
                {
                    case SizeUnit.Pixels:
                        generator.Parameters.ImageWidth.Pixels = sizeValue;
                        break;
                    case SizeUnit.Millimeters:
                        generator.Parameters.ImageWidth.Millimeters = sizeValue;
                        break;
                    case SizeUnit.Inches:
                        generator.Parameters.ImageWidth.Inches = sizeValue;
                        break;
                    case SizeUnit.Point:
                        generator.Parameters.ImageWidth.Point = sizeValue;
                        break;
                    case SizeUnit.Document:
                        generator.Parameters.ImageWidth.Document = sizeValue;
                        break;
                }

                // Return the width converted to pixels
                return generator.Parameters.ImageWidth.Pixels;
            }
        }

        /// <summary>
        /// Entry point that prints pixel widths for various unit inputs.
        /// </summary>
        static void Main()
        {
            // Example: 2 millimeters converted to pixels
            float widthPixels = GetPixelWidth(2f, SizeUnit.Millimeters);
            Console.WriteLine($"Width in pixels (2 mm): {widthPixels}");

            // Example: 100 pixels (no conversion needed)
            widthPixels = GetPixelWidth(100f, SizeUnit.Pixels);
            Console.WriteLine($"Width in pixels (100 px): {widthPixels}");

            // Example: 1 inch converted to pixels
            widthPixels = GetPixelWidth(1f, SizeUnit.Inches);
            Console.WriteLine($"Width in pixels (1 inch): {widthPixels}");
        }
    }
}