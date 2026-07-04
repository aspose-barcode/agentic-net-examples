// Title: Calculate optimal XDimension for barcode generation
// Description: Demonstrates how to compute the XDimension (module size) to achieve a desired image width for a given barcode symbology and data.
// Prompt: Develop a method to calculate optimal XDimension based on desired image width and barcode symbology specifications.
// Tags: barcode symbology, xdimension calculation, aspose.barcode, image generation, csharp

using System;
using System.Collections.Generic;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that calculates an optimal XDimension for a barcode and generates the image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Calculates XDimension, resolves the symbology, and generates a barcode image.
    /// </summary>
    static void Main()
    {
        // Desired image width in pixels
        float desiredWidth = 300f;

        // Sample barcode data
        string codeText = "1234567890";

        // Symbology name (must match a field name in EncodeTypes)
        string symbologyName = "Code128";

        // Calculate optimal XDimension based on desired width and symbology
        float xDimension = CalculateOptimalXDimension(desiredWidth, symbologyName, codeText);
        if (xDimension <= 0f)
        {
            Console.WriteLine("Failed to calculate XDimension.");
            return;
        }

        // Resolve symbology to BaseEncodeType using reflection
        var field = typeof(EncodeTypes).GetField(symbologyName);
        if (field == null)
        {
            Console.WriteLine($"Unknown symbology: {symbologyName}");
            return;
        }
        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // Generate barcode with the calculated XDimension
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Disable auto‑size to use explicit XDimension
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Apply calculated XDimension (in points)
            generator.Parameters.Barcode.XDimension.Point = xDimension;

            // Save the barcode image to file
            generator.Save("barcode.png");
        }

        Console.WriteLine($"Barcode generated with XDimension = {xDimension}pt (desired width {desiredWidth}px).");
    }

    /// <summary>
    /// Calculates an approximate optimal XDimension (module size) so that the generated barcode
    /// width is close to the desired image width. The calculation uses a simple estimation of
    /// the number of modules based on the symbology and the length of the codetext.
    /// </summary>
    /// <param name="desiredWidth">Desired image width in pixels.</param>
    /// <param name="symbologyName">Name of the symbology (e.g., "Code128").</param>
    /// <param name="codeText">The text to encode.</param>
    /// <returns>Calculated XDimension in points; returns 0 if calculation cannot be performed.</returns>
    static float CalculateOptimalXDimension(float desiredWidth, string symbologyName, string codeText)
    {
        // Validate input parameters
        if (desiredWidth <= 0f)
            throw new ArgumentOutOfRangeException(nameof(desiredWidth), "Desired width must be positive.");
        if (string.IsNullOrEmpty(codeText))
            throw new ArgumentException("Code text cannot be null or empty.", nameof(codeText));

        // Approximate modules per character for common symbologies.
        // These values are rough estimates and may vary per implementation.
        var modulesPerCharMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
        {
            { "Code128", 11 },
            { "Code39", 13 },
            { "EAN13", 95 },   // Fixed length for EAN13
            { "QRCode", 0 },   // QR codes have variable module count; handled separately
            { "DataMatrix", 0 } // Handled separately
        };

        int totalModules;

        // Determine total module count based on symbology
        if (modulesPerCharMap.TryGetValue(symbologyName, out int modulesPerChar) && modulesPerChar > 0)
        {
            // Simple linear estimate: modules = characters * modules per character
            totalModules = codeText.Length * modulesPerChar;
        }
        else if (symbologyName.Equals("QRCode", StringComparison.OrdinalIgnoreCase) ||
                 symbologyName.Equals("DataMatrix", StringComparison.OrdinalIgnoreCase))
        {
            // For 2D barcodes, estimate a square matrix where modules ≈ sqrt(desiredWidth / desiredModuleSize)
            // Assume a default module count of 21 (Version 1 QR) for estimation.
            totalModules = 21;
        }
        else
        {
            Console.WriteLine($"Symbology '{symbologyName}' not recognized for XDimension calculation.");
            return 0f;
        }

        // Convert desired width (pixels) to points.
        // For simplicity, treat 1 pixel ≈ 1 point in this context.
        float xDim = desiredWidth / totalModules;

        // Ensure a minimum reasonable size
        if (xDim < 0.5f)
            xDim = 0.5f;

        return xDim;
    }
}