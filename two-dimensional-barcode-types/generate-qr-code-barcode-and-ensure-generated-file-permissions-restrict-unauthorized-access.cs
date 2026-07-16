// Title: Generate QR Code with restricted file permissions
// Description: This example creates a QR Code barcode image and then limits the file's access rights so only the current Windows user can read or modify it.
// Category-Description: Demonstrates how to use Aspose.BarCode to generate a barcode image and apply Windows file system security. The example covers barcode generation (BarcodeGenerator, EncodeTypes), image saving, and modifying file ACLs (FileSecurity, FileSystemAccessRule). Ideal for developers needing to protect generated barcode files from unauthorized access in desktop or server applications.
// Prompt: Generate QR Code barcode and ensure generated file permissions restrict unauthorized access.
// Tags: qr code, barcode generation, file security, windows, aspose.barcode, png

using System;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates QR Code generation and file permission restriction using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR Code image and restricts its file permissions to the current user.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated QR Code image.
        string outputPath = "qr_code.png";

        // Create a QR Code generator with the desired text (URL in this case).
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set a high error correction level to improve readability under adverse conditions.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the generated barcode image to the specified file.
            generator.Save(outputPath);
        }

        // Restrict file permissions so that only the current Windows user has full control.
        var fileInfo = new FileInfo(outputPath);
        var currentUser = WindowsIdentity.GetCurrent().User;

        if (currentUser != null)
        {
            // Create a new security descriptor without inheriting existing rules.
            var security = new FileSecurity();

            // Define a rule granting full control to the current user.
            var rule = new FileSystemAccessRule(
                currentUser,
                FileSystemRights.FullControl,
                AccessControlType.Allow);

            // Add the rule to the security descriptor.
            security.AddAccessRule(rule);

            // Apply the security settings to the file.
            fileInfo.SetAccessControl(security);
        }
        else
        {
            // If the current user cannot be determined, inform the user that permissions were not changed.
            Console.WriteLine("Unable to determine the current user. File permissions were not modified.");
        }

        // Inform the user that the QR Code has been generated and secured.
        Console.WriteLine($"QR Code generated and saved to '{outputPath}'. File permissions restricted to the current user.");
    }
}