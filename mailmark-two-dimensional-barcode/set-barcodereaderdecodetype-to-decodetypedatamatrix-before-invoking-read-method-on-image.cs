using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a DataMatrix barcode, saving it to a memory stream,
/// and then reading it back using Aspose.BarCode library.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a DataMatrix barcode, writes it to a memory stream,
    /// and reads the barcode back to display its type and text.
    /// </summary>
    static void Main()
    {
        // Define the text to encode in the DataMatrix barcode.
        const string codeText = "Aspose.DataMatrix";

        // Create a barcode generator for DataMatrix with the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            // Use a memory stream to hold the generated barcode image.
            using (var ms = new MemoryStream())
            {
                // Save the generated barcode as a PNG image into the memory stream.
                generator.Save(ms, BarCodeImageFormat.Png);

                // Reset the stream position to the beginning for subsequent reading.
                ms.Position = 0;

                // Initialize a barcode reader with the image stream.
                using (var reader = new BarCodeReader(ms))
                {
                    // Specify that we only want to decode DataMatrix barcodes.
                    reader.BarCodeReadType = DecodeType.DataMatrix;

                    // Perform the barcode recognition and retrieve results.
                    var results = reader.ReadBarCodes();

                    // Iterate through each recognized barcode and output its details.
                    foreach (var result in results)
                    {
                        Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                        Console.WriteLine($"Code Text: {result.CodeText}");
                    }
                }
            }
        }
    }
}