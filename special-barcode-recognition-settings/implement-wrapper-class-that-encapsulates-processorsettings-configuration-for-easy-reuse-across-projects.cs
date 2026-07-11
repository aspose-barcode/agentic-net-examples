// Title: Wrapper for Aspose.BarCode ProcessorSettings
// Description: Demonstrates a reusable ProcessorSettings wrapper that configures barcode generation parameters for consistent output across projects.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to encapsulate common barcode settings using the BarcodeGenerator class and related parameter objects. Developers often need a centralized configuration to apply consistent symbology, dimensions, colors, and padding when creating barcodes in .NET applications.
// Prompt: Implement a wrapper class that encapsulates ProcessorSettings configuration for easy reuse across projects.
// Tags: barcode symbology, generation, png, aspose.barcode, aspose.drawing

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Encapsulates common barcode generation settings for reuse across projects.
/// </summary>
public class ProcessorSettings
{
    // Symbology type (e.g., Code128, QR, etc.)
    public BaseEncodeType EncodeType { get; set; }

    // Text to encode into the barcode
    public string CodeText { get; set; }

    // Module size (x-dimension) in points; default 2f
    public float XDimension { get; set; } = 2f;

    // Height of 1D bars when AutoSize is disabled; default 40f
    public float BarHeight { get; set; } = 40f;

    // Determines whether the generator should auto‑size the image
    public bool AutoSize { get; set; } = false;

    // Foreground (bar) color; default black
    public Color BarColor { get; set; } = Color.Black;

    // Background color; default white
    public Color BackColor { get; set; } = Color.White;

    // Uniform padding around the barcode in points; default 5f
    public float Padding { get; set; } = 5f;

    /// <summary>
    /// Applies the stored settings to the specified <see cref="BarcodeGenerator"/> instance.
    /// </summary>
    /// <param name="generator">The barcode generator to configure.</param>
    public void Apply(BarcodeGenerator generator)
    {
        if (generator == null) throw new ArgumentNullException(nameof(generator));

        // Set the text to encode (fallback to empty string if null)
        generator.CodeText = CodeText ?? string.Empty;

        // Configure X‑dimension (module size)
        generator.Parameters.Barcode.XDimension.Point = XDimension;

        // Handle auto‑size mode and bar height
        if (AutoSize)
        {
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
        }
        else
        {
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;
            generator.Parameters.Barcode.BarHeight.Point = BarHeight;
        }

        // Apply foreground and background colors
        generator.Parameters.Barcode.BarColor = BarColor;
        generator.Parameters.BackColor = BackColor;

        // Apply uniform padding on all sides
        generator.Parameters.Barcode.Padding.Left.Point = Padding;
        generator.Parameters.Barcode.Padding.Top.Point = Padding;
        generator.Parameters.Barcode.Padding.Right.Point = Padding;
        generator.Parameters.Barcode.Padding.Bottom.Point = Padding;
    }
}

class Program
{
    /// <summary>
    /// Entry point demonstrating the use of <see cref="ProcessorSettings"/> with Aspose.BarCode.
    /// </summary>
    static void Main()
    {
        // Create a reusable settings instance with desired configuration
        var settings = new ProcessorSettings
        {
            EncodeType = EncodeTypes.Code128,
            CodeText = "Sample123",
            XDimension = 2f,
            BarHeight = 50f,
            AutoSize = false,
            BarColor = Color.Blue,
            BackColor = Color.White,
            Padding = 4f
        };

        // Instantiate the generator using the specified symbology
        using (var generator = new BarcodeGenerator(settings.EncodeType))
        {
            // Apply the common configuration to the generator
            settings.Apply(generator);

            // Define output file path and save the barcode image as PNG
            const string outputPath = "barcode.png";
            generator.Save(outputPath);

            // Inform the user where the file was saved
            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }
}