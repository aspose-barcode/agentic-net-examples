// Title: Check Code128 barcode checksum in a BMP image
// Description: Generates a Code128 barcode, saves it as a BMP file, then reads the image to display the barcode text and checksum status.
// Category-Description: This example demonstrates Aspose.BarCode generation and recognition for 1D symbologies. It uses BarcodeGenerator to create a barcode image and BarCodeReader with checksum validation to extract barcode data. Developers working with barcode creation, image-based scanning, or checksum verification will find this pattern useful for automated processing pipelines.
// Prompt: Check 1D barcode checksum status for Code128 barcodes detected in a BMP file.
// Tags: code128, checksum, barcode, generation, recognition, bmp, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates how to generate a Code128 barcode, save it as BMP,
/// read it back, and display checksum information using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary folder, generates a barcode,
    /// reads it with checksum validation, and cleans up resources.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "Code128ChecksumDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the BMP image to be created
        string imagePath = Path.Combine(tempFolder, "code128.bmp");

        try
        {
            // ------------------------------------------------------------
            // Generate a Code128 barcode image (BMP)
            // ------------------------------------------------------------
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Aspose1234"))
            {
                // Set the X-dimension (module width) in pixels for better readability
                generator.Parameters.Barcode.XDimension.Pixels = 2;

                // Save the generated barcode as a BMP file
                generator.Save(imagePath, BarCodeImageFormat.Bmp);
            }

            // Verify that the image file was successfully created
            if (!File.Exists(imagePath))
            {
                Console.WriteLine("Failed to create barcode image.");
                return;
            }

            // ------------------------------------------------------------
            // Read the barcode from the BMP file and check checksum status
            // ------------------------------------------------------------
            using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Code128))
            {
                // Enable checksum validation (required for Code128)
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                bool anyFound = false;

                // Iterate through all detected barcodes (should be only one in this demo)
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    anyFound = true;

                    Console.WriteLine($"Code Type: {result.CodeTypeName}");
                    Console.WriteLine($"Code Text: {result.CodeText}");

                    // For Code128, checksum is obligatory; display it if available via extended parameters
                    if (result.Extended?.OneD != null)
                    {
                        Console.WriteLine($"Checksum: {result.Extended.OneD.CheckSum}");
                    }
                    else
                    {
                        Console.WriteLine("Checksum information not available via OneD extended parameters.");
                    }

                    Console.WriteLine(new string('-', 40));
                }

                if (!anyFound)
                {
                    Console.WriteLine("No Code128 barcode detected in the image.");
                }
            }
        }
        catch (Exception ex)
        {
            // Output any unexpected errors
            Console.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // ------------------------------------------------------------
            // Clean up temporary files and folder
            // ------------------------------------------------------------
            try
            {
                if (Directory.Exists(tempFolder))
                {
                    Directory.Delete(tempFolder, true);
                }
            }
            catch
            {
                // Suppress any exceptions that occur during cleanup
            }
        }
    }
}