using System;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set the resolution to 72 DPI for quick thumbnail preview.
            generator.Parameters.Resolution = 72f;

            // Save the generated barcode image.
            generator.Save("barcode_thumbnail.png");
        }
    }
}