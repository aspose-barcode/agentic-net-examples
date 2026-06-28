using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode and saving it in a user‑specified image format.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Accepts an optional command‑line argument to specify the output image format (PNG, JPEG, or TIFF).
    /// Generates a barcode and saves it to the current directory.
    /// </summary>
    /// <param name="args">Command‑line arguments; the first argument may be the desired image format.</param>
    static void Main(string[] args)
    {
        // Determine desired image format (PNG, JPEG, TIFF). Default to PNG if none provided.
        string formatInput = args.Length > 0 ? args[0].Trim().ToUpperInvariant() : "PNG";

        BarCodeImageFormat imageFormat;
        string extension;

        // Map the textual format to Aspose.BarCode image format and file extension.
        if (formatInput == "PNG")
        {
            imageFormat = BarCodeImageFormat.Png;
            extension = ".png";
        }
        else if (formatInput == "JPEG" || formatInput == "JPG")
        {
            imageFormat = BarCodeImageFormat.Jpeg;
            extension = ".jpg";
        }
        else if (formatInput == "TIFF")
        {
            imageFormat = BarCodeImageFormat.Tiff;
            extension = ".tiff";
        }
        else
        {
            // Fallback to PNG for unsupported formats and inform the user.
            Console.WriteLine($"Unsupported format '{formatInput}'. Falling back to PNG.");
            imageFormat = BarCodeImageFormat.Png;
            extension = ".png";
        }

        // Build the full output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode" + extension);

        // Generate a simple Code128 barcode with the specified data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Optional: set the image resolution (dots per inch).
            generator.Parameters.Resolution = 300f;

            // Save the barcode image to the file using the selected format.
            generator.Save(outputPath, imageFormat);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}