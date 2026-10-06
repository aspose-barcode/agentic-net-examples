// Title: Save Barcode to Temporary File and Move to Permanent Directory
// Description: Demonstrates generating a barcode image, saving it to a temporary location, then moving it to a permanent folder with a unique filename.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes and BarCodeImageFormat to create barcode images. Typical use cases include creating barcodes for inventory, shipping, or ticketing systems where a temporary file is needed before committing to a final storage location. Developers often need to ensure unique file names and proper directory handling, which this snippet illustrates for easy integration into larger applications.
// Prompt: Save a barcode to a temporary file, then move it to a permanent directory with a unique name.
// Tags: barcode, code128, generation, png, temporary-file, file-move, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a Code128 barcode, saves it to a temporary file, and then moves it to a permanent directory with a unique name.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode creation, temporary storage, and final placement.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary file path in the system's temp folder
        string tempFilePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N") + ".png");

        // Define the permanent directory (relative to the current working directory) and ensure it exists
        string permanentDir = Path.Combine(Environment.CurrentDirectory, "Barcodes");
        if (!Directory.Exists(permanentDir))
        {
            Directory.CreateDirectory(permanentDir);
        }

        // Generate a unique permanent file path within the permanent directory
        string permanentFilePath = Path.Combine(permanentDir, Guid.NewGuid().ToString("N") + ".png");

        // Generate the barcode and save it directly to the temporary file
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            generator.Save(tempFilePath, BarCodeImageFormat.Png);
        }

        // Move the temporary file to the permanent location, preserving the generated image
        File.Move(tempFilePath, permanentFilePath);

        // Output the locations for verification
        Console.WriteLine("Barcode saved to temporary file: " + tempFilePath);
        Console.WriteLine("Barcode moved to permanent location: " + permanentFilePath);
    }
}