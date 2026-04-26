using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

namespace SwissQrCodeReader
{
    class Program
    {
        static void Main(string[] args)
        {
            // Path to the Swiss QR Code image (adjust as needed)
            string imagePath = "SwissQR.png";

            // Verify that the file exists before attempting to read
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"Error: File not found - {imagePath}");
                return;
            }

            try
            {
                // Initialize the barcode reader for all supported types
                using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
                {
                    // Read all barcodes from the image
                    BarCodeResult[] results = reader.ReadBarCodes();

                    if (results.Length == 0)
                    {
                        Console.WriteLine("No barcodes were detected in the image.");
                        return;
                    }

                    foreach (var result in results)
                    {
                        Console.WriteLine($"Detected barcode type: {result.CodeTypeName}");
                        Console.WriteLine($"Raw codetext: {result.CodeText}");

                        // Attempt to decode Swiss QR specific codetext
                        SwissQRCodetext decoded = ComplexCodetextReader.TryDecodeSwissQR(result.CodeText);

                        if (decoded != null)
                        {
                            // Output some key fields from the decoded Swiss QR bill
                            Console.WriteLine("Successfully decoded Swiss QR bill:");
                            Console.WriteLine($"  Account: {decoded.Bill.Account}");
                            Console.WriteLine($"  Amount: {decoded.Bill.Amount}");
                            Console.WriteLine($"  Currency: {decoded.Bill.Currency}");
                            Console.WriteLine($"  Creditor Name: {decoded.Bill.Creditor.Name}");
                            Console.WriteLine($"  Version: {decoded.Bill.Version}");
                        }
                        else
                        {
                            Console.WriteLine("The codetext could not be decoded as Swiss QR.");
                        }

                        Console.WriteLine(); // Blank line between results
                    }
                }
            }
            catch (Exception ex)
            {
                // Gracefully handle any unexpected exceptions during processing
                Console.WriteLine($"An error occurred while processing the image: {ex.Message}");
            }
        }
    }
}