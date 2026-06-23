using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode, saving it to a temporary file,
/// and moving it to a permanent directory.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point. Generates a barcode image and stores it permanently.
    /// </summary>
    static void Main()
    {
        // Define the barcode symbology (Code128) and the text to encode.
        BaseEncodeType encodeType = EncodeTypes.Code128;
        string codeText = "Sample123";

        // Build a temporary file path in the system's temp folder with a .png extension.
        string tempFilePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".png");

        // Generate the barcode and save it directly to the temporary file.
        try
        {
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Save the barcode image to the temporary location.
                generator.Save(tempFilePath);
            }
        }
        catch (Exception ex)
        {
            // Output any errors that occur during barcode generation.
            Console.WriteLine("Error generating barcode: " + ex.Message);
            return;
        }

        // Ensure the temporary file was created successfully.
        if (!File.Exists(tempFilePath))
        {
            Console.WriteLine("Temporary barcode file was not created.");
            return;
        }

        // Determine the permanent directory path relative to the application's base directory.
        string permanentDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Barcodes");

        // Create the permanent directory if it does not already exist.
        try
        {
            if (!Directory.Exists(permanentDir))
            {
                Directory.CreateDirectory(permanentDir);
            }
        }
        catch (Exception ex)
        {
            // Output any errors that occur while creating the directory.
            Console.WriteLine("Error creating permanent directory: " + ex.Message);
            return;
        }

        // Build a unique file name for the permanent location.
        string permanentFilePath = Path.Combine(permanentDir, Guid.NewGuid().ToString() + ".png");

        // Move the file from the temporary location to the permanent directory.
        try
        {
            File.Move(tempFilePath, permanentFilePath);
            Console.WriteLine("Barcode saved to: " + permanentFilePath);
        }
        catch (Exception ex)
        {
            // Output any errors that occur during the file move operation.
            Console.WriteLine("Error moving barcode file: " + ex.Message);
        }
    }
}