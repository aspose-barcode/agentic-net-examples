using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Text to encode
        const string codeText = "1234567890";

        // Output file names
        string[] fileNames = {
            "barcode_red.png",
            "barcode_orange.png",
            "barcode_yellow.png",
            "barcode_green.png",
            "barcode_blue.png"
        };

        // Corresponding bar colors for a simple gradient effect
        Color[] barColors = {
            Color.Red,
            Color.Orange,
            Color.Yellow,
            Color.Green,
            Color.Blue
        };

        // Generate a barcode image for each color
        for (int i = 0; i < fileNames.Length; i++)
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Set the bar (foreground) color
                generator.Parameters.Barcode.BarColor = barColors[i];

                // Save the barcode image
                generator.Save(fileNames[i]);
            }
        }
    }
}