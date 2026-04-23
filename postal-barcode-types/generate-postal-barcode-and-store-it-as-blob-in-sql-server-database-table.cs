using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Sample postal barcode (Postnet) with a numeric zip code
        const string postalCode = "12345678";

        // Create the barcode generator for Postnet symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, postalCode))
        {
            // Optional: configure short bar height for postal barcodes
            generator.Parameters.Barcode.Postal.PostalShortBarHeight.Point = 10f;

            // Use interpolation auto‑size mode so the image size is controlled by width/height
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Generate the barcode image
            using (Bitmap barcodeImage = generator.GenerateBarCodeImage())
            {
                // Save the image to a memory stream (PNG format)
                using (var ms = new MemoryStream())
                {
                    barcodeImage.Save(ms, ImageFormat.Png);
                    byte[] imageBytes = ms.ToArray();

                    // ----- Store the image bytes as a BLOB -----
                    // In a real environment you would insert the byte[] into a SQL Server table.
                    // Example (requires System.Data.SqlClient and a valid connection string):
                    /*
                    using (var connection = new SqlConnection("Data Source=SERVER;Initial Catalog=DB;Integrated Security=True"))
                    {
                        connection.Open();
                        using (var command = new SqlCommand("INSERT INTO Barcodes (Id, ImageBlob) VALUES (@Id, @Blob)", connection))
                        {
                            command.Parameters.Add("@Id", SqlDbType.Int).Value = 1;
                            command.Parameters.Add("@Blob", SqlDbType.VarBinary, -1).Value = imageBytes;
                            command.ExecuteNonQuery();
                        }
                    }
                    */

                    // Since the runner environment may not have SQL Server libraries,
                    // write the BLOB to a local file as a practical substitute.
                    const string outputPath = "postal_barcode_blob.bin";
                    File.WriteAllBytes(outputPath, imageBytes);
                    Console.WriteLine($"Barcode image saved as BLOB to '{outputPath}'.");
                }
            }
        }
    }
}