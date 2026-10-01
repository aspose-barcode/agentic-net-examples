// Title: Save Barcode to Temporary File and Move to Permanent Directory
// Description: Demonstrates generating a barcode, saving it to a temporary location, then moving it to a permanent folder with a unique filename.
// Category-Description: This example belongs to the Aspose.BarCode generation and file handling category. It showcases the use of BarcodeGenerator (EncodeTypes) to create barcodes, BarCodeImageFormat for image output, and standard .NET I/O classes for temporary file creation and moving files. Developers often need to generate barcodes on the fly, store them temporarily for processing, and then persist them with unique names in a permanent directory.
// Prompt: Save a barcode to a temporary file, then move it to a permanent directory with a unique name.
// Tags: barcode, code128, generation, file-io, temporary-file, permanent-storage, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a Code128 barcode, saves it to a temporary file, and then moves it to a permanent directory with a unique name.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Handles temporary file creation, barcode generation, and moving the file to a permanent location.
    /// </summary>
    static void Main()
    {
        // Define the text to encode in the barcode.
        string codeText = "1234567890";

        // Build a unique temporary file path in the system's temp folder.
        string tempFilePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".png");

        // Generate the barcode image and save it to the temporary file.
        GenerateBarcode(codeText, tempFilePath);

        // Determine the permanent directory relative to the current working directory.
        string permanentDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");

        // Ensure the permanent directory exists; create it if necessary.
        if (!Directory.Exists(permanentDir))
        {
            Directory.CreateDirectory(permanentDir);
        }

        // Build a unique file name for the permanent location.
        string permanentFilePath = Path.Combine(permanentDir, Guid.NewGuid().ToString() + ".png");

        // Move the barcode image from the temporary location to the permanent directory.
        File.Move(tempFilePath, permanentFilePath);

        // Output the locations for verification.
        Console.WriteLine($"Barcode saved to temporary file: {tempFilePath}");
        Console.WriteLine($"Barcode moved to permanent location: {permanentFilePath}");
    }

    /// <summary>
    /// Generates a barcode using Code128 symbology and saves it as a PNG image.
    /// </summary>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="outputPath">The full file path where the barcode image will be saved.</param>
    static void GenerateBarcode(string codeText, string outputPath)
    {
        // Initialize the barcode generator with Code128 encoding.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Save the generated barcode directly to the specified path in PNG format.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
    }
}