using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Swiss Post Parcel barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode image and saves it to a file.
    /// </summary>
    static void Main()
    {
        // Define the original identifier to encode in the barcode.
        string originalIdentifier = "123456789012";

        // Specify the output file path for the generated PNG image.
        string outputPath = "SwissPostParcel.png";

        // Initialize the barcode generator with Swiss Post Parcel symbology and the identifier.
        using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, originalIdentifier))
        {
            // Configure padding (margin) around the barcode: 20 points on each side.
            generator.Parameters.Barcode.Padding.Left.Point = 20f;
            generator.Parameters.Barcode.Padding.Top.Point = 20f;
            generator.Parameters.Barcode.Padding.Right.Point = 20f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 20f;

            // Save the generated barcode image to the specified PNG file.
            generator.Save(outputPath);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Swiss Post Parcel barcode saved to: {outputPath}");
    }
}