using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Sample QR code text
        string qrText = "https://example.com";

        // Generate QR code and obtain image bytes
        byte[] barcodeBytes;
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Set the text to encode (UTF-8)
            generator.SetCodeText(qrText, Encoding.UTF8);

            // Optional: set error correction level
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Optional: set module size
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Save to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                barcodeBytes = ms.ToArray();
            }
        }

        // Store the barcode image bytes locally (simulating a BLOB column)
        string outputPath = Path.Combine(Environment.CurrentDirectory, "qr_blob.bin");
        File.WriteAllBytes(outputPath, barcodeBytes);
        Console.WriteLine($"QR code image saved to {outputPath}");

        // Also write the image to a file for visual verification
        string imagePath = Path.Combine(Environment.CurrentDirectory, "qr.png");
        File.WriteAllBytes(imagePath, barcodeBytes);
        Console.WriteLine($"QR code image written to {imagePath}");

        // ----------------------------------------------------------------------
        // Real SQL Server storage (requires System.Data.SqlClient and a valid DB):
        // ----------------------------------------------------------------------
        // using System.Data.SqlClient;
        // string connectionString = "Data Source=SERVER;Initial Catalog=Database;Integrated Security=True;";
        // string insertSql = "INSERT INTO Barcodes (CodeImage) VALUES (@Image)";
        // using (var connection = new SqlConnection(connectionString))
        // {
        //     connection.Open();
        //     using (var command = new SqlCommand(insertSql, connection))
        //     {
        //         command.Parameters.Add("@Image", System.Data.SqlDbType.VarBinary, barcodeBytes.Length).Value = barcodeBytes;
        //         command.ExecuteNonQuery();
        //     }
        // }
        // Console.WriteLine("QR code image stored in SQL Server database.");
    }
}