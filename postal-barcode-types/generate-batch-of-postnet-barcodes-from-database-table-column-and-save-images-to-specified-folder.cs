// Title: Generate Postnet barcodes from database values and save as PNG images
// Description: Demonstrates how to create Postnet barcodes for ZIP codes retrieved from a data source and store each barcode as an image file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator with EncodeTypes.Postnet. It shows typical steps such as preparing output folders, validating numeric input, and saving images, which developers often need when integrating postal barcode creation into batch processing or reporting workflows.
// Prompt: Generate a batch of Postnet barcodes from a database table column and save images to a specified folder.
// Tags: postnet, barcode, generation, image, aspose.barcode

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates batch generation of Postnet barcodes from a collection of ZIP codes
/// and saves each barcode as a PNG image in a dedicated output folder.
/// </summary>
class Program
{
    /// <summary>
    /// Main entry point. Retrieves ZIP codes, validates them, generates Postnet barcodes,
    /// and writes the resulting images to disk.
    /// </summary>
    static void Main()
    {
        // Define the folder where barcode images will be stored.
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "PostnetBarcodes");
        if (!Directory.Exists(outputFolder))
        {
            // Create the folder if it does not already exist.
            Directory.CreateDirectory(outputFolder);
        }

        // ------------------------------------------------------------
        // Simulated data retrieval from a database table column.
        // Replace this block with actual DB access code, e.g., using
        // System.Data.SqlClient to read the column values.
        // ------------------------------------------------------------
        // Example real implementation (commented out because the
        // required database provider may not be available in the runner):
        // List<string> values = new List<string>();
        // using (var connection = new SqlConnection(connectionString))
        // {
        //     connection.Open();
        //     using (var command = new SqlCommand("SELECT ZipCode FROM Addresses", connection))
        //     using (var reader = command.ExecuteReader())
        //     {
        //         while (reader.Read())
        //         {
        //             values.Add(reader.GetString(0));
        //         }
        //     }
        // }
        List<string> values = new List<string> { "12345", "67890", "24680", "13579", "11223" };

        // Iterate over each ZIP code and generate a barcode if the code is valid.
        foreach (string code in values)
        {
            // Postnet requires a numeric ZIP code of 5 or 9 digits.
            if (string.IsNullOrWhiteSpace(code) || (code.Length != 5 && code.Length != 9) || !IsAllDigits(code))
            {
                Console.WriteLine($"Skipping invalid Postnet code: {code}");
                continue;
            }

            // Create a barcode generator for the Postnet symbology.
            using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, code))
            {
                // Optional: adjust short bar height for postal barcodes.
                // generator.Parameters.Postal.ShortBarHeight.Point = 2f;

                // Build the full file path for the output image.
                string filePath = Path.Combine(outputFolder, $"{code}.png");

                // Save the generated barcode as a PNG file.
                generator.Save(filePath);
                Console.WriteLine($"Saved Postnet barcode for {code} to {filePath}");
            }
        }
    }

    /// <summary>
    /// Determines whether the supplied string consists solely of digit characters.
    /// </summary>
    /// <param name="s">The string to evaluate.</param>
    /// <returns>True if all characters are digits; otherwise, false.</returns>
    static bool IsAllDigits(string s)
    {
        foreach (char c in s)
        {
            if (!char.IsDigit(c))
                return false;
        }
        return true;
    }
}