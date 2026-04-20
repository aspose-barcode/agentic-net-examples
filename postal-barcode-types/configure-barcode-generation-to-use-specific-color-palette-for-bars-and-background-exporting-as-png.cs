using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        const string outputFile = "colored_barcode.png";

        // Create a barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set the color of the bars (foreground)
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Set the background color of the image
            generator.Parameters.BackColor = Color.LightYellow;

            // Define image dimensions (optional)
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Save the barcode as a PNG file
            generator.Save(outputFile);
        }

        Console.WriteLine($"Barcode image saved to {outputFile}");
    }
}