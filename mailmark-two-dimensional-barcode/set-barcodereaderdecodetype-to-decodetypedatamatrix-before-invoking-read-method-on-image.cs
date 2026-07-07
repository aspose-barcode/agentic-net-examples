// Title: Decode DataMatrix barcode from generated image
// Description: This example generates a DataMatrix barcode, saves it as PNG, then reads it back using BarCodeReader with DecodeType set to DataMatrix.
// Category-Description: Demonstrates Aspose.BarCode barcode generation and recognition focusing on DataMatrix symbology. It uses BarcodeGenerator to create the barcode and BarCodeReader with DecodeType to limit decoding. Developers working with specific symbologies often need to restrict decoding for performance or accuracy, making this pattern common in scanning applications.
// Prompt: Set BarCodeReader.DecodeType to DecodeType.DataMatrix before invoking the Read method on the image.
// Tags: datamatrix, decode, png, barcodegenerator, barcodereader, aspnet, csharp

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a DataMatrix barcode, saving it as PNG, and reading it back with decoding limited to DataMatrix.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode image, verifies its existence, and reads the barcode using BarCodeReader with DecodeType.DataMatrix.
    /// </summary>
    static void Main()
    {
        // Define the file path for the sample DataMatrix barcode image.
        string imagePath = "datamatrix.png";

        // Generate a DataMatrix barcode and save it to a PNG file.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "SampleData"))
        {
            // Save the generated barcode image.
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was successfully created.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: Barcode image '{imagePath}' was not found.");
            return;
        }

        // Create a BarCodeReader to read the barcode from the image.
        using (var reader = new BarCodeReader(imagePath))
        {
            // Set the decode type to DataMatrix before reading to limit detection to this symbology.
            reader.BarCodeReadType = DecodeType.DataMatrix;

            // Perform barcode detection and iterate through any results.
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Decoded Text: {result.CodeText}");
            }
        }
    }
}