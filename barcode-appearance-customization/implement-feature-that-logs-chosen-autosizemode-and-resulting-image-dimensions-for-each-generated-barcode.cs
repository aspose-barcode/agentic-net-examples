using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation of barcodes with different <see cref="AutoSizeMode"/> settings
/// and logs the resulting image dimensions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates an output directory and generates three sample barcodes.
    /// </summary>
    static void Main()
    {
        // Ensure the output folder exists
        string outputDir = "Barcodes";
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Example 1: Code128 with AutoSizeMode.None (fixed bar height)
        GenerateAndLog(
            EncodeTypes.Code128,
            "ABC123",
            AutoSizeMode.None,
            barHeight: 40f,
            outputDir: outputDir,
            fileName: "code128_none.png");

        // Example 2: Code39FullASCII with AutoSizeMode.Nearest (nearest size fit)
        GenerateAndLog(
            EncodeTypes.Code39FullASCII,
            "CODE39FULL",
            AutoSizeMode.Nearest,
            width: 200f,
            height: 100f,
            outputDir: outputDir,
            fileName: "code39_nearest.png");

        // Example 3: QR with AutoSizeMode.Interpolation (interpolated size fit)
        GenerateAndLog(
            EncodeTypes.QR,
            "https://example.com",
            AutoSizeMode.Interpolation,
            width: 250f,
            height: 250f,
            outputDir: outputDir,
            fileName: "qr_interpolation.png");
    }

    /// <summary>
    /// Generates a barcode image using the specified parameters, saves it to disk,
    /// and writes information about the generated image to the console.
    /// </summary>
    /// <param name="encodeType">The barcode symbology to use.</param>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="autoSizeMode">The auto‑size mode that determines how the image size is calculated.</param>
    /// <param name="width">Optional target image width (used for Nearest and Interpolation modes).</param>
    /// <param name="height">Optional target image height (used for Nearest and Interpolation modes).</param>
    /// <param name="barHeight">Optional bar height (used when AutoSizeMode is None).</param>
    /// <param name="outputDir">Directory where the image will be saved.</param>
    /// <param name="fileName">File name for the saved image.</param>
    static void GenerateAndLog(
        BaseEncodeType encodeType,
        string codeText,
        AutoSizeMode autoSizeMode,
        float? width = null,
        float? height = null,
        float? barHeight = null,
        string outputDir = "",
        string fileName = "")
    {
        // Combine directory and file name to get the full path
        string filePath = Path.Combine(outputDir, fileName);

        // Create and configure the barcode generator
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Apply the selected auto‑size mode
            generator.Parameters.AutoSizeMode = autoSizeMode;

            // Configure size based on the chosen AutoSizeMode
            if (autoSizeMode == AutoSizeMode.Nearest || autoSizeMode == AutoSizeMode.Interpolation)
            {
                // For size‑based modes, set explicit image dimensions if provided
                if (width.HasValue && height.HasValue)
                {
                    generator.Parameters.ImageWidth.Point = width.Value;
                    generator.Parameters.ImageHeight.Point = height.Value;
                }
            }
            else // AutoSizeMode.None
            {
                // For fixed‑size mode, set the bar height if supplied
                if (barHeight.HasValue)
                {
                    generator.Parameters.Barcode.BarHeight.Point = barHeight.Value;
                }

                // Example XDimension setting (optional, controls bar width)
                generator.Parameters.Barcode.XDimension.Point = 2f;
            }

            // Save the generated barcode image to the specified file
            generator.Save(filePath, BarCodeImageFormat.Png);
        }

        // Load the saved image to obtain its actual dimensions
        using (var image = Image.FromFile(filePath))
        {
            int actualWidth = image.Width;
            int actualHeight = image.Height;

            // Output details to the console
            Console.WriteLine($"Symbology: {encodeType.TypeName}");
            Console.WriteLine($"AutoSizeMode: {autoSizeMode}");
            Console.WriteLine($"Saved Image: {filePath}");
            Console.WriteLine($"Actual Dimensions: {actualWidth}x{actualHeight}");
            Console.WriteLine(new string('-', 40));
        }
    }
}