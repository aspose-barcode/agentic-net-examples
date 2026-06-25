using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

/// <summary>
/// Program that generates barcode images for a predefined list of URLs using Aspose.BarCode.
/// </summary>
public class Program
{
    /// <summary>
    /// Entry point of the application. Generates barcode PNG files for each URL in the list.
    /// </summary>
    public static void Main()
    {
        // Define a sample list of URLs to encode as barcodes.
        var urls = new List<string>
        {
            "https://example.com",
            "https://openai.com",
            "https://github.com",
            "https://dotnet.microsoft.com",
            "https://aspose.com"
        };

        // Ensure the output directory exists; create it if it does not.
        string outputDir = "Barcodes";
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Choose a symbology. Code128 works well for alphanumeric strings.
        BaseEncodeType encodeType = EncodeTypes.Code128;

        int index = 1;
        // Iterate over each URL and generate a corresponding barcode image.
        foreach (var url in urls)
        {
            // Build the output file name and full path for the current barcode.
            string fileName = $"barcode_{index}.png";
            string outputPath = Path.Combine(outputDir, fileName);

            try
            {
                // Create a barcode generator with the selected symbology and the URL as the code text.
                using (var generator = new BarcodeGenerator(encodeType, url))
                {
                    // Optional: set the image resolution (dots per inch).
                    generator.Parameters.Resolution = 300f;

                    // Save the generated barcode as a PNG file.
                    generator.Save(outputPath);
                }

                // Inform the user that the barcode was generated successfully.
                Console.WriteLine($"Generated barcode for '{url}' -> {outputPath}");
            }
            catch (Exception ex)
            {
                // Report any errors that occur during barcode generation.
                Console.WriteLine($"Failed to generate barcode for '{url}': {ex.Message}");
            }

            index++;
        }
    }
}