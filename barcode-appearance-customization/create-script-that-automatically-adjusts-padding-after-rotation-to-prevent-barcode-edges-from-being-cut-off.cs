using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the data to encode.
            generator.CodeText = "1234567890";

            // Rotate the barcode image (example: 45 degrees).
            generator.Parameters.RotationAngle = 45f;

            // Add sufficient padding on all sides to avoid clipping after rotation.
            // Here we use a fixed padding of 30 points; adjust as needed for different angles.
            float padding = 30f;
            generator.Parameters.Barcode.Padding.Top.Point = padding;
            generator.Parameters.Barcode.Padding.Bottom.Point = padding;
            generator.Parameters.Barcode.Padding.Left.Point = padding;
            generator.Parameters.Barcode.Padding.Right.Point = padding;

            // Optionally define image dimensions to ensure enough space.
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Save the generated barcode image.
            generator.Save("rotated_barcode.png");
        }

        Console.WriteLine("Barcode generated with adjusted padding.");
    }
}