using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a barcode image with optional rotation based on external metadata.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Reads rotation metadata, generates a barcode, and saves it to a temporary file.
    /// </summary>
    static void Main()
    {
        // Define the path where the generated barcode image will be saved.
        string outputPath = Path.Combine(Path.GetTempPath(), "rotated_barcode.png");

        // Path to a simulated orientation metadata source (e.g., a text file containing a degree value).
        string orientationFile = Path.Combine(Path.GetTempPath(), "orientation.txt");

        // Default rotation angle (no rotation) in degrees.
        float rotationAngle = 0f;

        // Check if the orientation metadata file exists.
        if (File.Exists(orientationFile))
        {
            try
            {
                // Read the file content and trim any whitespace.
                string content = File.ReadAllText(orientationFile).Trim();

                // Attempt to parse the content as an integer angle.
                if (int.TryParse(content, out int angle))
                {
                    // Accept only standard rotation angles for reliable scanning.
                    if (angle == 0 || angle == 90 || angle == 180 || angle == 270)
                    {
                        rotationAngle = (float)angle;
                    }
                    else
                    {
                        Console.WriteLine($"Unsupported rotation angle '{angle}'. Using default 0°.");
                    }
                }
                else
                {
                    Console.WriteLine($"Failed to parse rotation angle from '{orientationFile}'. Using default 0°.");
                }
            }
            catch (Exception ex)
            {
                // Handle any errors that occur while reading the file.
                Console.WriteLine($"Error reading orientation metadata: {ex.Message}. Using default 0°.");
            }
        }
        else
        {
            // Inform the user that the metadata file was not found.
            Console.WriteLine($"Orientation metadata file not found at '{orientationFile}'. Using default 0°.");
        }

        // Generate the barcode using the determined rotation angle.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
        {
            // Apply the rotation angle to the barcode parameters.
            generator.Parameters.RotationAngle = rotationAngle;

            // Save the generated barcode image to the specified output path.
            generator.Save(outputPath);
        }

        // Output the location of the saved barcode image.
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}