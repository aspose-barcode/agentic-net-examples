using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Path to a sample barcode image file (acts as a stand‑in for a BLOB retrieved from a DB)
        const string barcodeImagePath = "barcode.png";

        // Verify the file exists before attempting to read it
        if (!File.Exists(barcodeImagePath))
        {
            Console.WriteLine($"File not found: {barcodeImagePath}");
            return;
        }

        // Expected code text that should be encoded in the barcode.
        // In a real scenario this value would be stored alongside the BLOB in the database.
        const string expectedCodeText = "12345";

        // Load the image into a memory stream (simulating a BLOB retrieved from a DB)
        using (FileStream fileStream = new FileStream(barcodeImagePath, FileMode.Open, FileAccess.Read))
        using (MemoryStream ms = new MemoryStream())
        {
            fileStream.CopyTo(ms);
            ms.Position = 0; // Reset stream position for reading

            // Create a BarCodeReader that will attempt to decode any supported symbology
            using (BarCodeReader reader = new BarCodeReader(ms, DecodeType.AllSupportedTypes))
            {
                // Perform the recognition
                BarCodeResult[] results = reader.ReadBarCodes();

                if (results.Length == 0)
                {
                    Console.WriteLine("No barcode detected.");
                    return;
                }

                // Iterate through all detected barcodes
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"Detected Type   : {result.CodeTypeName}");
                    Console.WriteLine($"Decoded Text    : {result.CodeText}");
                    Console.WriteLine($"Confidence      : {result.Confidence}");
                    Console.WriteLine($"Reading Quality : {result.ReadingQuality}");
                    Console.WriteLine();

                    // Verify data integrity by comparing with the expected value
                    if (string.Equals(result.CodeText, expectedCodeText, StringComparison.Ordinal))
                    {
                        Console.WriteLine("Data integrity check: PASS");
                    }
                    else
                    {
                        Console.WriteLine("Data integrity check: FAIL");
                    }
                }
            }
        }

        // -------------------------------------------------------------------------
        // Real database retrieval (commented out – requires appropriate DB provider)
        // -------------------------------------------------------------------------
        // using (var connection = new SqlConnection("Data Source=.;Initial Catalog=Barcodes;Integrated Security=True"))
        // {
        //     connection.Open();
        //     using (var command = new SqlCommand("SELECT BarcodeImage FROM BarcodeTable WHERE Id = @Id", connection))
        //     {
        //         command.Parameters.AddWithValue("@Id", 1);
        //         using (var reader = command.ExecuteReader())
        //         {
        //             if (reader.Read())
        //             {
        //                 byte[] blob = (byte[])reader["BarcodeImage"];
        //                 using (var ms = new MemoryStream(blob))
        //                 {
        //                     // Decode as shown above
        //                 }
        //             }
        //         }
        //     }
        // }
    }
}