using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Define different color schemes and output file names
        var schemes = new (Color BarColor, Color BackColor, string FileName)[]
        {
            (Color.Blue, Color.White, "barcode_blue_on_white.png"),
            (Color.White, Color.Black, "barcode_white_on_black.png"),
            (Color.Red, Color.Yellow, "barcode_red_on_yellow.png"),
            (Color.Green, Color.LightGray, "barcode_green_on_lightgray.png")
        };

        // Single BarcodeGenerator instance for Code128 barcode
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
        {
            // Optional common settings
            generator.Parameters.Barcode.BarHeight.Point = 40f;
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Generate and save barcodes with each color scheme
            foreach (var scheme in schemes)
            {
                generator.Parameters.Barcode.BarColor = scheme.BarColor;
                generator.Parameters.BackColor = scheme.BackColor;
                generator.Save(scheme.FileName, BarCodeImageFormat.Png);
            }
        }
    }
}