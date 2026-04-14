using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Define canvas size (points)
        const float canvasWidth = 300f;
        const float canvasHeight = 300f;

        // Create MaxiCode codetext (Mode 2 example)
        var maxiCodeCodetext = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",
            CountryCode = 56,
            ServiceCategory = 999,
            SecondMessage = new MaxiCodeStandardSecondMessage { Message = "Sample MaxiCode" }
        };

        // Initialize ComplexBarcodeGenerator with the codetext
        using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            // Use interpolation auto-size to fit the canvas
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            generator.Parameters.ImageWidth.Point = canvasWidth;
            generator.Parameters.ImageHeight.Point = canvasHeight;

            // Set custom padding (points)
            generator.Parameters.Barcode.Padding.Left.Point = 10f;
            generator.Parameters.Barcode.Padding.Top.Point = 10f;
            generator.Parameters.Barcode.Padding.Right.Point = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 10f;

            // Generate the barcode image
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Rotate the image by 90 degrees
                bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);

                // Save the final image
                bitmap.Save("MaxiCode.png", ImageFormat.Png);
            }
        }

        Console.WriteLine("MaxiCode barcode generated and saved as MaxiCode.png");
    }
}