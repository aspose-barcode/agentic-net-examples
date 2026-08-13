// Title: Generate Postnet barcodes batch from a data source
// Description: Demonstrates how to create multiple Postnet barcode images using Aspose.BarCode and save them to a folder. The example simulates reading zip codes from a database.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on batch processing of barcodes. It showcases the BarcodeGenerator class with EncodeTypes.Postnet, file output handling, and typical customization points. Developers looking for ways to automate barcode creation from data collections will find this pattern useful.
// Prompt: Generate a batch of Postnet barcodes from a database table column and save images to a specified folder.
// Tags: postnet, barcode, batch generation, image output, aspose.barcode, c#, png

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace PostnetBatchGenerator
{
    /// <summary>
    /// Provides an entry point that generates a set of Postnet barcodes from a list of codes
    /// (simulating a database column) and saves each barcode as a PNG file.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main method that orchestrates folder creation, data preparation, barcode generation,
        /// and file saving for a batch of Postnet barcodes.
        /// </summary>
        static void Main()
        {
            // Determine the output folder path relative to the current working directory.
            string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
            if (!Directory.Exists(outputFolder))
            {
                // Create the folder if it does not already exist.
                Directory.CreateDirectory(outputFolder);
            }

            // -----------------------------------------------------------------
            // In a real scenario, replace the following block with code that
            // reads the desired column from a database table (e.g., using
            // ADO.NET, Dapper, Entity Framework, etc.).
            // Example (pseudo‑code):
            //   using (var connection = new SqlConnection(connectionString))
            //   {
            //       connection.Open();
            //       var command = new SqlCommand("SELECT ZipCode FROM Addresses", connection);
            //       using (var reader = command.ExecuteReader())
            //       {
            //           while (reader.Read())
            //               postnetCodes.Add(reader.GetString(0));
            //       }
            //   }
            // -----------------------------------------------------------------

            // Sample data to simulate database column values.
            List<string> postnetCodes = new List<string>
            {
                "12345",
                "67890",
                "123456789",
                "00123",
                "98765"
            };

            // Iterate over each code and generate a corresponding Postnet barcode image.
            foreach (string code in postnetCodes)
            {
                // Initialize the barcode generator for the Postnet symbology with the current code.
                using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, code))
                {
                    // Optional: customize barcode appearance here, e.g.:
                    // generator.Parameters.Barcode.XDimension.Point = 2f;
                    // generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

                    // Build the full file path using the code as the file name.
                    string filePath = Path.Combine(outputFolder, $"{code}.png");

                    // Save the generated barcode image as a PNG file.
                    generator.Save(filePath);

                    // Inform the user about the generated file.
                    Console.WriteLine($"Generated Postnet barcode for '{code}' at '{filePath}'.");
                }
            }

            Console.WriteLine("Barcode batch generation completed.");
        }
    }
}