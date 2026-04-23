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
        // Sample barcode text and type
        const string codeText = "1234567890";
        const string file120 = "barcode_120dpi.png";
        const string file300 = "barcode_300dpi.png";

        // Generate barcode at 120 DPI
        using (var generator120 = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator120.Parameters.Resolution = 120f; // set resolution to 120 DPI
            generator120.Save(file120, BarCodeImageFormat.Png);
        }

        // Generate barcode at 300 DPI (reference)
        using (var generator300 = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator300.Parameters.Resolution = 300f; // set resolution to 300 DPI
            generator300.Save(file300, BarCodeImageFormat.Png);
        }

        // Verify that files were created
        if (!File.Exists(file120) || !File.Exists(file300))
        {
            Console.WriteLine("Failed to create one or both barcode images.");
            return;
        }

        // Load images to read their DPI metadata
        using (var img120 = Image.FromFile(file120))
        using (var img300 = Image.FromFile(file300))
        {
            float dpi120 = img120.HorizontalResolution; // should be 120
            float dpi300 = img300.HorizontalResolution; // should be 300

            Console.WriteLine($"120 DPI image resolution: {dpi120} DPI");
            Console.WriteLine($"300 DPI reference resolution: {dpi300} DPI");

            // Simple visual quality comparison based on DPI
            if (dpi120 < dpi300)
            {
                Console.WriteLine("The 120 DPI barcode has lower visual quality compared to the 300 DPI reference.");
            }
            else if (dpi120 > dpi300)
            {
                Console.WriteLine("The 120 DPI barcode unexpectedly has higher DPI than the reference.");
            }
            else
            {
                Console.WriteLine("Both images have the same DPI.");
            }
        }
    }
}