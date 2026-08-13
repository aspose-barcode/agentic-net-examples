// Title: Unit conversion helper for barcode dimensions
// Description: Demonstrates converting inches to millimeters and vice‑versa for sizing Aspose.BarCode images and XDimension.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how to use the BarcodeGenerator class with size parameters. It shows typical use cases such as setting image dimensions in inches and barcode module size (XDimension) in millimeters, helping developers who need precise physical measurements for printed barcodes. The snippet highlights the Parameters.ImageWidth, Parameters.Barcode.XDimension properties and custom conversion helpers.
// Prompt: Create helper method converting values between Inches and Millimeters for barcode size calculations.
// Tags: barcode, conversion, inches, millimeters, aspose.barcode, generation, dimensions

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeConversionHelper
{
    /// <summary>
    /// Helper methods for converting between inches and millimeters.
    /// </summary>
    public static class UnitConverter
    {
        // Conversion factor: 1 inch = 25.4 millimeters
        private const float InchesToMillimetersFactor = 25.4f;

        /// <summary>
        /// Converts inches to millimeters.
        /// </summary>
        /// <param name="inches">Value in inches.</param>
        /// <returns>Equivalent value in millimeters.</returns>
        public static float InchesToMillimeters(float inches)
        {
            return inches * InchesToMillimetersFactor;
        }

        /// <summary>
        /// Converts millimeters to inches.
        /// </summary>
        /// <param name="millimeters">Value in millimeters.</param>
        /// <returns>Equivalent value in inches.</returns>
        public static float MillimetersToInches(float millimeters)
        {
            return millimeters / InchesToMillimetersFactor;
        }
    }

    /// <summary>
    /// Demonstrates usage of UnitConverter with Aspose.BarCode to generate a barcode image.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the example. Performs sample conversions and creates a barcode image.
        /// </summary>
        static void Main()
        {
            // Sample conversion: inches to millimeters
            float widthInInches = 2.5f;
            float widthInMillimeters = UnitConverter.InchesToMillimeters(widthInInches);
            Console.WriteLine($"Width: {widthInInches} inches = {widthInMillimeters} mm");

            // Sample conversion: millimeters to inches
            float heightInMillimeters = 50f;
            float heightInInches = UnitConverter.MillimetersToInches(heightInMillimeters);
            Console.WriteLine($"Height: {heightInMillimeters} mm = {heightInInches} inches");

            // Example usage with Aspose.BarCode:
            // - Set image width in inches
            // - Convert desired XDimension from millimeters to inches before assigning
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                // Set image width to 3 inches
                generator.Parameters.ImageWidth.Inches = 3f;

                // Desired XDimension is 0.5 mm; convert to inches for the generator
                float xDimMillimeters = 0.5f;
                float xDimInches = UnitConverter.MillimetersToInches(xDimMillimeters);
                generator.Parameters.Barcode.XDimension.Inches = xDimInches;

                // Set the code text to encode
                generator.CodeText = "12345";

                // Save the barcode image to file
                generator.Save("barcode.png");
            }

            Console.WriteLine("Barcode generated and saved as barcode.png");
        }
    }
}