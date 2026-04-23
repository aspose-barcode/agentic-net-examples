using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Sample original identifier for Swiss Post Parcel domestic barcode
        const string codeText = "12345678901234567890";

        // Create the barcode generator for Swiss Post Parcel
        using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, codeText))
        {
            // Set custom margins (padding) around the barcode image
            generator.Parameters.Barcode.Padding.Left.Point = 15f;
            generator.Parameters.Barcode.Padding.Top.Point = 15f;
            generator.Parameters.Barcode.Padding.Right.Point = 15f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 15f;

            // Optionally set image size
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Generate the barcode image
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Save the image to a PNG file
                bitmap.Save("SwissPostParcel.png", ImageFormat.Png);
            }
        }
    }
}