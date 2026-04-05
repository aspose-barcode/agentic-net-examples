using System;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Define output file path
        string outputPath = "rotated_barcode.png";

        // Create a barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            // Set rotation angle (e.g., 90 degrees)
            generator.Parameters.RotationAngle = 90f;

            // Increase padding on all sides to avoid cutting off edges after rotation
            // Padding values are set in points; adjust as needed
            generator.Parameters.Barcode.Padding.Left.Point = 15f;
            generator.Parameters.Barcode.Padding.Top.Point = 15f;
            generator.Parameters.Barcode.Padding.Right.Point = 15f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 15f;

            // Save the rotated barcode image
            generator.Save(outputPath);
        }

        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}