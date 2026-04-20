using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    // Generates a barcode image and returns it as a PNG byte array.
    // encodeType: the barcode symbology (e.g., EncodeTypes.Code128)
    // codeText: the text to encode in the barcode
    static byte[] GenerateBarcode(BaseEncodeType encodeType, string codeText)
    {
        if (string.IsNullOrEmpty(codeText))
            throw new ArgumentException("codeText cannot be null or empty.", nameof(codeText));

        using (var memoryStream = new MemoryStream())
        {
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                generator.Parameters.AutoSizeMode = AutoSizeMode.None;
                generator.Parameters.Barcode.BarColor = Color.Black;
                generator.Parameters.BackColor = Color.White;

                generator.Save(memoryStream, BarCodeImageFormat.Png);
            }

            return memoryStream.ToArray();
        }
    }

    static void Main()
    {
        byte[] pngData = GenerateBarcode(EncodeTypes.Code128, "123ABC");
        File.WriteAllBytes("sample_barcode.png", pngData);
        Console.WriteLine("Barcode generated successfully. Bytes length: " + pngData.Length);
    }
}