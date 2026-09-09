// Title: Save barcode to temporary file and move to permanent directory
// Description: Demonstrates generating a barcode image, saving it to a temporary location, then moving it to a permanent folder with a unique filename.
// Category-Description: This example belongs to the Aspose.BarCode generation and file handling category. It showcases the use of BarcodeGenerator, BarCodeImageFormat, and standard .NET I/O classes to create a barcode, store it temporarily, and then relocate it to a persistent directory. Developers often need to generate barcodes on the fly and manage their storage, making this pattern common for web services, batch processing, and reporting scenarios.
// Prompt: Save a barcode to a temporary file, then move it to a permanent directory with a unique name.
// Tags: barcode, code128, save, file, temporary, permanent, aspose.barcode, generation, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a Code128 barcode, saves it to a temporary file, and then moves it to a permanent directory with a unique name.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, temporary storage, and final relocation.
    /// </summary>
    static void Main()
    {
        // Define the data to encode in the barcode.
        string codeText = "Sample123";

        // Choose the barcode symbology (Code128).
        BaseEncodeType encodeType = EncodeTypes.Code128;

        // Build a unique temporary file path with a .png extension.
        string tempFilePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N") + ".png");

        // Generate the barcode and save it directly to the temporary file.
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            generator.Save(tempFilePath, BarCodeImageFormat.Png);
        }

        // Determine the permanent directory relative to the current working directory.
        string permanentDir = Path.Combine(Directory.GetCurrentDirectory(), "PermanentBarcodes");

        // Ensure the permanent directory exists; create it if necessary.
        if (!Directory.Exists(permanentDir))
        {
            Directory.CreateDirectory(permanentDir);
        }

        // Create a unique file name for the permanent location.
        string permanentFilePath = Path.Combine(permanentDir, Guid.NewGuid().ToString("N") + ".png");

        // Move the barcode image from the temporary location to the permanent directory.
        try
        {
            File.Move(tempFilePath, permanentFilePath);
            Console.WriteLine("Barcode saved to: " + permanentFilePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error moving file: " + ex.Message);
        }
    }
}