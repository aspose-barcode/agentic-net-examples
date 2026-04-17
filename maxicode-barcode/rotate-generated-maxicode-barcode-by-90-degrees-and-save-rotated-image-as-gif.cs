using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Create a MaxiCode barcode generator with sample codetext
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "Test MaxiCode"))
        {
            // Rotate the generated image by 90 degrees
            generator.Parameters.RotationAngle = 90f;

            // Save the rotated barcode as a GIF image
            generator.Save("maxicode_rotated.gif");
        }
    }
}