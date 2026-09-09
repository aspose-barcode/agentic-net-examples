// Title: Demonstrate post-save color changes do not affect saved barcode image
// Description: This example generates a Code128 barcode, saves it, then changes its colors and saves again, showing the first saved file remains unchanged.
// Category-Description: Shows how to use Aspose.BarCode's BarcodeGenerator and its Parameters to control barcode appearance. Typical use cases include generating barcodes with specific colors, saving them, and ensuring saved images are immutable after further modifications. Developers often need to verify that subsequent property changes do not retroactively alter previously saved files.
// Prompt: Demonstrate that modifying color properties after calling Save does not alter the already saved image.
// Tags: code128, color, png, barcodegenerator, parameters

using System;
using System.IO;
using System.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that demonstrates that changing barcode color properties after a save operation
/// does not modify the already saved image file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Generates a barcode, saves it, modifies colors, saves again,
    /// and verifies the first saved image remains unchanged.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for the initial and modified barcode images
        string filePath1 = Path.Combine(tempFolder, "barcode_initial.png");
        string filePath2 = Path.Combine(tempFolder, "barcode_modified.png");

        // Generate a Code128 barcode, set initial colors, and save the first image
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            // Set background to white and barcode bars to black
            generator.Parameters.BackColor = Color.White;
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save the initial barcode image
            generator.Save(filePath1, BarCodeImageFormat.Png);

            // Capture the byte content of the first saved file for later comparison
            byte[] originalBytes = File.ReadAllBytes(filePath1);

            // Change color properties after the first save
            generator.Parameters.BackColor = Color.Yellow;
            generator.Parameters.Barcode.BarColor = Color.Red;

            // Save the barcode again with the new colors
            generator.Save(filePath2, BarCodeImageFormat.Png);

            // Re-read the first file to ensure its content has not changed
            byte[] afterBytes = File.ReadAllBytes(filePath1);

            // Compare the original and after bytes to confirm immutability
            bool unchanged = originalBytes.SequenceEqual(afterBytes);
            Console.WriteLine("First saved image unchanged after modifying colors: " + unchanged);
        }

        // Optional cleanup: uncomment the line below to delete the temporary folder and its contents
        // Directory.Delete(tempFolder, true);
    }
}