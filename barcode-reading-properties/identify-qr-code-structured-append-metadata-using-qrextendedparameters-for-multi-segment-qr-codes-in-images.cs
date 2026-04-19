using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main(string[] args)
    {
        // Determine image path: use first argument or default sample file.
        string imagePath = args.Length > 0 ? args[0] : "sample_qr.png";

        // Verify that the file exists.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Create a BarCodeReader for QR codes.
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            // Read all barcodes in the image.
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"BarCode Type: {result.CodeTypeName}");
                Console.WriteLine($"BarCode CodeText: {result.CodeText}");

                // Access QR extended parameters.
                var qrExt = result.Extended?.QR;
                if (qrExt == null || qrExt.IsEmpty)
                {
                    Console.WriteLine("No QR Structured Append metadata found.");
                    continue;
                }

                // Output Structured Append information.
                Console.WriteLine($"QR Structured Append Quantity (QRStructuredAppendModeBarCodesQuantity): {qrExt.QRStructuredAppendModeBarCodesQuantity}");
                Console.WriteLine($"QR Structured Append Index (QRStructuredAppendModeBarCodeIndex): {qrExt.QRStructuredAppendModeBarCodeIndex}");
                Console.WriteLine($"QR Structured Append Parity Data (QRStructuredAppendModeParityData): {qrExt.QRStructuredAppendModeParityData}");

                // Also display the generic Structured Append properties (they map to the same values).
                Console.WriteLine($"Structured Append Quantity (StructuredAppendModeBarCodesQuantity): {qrExt.StructuredAppendModeBarCodesQuantity}");
                Console.WriteLine($"Structured Append Index (StructuredAppendModeBarCodeIndex): {qrExt.StructuredAppendModeBarCodeIndex}");
                Console.WriteLine($"Structured Append Parity Data (StructuredAppendModeParityData): {qrExt.StructuredAppendModeParityData}");
                Console.WriteLine();
            }
        }
    }
}