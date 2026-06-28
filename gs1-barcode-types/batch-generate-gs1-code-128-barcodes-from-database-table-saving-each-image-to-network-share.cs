using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating GS1‑Code128 barcodes and saving them to a network share.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates barcode images for a set of sample data and stores them on a UNC path.
    /// </summary>
    static void Main()
    {
        // NOTE: In a real scenario the barcode data would be read from a database.
        // The following list simulates rows retrieved from a database table.
        List<string> barcodeData = GetSampleData();

        // Destination folder on a network share (UNC path). Adjust as needed.
        string networkSharePath = @"\\MyServer\BarcodeShare";

        // Ensure the destination directory exists; create it if it does not.
        try
        {
            if (!Directory.Exists(networkSharePath))
            {
                Directory.CreateDirectory(networkSharePath);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to access or create the network share folder: {ex.Message}");
            return;
        }

        // Process each barcode value.
        foreach (string codeText in barcodeData)
        {
            // Build a safe file name (remove invalid characters) and add PNG extension.
            string safeFileName = GetSafeFileName(codeText) + ".png";
            string outputPath = Path.Combine(networkSharePath, safeFileName);

            try
            {
                // Initialise the barcode generator with GS1‑Code128 encoding.
                using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, codeText))
                {
                    // Optional: set image resolution if required.
                    generator.Parameters.Resolution = 300f;

                    // Save the barcode image directly to the network share.
                    generator.Save(outputPath);
                }

                Console.WriteLine($"Generated barcode for '{codeText}' at '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating barcode for '{codeText}': {ex.Message}");
            }
        }
    }

    // Simulated data retrieval. Replace with actual DB access code.
    static List<string> GetSampleData()
    {
        // Example GS1 Code 128 values (must contain parentheses for AI).
        return new List<string>
        {
            "(01)12345678901231",
            "(01)98765432109876",
            "(01)55555555555555",
            "(01)00011122233344",
            "(01)99988877766655"
        };
    }

    // Creates a file‑system‑safe name from the barcode text.
    static string GetSafeFileName(string input)
    {
        // Replace each invalid file name character with an underscore.
        foreach (char c in Path.GetInvalidFileNameChars())
        {
            input = input.Replace(c, '_');
        }

        // Trim to a reasonable length to avoid excessively long file names.
        return input.Length > 50 ? input.Substring(0, 50) : input;
    }
}