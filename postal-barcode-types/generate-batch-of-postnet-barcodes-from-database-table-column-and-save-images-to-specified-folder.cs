// Title: Generate Postnet barcodes from a list and save as PNG files
// Description: Demonstrates how to create Postnet barcodes for a collection of ZIP codes and store each barcode image in a temporary folder.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing the use of BarcodeGenerator with EncodeTypes.Postnet. It covers setting visual parameters, validating input data, and saving images in PNG format—common tasks for developers building mailing or shipping applications that require postal barcode output.
// Prompt: Generate a batch of Postnet barcodes from a database table column and save images to a specified folder.
// Tags: postnet, barcode, generation, png, aspose.barcode, csharp

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates Postnet barcodes for a set of ZIP codes
/// and writes each barcode image to a uniquely named PNG file in a temporary folder.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Iterates over a list of ZIP codes,
    /// validates each entry, creates a Postnet barcode, and saves the image to disk.
    /// </summary>
    static void Main()
    {
        // -----------------------------------------------------------------
        // In a real scenario, the following list would be populated from a
        // database table column (e.g., SELECT ZipCode FROM PostalCodes).
        // The database code is omitted because the required ADO.NET
        // providers are not available in the snippet runner.
        // -----------------------------------------------------------------
        List<string> zipCodes = new List<string>
        {
            "12345",
            "90210",
            "10001",
            "33109",
            "60614"
        };

        // Create a dedicated output folder inside the system temporary directory.
        string outputFolder = Path.Combine(Path.GetTempPath(),
            "PostnetBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        Console.WriteLine("Generating Postnet barcodes in: " + outputFolder);

        int index = 1;
        foreach (string zip in zipCodes)
        {
            // Validate that the ZIP code consists only of numeric characters.
            if (string.IsNullOrWhiteSpace(zip) || !IsAllDigits(zip))
            {
                Console.WriteLine($"Skipping invalid zip code '{zip}'.");
                continue;
            }

            // Build the full file path for the current barcode image.
            string filePath = Path.Combine(outputFolder,
                $"Postnet_{index:D3}_{zip}.png");

            // Initialize the barcode generator with Postnet symbology and the ZIP code text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, zip))
            {
                // Configure visual appearance of the barcode.
                generator.Parameters.Barcode.XDimension.Pixels = 3f;          // Width of a single bar.
                generator.Parameters.Barcode.BarHeight.Pixels = 50f;        // Height of the tall bars.
                generator.Parameters.Barcode.Postal.ShortBarHeight.Pixels = 20f; // Height of the short bars.

                // Save the generated barcode as a PNG image.
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            Console.WriteLine($"Saved: {filePath}");
            index++;
        }

        Console.WriteLine("Barcode generation completed.");
    }

    /// <summary>
    /// Determines whether the supplied string consists exclusively of decimal digits.
    /// </summary>
    /// <param name="s">String to evaluate.</param>
    /// <returns>True if every character is a digit; otherwise, false.</returns>
    static bool IsAllDigits(string s)
    {
        foreach (char c in s)
        {
            if (c < '0' || c > '9')
                return false;
        }
        return true;
    }
}