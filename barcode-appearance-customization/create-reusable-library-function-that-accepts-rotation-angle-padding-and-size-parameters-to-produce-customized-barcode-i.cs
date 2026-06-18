using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a barcode image with custom rotation, padding, and size using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a barcode image with the specified parameters and saves it to the given path.
    /// </summary>
    /// <param name="type">The barcode symbology to use.</param>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="rotationAngle">Rotation angle in degrees (0‑359).</param>
    /// <param name="padding">Uniform padding (in points) applied to all sides.</param>
    /// <param name="width">Desired image width (in points).</param>
    /// <param name="height">Desired image height (in points).</param>
    /// <param name="outputPath">File path where the barcode image will be saved.</param>
    static void CreateBarcode(BaseEncodeType type, string codeText, float rotationAngle, float padding, float width, float height, string outputPath)
    {
        // Validate required parameters.
        if (string.IsNullOrWhiteSpace(codeText))
            throw new ArgumentException("Code text must be provided.", nameof(codeText));
        if (rotationAngle < 0f || rotationAngle >= 360f)
            throw new ArgumentOutOfRangeException(nameof(rotationAngle), "Rotation angle must be between 0 and 359 degrees.");
        if (padding < 0f)
            throw new ArgumentOutOfRangeException(nameof(padding), "Padding cannot be negative.");
        if (width <= 0f || height <= 0f)
            throw new ArgumentOutOfRangeException("Width and height must be greater than zero.");

        // Ensure the output directory exists.
        string directory = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        // Create and configure the barcode generator.
        using (var generator = new BarcodeGenerator(type, codeText))
        {
            // Apply rotation.
            generator.Parameters.RotationAngle = rotationAngle;

            // Set uniform padding on all sides.
            generator.Parameters.Barcode.Padding.Left.Point = padding;
            generator.Parameters.Barcode.Padding.Top.Point = padding;
            generator.Parameters.Barcode.Padding.Right.Point = padding;
            generator.Parameters.Barcode.Padding.Bottom.Point = padding;

            // Set desired image dimensions.
            generator.Parameters.ImageWidth.Point = width;
            generator.Parameters.ImageHeight.Point = height;

            // Save the image (format inferred from file extension).
            generator.Save(outputPath);
        }
    }

    /// <summary>
    /// Entry point of the program. Demonstrates usage of the CreateBarcode method.
    /// </summary>
    static void Main()
    {
        // Sample usage: Code128 barcode with 45° rotation, 20pt padding, 300x150 size.
        try
        {
            CreateBarcode(
                EncodeTypes.Code128,
                "1234567890",
                45f,
                20f,
                300f,
                150f,
                "barcode.png");

            Console.WriteLine("Barcode generated successfully: barcode.png");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error generating barcode: {ex.Message}");
        }
    }
}