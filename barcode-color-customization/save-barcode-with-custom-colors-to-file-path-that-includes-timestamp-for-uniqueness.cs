// Title: Save Barcode with Custom Colors and Timestamped Filename
// Description: Demonstrates generating a Code128 barcode, applying custom foreground, background, and border colors, and saving it as a PNG file with a unique timestamped name.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize barcode appearance using the BarcodeGenerator class and its Parameters properties. Typical use cases include branding, visual distinction, and creating uniquely named image files for batch processing. Developers often need to set BarColor, BackColor, and Border.Color, then save the image in a desired format.
// Prompt: Save a barcode with custom colors to a file path that includes a timestamp for uniqueness.
// Tags: code128, barcode generation, custom colors, timestamped filename, png, aspose.barcode, aspnet

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Code128 barcode with custom colors
/// and saves it to a uniquely named PNG file using a timestamp.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates the barcode, applies color customizations, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Determine the folder where the executable is running.
        string outputFolder = Directory.GetCurrentDirectory();

        // Create a timestamp string to ensure the file name is unique.
        string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmssfff");

        // Combine folder path and file name with timestamp.
        string filePath = Path.Combine(outputFolder, $"barcode_{timestamp}.png");

        // Initialize the barcode generator for Code128 with the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            // Set the barcode (foreground) color to blue.
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Set the background color of the image to yellow.
            generator.Parameters.BackColor = Color.Yellow;

            // Set the border color around the barcode to red.
            generator.Parameters.Border.Color = Color.Red;

            // Save the generated barcode as a PNG file to the specified path.
            generator.Save(filePath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {filePath}");
    }
}