// Title: Batch generate GS1 Code 128 barcodes and save as PNG files
// Description: Demonstrates how to generate multiple GS1 Code 128 barcodes from a list (simulating a database) and save each image to a specified folder, such as a network share.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category. It shows how to use the BarcodeGenerator class with EncodeTypes.GS1Code128, configure basic appearance settings, and output PNG images. Developers often need to batch‑create barcodes from data sources like databases for inventory, shipping, or retail applications, and this pattern illustrates the typical workflow.
// Prompt: Batch generate GS1 Code 128 barcodes from a database table, saving each image to a network share.
// Tags: gs1,code128,barcode,generation,png,aspose.barcode,batch,database,network share

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates GS1 Code 128 barcodes in batch and saves them as PNG files to a target folder.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates the output directory, validates each code,
    /// generates the barcode image, and writes it to disk.
    /// </summary>
    static void Main()
    {
        // Define the output folder (replace with actual network share UNC path in production)
        string outputFolder = @"C:\Barcodes\Output";

        // Ensure the output directory exists
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // -----------------------------------------------------------------
        // In a real scenario, replace the following hard‑coded list with a
        // database query (e.g., using System.Data.SqlClient) that retrieves
        // the GS1 Code 128 values to encode.
        // -----------------------------------------------------------------
        List<string> gs1Code128Values = new List<string>
        {
            // AI (01) – GTIN (14 digits). Example GTIN‑14: 00123456789012
            "(01)00123456789012",
            "(01)00123456789013",
            "(01)00123456789014",
            "(01)00123456789015",
            "(01)00123456789016"
        };
        // -----------------------------------------------------------------

        int index = 1;
        foreach (string codeText in gs1Code128Values)
        {
            // Validate that the code text contains a valid (01) AI with 14 digits
            if (!IsValidGs1Code128(codeText))
            {
                Console.WriteLine($"Skipping invalid code text: {codeText}");
                continue;
            }

            // Build the output file name (e.g., Barcode_0001.png)
            string fileName = $"Barcode_{index:D4}.png";
            string outputPath = Path.Combine(outputFolder, fileName);

            // Generate and save the barcode
            using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, codeText))
            {
                // Optional: configure appearance
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;
                generator.Parameters.Barcode.XDimension.Point = 2f; // module size
                generator.Parameters.Barcode.BarHeight.Point = 50f; // bar height

                // Save as PNG
                generator.Save(outputPath);
            }

            Console.WriteLine($"Saved barcode {index} to {outputPath}");
            index++;
        }

        Console.WriteLine("Barcode generation completed.");
    }

    // Simple validation for GS1 Code 128 with AI (01) – ensures 14 digits after the AI
    static bool IsValidGs1Code128(string codeText)
    {
        if (string.IsNullOrEmpty(codeText))
            return false;

        const string prefix = "(01)";
        if (!codeText.StartsWith(prefix))
            return false;

        string digits = codeText.Substring(prefix.Length);
        return digits.Length == 14 && long.TryParse(digits, out _);
    }
}