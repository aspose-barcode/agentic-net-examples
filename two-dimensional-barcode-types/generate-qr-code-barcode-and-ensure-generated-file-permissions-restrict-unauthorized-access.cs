using System;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR code image and restricting its file permissions to the current user.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR code, saves it to a file, and applies restrictive ACLs.
    /// </summary>
    static void Main()
    {
        const string outputPath = "qr.png";

        try
        {
            // -------------------------------------------------
            // Generate QR code and save it to a PNG file
            // -------------------------------------------------
            using (var generator = new BarcodeGenerator(EncodeTypes.QR))
            {
                // Text to be encoded in the QR code
                generator.CodeText = "https://example.com";

                // Optional settings: error correction level and image resolution
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;
                generator.Parameters.Resolution = 300f;

                // Write the QR code image to the specified path
                generator.Save(outputPath);
            }

            // -------------------------------------------------
            // Restrict file permissions so only the current user can read the file
            // -------------------------------------------------
            var fileInfo = new FileInfo(outputPath);
            var security = fileInfo.GetAccessControl();

            // Disable inheritance and remove any existing access rules
            security.SetAccessRuleProtection(isProtected: true, preserveInheritance: false);

            // Retrieve the SID of the current Windows user
            var currentUser = WindowsIdentity.GetCurrent().User;
            if (currentUser != null)
            {
                // Grant read permission to the current user
                var rule = new FileSystemAccessRule(
                    currentUser,
                    FileSystemRights.Read,
                    AccessControlType.Allow);
                security.AddAccessRule(rule);
            }

            // Apply the modified ACL to the file
            fileInfo.SetAccessControl(security);

            Console.WriteLine($"QR code generated and saved to '{outputPath}'. File permissions restricted to the current user.");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during generation or permission setting
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}