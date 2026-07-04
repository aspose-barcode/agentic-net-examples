// Title: Changing BarCodeReader Source Image at Runtime
// Description: Demonstrates how to read barcodes from an initial image, then switch to a different image using SetBarCodeImage, and read again.
// Prompt: Change the source image after construction by calling SetBarCodeImage with a new file path.
// Tags: barcode, setimage, reader, decode, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode;

/// <summary>
/// Example program that shows how to change the source image of a <see cref="BarCodeReader"/>
/// after it has been constructed, using <c>SetBarCodeImage</c>.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Reads barcodes from an initial image,
    /// switches to a new image, and reads barcodes again.
    /// </summary>
    static void Main()
    {
        // Paths to the initial and the new barcode images.
        string initialImagePath = "barcode1.png";
        string newImagePath = "barcode2.png";

        // Verify that the initial image exists.
        if (!File.Exists(initialImagePath))
        {
            Console.WriteLine($"Initial image not found: {initialImagePath}");
            return;
        }

        // Verify that the new image exists.
        if (!File.Exists(newImagePath))
        {
            Console.WriteLine($"New image not found: {newImagePath}");
            return;
        }

        // Create a BarCodeReader for the initial image, supporting all barcode types.
        using (var reader = new BarCodeReader(initialImagePath, DecodeType.AllSupportedTypes))
        {
            // Read and display barcodes from the initial image.
            Console.WriteLine("Reading barcodes from the initial image:");
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }

            // Change the source image to the new file.
            reader.SetBarCodeImage(newImagePath);

            // Read and display barcodes from the new image.
            Console.WriteLine("Reading barcodes after changing the source image:");
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }
        }
    }
}