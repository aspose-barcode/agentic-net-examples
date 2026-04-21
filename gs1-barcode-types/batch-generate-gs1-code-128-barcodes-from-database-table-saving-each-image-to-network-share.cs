using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Path to the CSV file that simulates a database table.
        // Each line should contain a GS1 Code 128 value, e.g. "(01)12345678901231(21)ABC123".
        string csvPath = "data.csv";

        if (!File.Exists(csvPath))
        {
            Console.WriteLine($"Data file not found: {csvPath}");
            // In a real scenario, replace the above with database access code, e.g.:
            // using var connection = new SqlConnection(connectionString);
            // var command = new SqlCommand("SELECT CodeText FROM Barcodes", connection);
            // connection.Open();
            // using var reader = command.ExecuteReader();
            // while (reader.Read()) { /* process each row */ }
            return;
        }

        // Network share where barcode images will be saved.
        // Ensure the UNC path is accessible from the running environment.
        string outputFolder = @"\\Server\Share\Barcodes";

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

        // Read up to 5 lines from the CSV file.
        string[] lines = File.ReadAllLines(csvPath);
        int maxCount = Math.Min(5, lines.Length);
        for (int i = 0; i < maxCount; i++)
        {
            string codeText = lines[i].Trim();
            if (string.IsNullOrEmpty(codeText))
                continue;

            // Generate the barcode.
            using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, codeText))
            {
                // Optional: set barcode colors.
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Build a safe file name.
                string safeFileName = $"barcode_{i + 1}.png";
                string outputPath = Path.Combine(outputFolder, safeFileName);

                try
                {
                    generator.Save(outputPath);
                    Console.WriteLine($"Saved barcode to: {outputPath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error saving barcode for '{codeText}': {ex.Message}");
                }
            }
        }
    }
}