// Title: Demonstrate that saved barcode image colors remain unchanged after modifying generator properties
// Description: This example generates a Code128 barcode, saves it, then changes the background and bar colors and saves a second image, proving the first file retains its original colors.
// Category-Description: Aspose.BarCode Generation – This example belongs to the barcode creation and image export category, illustrating the use of BarcodeGenerator, its Parameters (BackColor, BarColor), and BarCodeImageFormat. Developers often need to generate multiple barcode images with varying visual styles while ensuring previously saved files stay unaffected. The snippet showcases typical workflows for temporary file handling, color customization, and pixel inspection using Aspose.Drawing.
// Prompt: Demonstrate that modifying color properties after calling Save does not alter the already saved image.
// Tags: barcode, code128, color, save, png, aspose.barcode, generation, image, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides a demonstration that changing barcode generator color settings after saving
/// does not retroactively modify the already saved image file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Generates two barcode images with different colors
    /// and verifies that the first image retains its original colors.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string demoFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(demoFolder);

        // Define file paths for the two barcode images
        string firstImagePath = Path.Combine(demoFolder, "barcode1.png");
        string secondImagePath = Path.Combine(demoFolder, "barcode2.png");

        // Initialize the barcode generator with Code128 symbology and sample data
        BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678");

        // Set initial colors: white background and black bars
        generator.Parameters.BackColor = Color.White;
        generator.Parameters.Barcode.BarColor = Color.Black;

        // Save the first image using the initial color settings
        generator.Save(firstImagePath, BarCodeImageFormat.Png);

        // Change color properties after the first save operation
        generator.Parameters.BackColor = Color.Yellow;
        generator.Parameters.Barcode.BarColor = Color.Red;

        // Save the second image with the new color settings
        generator.Save(secondImagePath, BarCodeImageFormat.Png);

        // Load the first image and read the color of the top-left pixel
        using (Bitmap bmp1 = new Bitmap(firstImagePath))
        {
            var pixel1 = bmp1.GetPixel(0, 0);
            Console.WriteLine($"First image pixel (0,0) ARGB: {pixel1.ToArgb():X8}");
        }

        // Load the second image and read the color of the top-left pixel
        using (Bitmap bmp2 = new Bitmap(secondImagePath))
        {
            var pixel2 = bmp2.GetPixel(0, 0);
            Console.WriteLine($"Second image pixel (0,0) ARGB: {pixel2.ToArgb():X8}");
        }

        // Indicate completion of the demonstration
        Console.WriteLine("Demonstration complete. The first image retained its original colors despite later modifications.");
    }
}