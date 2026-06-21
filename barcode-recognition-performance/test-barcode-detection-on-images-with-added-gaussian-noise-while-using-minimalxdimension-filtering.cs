using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a QR barcode, adding Gaussian noise, and recognizing it using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a QR code, adds noise, saves images, and attempts to read the noisy barcode.
    /// </summary>
    static void Main()
    {
        // Prepare sample data
        const string codeText = "AsposeTest123";
        const string originalPath = "barcode_original.png";
        const string noisyPath = "barcode_noisy.png";

        // Generate a QR barcode image
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Save the clean barcode image for reference
            generator.Save(originalPath);

            // Create a bitmap from the generator for further processing
            using (Bitmap originalBitmap = generator.GenerateBarCodeImage())
            {
                // Add Gaussian noise to the bitmap
                using (Bitmap noisyBitmap = AddGaussianNoise(originalBitmap, 30f))
                {
                    // Save the noisy image to disk
                    noisyBitmap.Save(noisyPath, Aspose.Drawing.Imaging.ImageFormat.Png);

                    // Recognize the barcode from the noisy image
                    using (var reader = new BarCodeReader(noisyBitmap, DecodeType.QR))
                    {
                        // Configure quality settings to improve detection on noisy data
                        reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                        reader.QualitySettings.MinimalXDimension = 2f; // minimal element size in pixels
                        reader.QualitySettings.AllowIncorrectBarcodes = true; // increase tolerance

                        // Iterate through all detected barcodes
                        foreach (var result in reader.ReadBarCodes())
                        {
                            Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                            Console.WriteLine($"Decoded Text : {result.CodeText}");
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Adds Gaussian noise to a bitmap and returns a new bitmap containing the noisy image.
    /// </summary>
    /// <param name="source">The original bitmap.</param>
    /// <param name="sigma">Standard deviation of the Gaussian noise.</param>
    /// <returns>A new bitmap with Gaussian noise applied.</returns>
    private static Bitmap AddGaussianNoise(Bitmap source, float sigma)
    {
        int width = source.Width;
        int height = source.Height;
        var noisy = new Bitmap(width, height);
        var rand = new Random();

        // Process each pixel to add noise per color channel
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                // Retrieve the original pixel color
                var originalColor = source.GetPixel(x, y);

                // Apply Gaussian noise to each RGB channel
                int r = AddNoiseToChannel(originalColor.R, sigma, rand);
                int g = AddNoiseToChannel(originalColor.G, sigma, rand);
                int b = AddNoiseToChannel(originalColor.B, sigma, rand);

                // Preserve the original alpha channel
                var noisyColor = Color.FromArgb(originalColor.A, r, g, b);

                // Set the noisy pixel in the new bitmap
                noisy.SetPixel(x, y, noisyColor);
            }
        }

        return noisy;
    }

    /// <summary>
    /// Applies Gaussian noise to a single color channel value and clamps the result to the valid byte range.
    /// </summary>
    /// <param name="value">Original channel value (0-255).</param>
    /// <param name="sigma">Standard deviation of the Gaussian noise.</param>
    /// <param name="rand">Random number generator.</param>
    /// <returns>The noisy channel value, clamped between 0 and 255.</returns>
    private static int AddNoiseToChannel(int value, float sigma, Random rand)
    {
        // Box-Muller transform to generate a normally distributed random value
        double u1 = 1.0 - rand.NextDouble(); // avoid log(0)
        double u2 = 1.0 - rand.NextDouble();
        double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(2.0 * Math.PI * u2);
        double noise = randStdNormal * sigma;

        // Add noise to the original value and round to nearest integer
        int newValue = (int)Math.Round(value + noise);

        // Clamp the result to the valid byte range
        if (newValue < 0) newValue = 0;
        if (newValue > 255) newValue = 255;

        return newValue;
    }
}