using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation of GS1 Composite barcodes with different linear components
/// and validates that the resulting images differ in dimensions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates two composite barcodes, compares their image sizes, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Sample codetext: linear part and 2D part separated by '|'
        const string codetext = "(01)03212345678906|(21)A1B2C3D4E5F6G7H8";

        // Paths for the generated images (stored in the system temporary folder)
        string pathGs1Code128 = Path.Combine(Path.GetTempPath(), "gs1_composite_gs1code128.png");
        string pathEan13 = Path.Combine(Path.GetTempPath(), "gs1_composite_ean13.png");

        // ------------------------------------------------------------
        // First barcode: Linear component = GS1Code128
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codetext))
        {
            // Set linear component type to GS1Code128
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
            // Set 2D component type to CC_A (Composite Component A)
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;
            // Configure visual parameters
            generator.Parameters.Barcode.XDimension.Pixels = 3f;
            generator.Parameters.Barcode.BarHeight.Pixels = 100f;
            // Save the generated barcode image
            generator.Save(pathGs1Code128);

            // Verify that the linear component type was set correctly
            if (generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType != EncodeTypes.GS1Code128)
            {
                Console.WriteLine("Failed to set LinearComponentType to GS1Code128.");
                return;
            }
        }

        // ------------------------------------------------------------
        // Second barcode: Linear component = EAN13
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codetext))
        {
            // Set linear component type to EAN13
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.EAN13;
            // Keep the same 2D component type
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;
            // Configure visual parameters (same as first barcode)
            generator.Parameters.Barcode.XDimension.Pixels = 3f;
            generator.Parameters.Barcode.BarHeight.Pixels = 100f;
            // Save the generated barcode image
            generator.Save(pathEan13);

            // Verify that the linear component type was set correctly
            if (generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType != EncodeTypes.EAN13)
            {
                Console.WriteLine("Failed to set LinearComponentType to EAN13.");
                return;
            }
        }

        // ------------------------------------------------------------
        // Load images to compare dimensions
        // ------------------------------------------------------------
        int widthGs1, heightGs1, widthEan, heightEan;
        using (var imgGs1 = Image.FromFile(pathGs1Code128))
        {
            widthGs1 = imgGs1.Width;
            heightGs1 = imgGs1.Height;
        }
        using (var imgEan = Image.FromFile(pathEan13))
        {
            widthEan = imgEan.Width;
            heightEan = imgEan.Height;
        }

        // Simple validation: dimensions should differ when linear component type changes
        if (widthGs1 == widthEan && heightGs1 == heightEan)
        {
            Console.WriteLine("Test Failed: Images have identical dimensions; linear component change may not have affected the barcode.");
        }
        else
        {
            Console.WriteLine("Test Passed: Images differ after changing LinearComponentType, indicating the barcode structure was updated.");
            Console.WriteLine($"GS1Code128 image size: {widthGs1}x{heightGs1}");
            Console.WriteLine($"EAN13 image size: {widthEan}x{heightEan}");
        }

        // ------------------------------------------------------------
        // Clean up temporary files (optional)
        // ------------------------------------------------------------
        try { File.Delete(pathGs1Code128); } catch { }
        try { File.Delete(pathEan13); } catch { }
    }
}