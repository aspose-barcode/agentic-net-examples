using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demo program that generates a Postnet barcode and optionally stores it in a database.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode image from a sample postal code and outputs its size.
    /// </summary>
    static void Main()
    {
        // Sample postal code text for a Postnet barcode
        const string postalCode = "12345";

        // Byte array that will hold the generated barcode image
        byte[] barcodeBytes;

        // Create a BarcodeGenerator for the Postnet format using the sample postal code
        using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, postalCode))
        {
            // Optional: configure barcode appearance if needed
            // generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            // generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Use a memory stream to capture the barcode image in PNG format
            using (var ms = new MemoryStream())
            {
                // Save the barcode image as PNG into the memory stream
                generator.Save(ms, BarCodeImageFormat.Png);

                // Convert the memory stream contents to a byte array
                barcodeBytes = ms.ToArray();
            }
        }

        // Output the size of the generated barcode image in bytes
        Console.WriteLine($"Generated barcode image size: {barcodeBytes.Length} bytes");

        // ----------------------------------------------------------------------
        // Real database insertion (requires System.Data.SqlClient or Microsoft.Data.SqlClient)
        // The following code is provided as a reference but is commented out because
        // the required NuGet package is not available in the snippet runner environment.
        // ----------------------------------------------------------------------
        /*
        using (var connection = new SqlConnection("Data Source=YOUR_SERVER;Initial Catalog=YOUR_DB;Integrated Security=True"))
        {
            connection.Open();
            using (var command = new SqlCommand("INSERT INTO Barcodes (BarcodeImage) VALUES (@Image)", connection))
            {
                var param = command.Parameters.Add("@Image", SqlDbType.VarBinary, barcodeBytes.Length);
                param.Value = barcodeBytes;
                command.ExecuteNonQuery();
            }
        }
        */
    }
}