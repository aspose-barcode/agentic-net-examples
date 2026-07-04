// Title: Save Barcode with Custom Colors and Unique File Name
// Description: Demonstrates generating a Code128 barcode, applying custom foreground and background colors, and saving it to a uniquely named PNG file using a GUID.
// Prompt: Save a barcode with custom colors to a file path that includes a GUID for uniqueness.
// Tags: code128, barcode generation, custom colors, file output, guid, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Code128 barcode with custom colors
/// and saves it to a uniquely named PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, applies custom colors, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Generate a unique file name using a GUID to avoid collisions
        string guid = Guid.NewGuid().ToString();
        string fileName = $"barcode_{guid}.png";
        string outputPath = Path.Combine(Environment.CurrentDirectory, fileName);

        // Initialize a barcode generator for Code128 with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set the foreground (bars) color to blue
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;

            // Set the background color to yellow
            generator.Parameters.BackColor = Aspose.Drawing.Color.Yellow;

            // Save the generated barcode image to the unique file path
            generator.Save(outputPath);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}