using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ExamPrep.Chapter4
{
    class InfoManager
    {
        public static DriveInfo[] GetDriveInformation()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();

            return drives;
        }

        public static void BuildTestDirectory()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo("C:\\ExamDirectoryTesting");

            // Clear Folder if Left Over
            if (directoryInfo.Exists)
            {
                directoryInfo.Delete(true);
            }

            // Create Folder
            directoryInfo.Create();

            //Add Security
            DirectorySecurity sec = directoryInfo.GetAccessControl();
            sec.AddAccessRule(new FileSystemAccessRule("everyone", FileSystemRights.ReadAndExecute, AccessControlType.Allow));

            directoryInfo.SetAccessControl(sec);
        }

        public static void BuildTestFile()
        {
            FileInfo fileInfo = new FileInfo("C:\\ExamTesting\\TestFile.txt");

            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            }
                        
            using (var writer = fileInfo.AppendText())
            {
                writer.Write("Here's some text. The Current Time is: {0}", DateTime.Now);
            }
        }
    }

    class TestAsync
    {
        public static async Task DoSomethingAsync()
        {
            // Get Big file to write
            Random gen = new Random();
            byte[] bigFileBytes = new byte[100000000];
            gen.NextBytes(bigFileBytes);

            // Write big file
            using (FileStream file = File.OpenWrite(Path.Combine("E:/TestFileIO", "TestFile.tmp")))
            {
                await file.WriteAsync(bigFileBytes, 0, bigFileBytes.Length);
                Console.WriteLine("Wrote File");
            }
        }
    }
}