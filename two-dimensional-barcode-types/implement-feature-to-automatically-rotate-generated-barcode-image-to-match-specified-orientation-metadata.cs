using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Path to the metadata file that contains the desired rotation angle (in degrees)
        const string metadataFile = "metadata.txt";

        // Default rotation angle
        float rotationAngle = 0f;

        // Read the rotation angle from the metadata file if it exists
        if (File.Exists(metadataFile))
        {
            string content = File.ReadAllText(metadataFile).Trim();
            if (!float.TryParse(content, out rotationAngle))
            {
                Console.WriteLine("Invalid rotation angle in metadata file. Using default angle 0.");
                rotationAngle = 0f;
            }
        }
        else
        {
            Console.WriteLine("Metadata file not found. Using default rotation angle 0.");
        }

        // Create a barcode generator for Code128 with sample code text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
        {
            // Apply the rotation angle read from metadata
            generator.Parameters.RotationAngle = rotationAngle;

            // Save the rotated barcode image
            const string outputFile = "barcode.png";
            generator.Save(outputFile);
            Console.WriteLine($"Barcode saved to '{outputFile}' with rotation angle {rotationAngle} degrees.");
        }
    }
}