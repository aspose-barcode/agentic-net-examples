using System;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeExample
{
    class Program
    {
        static void Main()
        {
            // Output file path for the QR code image
            string outputPath = "qr_code.png";

            // Create a QR Code generator with sample text
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
            {
                // Set a high error correction level (Level H)
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

                // Save the generated QR code image
                generator.Save(outputPath);
            }

            // Restrict file permissions so only the current user can access the file
            try
            {
                // Obtain the SID of the current Windows user
                SecurityIdentifier currentUserSid = WindowsIdentity.GetCurrent()?.User;
                if (currentUserSid == null)
                {
                    throw new InvalidOperationException("Unable to retrieve the current user SID.");
                }

                // Get the existing security settings of the file
                FileInfo fileInfo = new FileInfo(outputPath);
                FileSecurity fileSecurity = fileInfo.GetAccessControl();

                // Disable inheritance and remove existing access rules
                fileSecurity.SetAccessRuleProtection(isProtected: true, preserveInheritance: false);

                // Grant full control to the current user
                FileSystemAccessRule allowRule = new FileSystemAccessRule(
                    currentUserSid,
                    FileSystemRights.FullControl,
                    AccessControlType.Allow);
                fileSecurity.AddAccessRule(allowRule);

                // Apply the updated security settings to the file
                fileInfo.SetAccessControl(fileSecurity);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error setting file permissions: {ex.Message}");
            }
        }
    }
}