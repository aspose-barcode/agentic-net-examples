using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation of a DataMatrix barcode and outputs it as a Base64‑encoded PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the console application.
    /// Generates a DataMatrix barcode, encodes it as Base64, and writes an HTML <img> tag to the console.
    /// </summary>
    static void Main()
    {
        // NOTE: Full ASP.NET MVC integration cannot be demonstrated in this console
        // application. The core DataMatrix generation logic is shown below, and the
        // resulting image is emitted as a Base64 data URI that can be placed in an
        // <img> tag within an MVC view.

        // Sample data to encode
        const string codeText = "Hello DataMatrix";

        // Generate the DataMatrix barcode and obtain a Base64 string
        string base64Image = GenerateDataMatrixBase64(codeText);

        // Output an HTML <img> tag that can be used in a Razor view
        Console.WriteLine("<img src=\"data:image/png;base64,{0}\" alt=\"DataMatrix Barcode\" />", base64Image);
    }

    /// <summary>
    /// Generates a DataMatrix barcode for the specified text and returns the image as a Base64 string.
    /// </summary>
    /// <param name="text">The text to encode in the DataMatrix barcode.</param>
    /// <returns>Base64‑encoded PNG image of the generated barcode.</returns>
    static string GenerateDataMatrixBase64(string text)
    {
        // Create a memory stream to hold the generated PNG image
        using (var imageStream = new MemoryStream())
        {
            // Initialize the barcode generator for DataMatrix
            using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, text))
            {
                // Configure DataMatrix specific parameters
                generator.Parameters.Barcode.DataMatrix.DataMatrixVersion = DataMatrixVersion.ECC200_20x20;
                generator.Parameters.Barcode.DataMatrix.AspectRatio = 1f; // square
                generator.Parameters.Resolution = 300f; // optional high resolution

                // Save the barcode image to the memory stream in PNG format
                generator.Save(imageStream, BarCodeImageFormat.Png);
            }

            // Convert the image bytes to a Base64 string
            byte[] imageBytes = imageStream.ToArray();
            return Convert.ToBase64String(imageBytes);
        }
    }
}