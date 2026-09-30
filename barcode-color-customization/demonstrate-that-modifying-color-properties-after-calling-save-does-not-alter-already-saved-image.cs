// Title: Demonstrate barcode color change does not affect previously saved image
// Description: This example generates a Code128 barcode, saves it, changes its colors, saves again, and verifies the first saved image remains unchanged by comparing file hashes.
// Category-Description: Shows how to use Aspose.BarCode's BarcodeGenerator to modify visual properties after saving. Covers barcode generation, color customization, image export, and file integrity verification using SHA256. Useful for developers needing to generate multiple barcode images with different styles without reprocessing earlier files.
// Prompt: Demonstrate that modifying color properties after calling Save does not alter the already saved image.
// Tags: barcode, code128, color, image, save, hash, aspose.barcode, aspose.drawing, sha256

using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates that changing barcode color properties after saving does not modify the already saved image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Generates a barcode, saves it, changes colors, saves again, and compares file hashes.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeColorDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for the two barcode images
        string firstImagePath = Path.Combine(tempFolder, "barcode_first.png");
        string secondImagePath = Path.Combine(tempFolder, "barcode_second.png");

        // Initialize the barcode generator with Code128 symbology and sample text
        BaseEncodeType encodeType = EncodeTypes.Code128;
        string firstHashBefore = null;

        using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, "123456"))
        {
            // Set initial colors: black bars on white background
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Save the first image with the initial colors
            generator.Save(firstImagePath, BarCodeImageFormat.Png);
            Console.WriteLine($"First barcode saved to: {firstImagePath}");

            // Compute hash of the first image after saving
            firstHashBefore = ComputeFileHash(firstImagePath);
            Console.WriteLine($"Hash of first image after first save: {firstHashBefore}");

            // Change colors after the first save: red bars on yellow background
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Red;
            generator.Parameters.BackColor = Aspose.Drawing.Color.Yellow;

            // Save the second image with the new colors
            generator.Save(secondImagePath, BarCodeImageFormat.Png);
            Console.WriteLine($"Second barcode saved to: {secondImagePath}");
        }

        // Compute hash of the first image again to verify it hasn't changed
        string firstHashAfter = ComputeFileHash(firstImagePath);
        Console.WriteLine($"Hash of first image after second save: {firstHashAfter}");

        // Compute hash of the second image
        string secondHash = ComputeFileHash(secondImagePath);
        Console.WriteLine($"Hash of second image: {secondHash}");

        // Compare hashes to demonstrate that the first image remained unchanged
        if (firstHashBefore == firstHashAfter)
        {
            Console.WriteLine("Success: The first saved image was not altered after modifying colors.");
        }
        else
        {
            Console.WriteLine("Failure: The first saved image was altered.");
        }

        // Cleanup: (optional) delete temporary files and folder
        // Uncomment the following lines if you want to remove the demo files after execution
        // File.Delete(firstImagePath);
        // File.Delete(secondImagePath);
        // Directory.Delete(tempFolder);
    }

    // Helper method to compute SHA256 hash of a file and return it as a hex string
    private static string ComputeFileHash(string filePath)
    {
        using (FileStream stream = File.OpenRead(filePath))
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashBytes = sha256.ComputeHash(stream);
            return BitConverter.ToString(hashBytes).Replace("-", string.Empty);
        }
    }
}