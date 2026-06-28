using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR code image using Aspose.BarCode and logs the generation parameters.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR code, saves it to a file, and prints configuration details.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated QR code image.
        string outputPath = "qr.png";

        // Specify the barcode type as QR Code.
        BaseEncodeType encodeType = EncodeTypes.QR;

        // Initialize the barcode generator within a using block to ensure proper disposal.
        using (var generator = new BarcodeGenerator(encodeType))
        {
            // -------------------------------------------------
            // Set the data to be encoded in the QR code.
            // -------------------------------------------------
            generator.CodeText = "https://example.com";

            // -------------------------------------------------
            // Configure QR-specific parameters.
            // -------------------------------------------------
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;          // Error correction level M.
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Auto;          // Automatic encoding mode.
            generator.Parameters.Barcode.QR.Version = QRVersion.Auto;                // Automatic QR version selection.
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;          // Use UTF-8 character encoding.

            // -------------------------------------------------
            // Set general image properties.
            // -------------------------------------------------
            generator.Parameters.Resolution = 300f;                                   // Image resolution in DPI.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;          // Auto-size mode using interpolation.
            generator.Parameters.ImageWidth.Point = 300f;                             // Image width in points.
            generator.Parameters.ImageHeight.Point = 300f;                            // Image height in points.

            // -------------------------------------------------
            // Define padding around the barcode (in points).
            // -------------------------------------------------
            generator.Parameters.Barcode.Padding.Left.Point = 10f;
            generator.Parameters.Barcode.Padding.Top.Point = 10f;
            generator.Parameters.Barcode.Padding.Right.Point = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 10f;

            // -------------------------------------------------
            // Save the generated QR code image to the specified path.
            // -------------------------------------------------
            generator.Save(outputPath);

            // -------------------------------------------------
            // Output detailed generation parameters to the console for verification.
            // -------------------------------------------------
            Console.WriteLine("QR Code generated successfully.");
            Console.WriteLine($"Output Path: {outputPath}");
            Console.WriteLine($"CodeText: {generator.CodeText}");
            Console.WriteLine($"ErrorLevel: {generator.Parameters.Barcode.QR.ErrorLevel}");
            Console.WriteLine($"EncodeMode: {generator.Parameters.Barcode.QR.EncodeMode}");
            Console.WriteLine($"Version: {generator.Parameters.Barcode.QR.Version}");
            Console.WriteLine($"ECIEncoding: {generator.Parameters.Barcode.QR.ECIEncoding}");
            Console.WriteLine($"Resolution (DPI): {generator.Parameters.Resolution}");
            Console.WriteLine($"AutoSizeMode: {generator.Parameters.AutoSizeMode}");
            Console.WriteLine($"ImageWidth (pt): {generator.Parameters.ImageWidth.Point}");
            Console.WriteLine($"ImageHeight (pt): {generator.Parameters.ImageHeight.Point}");
            Console.WriteLine($"Padding Left (pt): {generator.Parameters.Barcode.Padding.Left.Point}");
            Console.WriteLine($"Padding Top (pt): {generator.Parameters.Barcode.Padding.Top.Point}");
            Console.WriteLine($"Padding Right (pt): {generator.Parameters.Barcode.Padding.Right.Point}");
            Console.WriteLine($"Padding Bottom (pt): {generator.Parameters.Barcode.Padding.Bottom.Point}");
        }
    }
}