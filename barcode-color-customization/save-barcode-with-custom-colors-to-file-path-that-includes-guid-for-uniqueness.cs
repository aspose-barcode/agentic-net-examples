// Title: Save barcode with custom colors and unique GUID filename
// Description: Demonstrates generating a Code128 barcode, applying custom foreground and background colors, and saving it to a uniquely named PNG file using a GUID.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize barcode appearance with the BarcodeGenerator class, set color parameters, and export the image in PNG format. Developers often need to create visually distinct barcodes for branding or UI integration, requiring control over colors and unique file naming to avoid collisions.
// Prompt: Save a barcode with custom colors to a file path that includes a GUID for uniqueness.
// Tags: barcode, code128, custom colors, png, guid, filename, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Code128 barcode with custom colors
/// and saves it to a uniquely named PNG file using a GUID.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates the barcode, applies color customizations, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Generate a new GUID and format it as a 32‑character string without hyphens.
        string guid = Guid.NewGuid().ToString("N");

        // Build a file name that includes the GUID to ensure uniqueness.
        string fileName = $"barcode_{guid}.png";

        // Determine a temporary folder for storing the barcode image.
        string folder = Path.Combine(Path.GetTempPath(), "AsposeBarcodes");

        // Ensure the target directory exists.
        Directory.CreateDirectory(folder);

        // Combine folder and file name to obtain the full file path.
        string filePath = Path.Combine(folder, fileName);

        // Create a BarcodeGenerator for Code128 with the desired data.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            // Set the background color of the image.
            generator.Parameters.BackColor = Color.Yellow;

            // Set the color of the barcode bars.
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Set the color of the human‑readable text.
            generator.Parameters.Barcode.CodeTextParameters.Color = Color.Red;

            // Save the generated barcode as a PNG file to the specified path.
            generator.Save(filePath, BarCodeImageFormat.Png);
        }

        // Output the location of the saved barcode image.
        Console.WriteLine($"Barcode saved to: {filePath}");
    }
}