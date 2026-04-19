using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // GS1 AI data: (01) – GTIN, (10) – Batch/Lot
        string codeText = "(01)01234567890128(10)ABC123";

        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, codeText))
        {
            // Optional: set image dimensions
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Save the barcode as PNG
            generator.Save("gs1code128.png");
        }
    }
}