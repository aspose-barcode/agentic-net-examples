using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Generate barcode with default BarHeight
        int defaultWidth, defaultHeight;
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;
            generator.Parameters.Barcode.XDimension.Point = 2f; // module width
            // Do NOT set BarHeight to let it use automatic calculation
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0;
                using (var image = Image.FromStream(ms))
                {
                    defaultWidth = image.Width;
                    defaultHeight = image.Height;
                }
            }
        }

        // Generate barcode with overridden BarHeight
        int customWidth, customHeight;
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;
            generator.Parameters.Barcode.XDimension.Point = 2f; // same module width
            generator.Parameters.Barcode.BarHeight.Point = 100f; // explicit bar height
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0;
                using (var image = Image.FromStream(ms))
                {
                    customWidth = image.Width;
                    customHeight = image.Height;
                }
            }
        }

        // Verify that custom BarHeight changes image height but not width
        if (customHeight <= defaultHeight)
            throw new Exception($"BarHeight override failed: custom height {customHeight} is not greater than default height {defaultHeight}.");

        if (customWidth != defaultWidth)
            throw new Exception($"BarHeight override affected width: default width {defaultWidth}, custom width {customWidth}.");

        Console.WriteLine("BarHeight property overrides automatic height calculation without affecting module width. Test passed.");
    }
}