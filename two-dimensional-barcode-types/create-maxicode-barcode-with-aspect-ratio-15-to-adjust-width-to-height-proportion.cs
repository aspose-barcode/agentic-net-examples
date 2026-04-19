using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a MaxiCode barcode generator with sample codetext
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "Sample MaxiCode"))
        {
            // Set the aspect ratio (Height/Width) to 1.5
            generator.Parameters.Barcode.MaxiCode.AspectRatio = 1.5f;

            // Save the generated barcode image to a file
            generator.Save("maxicode.png");
        }
    }
}