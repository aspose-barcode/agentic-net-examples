// Title: Read barcode from image byte array and output JSON metadata
// Description: Generates a QR barcode, obtains its image as a byte array, reads the barcode from that array, and prints decoded information in JSON format.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition APIs. The example uses BarcodeGenerator to create a barcode image, then BarCodeReader to decode it from a MemoryStream. Typical scenarios include processing barcode images received over network or stored in databases where only raw byte data is available. Developers often need to extract barcode data and related region metadata for further processing or logging.
// Prompt: Read barcode information from a byte array representing an image and output JSON metadata.
// Tags: qr,barcode,read,bytearray,json,aspose.barcode,barcodegeneration,barcoderecognition

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a QR barcode, reads it from a byte array,
/// and outputs the decoded information as JSON.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, decodes it from a byte array,
    /// and writes JSON metadata to the console.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // ------------------------------------------------------------
        // Generate a sample QR barcode and capture its image as a byte array.
        // ------------------------------------------------------------
        byte[] imageBytes;
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "SampleText"))
        {
            // Set the module size (pixel dimension) for the QR code.
            generator.Parameters.Barcode.XDimension.Pixels = 4;

            // Save the generated barcode to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                imageBytes = ms.ToArray(); // Extract the raw image bytes.
            }
        }

        // ------------------------------------------------------------
        // Decode the barcode directly from the byte array.
        // ------------------------------------------------------------
        using (var msRead = new MemoryStream(imageBytes))
        {
            // Allow recognition of all supported barcode types.
            BaseDecodeType decodeType = DecodeType.AllSupportedTypes;

            using (var reader = new BarCodeReader(msRead, decodeType))
            {
                // Read all barcodes found in the image.
                BarCodeResult[] results = reader.ReadBarCodes();

                // Build a JSON array containing metadata for each detected barcode.
                var jsonBuilder = new StringBuilder();
                jsonBuilder.Append('[');
                for (int i = 0; i < results.Length; i++)
                {
                    BarCodeResult result = results[i];

                    // Escape backslashes and quotes in the decoded text for valid JSON.
                    string escapedText = (result.CodeText ?? string.Empty)
                        .Replace("\\", "\\\\")
                        .Replace("\"", "\\\"");

                    jsonBuilder.Append('{');
                    jsonBuilder.AppendFormat("\"CodeText\":\"{0}\",", escapedText);
                    jsonBuilder.AppendFormat("\"CodeTypeName\":\"{0}\",", result.CodeTypeName);

                    // Include the region (bounding rectangle) of the barcode.
                    var rect = result.Region.Rectangle;
                    jsonBuilder.AppendFormat("\"Region\":{{\"X\":{0},\"Y\":{1},\"Width\":{2},\"Height\":{3}}},",
                        rect.X, rect.Y, rect.Width, rect.Height);

                    // Include the rotation angle of the barcode region.
                    jsonBuilder.AppendFormat("\"Angle\":{0}", result.Region.Angle);
                    jsonBuilder.Append('}');

                    // Separate multiple barcode entries with a comma.
                    if (i < results.Length - 1)
                        jsonBuilder.Append(',');
                }
                jsonBuilder.Append(']');

                // Output the resulting JSON string.
                Console.WriteLine(jsonBuilder.ToString());
            }
        }
    }
}