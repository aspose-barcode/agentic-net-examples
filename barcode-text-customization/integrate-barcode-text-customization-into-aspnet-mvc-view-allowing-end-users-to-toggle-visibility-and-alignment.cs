using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Simulated user options
        bool showHumanReadableText = true;               // Toggle visibility
        TextAlignment textAlignment = TextAlignment.Right; // Choose alignment

        // Create a barcode generator for Code128
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Configure barcode text visibility
            generator.Parameters.Barcode.CodeTextParameters.Location = showHumanReadableText
                ? CodeLocation.Below
                : CodeLocation.None;

            // Configure barcode text alignment
            generator.Parameters.Barcode.CodeTextParameters.Alignment = textAlignment;

            // Optional: customize font and color
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 10f;
            generator.Parameters.Barcode.CodeTextParameters.Color = Aspose.Drawing.Color.DarkBlue;

            // Save the generated barcode image
            string outputPath = "customized_barcode.png";
            generator.Save(outputPath, BarCodeImageFormat.Png);
            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }
}