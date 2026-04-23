using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Output folder
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "DataBarOutputs");
        Directory.CreateDirectory(outputDir);

        // Define DataBar types to process
        BaseEncodeType[] dataBarTypes = new BaseEncodeType[]
        {
            EncodeTypes.DatabarOmniDirectional,
            EncodeTypes.DatabarStacked,
            EncodeTypes.DatabarStackedOmniDirectional,
            EncodeTypes.DatabarExpanded,
            EncodeTypes.DatabarExpandedStacked,
            EncodeTypes.DatabarLimited,
            EncodeTypes.DatabarTruncated
        };

        foreach (BaseEncodeType type in dataBarTypes)
        {
            // Choose appropriate sample code text per type
            string codeText = type == EncodeTypes.DatabarLimited
                ? "(01)08888888888888"
                : "(01)12345678901231";

            // Create generator with specified type and code text
            using (var generator = new BarcodeGenerator(type, codeText))
            {
                // Ensure BarHeight is respected
                generator.Parameters.AutoSizeMode = AutoSizeMode.None;
                // Set barcode bar height to 70 pixels
                generator.Parameters.Barcode.BarHeight.Pixels = 70f;

                // Optional: set a visible bar color
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

                // Build file names
                string typeName = type.TypeName;
                string jpegPath = Path.Combine(outputDir, $"{typeName}_70px.jpeg");
                string pngPath = Path.Combine(outputDir, $"{typeName}_70px.png");

                // Save as JPEG
                generator.Save(jpegPath, BarCodeImageFormat.Jpeg);
                // Save as PNG
                generator.Save(pngPath, BarCodeImageFormat.Png);
            }
        }

        // Simple comparison output (file sizes)
        Console.WriteLine("DataBar barcode generation completed. File size comparison:");
        foreach (BaseEncodeType type in dataBarTypes)
        {
            string typeName = type.TypeName;
            string jpegPath = Path.Combine(outputDir, $"{typeName}_70px.jpeg");
            string pngPath = Path.Combine(outputDir, $"{typeName}_70px.png");

            if (File.Exists(jpegPath) && File.Exists(pngPath))
            {
                long jpegSize = new FileInfo(jpegPath).Length;
                long pngSize = new FileInfo(pngPath).Length;
                Console.WriteLine($"{typeName}: JPEG = {jpegSize} bytes, PNG = {pngSize} bytes");
            }
        }
    }
}