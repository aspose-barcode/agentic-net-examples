using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generation and recognition of UPC-A, UPC-E, and EAN-8 barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo application.
    /// Generates sample barcode images, reads them back using a multi‑decode configuration,
    /// and outputs the detected barcode types and values.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a temporary folder to store generated barcode images.
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodesDemo");
        if (!Directory.Exists(tempFolder))
        {
            Directory.CreateDirectory(tempFolder);
        }

        // --------------------------------------------------------------------
        // Define sample data for each retail symbology (encode type, text, output file).
        // --------------------------------------------------------------------
        var samples = new (BaseEncodeType encode, string text, string file)[]
        {
            // UPC-A: 12 digits (including checksum)
            (EncodeTypes.UPCA, "012345678905", Path.Combine(tempFolder, "upca.png")),
            // UPC-E: 8 digits (including checksum)
            (EncodeTypes.UPCE, "01234565",   Path.Combine(tempFolder, "upce.png")),
            // EAN-8: 7 digits (checksum will be calculated automatically)
            (EncodeTypes.EAN8, "1234567",    Path.Combine(tempFolder, "ean8.png"))
        };

        // --------------------------------------------------------------------
        // Generate barcode images for each sample.
        // --------------------------------------------------------------------
        foreach (var (encode, text, file) in samples)
        {
            using (var generator = new BarcodeGenerator(encode, text))
            {
                generator.Save(file);
                Console.WriteLine($"Generated {encode.TypeName} barcode: {file}");
            }
        }

        // --------------------------------------------------------------------
        // Configure a MultiDecodeType to enable detection of UPC-A, UPC-E, and EAN-8.
        // --------------------------------------------------------------------
        var multiDecode = new MultiDecodeType(DecodeType.UPCA, DecodeType.UPCE, DecodeType.EAN8);

        // --------------------------------------------------------------------
        // Read each generated barcode using the same MultiDecodeType configuration.
        // --------------------------------------------------------------------
        foreach (var (_, _, file) in samples)
        {
            using (var reader = new BarCodeReader(file))
            {
                // Apply the multi‑decode setting to the reader.
                reader.BarCodeReadType = multiDecode;

                // Iterate over all detected barcodes in the image.
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Detected Type: {result.CodeTypeName}, CodeText: {result.CodeText}");
                }
            }
        }

        // --------------------------------------------------------------------
        // Optional clean‑up: delete generated files.
        // Uncomment the following line to remove the temporary images after execution.
        // --------------------------------------------------------------------
        // foreach (var (_, _, file) in samples) File.Delete(file);
    }
}