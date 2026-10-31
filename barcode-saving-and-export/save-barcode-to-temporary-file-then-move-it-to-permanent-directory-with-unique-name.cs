// Title: Save barcode to temporary file and move to permanent directory
// Description: This example generates a Code128 barcode, saves it as a PNG to a temporary location, then moves it to a permanent folder with a unique name.
// Category-Description: Shows how to use Aspose.BarCode Generation API to create barcodes, persist them to the file system, and manage temporary files. Typical use cases include batch barcode creation, temporary storage before further processing, and ensuring unique filenames in a target directory. Developers often need to combine BarcodeGenerator with .NET file I/O for reliable output handling.
// Prompt: Save a barcode to a temporary file, then move it to a permanent directory with a unique name.
// Tags: barcode, code128, save, file, temporary, permanent, aspose.barcode, generation, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a barcode, saving it to a temporary file, and moving it to a permanent directory with a unique name.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, writes it to a temp PNG file, ensures the target directory exists, and moves the file with a new unique name.
    /// </summary>
    static void Main()
    {
        // Define the barcode content and symbology
        string codeText = "12345678";
        BaseEncodeType encodeType = EncodeTypes.Code128;

        // Build a unique temporary file path in the system temp folder
        string tempFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N") + ".png");

        // Generate the barcode and save it directly to the temporary file
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            generator.Save(tempFile, BarCodeImageFormat.Png);
        }

        // Determine the permanent directory relative to the current working directory
        string permanentDir = Path.Combine(Environment.CurrentDirectory, "Barcodes");

        // Create the permanent directory if it does not already exist
        if (!Directory.Exists(permanentDir))
        {
            Directory.CreateDirectory(permanentDir);
        }

        // Build a unique destination file path within the permanent directory
        string destFile = Path.Combine(permanentDir, Guid.NewGuid().ToString("N") + ".png");

        // Move the temporary file to the permanent location
        File.Move(tempFile, destFile);

        // Output the final file location
        Console.WriteLine($"Barcode saved to: {destFile}");
    }
}