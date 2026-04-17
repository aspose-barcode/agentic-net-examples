using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

class Program
{
    static void Main()
    {
        // Define output file path
        const string outputPath = "maxicode_custom_quietzone.png";

        // Create a MaxiCode generator with sample codetext
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "Sample MaxiCode"))
        {
            // Set custom quiet zone (padding) around the barcode
            // Values are in points; adjust as needed for scanning requirements
            generator.Parameters.Barcode.Padding.Left.Point = 20f;
            generator.Parameters.Barcode.Padding.Top.Point = 20f;
            generator.Parameters.Barcode.Padding.Right.Point = 20f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 20f;

            // Optional: set barcode colors if desired
            // generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            // generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Save the generated barcode image
            generator.Save(outputPath);
        }

        Console.WriteLine($"MaxiCode barcode saved to: {outputPath}");
    }
}