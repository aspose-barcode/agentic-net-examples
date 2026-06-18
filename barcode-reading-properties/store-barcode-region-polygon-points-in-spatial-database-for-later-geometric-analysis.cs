using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a barcode, reading it, and exporting the barcode region's polygon points to a CSV file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the program.
    /// Generates a Code128 barcode, reads the barcode to obtain its region polygon points,
    /// writes those points to a CSV file, and outputs them to the console.
    /// </summary>
    static void Main()
    {
        // Define file paths for the temporary barcode image and the output CSV file.
        string barcodePath = "barcode.png";
        string csvPath = "barcode_points.csv";

        // --------------------------------------------------------------------
        // Generate a simple Code128 barcode and save it to a file.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Save the generated barcode image to the specified path.
            generator.Save(barcodePath);
        }

        // Verify that the barcode image was successfully created.
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // --------------------------------------------------------------------
        // Read the barcode image and extract the region polygon points.
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
        {
            // Open a StreamWriter to create (or overwrite) the CSV file.
            using (var writer = new StreamWriter(csvPath, false))
            {
                // Write the CSV header line.
                writer.WriteLine("X,Y");

                // Iterate over each detected barcode (only one expected in this example).
                foreach (var result in reader.ReadBarCodes())
                {
                    // Retrieve the polygon corner points of the barcode region.
                    var points = result.Region.Points;

                    // Write each point as a separate line in the CSV file.
                    foreach (var pt in points)
                    {
                        writer.WriteLine($"{pt.X},{pt.Y}");
                    }

                    // Output the points to the console for demonstration purposes.
                    Console.WriteLine("Barcode region polygon points:");
                    foreach (var pt in points)
                    {
                        Console.WriteLine($"({pt.X}, {pt.Y})");
                    }
                }
            }
        }

        // NOTE: In a real application, the points would be inserted into a spatial database
        // (e.g., SQL Server with spatial types, PostGIS, etc.) using appropriate ADO.NET or ORM code.
        // The CSV file serves as a placeholder for that persistence layer.

        // Inform the user that the CSV file has been created.
        Console.WriteLine($"Polygon points have been saved to '{csvPath}'.");
    }
}