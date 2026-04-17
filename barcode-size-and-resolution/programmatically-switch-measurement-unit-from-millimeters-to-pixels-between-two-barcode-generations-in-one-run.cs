using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeUnitSwitchExample
{
    class Program
    {
        static void Main()
        {
            // Generate first barcode using millimeter units
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "MM123"))
            {
                // Set dimensions in millimeters
                generator.Parameters.Barcode.BarHeight.Millimeters = 15f;
                generator.Parameters.ImageWidth.Millimeters = 50f;
                generator.Parameters.ImageHeight.Millimeters = 30f;

                // Save the image
                generator.Save("barcode_mm.png");
            }

            // Generate second barcode using pixel units
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "PX456"))
            {
                // Set dimensions in pixels
                generator.Parameters.Barcode.BarHeight.Pixels = 60f;
                generator.Parameters.ImageWidth.Pixels = 200f;
                generator.Parameters.ImageHeight.Pixels = 100f;

                // Save the image
                generator.Save("barcode_px.png");
            }
        }
    }
}