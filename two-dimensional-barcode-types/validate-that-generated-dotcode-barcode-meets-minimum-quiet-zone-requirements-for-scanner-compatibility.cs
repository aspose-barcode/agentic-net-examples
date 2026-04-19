using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Minimum quiet zone (padding) required for scanner compatibility, in points.
        const float minimumQuietZonePoints = 10f;

        // Sample codetext for DotCode. It can be any string; using numeric for simplicity.
        const string dotCodeText = "1234567890";

        // Create the DotCode barcode generator.
        using (var generator = new BarcodeGenerator(EncodeTypes.DotCode, dotCodeText))
        {
            // Retrieve current padding (quiet zone) values.
            float leftPadding = generator.Parameters.Barcode.Padding.Left.Point;
            float rightPadding = generator.Parameters.Barcode.Padding.Right.Point;
            float topPadding = generator.Parameters.Barcode.Padding.Top.Point;
            float bottomPadding = generator.Parameters.Barcode.Padding.Bottom.Point;

            // Validate each side against the minimum requirement.
            bool isValid = leftPadding >= minimumQuietZonePoints &&
                           rightPadding >= minimumQuietZonePoints &&
                           topPadding >= minimumQuietZonePoints &&
                           bottomPadding >= minimumQuietZonePoints;

            if (!isValid)
            {
                Console.WriteLine("Quiet zone is insufficient. Adjusting to meet the minimum requirement...");

                // Adjust padding to meet the minimum quiet zone.
                generator.Parameters.Barcode.Padding.Left.Point = minimumQuietZonePoints;
                generator.Parameters.Barcode.Padding.Right.Point = minimumQuietZonePoints;
                generator.Parameters.Barcode.Padding.Top.Point = minimumQuietZonePoints;
                generator.Parameters.Barcode.Padding.Bottom.Point = minimumQuietZonePoints;
            }
            else
            {
                Console.WriteLine("Quiet zone meets the minimum requirement.");
            }

            // Save the barcode image.
            const string outputFile = "dotcode.png";
            generator.Save(outputFile);
            Console.WriteLine($"DotCode barcode saved to '{outputFile}'.");
        }
    }
}