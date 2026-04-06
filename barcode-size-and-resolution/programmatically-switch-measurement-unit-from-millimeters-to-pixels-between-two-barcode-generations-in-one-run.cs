using System;
using Aspose.BarCode.Generation;

namespace BarcodeUnitSwitchExample
{
    class Program
    {
        static void Main()
        {
            // Generate first barcode using millimeter units
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "FirstMM"))
            {
                // Set image size in millimeters
                generator.Parameters.ImageWidth.Millimeters = 50f;
                generator.Parameters.ImageHeight.Millimeters = 30f;

                // Set barcode dimensions in millimeters
                generator.Parameters.Barcode.XDimension.Millimeters = 0.5f;
                generator.Parameters.Barcode.BarHeight.Millimeters = 10f;

                // Save the barcode image
                generator.Save("barcode_mm.png");
            }

            // Generate second barcode using pixel units
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "SecondPx"))
            {
                // Set image size in pixels
                generator.Parameters.ImageWidth.Pixels = 400f;
                generator.Parameters.ImageHeight.Pixels = 200f;

                // Set barcode dimensions in pixels
                generator.Parameters.Barcode.XDimension.Pixels = 4f;
                generator.Parameters.Barcode.BarHeight.Pixels = 80f;

                // Save the barcode image
                generator.Save("barcode_px.png");
            }
        }
    }
}