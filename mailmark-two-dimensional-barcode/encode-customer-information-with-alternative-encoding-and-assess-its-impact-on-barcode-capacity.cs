// Title: DataMatrix Barcode Encoding Comparison – Default vs UTF-8
// Description: Demonstrates generating a DataMatrix barcode with the default automatic encoding and with an explicit UTF‑8 encoding, then compares image dimensions to assess capacity impact.
// Category-Description: This example belongs to the Aspose.BarCode encoding and capacity assessment category. It showcases the use of BarcodeGenerator, EncodeTypes, and image handling classes (Bitmap, BarCodeImageFormat) to create barcodes, control text encoding, and evaluate how encoding choices affect barcode size. Developers often need to understand encoding effects when optimizing barcode data density for packaging, inventory, or document workflows.
// Prompt: Encode customer information with an alternative encoding and assess its impact on barcode capacity.
// Tags: datamatrix, barcode, encoding, capacity, aspose.barcode, image, png, c#

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates DataMatrix barcodes using default and explicit UTF‑8 encoding
/// and compares their image sizes to evaluate encoding impact on barcode capacity.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates two barcodes, saves them, and prints a size comparison.
    /// </summary>
    static void Main()
    {
        // Sample customer information to encode
        string customerInfo = "John Doe, 123 Main St, City, Country";

        // File paths for the generated PNG images
        string defaultPath = "customer_default.png";
        string altPath = "customer_alt.png";

        // ------------------------------------------------------------
        // Generate barcode using the default (auto‑detected) encoding
        // ------------------------------------------------------------
        int defaultWidth, defaultHeight;
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, customerInfo))
        {
            // Save the barcode image to a PNG file
            generator.Save(defaultPath, BarCodeImageFormat.Png);

            // Retrieve the image dimensions for capacity comparison
            using (Bitmap bmp = generator.GenerateBarCodeImage())
            {
                defaultWidth = bmp.Width;
                defaultHeight = bmp.Height;
            }
        }

        // ------------------------------------------------------------
        // Generate barcode using an explicit UTF‑8 encoding via SetCodeText
        // ------------------------------------------------------------
        int altWidth, altHeight;
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix))
        {
            // Encode the same text with explicit UTF‑8 encoding
            generator.SetCodeText(customerInfo, Encoding.UTF8);

            // Save the barcode image to a PNG file
            generator.Save(altPath, BarCodeImageFormat.Png);

            // Retrieve the image dimensions for capacity comparison
            using (Bitmap bmp = generator.GenerateBarCodeImage())
            {
                altWidth = bmp.Width;
                altHeight = bmp.Height;
            }
        }

        // ------------------------------------------------------------
        // Output comparison results to the console
        // ------------------------------------------------------------
        Console.WriteLine("Barcode capacity assessment (image size reflects data capacity):");
        Console.WriteLine($"Default encoding (auto):   {defaultWidth}x{defaultHeight} pixels");
        Console.WriteLine($"Alternative UTF-8 encoding: {altWidth}x{altHeight} pixels");
        Console.WriteLine();

        if (defaultWidth == altWidth && defaultHeight == altHeight)
        {
            Console.WriteLine("Both encodings produced identical image sizes; capacity impact is negligible.");
        }
        else
        {
            Console.WriteLine("Different image sizes indicate a capacity impact due to encoding differences.");
            Console.WriteLine("Larger dimensions mean more modules were needed, reducing effective capacity.");
        }

        // Inform the user where the barcode images have been saved
        Console.WriteLine($"Default barcode saved to: {Path.GetFullPath(defaultPath)}");
        Console.WriteLine($"Alternative barcode saved to: {Path.GetFullPath(altPath)}");
    }
}