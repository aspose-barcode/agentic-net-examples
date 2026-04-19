using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

namespace BarcodeExportExample
{
    class Program
    {
        static void Main()
        {
            // Define output file name
            const string outputPath = "custom_barcode.png";

            // Create a barcode generator for Code128 with sample codetext
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // ----- Human‑readable text (CodeText) customization -----
                // Set custom font
                generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
                generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

                // Align the text to the center below the barcode
                generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;

                // Add extra space between the barcode and the text
                generator.Parameters.Barcode.CodeTextParameters.Space.Point = 5f;

                // ----- Padding (spacing around the barcode image) -----
                generator.Parameters.Barcode.Padding.Left.Point = 10f;
                generator.Parameters.Barcode.Padding.Top.Point = 10f;
                generator.Parameters.Barcode.Padding.Right.Point = 10f;
                generator.Parameters.Barcode.Padding.Bottom.Point = 10f;

                // ----- Image size and resolution (optional) -----
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 150f;
                generator.Parameters.Resolution = 300; // DPI

                // Save the barcode as PNG
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            Console.WriteLine($"Barcode image saved to: {outputPath}");
        }
    }
}