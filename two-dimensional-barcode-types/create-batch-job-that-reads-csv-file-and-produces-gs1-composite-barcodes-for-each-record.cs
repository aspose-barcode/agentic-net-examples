using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating GS1 Composite barcodes from a CSV file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Reads a CSV file, creates sample data if needed,
    /// and generates barcode images for each data row.
    /// </summary>
    static void Main()
    {
        // Path to the input CSV file
        string csvPath = "data.csv";

        // Directory where generated barcode images will be saved
        string outputDir = "Barcodes";

        // Ensure the output directory exists; create it if it does not
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // If the CSV file is missing, create a sample file with a header and a few records
        if (!File.Exists(csvPath))
        {
            string[] sampleLines =
            {
                "LinearPart,TwoDPart",
                "(01)03212345678906,(21)A1B2C3D4E5F6G7H8",
                "(01)12345678901231,(21)B9876543210",
                "(01)09876543210987,(21)C1122334455"
            };
            File.WriteAllLines(csvPath, sampleLines);
            Console.WriteLine($"Sample CSV created at '{csvPath}'.");
        }

        // Read all lines from the CSV file
        string[] lines = File.ReadAllLines(csvPath);

        // Verify that there is at least one data row (excluding the header)
        if (lines.Length <= 1)
        {
            Console.WriteLine("CSV file contains no data rows.");
            return;
        }

        // Iterate over each data row, starting after the header (index 1)
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];

            // Skip empty or whitespace-only lines
            if (string.IsNullOrWhiteSpace(line))
                continue;

            // Split the line into linear and 2D components using a comma delimiter
            string[] parts = line.Split(',');

            // Validate that both components are present
            if (parts.Length < 2)
            {
                Console.WriteLine($"Skipping malformed line {i + 1}: '{line}'");
                continue;
            }

            // Trim whitespace from each component
            string linearPart = parts[0].Trim();
            string twoDPart = parts[1].Trim();

            // Combine linear and 2D parts with the '|' separator required for GS1 Composite barcodes
            string codeText = $"{linearPart}|{twoDPart}";

            // Set the barcode type to GS1 Composite Bar
            BaseEncodeType encodeType = EncodeTypes.GS1CompositeBar;

            // Create a barcode generator instance with the specified type and data
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Configure the linear component to use GS1-128 encoding
                generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;

                // Configure the 2D component to use CC-A (Composite Component A)
                generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

                // Example additional settings for visual appearance
                generator.Parameters.Barcode.Pdf417.AspectRatio = 3f;
                generator.Parameters.Barcode.XDimension.Pixels = 3f;
                generator.Parameters.Barcode.BarHeight.Pixels = 100f;

                // Determine the output file path for the current barcode image
                string outputPath = Path.Combine(outputDir, $"barcode_{i}.png");

                // Save the generated barcode image to the file system
                generator.Save(outputPath);
                Console.WriteLine($"Generated barcode for line {i + 1}: {outputPath}");
            }
        }

        // Indicate that the barcode generation process has finished
        Console.WriteLine("Barcode generation completed.");
    }
}