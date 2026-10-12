// Title: Demonstrate Color Property Independence After Save
// Description: Shows that changing barcode and background colors after saving does not affect the previously saved image file.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how the BarcodeGenerator and its Parameters (Barcode, BackColor) can be configured. Developers often need to generate multiple barcode images with different visual styles while ensuring earlier outputs remain unchanged. The snippet highlights typical use cases such as dynamic color changes and file integrity verification using hash comparison.
// Prompt: Demonstrate that modifying color properties after calling Save does not alter the already saved image.
// Tags: code128, barcode, color, png, save, hash, aspose.barcode, barcodegenerator, parameters

using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates two barcode images, modifies color settings between saves,
/// and verifies that the first saved image remains unchanged by comparing file hashes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcodes, saves them with different colors,
    /// and checks that the first saved file is not affected by later color changes.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary output directory
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Define file paths for the two barcode images
        string file1 = Path.Combine(outputDir, "barcode1.png");
        string file2 = Path.Combine(outputDir, "barcode2.png");

        // --------------------------------------------------------------------
        // Create the first barcode generator instance with initial colors
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            // Set initial barcode (foreground) and background colors
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Save the first image to disk
            generator.Save(file1, BarCodeImageFormat.Png);
        }

        // Compute and display the hash of the first saved file
        string hashBefore = ComputeFileHash(file1);
        Console.WriteLine($"Hash of first saved image: {hashBefore}");

        // --------------------------------------------------------------------
        // Create a second generator (or reuse) and change colors before saving
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            // Change barcode and background colors after the first save
            generator.Parameters.Barcode.BarColor = Color.Red;
            generator.Parameters.BackColor = Color.Yellow;

            // Save the second image to disk
            generator.Save(file2, BarCodeImageFormat.Png);
        }

        // Compute the hash of the first file again to verify it hasn't changed
        string hashAfter = ComputeFileHash(file1);
        Console.WriteLine($"Hash of first image after modifying colors: {hashAfter}");

        // Compute and display the hash of the second file
        string hashSecond = ComputeFileHash(file2);
        Console.WriteLine($"Hash of second saved image: {hashSecond}");

        // Compare hashes to confirm the first image remained unchanged
        if (hashBefore == hashAfter)
        {
            Console.WriteLine("The first saved image remained unchanged after modifying color properties.");
        }
        else
        {
            Console.WriteLine("Unexpected change detected in the first saved image.");
        }

        // Optional cleanup (commented out)
        // File.Delete(file1);
        // File.Delete(file2);
        // Directory.Delete(outputDir);
    }

    /// <summary>
    /// Computes the MD5 hash of a file and returns it as a lowercase hexadecimal string.
    /// </summary>
    /// <param name="filePath">Full path to the file whose hash is to be computed.</param>
    /// <returns>Lowercase hexadecimal representation of the file's MD5 hash.</returns>
    static string ComputeFileHash(string filePath)
    {
        using (var md5 = MD5.Create())
        {
            using (var stream = File.OpenRead(filePath))
            {
                byte[] hash = md5.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }
    }
}