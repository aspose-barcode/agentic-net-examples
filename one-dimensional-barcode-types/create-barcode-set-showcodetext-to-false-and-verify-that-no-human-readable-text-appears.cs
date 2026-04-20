using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Hide the human‑readable text
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Verify that the text is hidden
            bool isHidden = generator.Parameters.Barcode.CodeTextParameters.Location == CodeLocation.None;
            Console.WriteLine("Code text hidden: " + isHidden);

            // Save the barcode image
            generator.Save("barcode.png");
        }
    }
}