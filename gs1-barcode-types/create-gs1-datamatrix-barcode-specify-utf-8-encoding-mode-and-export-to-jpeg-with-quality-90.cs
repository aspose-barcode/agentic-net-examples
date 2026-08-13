// Title: Generate GS1 DataMatrix Barcode and Save as JPEG
// Description: Demonstrates creating a GS1 DataMatrix barcode with UTF‑8 encoding and exporting it to a JPEG file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to configure DataMatrix encoding options such as GS1 mode and ECI UTF‑8, and how to save the generated barcode as an image file. Developers working with product identification, inventory, or logistics often need to produce GS1 DataMatrix symbols for GTINs and other application identifiers, using classes like BarcodeGenerator, EncodeTypes, and DataMatrixEncodeMode.
// Prompt: Create a GS1 DataMatrix barcode, specify UTF‑8 encoding mode, and export to JPEG with quality 90.
// Tags: gs1, datamatrix, barcode, generation, utf-8, jpeg, aspose.barcode, aspose.drawing

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates creating a GS1 DataMatrix barcode with UTF‑8 encoding and saving it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates the barcode and writes it to disk.
    /// </summary>
    static void Main()
    {
        // Define the GS1 DataMatrix payload: Application Identifier (01) followed by a 14‑digit GTIN.
        string codeText = "(01)00123456789012";

        // Initialize the barcode generator for GS1 DataMatrix with the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
        {
            // Configure the DataMatrix to use ECI encoding mode with UTF‑8 for proper Unicode support.
            generator.Parameters.Barcode.DataMatrix.EncodeMode = DataMatrixEncodeMode.ECI;
            generator.Parameters.Barcode.DataMatrix.ECIEncoding = ECIEncodings.UTF8;

            // Save the generated barcode as a JPEG image.
            // Note: Aspose.BarCode uses the default JPEG quality; a specific quality setting is not exposed.
            generator.Save("gs1_datamatrix.jpg");
        }

        // Inform the user that the file has been created.
        Console.WriteLine("GS1 DataMatrix barcode saved as gs1_datamatrix.jpg");
    }
}