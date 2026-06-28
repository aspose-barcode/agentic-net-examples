using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a Code128 barcode, storing it in memory,
/// and then reading it back using Aspose.BarCode library.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, writes it to a memory stream,
    /// configures the reader to use a limited number of CPU cores,
    /// and then reads and displays the barcode data.
    /// </summary>
    static void Main()
    {
        // Create a memory stream to hold the generated barcode image.
        using (var memoryStream = new MemoryStream())
        {
            // Generate a Code128 barcode with the text "123456".
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
            {
                // Save the barcode image to the memory stream in PNG format.
                generator.Save(memoryStream, BarCodeImageFormat.Png);
            }

            // Reset the stream position to the beginning for reading.
            memoryStream.Position = 0;

            // Limit barcode recognition processing to use only 4 CPU cores.
            BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = 4;

            // Initialize a barcode reader for Code128 type using the memory stream.
            using (var reader = new BarCodeReader(memoryStream, DecodeType.Code128))
            {
                // Iterate through all detected barcodes in the stream.
                foreach (var result in reader.ReadBarCodes())
                {
                    // Output the decoded text of each barcode to the console.
                    Console.WriteLine("Detected CodeText: " + result.CodeText);
                }
            }
        }
    }
}