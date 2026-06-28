using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates reading Swiss Post Parcel barcodes from an image file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Accepts an optional command‑line argument specifying the image file path.
    /// </summary>
    /// <param name="args">Command‑line arguments.</param>
    static void Main(string[] args)
    {
        // Determine the image file path: use the first argument if provided, otherwise default to "SwissPostParcel.png".
        string imagePath = args.Length > 0 ? args[0] : "SwissPostParcel.png";

        // Verify that the specified file exists before attempting to read it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Initialize a barcode reader for the Swiss Post Parcel symbology.
        using (var reader = new BarCodeReader(imagePath, DecodeType.SwissPostParcel))
        {
            // Enable checksum validation to ensure the identifier is correct.
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            // Perform barcode recognition on the image.
            var results = reader.ReadBarCodes();

            // If no barcodes were detected, inform the user and exit.
            if (results.Length == 0)
            {
                Console.WriteLine("No Swiss Post Parcel barcode detected.");
                return;
            }

            // Iterate over each detected barcode and display its details.
            foreach (var result in results)
            {
                // Determine validity: a barcode is considered valid if its CodeText is not null or empty.
                bool isValid = !string.IsNullOrEmpty(result.CodeText);

                // Output barcode type, raw text, and validity status.
                Console.WriteLine($"Detected Barcode Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text: {result.CodeText ?? "<null>"}");
                Console.WriteLine($"Identifier Valid: {isValid}");
            }
        }
    }
}