// Title: Batch Generation of GS1 Code 128 Barcodes from a List
// Description: Demonstrates creating GS1 Code 128 barcodes for each record and saving them as PNG files to a network share.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.GS1Code128. Typical use cases include bulk barcode creation from database tables, exporting to shared locations, and integrating barcode assets into enterprise workflows. Developers often need to validate input, configure checksum display, and handle file system operations when automating barcode production.
// Prompt: Batch generate GS1 Code 128 barcodes from a database table, saving each image to a network share.
// Tags: gs1,code128,barcode,generation,output,network,aspobarcodes,aspnet

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates batch generation of GS1 Code 128 barcodes and saving them to a network share.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcodes for a set of GS1 codes and writes PNG files to the specified folder.
    /// </summary>
    static void Main()
    {
        // NOTE: In a real scenario, replace the sample data with a database query.
        // Example of real implementation (requires a database provider):
        // var connectionString = "your_connection_string";
        // using (var connection = new SqlConnection(connectionString))
        // {
        //     connection.Open();
        //     var command = new SqlCommand("SELECT GS1Code FROM YourTable", connection);
        //     using (var reader = command.ExecuteReader())
        //     {
        //         while (reader.Read())
        //         {
        //             var codeText = reader.GetString(0);
        //             // Generate barcode...
        //         }
        //     }
        // }

        // Sample GS1 Code 128 data (AI format) for demonstration.
        List<string> gs1Codes = new List<string>
        {
            "(01)12345678901231",
            "(01)98765432109876",
            "(01)55555555555555",
            "(01)11111111111111",
            "(01)22222222222222"
        };

        // Destination folder (network share or local path). Ensure the folder exists.
        string outputFolder = @"\\networkshare\Barcodes"; // Replace with actual network share.
        // For testing on a local machine, you may use:
        // string outputFolder = Path.Combine(Environment.CurrentDirectory, "Barcodes");

        // Create the output directory if it does not exist.
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to create output directory: {ex.Message}");
                return;
            }
        }

        int index = 1;
        foreach (string codeText in gs1Codes)
        {
            // Validate that the codetext follows GS1 format (basic check).
            if (string.IsNullOrWhiteSpace(codeText) || !codeText.StartsWith("("))
            {
                Console.WriteLine($"Skipping invalid GS1 code: {codeText}");
                continue;
            }

            try
            {
                // Initialize the barcode generator with GS1 Code 128 symbology.
                using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, codeText))
                {
                    // Optional: always show checksum in human‑readable text.
                    generator.Parameters.Barcode.ChecksumAlwaysShow = true;

                    // Build the full file path for the PNG image.
                    string fileName = Path.Combine(outputFolder, $"GS1Code128_{index}.png");

                    // Save the barcode image as PNG.
                    generator.Save(fileName);
                    Console.WriteLine($"Saved barcode {index} to {fileName}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating barcode for '{codeText}': {ex.Message}");
            }

            index++;
        }
    }
}