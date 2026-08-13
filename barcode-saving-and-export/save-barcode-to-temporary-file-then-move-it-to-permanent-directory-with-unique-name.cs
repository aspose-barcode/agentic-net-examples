// Title: Save barcode to temporary file then move to permanent location
// Description: Demonstrates generating a barcode image, saving it to a temporary file, and moving it to a permanent directory with a unique filename.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator to create barcodes, save them as images, and manage file storage. Developers often need to generate barcodes on the fly, store them temporarily, and then move them to a persistent location for later use, such as embedding in documents or serving via web APIs.
// Prompt: Save a barcode to a temporary file, then move it to a permanent directory with a unique name.
// Tags: barcode generation, code128, png, temporary file, file move, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates creating a barcode, saving it to a temporary file, and moving it to a permanent directory with a unique name.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, stores it temporarily, then moves it to a permanent folder.
    /// </summary>
    static void Main()
    {
        // Define the barcode content and symbology (Code128)
        string codeText = "1234567890";
        BaseEncodeType encodeType = EncodeTypes.Code128;

        // Build a unique temporary file path in the system's temp folder
        string tempFilePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".png");

        // Generate the barcode and save it directly to the temporary file (PNG format by default)
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            generator.Save(tempFilePath);
        }

        // Determine the permanent directory relative to the current working directory
        string permanentDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");

        // Ensure the permanent directory exists; create it if necessary
        if (!Directory.Exists(permanentDir))
        {
            Directory.CreateDirectory(permanentDir);
        }

        // Create a unique file name for the permanent location to avoid collisions
        string permanentFilePath = Path.Combine(permanentDir, Guid.NewGuid().ToString() + ".png");

        // Move the barcode image from the temporary location to the permanent directory
        File.Move(tempFilePath, permanentFilePath);

        // Output the final location of the saved barcode
        Console.WriteLine("Barcode saved to: " + permanentFilePath);
    }
}