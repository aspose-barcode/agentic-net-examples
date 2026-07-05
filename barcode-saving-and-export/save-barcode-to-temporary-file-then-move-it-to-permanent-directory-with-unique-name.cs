// Title: Save Barcode to Temporary File and Move to Permanent Directory
// Description: Generates a Code128 barcode, saves it to a temporary PNG file, then moves it to a permanent folder with a unique filename.
// Prompt: Save a barcode to a temporary file, then move it to a permanent directory with a unique name.
// Tags: barcode, code128, save, temporary file, permanent directory, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a barcode, storing it temporarily, and then moving it to a permanent location with a unique name.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, saves it to a temporary file,
    /// creates a permanent directory if needed, and moves the file there with a unique filename.
    /// </summary>
    static void Main()
    {
        // Define the barcode content.
        string codeText = "1234567890";

        // Build a temporary file path with a .png extension.
        string tempFilePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".png");

        // Generate the barcode and write it to the temporary file.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Optional visual customization.
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Save the generated barcode image.
            generator.Save(tempFilePath);
        }

        // Determine the permanent directory path (relative to the current working directory).
        string permanentDir = Path.Combine(Environment.CurrentDirectory, "Barcodes");

        // Ensure the permanent directory exists.
        if (!Directory.Exists(permanentDir))
        {
            Directory.CreateDirectory(permanentDir);
        }

        // Create a unique file name for the permanent location.
        string permanentFilePath = Path.Combine(permanentDir, Guid.NewGuid().ToString() + ".png");

        // Move the barcode image from the temporary location to the permanent directory.
        File.Move(tempFilePath, permanentFilePath);

        // Inform the user where the barcode was saved.
        Console.WriteLine($"Barcode saved to: {permanentFilePath}");
    }
}