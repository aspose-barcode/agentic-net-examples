// Title: Barcode Generation with Retry on Save Errors
// Description: Demonstrates generating a Code128 barcode image and saving it with a retry mechanism to handle transient I/O errors.
// Category-Description: Shows how to use Aspose.BarCode's BarcodeGenerator to create barcodes, configure visual parameters, and persist the image while implementing retry logic for transient failures. This example belongs to the barcode generation and image output category, illustrating typical use cases such as handling file system errors during Save operations. Developers working with Aspose.BarCode often need to ensure reliable image creation in automated pipelines.
// Prompt: Implement a retry mechanism for barcode generation when transient errors occur during image saving.
// Tags: barcode, code128, retry, ioexception, barcodelibrary, aspose.barcode, image-saving, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code128 barcode and saves it to a PNG file,
/// retrying the save operation if transient errors occur.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Executes the barcode generation and save logic with retry handling.
    /// </summary>
    static void Main()
    {
        const string outputFile = "barcode.png";
        const int maxRetries = 3;
        int attempt = 0;
        bool saved = false;

        // Ensure the directory for the output file exists
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputFile));
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Attempt to generate and save the barcode, retrying on failure up to maxRetries
        while (attempt < maxRetries && !saved)
        {
            attempt++;
            try
            {
                // Create and configure the barcode generator
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
                {
                    // Set the barcode color (optional visual customization)
                    generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

                    // Save the barcode image to the specified file
                    generator.Save(outputFile);
                }

                // Mark as saved and report success
                saved = true;
                Console.WriteLine($"Barcode saved successfully on attempt {attempt}.");
            }
            catch (BarCodeException ex)
            {
                // Handle barcode-specific errors (e.g., invalid data or configuration)
                Console.WriteLine($"BarCodeException on attempt {attempt}: {ex.Message}");
                if (attempt >= maxRetries)
                {
                    Console.WriteLine("Maximum retry attempts reached. Operation failed.");
                }
            }
            catch (IOException ex)
            {
                // Handle I/O errors such as file access conflicts or disk issues
                Console.WriteLine($"IOException on attempt {attempt}: {ex.Message}");
                if (attempt >= maxRetries)
                {
                    Console.WriteLine("Maximum retry attempts reached. Operation failed.");
                }
            }
        }
    }
}