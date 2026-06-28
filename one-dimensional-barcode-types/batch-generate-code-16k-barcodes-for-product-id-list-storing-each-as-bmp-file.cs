using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates Code16K barcodes for a list of product IDs and saves them as BMP files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Iterates over a sample list of product IDs,
    /// creates a Code16K barcode for each, and writes the image to the output folder.
    /// </summary>
    static void Main()
    {
        // Define a sample list of product IDs (replace with actual IDs as needed)
        List<string> productIds = new List<string>
        {
            "PROD001",
            "PROD002",
            "PROD003",
            "PROD004",
            "PROD005"
        };

        // Determine the output directory relative to the current working directory
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");

        // Ensure the output directory exists; create it if it does not
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Process each product ID in the list
        foreach (string id in productIds)
        {
            // Build the file name and full path for the barcode image
            string fileName = $"{id}_code16k.bmp";
            string outputPath = Path.Combine(outputFolder, fileName);

            try
            {
                // Initialize the barcode generator for Code16K with the current ID as data
                using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, id))
                {
                    // Optional: set the aspect ratio for the Code16K barcode
                    generator.Parameters.Barcode.Code16K.AspectRatio = 1.0f;

                    // Save the generated barcode as a BMP image to the specified path
                    generator.Save(outputPath, BarCodeImageFormat.Bmp);
                }

                // Inform the user that the barcode was generated successfully
                Console.WriteLine($"Generated barcode for '{id}' at '{outputPath}'.");
            }
            catch (Exception ex)
            {
                // Report any errors that occur during barcode generation
                Console.WriteLine($"Failed to generate barcode for '{id}': {ex.Message}");
            }
        }
    }
}