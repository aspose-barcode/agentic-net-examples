using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Sample code texts for DataBar Omnidirectional barcodes
        string[] codeTexts = new string[]
        {
            "01234567890123",
            "12345678901234",
            "23456789012345",
            "34567890123456",
            "45678901234567"
        };

        // Generate a barcode image for each code text
        for (int i = 0; i < codeTexts.Length; i++)
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.DatabarOmniDirectional, codeTexts[i]))
            {
                // Ensure BarHeight is respected
                generator.Parameters.AutoSizeMode = AutoSizeMode.None;
                // Set bar height to 50 pixels
                generator.Parameters.Barcode.BarHeight.Pixels = 50f;
                // Optional: set background to white and bar color to black (defaults are already set)
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

                // Save as JPEG
                string fileName = $"databar_{i + 1}.jpg";
                generator.Save(fileName);
            }
        }
    }
}