using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a code text that is intentionally too large for the smallest Han Xin symbol.
        string oversizedText = new string('A', 2000);

        try
        {
            // Initialize the generator with Han Xin symbology and the oversized text.
            using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, oversizedText))
            {
                // Force the smallest symbol size to trigger a capacity overflow.
                // The Version enum is part of HanXinParameters; Version01 represents the smallest size.
                generator.Parameters.Barcode.HanXin.Version = HanXinVersion.Version01;

                // Use the default encoding mode (Auto) – you can change it if needed.
                generator.Parameters.Barcode.HanXin.EncodeMode = HanXinEncodeMode.Auto;

                // Attempt to save the barcode image. This will throw if the data does not fit.
                generator.Save("hanxin_exceed.bmp");

                Console.WriteLine("Barcode generated successfully.");
            }
        }
        catch (BarCodeException ex)
        {
            // Handle the situation where the data exceeds the symbol's capacity.
            Console.WriteLine("Error generating Han Xin barcode: " + ex.Message);
        }
    }
}