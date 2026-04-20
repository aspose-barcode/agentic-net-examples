using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeBatchGenerator
{
    class Program
    {
        static void Main()
        {
            // Prepare output folder
            string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            // Retrieve identifiers from a data source (simulated here)
            List<string> identifiers = GetIdentifiers();

            // Generate a barcode for each identifier
            foreach (string id in identifiers)
            {
                // Build a safe file name
                string safeFileName = MakeSafeFileName(id) + ".jpeg";
                string filePath = Path.Combine(outputFolder, safeFileName);

                // Create and configure the barcode generator
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
                {
                    generator.CodeText = id;
                    // Save as JPEG
                    generator.Save(filePath, BarCodeImageFormat.Jpeg);
                }
            }

            Console.WriteLine("Barcode generation completed. Files saved to: " + outputFolder);
        }

        // Simulated data retrieval – replace with real DB query as needed
        private static List<string> GetIdentifiers()
        {
            // In a real scenario, execute a database query and populate this list.
            // For safety in this example, we return a small static set.
            return new List<string>
            {
                "ID001",
                "ID002",
                "ID003",
                "ID004",
                "ID005"
            };
        }

        // Replace characters that are invalid in file names
        private static string MakeSafeFileName(string name)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                name = name.Replace(c, '_');
            }
            return name;
        }
    }
}