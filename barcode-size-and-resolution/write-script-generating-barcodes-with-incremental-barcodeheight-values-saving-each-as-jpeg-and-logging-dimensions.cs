using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating barcode images with varying bar heights using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a series of barcode images,
    /// saves them to disk, and reports their dimensions.
    /// </summary>
    static void Main()
    {
        // Determine the output directory for generated barcode images.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputDir))
        {
            // Create the directory if it does not already exist.
            Directory.CreateDirectory(outputDir);
        }

        // Select the barcode symbology (Code128) and the sample text to encode.
        BaseEncodeType encodeType = EncodeTypes.Code128;
        string codeText = "Sample123";

        // Define the range of bar heights to generate.
        float startHeight = 20f; // initial bar height in points
        float step = 10f;        // increment per iteration
        int count = 5;           // total number of barcodes to generate

        // Loop to create each barcode with a different bar height.
        for (int i = 0; i < count; i++)
        {
            // Calculate the current bar height for this iteration.
            float barHeight = startHeight + i * step;

            // Initialize the barcode generator with the chosen symbology and text.
            using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Disable automatic sizing so we can set BarHeight manually.
                generator.Parameters.AutoSizeMode = AutoSizeMode.None;
                // Apply the calculated bar height (in points) to the barcode.
                generator.Parameters.Barcode.BarHeight.Point = barHeight;

                // Set a high resolution for better image quality.
                generator.Parameters.Resolution = 300f;

                // Construct a unique file name that includes the iteration and bar height.
                string fileName = $"barcode_{i + 1}_height_{barHeight}.jpg";
                string filePath = Path.Combine(outputDir, fileName);

                // Save the generated barcode as a JPEG image.
                generator.Save(filePath, BarCodeImageFormat.Jpeg);
            }

            // Load the saved image to retrieve its actual pixel dimensions.
            string savedPath = Path.Combine(outputDir, $"barcode_{i + 1}_height_{barHeight}.jpg");
            using (Image img = Image.FromFile(savedPath))
            {
                int width = img.Width;
                int height = img.Height;
                // Output the file name, pixel dimensions, and the bar height used.
                Console.WriteLine($"Saved '{Path.GetFileName(savedPath)}' - Width: {width}px, Height: {height}px, BarHeight: {barHeight}pt");
            }
        }

        // Indicate that the barcode generation process has finished.
        Console.WriteLine("Barcode generation completed.");
    }
}