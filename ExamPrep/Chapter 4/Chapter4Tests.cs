using ExamPrep.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamPrep.Chapter4
{
    [ChapterTestClass(4)]
    public static class Chapter4Tests
    {
        private const int bytesInKB = 1024;
        private const int bytesInMB = 1024 * 1024;
        private const int bytesInGB = 1024 * 1024 * 1024;

        [ChapterTest(1, Description = "Drives")]
        public static void Objective_1_Drives()
        {
            foreach (var drive in InfoManager.GetDriveInformation())
            {
                Console.WriteLine();
                Console.WriteLine("Drive: {0}, Type: {1}", drive.Name, drive.DriveType);

                if (drive.IsReady)
                {
                    Console.WriteLine("  Space Available: {0}GBs of {1}GBs", drive.TotalFreeSpace / bytesInGB, drive.TotalSize / bytesInGB);
                    Console.WriteLine("  Volume Name: {0}, Format: {1}", drive.VolumeLabel, drive.DriveFormat);
                }
                else
                {
                    Console.WriteLine("  Drive is Not Ready");
                }
            }
        }

        [ChapterTest(1, Description = "DirectoryInfo")]
        public static void Objective_1_Directories()
        {
            Console.WriteLine("Creating Directory");
            InfoManager.BuildTestDirectory();
        }

        [ChapterTest(1, Description = "FileInfo")]
        public static void Objective_1_Files()
        {
            InfoManager.BuildTestFile();
        }

        [ChapterTest(1, Description = "Paths")]
        public static void Objective_1_Paths()
        {
            string folder = "C:\\ExamTesting";
            string file = "Testing.txt";

            string path = Path.Combine(folder, file);

            Console.WriteLine("Path: {0}", path);
            Console.WriteLine("Root: {0}", Path.GetPathRoot(path));
            Console.WriteLine("Directory: {0}", Path.GetDirectoryName(path));
            Console.WriteLine("File Name: {0}", Path.GetFileName(path));
            Console.WriteLine("Extension: {0}", Path.GetExtension(path));
            Console.WriteLine("Random File Name: {0}", Path.GetRandomFileName());
            Console.WriteLine("Temp File Name: {0}", Path.GetTempFileName());
        }

        [ChapterTest(1, Description = "Async Testing")]
        public static void Objective_1_Async()
        {
            Console.WriteLine("Writing File...");

            Task fileWrite = TestAsync.DoSomethingAsync();

            Console.WriteLine("Thread Returned");
        }

        [ChapterTest(2, Description = "XML Document Creation")]
        public static void Objective_2_XmlDocument()
        {
            Console.WriteLine(XmlTesting.CreateXMLDocument());
        }

        [ChapterTest(3, Description = "LINQ to XML")]
        public static void Objective_3_LINQXML()
        {
            Console.WriteLine("Ham types: {0}", LinqTester.LinqXml());
        }

        [ChapterTest(4, Description = "Binary Serialization")]
        public static void Objective_4_BinarySeraialization()
        {
            byte[] xml = SerializationTesting.SerializeToXML();

            Console.WriteLine("Size of XML Stream: {0} Bytes", xml.Length);

            SomeDTO dto = SerializationTesting.DeserializeFromXML(xml);

            Console.WriteLine("Some DTO Id: {0}, Something Not Serialized: {1}, ImportantNumber: {2}", dto.Id, dto.SomethingNotSerialized, dto.GetPrivateVariable());

            byte[] byteArray = SerializationTesting.SerializeToBinary();

            Console.WriteLine("Size of Binary Stream: {0} Bytes", byteArray.Length);

            SomeDTO binDTO = SerializationTesting.DeserializeFromBinary(byteArray);

            Console.WriteLine("Some DTO Id: {0}, Something Not Serialized: {1}, ImportantNumber: {2}", binDTO.Id, binDTO.SomethingNotSerialized, binDTO.GetPrivateVariable());
        }

        [ChapterTest(5, Description = "Stack and Queue Testing")]
        public static void Objective_4_Stacks_and_Queues()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            Console.WriteLine("Stack:");
            StackTesting.StackTest(numbers);

            Console.WriteLine();
            Console.WriteLine("Queue:");
            QueueTesting.QueueTest(numbers);
        }
    }
}