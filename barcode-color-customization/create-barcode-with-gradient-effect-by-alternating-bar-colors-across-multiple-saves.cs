using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Text to encode in the barcode
        string codeText = "GRADIENT123";

        // Colors that will be used to create a gradient effect
        Aspose.Drawing.Color[] colors = new Aspose.Drawing.Color[]
        {
            Aspose.Drawing.Color.Red,
            Aspose.Drawing.Color.Orange,
            Aspose.Drawing.Color.Yellow,
            Aspose.Drawing.Color.Green,
            Aspose.Drawing.Color.Blue,
            Aspose.Drawing.Color.Indigo,
            Aspose.Drawing.Color.Violet
        };

        // Generate a barcode image for each color and save it
        for (int i = 0; i < colors.Length; i++)
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Apply the current bar color
                generator.Parameters.Barcode.BarColor = colors[i];

                // Optional: set image dimensions
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 150f;

                // Save the barcode with a distinct filename
                string fileName = $"barcode_gradient_{i + 1}.png";
                generator.Save(fileName);
            }
        }
    }
}