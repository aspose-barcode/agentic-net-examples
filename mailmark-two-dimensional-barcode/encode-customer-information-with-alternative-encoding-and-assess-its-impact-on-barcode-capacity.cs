// Title: Encode Customer Information with UTF-8 and Compare QR Code Capacity
// Description: Demonstrates generating QR codes using the default Unicode (UTF-16) encoding and an alternative UTF-8 encoding, then compares their byte counts to evaluate the impact on barcode data capacity.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on QR code creation and text encoding manipulation. It showcases the BarcodeGenerator class, EncodeTypes enumeration, and SetCodeText method—common tools for developers who need to optimize barcode size, support international characters, or assess encoding effects on data capacity.
// Prompt: Encode customer information with an alternative encoding and assess its impact on barcode capacity.
// Tags: qr, encoding, capacity, aspose.barcode, generation

using System;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates QR codes from a sample customer string using default Unicode encoding and UTF-8 encoding,
/// then compares the byte counts to illustrate how encoding choice affects barcode capacity.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates two QR codes with different text encodings and prints capacity analysis.
    /// </summary>
    static void Main()
    {
        // Sample customer information to encode
        string customerInfo = "John Doe 12345";

        // File paths for the generated barcode images
        string defaultPath = "qr_default.png";
        string utf8Path = "qr_utf8.png";

        // Generate QR code using the default encoding (Unicode/UTF-16 internal representation)
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            generator.CodeText = customerInfo; // Assign text with default encoding
            generator.Save(defaultPath);       // Save image to file
        }

        // Generate QR code using an alternative UTF-8 encoding via SetCodeText
        using (var generatorUtf8 = new BarcodeGenerator(EncodeTypes.QR))
        {
            generatorUtf8.SetCodeText(customerInfo, Encoding.UTF8); // Explicit UTF-8 encoding
            generatorUtf8.Save(utf8Path);                         // Save image to file
        }

        // Assess the impact on barcode capacity by comparing byte counts of each encoding
        int defaultByteCount = Encoding.Unicode.GetByteCount(customerInfo); // UTF-16 byte count
        int utf8ByteCount = Encoding.UTF8.GetByteCount(customerInfo);      // UTF-8 byte count

        // Output the original data and byte count comparison
        Console.WriteLine("Customer Information: " + customerInfo);
        Console.WriteLine("Default (UTF-16) byte count: " + defaultByteCount);
        Console.WriteLine("Alternative (UTF-8) byte count: " + utf8ByteCount);
        Console.WriteLine("Impact on capacity: " + (utf8ByteCount < defaultByteCount
            ? "UTF-8 uses fewer bytes, allowing more data in the same QR version."
            : "UTF-8 uses equal or more bytes, potentially reducing capacity."));

        // Inform the user where the barcode images have been saved
        Console.WriteLine("QR code with default encoding saved to: " + defaultPath);
        Console.WriteLine("QR code with UTF-8 encoding saved to: " + utf8Path);
    }
}