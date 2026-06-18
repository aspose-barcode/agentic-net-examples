using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation, XML serialization, and image comparison using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Execute the barcode serialization test
        RunBarcodeSerializationTest();
    }

    static void RunBarcodeSerializationTest()
    {
        const string codeText = "Test123";

        // Create original barcode generator and configure deterministic parameters
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Disable auto‑sizing to keep dimensions consistent
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;
            // Set X dimension (module width) in points
            generator.Parameters.Barcode.XDimension.Point = 2f;
            // Set barcode height in points
            generator.Parameters.Barcode.BarHeight.Point = 40f;

            // Generate the original barcode image
            using (var originalImage = generator.GenerateBarCodeImage())
            {
                // Export generator settings to an in‑memory XML stream
                using (var xmlStream = new MemoryStream())
                {
                    generator.ExportToXml(xmlStream);
                    // Reset stream position for reading
                    xmlStream.Position = 0;

                    // Import settings from XML into a new generator instance
                    using (var importedGenerator = BarcodeGenerator.ImportFromXml(xmlStream))
                    {
                        // Generate barcode image from the imported generator
                        using (var importedImage = importedGenerator.GenerateBarCodeImage())
                        {
                            // Compare the two images pixel by pixel
                            bool identical = CompareBitmaps(originalImage, importedImage);
                            // Output the comparison result
                            Console.WriteLine(
                                "Barcode images are {0} after XML round‑trip.",
                                identical ? "identical" : "different");
                        }
                    }
                }
            }
        }
    }

    // Pixel‑wise comparison of two Aspose.Drawing.Bitmap objects
    static bool CompareBitmaps(Bitmap bmp1, Bitmap bmp2)
    {
        // If dimensions differ, images cannot be identical
        if (bmp1.Width != bmp2.Width || bmp1.Height != bmp2.Height)
            return false;

        // Iterate over each pixel and compare ARGB values
        for (int y = 0; y < bmp1.Height; y++)
        {
            for (int x = 0; x < bmp1.Width; x++)
            {
                if (bmp1.GetPixel(x, y).ToArgb() != bmp2.GetPixel(x, y).ToArgb())
                    return false;
            }
        }

        // All pixels match
        return true;
    }
}