using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        int heightWithText = GetBarcodeHeight(showText: true);
        int heightWithoutText = GetBarcodeHeight(showText: false);

        Console.WriteLine($"Height with text: {heightWithText}");
        Console.WriteLine($"Height without text: {heightWithoutText}");

        if (heightWithoutText < heightWithText)
            Console.WriteLine("PASS: Hiding the CodeText reduces the image height.");
        else
            Console.WriteLine("FAIL: Image height did not change when CodeText was hidden.");
    }

    static int GetBarcodeHeight(bool showText)
    {
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            generator.Parameters.Barcode.CodeTextParameters.Location = showText ? CodeLocation.Below : CodeLocation.None;

            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                return bitmap.Height;
            }
        }
    }
}