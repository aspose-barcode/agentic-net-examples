using System;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a DataMatrix barcode generator with sample code text
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "ABC123"))
        {
            // Set individual paddings (in points)
            generator.Parameters.Barcode.Padding.Left.Point = 10f;   // left padding
            generator.Parameters.Barcode.Padding.Top.Point = 20f;    // top padding
            generator.Parameters.Barcode.Padding.Right.Point = 15f;  // right padding
            generator.Parameters.Barcode.Padding.Bottom.Point = 5f;  // bottom padding

            // Optionally adjust other parameters (e.g., image resolution)
            generator.Parameters.Resolution = 150;

            // Save the barcode image to a PNG file
            generator.Save("DataMatrixWithPadding.png");
        }

        Console.WriteLine("Barcode image generated successfully.");
    }
}