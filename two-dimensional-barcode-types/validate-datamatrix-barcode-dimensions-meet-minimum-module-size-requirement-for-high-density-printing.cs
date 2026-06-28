using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation of a DataMatrix barcode with a fixed version,
/// saves it to a PNG file, and verifies the module size against a minimum requirement.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a DataMatrix barcode, saves it, and checks the module size.
    /// </summary>
    static void Main()
    {
        // Output file name for the generated barcode image
        const string outputPath = "datamatrix.png";

        // Minimum acceptable module size (in pixels) for high‑density printing
        const float minModuleSizePixels = 2f;

        // ------------------------------------------------------------
        // 1. Generate a DataMatrix barcode with a known version (20x20 modules)
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "HelloWorld"))
        {
            // Force the barcode to use the 20x20 ECC200 version
            generator.Parameters.Barcode.DataMatrix.DataMatrixVersion = DataMatrixVersion.ECC200_20x20;

            // Disable automatic scaling so we can control module size directly
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Set the module (X) dimension in points (1 point = 1/72 inch)
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Save the generated barcode image to the specified path
            generator.Save(outputPath);
        }

        // Verify that the image file was created successfully
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to generate the barcode image.");
            return;
        }

        // ------------------------------------------------------------
        // 2. Load the generated image and calculate the actual module size
        // ------------------------------------------------------------
        using (var image = Image.FromFile(outputPath))
        {
            int pixelWidth = image.Width;   // Image width in pixels
            int pixelHeight = image.Height; // Image height in pixels

            // Number of modules per side for the selected version (20x20)
            const int modulesPerSide = 20;

            // Compute module size in X and Y directions
            double moduleSizeX = (double)pixelWidth / modulesPerSide;
            double moduleSizeY = (double)pixelHeight / modulesPerSide;

            // Use the smaller dimension to ensure a conservative estimate
            double moduleSize = Math.Min(moduleSizeX, moduleSizeY);

            // Output diagnostic information
            Console.WriteLine($"Barcode image dimensions: {pixelWidth}x{pixelHeight} pixels");
            Console.WriteLine($"Calculated module size: {moduleSize:F2} pixels");

            // Compare calculated module size with the minimum requirement
            if (moduleSize >= minModuleSizePixels)
                Console.WriteLine("Module size meets the minimum requirement.");
            else
                Console.WriteLine($"Module size is below the minimum of {minModuleSizePixels} pixels. Adjust XDimension or version.");
        }

        // ------------------------------------------------------------
        // 3. Optional cleanup (uncomment to delete the generated file)
        // ------------------------------------------------------------
        // File.Delete(outputPath);
    }
}