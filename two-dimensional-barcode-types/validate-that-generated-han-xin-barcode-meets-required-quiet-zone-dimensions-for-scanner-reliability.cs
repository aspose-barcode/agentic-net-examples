using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Define required quiet zone (padding) in points
        const float requiredQuietZonePoints = 5f;

        // Sample Han Xin code text
        const string codeText = "1234567890ABCDEFGabcdefg,Han Xin Code";

        // Output file name
        const string outputFile = "hanxin.png";

        // Create barcode generator for Han Xin symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, codeText))
        {
            // Set padding (quiet zone) on all sides
            generator.Parameters.Barcode.Padding.Left.Point = requiredQuietZonePoints;
            generator.Parameters.Barcode.Padding.Top.Point = requiredQuietZonePoints;
            generator.Parameters.Barcode.Padding.Right.Point = requiredQuietZonePoints;
            generator.Parameters.Barcode.Padding.Bottom.Point = requiredQuietZonePoints;

            // Save the barcode image
            generator.Save(outputFile);

            // Validate that the padding meets the required quiet zone dimensions
            bool isValid =
                generator.Parameters.Barcode.Padding.Left.Point >= requiredQuietZonePoints &&
                generator.Parameters.Barcode.Padding.Top.Point >= requiredQuietZonePoints &&
                generator.Parameters.Barcode.Padding.Right.Point >= requiredQuietZonePoints &&
                generator.Parameters.Barcode.Padding.Bottom.Point >= requiredQuietZonePoints;

            Console.WriteLine(isValid
                ? "Quiet zone dimensions meet the required specifications."
                : "Quiet zone dimensions are insufficient.");
        }
    }
}