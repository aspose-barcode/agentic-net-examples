// Title: Generate QR Code barcodes in batch from database query and save as JPEG
// Description: Demonstrates how to create multiple QR Code images from a list of strings (simulating a DB query) and store each as a JPEG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showing how to use BarcodeGenerator with EncodeTypes.QR, configure QR error correction, and export images. Developers often need to batch‑process data from databases to produce barcodes for marketing, inventory, or authentication purposes. The snippet illustrates typical API usage for bulk QR code creation and file handling.
// Prompt: Generate QR Code barcodes in batch from database query and store each as JPEG in folder.
// Tags: qr code, batch generation, jpeg output, aspose.barcode, barcodegenerator, encode types

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates batch generation of QR Code barcodes from a data source and saving them as JPEG images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates QR codes for each string in the mock list (replace with DB query) and writes JPEG files.
    /// </summary>
    static void Main()
    {
        // Determine the output folder path relative to the current working directory.
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "QrCodes");

        // Ensure the output directory exists; create it if it does not.
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // -----------------------------------------------------------------
        // In a real scenario, replace the following mock data with a
        // database query that returns the list of strings to encode.
        // Example (pseudo‑code):
        //   using (var connection = new SqlConnection(connectionString))
        //   {
        //       connection.Open();
        //       using (var command = new SqlCommand("SELECT Url FROM MyTable", connection))
        //       using (var reader = command.ExecuteReader())
        //       {
        //           while (reader.Read())
        //               codes.Add(reader.GetString(0));
        //       }
        //   }
        // -----------------------------------------------------------------
        List<string> codes = new List<string>
        {
            "https://example.com/1",
            "https://example.com/2",
            "https://example.com/3",
            "https://example.com/4",
            "https://example.com/5"
        };

        int index = 1;

        // Iterate over each code text, generate a QR code, and save it as a JPEG file.
        foreach (string codeText in codes)
        {
            // Initialize the QR Code generator with the desired symbology and data.
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
            {
                // Optionally set the QR error correction level (LevelM provides a good balance).
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

                // Build the output file name using a zero‑padded index.
                string fileName = Path.Combine(outputFolder, $"qr_{index:D3}.jpg");

                // Save the generated barcode image in JPEG format.
                generator.Save(fileName, BarCodeImageFormat.Jpeg);
            }

            // Log progress to the console.
            Console.WriteLine($"Generated QR code {index} -> {codeText}");
            index++;
        }

        // Indicate that the batch process has completed.
        Console.WriteLine("All QR codes have been generated.");
    }
}