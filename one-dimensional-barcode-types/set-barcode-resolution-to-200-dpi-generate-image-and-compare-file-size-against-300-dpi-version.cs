using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating Code128 barcodes at different DPI settings
/// and comparing the resulting file sizes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates two barcode images (200 DPI and 300 DPI),
    /// saves them to disk, and prints their file sizes.
    /// </summary>
    static void Main()
    {
        // Define common barcode settings
        const string codeText = "1234567890";          // Text to encode in the barcode
        const string baseFileName = "barcode";         // Base name for output files
        const string extension = ".png";               // Image file extension

        // ------------------------------
        // Generate barcode at 200 DPI
        // ------------------------------
        string path200 = baseFileName + "_200dpi" + extension; // Output path for 200 DPI image
        using (var generator200 = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator200.Parameters.Resolution = 200f; // Set resolution to 200 DPI
            generator200.Save(path200);                // Save the barcode image
        }

        // ------------------------------
        // Generate barcode at 300 DPI
        // ------------------------------
        string path300 = baseFileName + "_300dpi" + extension; // Output path for 300 DPI image
        using (var generator300 = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator300.Parameters.Resolution = 300f; // Set resolution to 300 DPI
            generator300.Save(path300);                // Save the barcode image
        }

        // Retrieve file sizes for both images
        long size200 = new FileInfo(path200).Length; // Size of 200 DPI image in bytes
        long size300 = new FileInfo(path300).Length; // Size of 300 DPI image in bytes

        // Output file size information to the console
        Console.WriteLine($"File: {path200}, Size: {size200} bytes");
        Console.WriteLine($"File: {path300}, Size: {size300} bytes");

        // Compare file sizes and display the result
        if (size200 > size300)
        {
            Console.WriteLine("200 DPI image is larger than 300 DPI image.");
        }
        else if (size200 < size300)
        {
            Console.WriteLine("300 DPI image is larger than 200 DPI image.");
        }
        else
        {
            Console.WriteLine("Both images have the same file size.");
        }
    }
}