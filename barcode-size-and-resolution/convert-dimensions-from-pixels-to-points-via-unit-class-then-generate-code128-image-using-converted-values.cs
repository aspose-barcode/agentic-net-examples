using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Pixel dimensions and resolution (dpi)
        int pixelWidth = 300;
        int pixelHeight = 150;
        int resolutionDpi = 96; // default Aspose.BarCode resolution

        // Convert pixels to points (1 point = 1/72 inch)
        float widthPoints = pixelWidth * 72f / resolutionDpi;
        float heightPoints = pixelHeight * 72f / resolutionDpi;

        // Create a Code128 barcode generator with sample code text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Apply the converted dimensions using the Unit.Point member
            generator.Parameters.ImageWidth.Point = widthPoints;
            generator.Parameters.ImageHeight.Point = heightPoints;

            // Optional: set resolution to match the original DPI
            generator.Parameters.Resolution = resolutionDpi;

            // Save the barcode image
            generator.Save("code128.png");
        }

        Console.WriteLine("Barcode generated and saved as code128.png");
    }
}