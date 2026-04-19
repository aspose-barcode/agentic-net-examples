using System;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeColorSchemes
{
    class Program
    {
        static void Main()
        {
            // Define a set of color schemes (bar color, background color, file suffix)
            var colorSchemes = new List<(Color BarColor, Color BackColor, string Suffix)>
            {
                (Color.Black, Color.White, "black_on_white"),
                (Color.White, Color.Black, "white_on_black"),
                (Color.Blue, Color.LightGray, "blue_on_lightgray"),
                (Color.Green, Color.Yellow, "green_on_yellow")
            };

            // Create a single BarcodeGenerator instance for Code128 with sample text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                // Iterate over each color scheme, apply colors, and save the image
                foreach (var scheme in colorSchemes)
                {
                    generator.Parameters.Barcode.BarColor = scheme.BarColor;
                    generator.Parameters.BackColor = scheme.BackColor;

                    string fileName = $"barcode_{scheme.Suffix}.png";
                    generator.Save(fileName);
                    Console.WriteLine($"Saved barcode image: {fileName}");
                }
            }
        }
    }
}