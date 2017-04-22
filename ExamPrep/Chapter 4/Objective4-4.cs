using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ExamPrep.Chapter4
{
    public class SerializationTesting
    {
        public static byte[] SerializeToXML()
        {
            using (MemoryStream m = new MemoryStream())
            {
                using (StreamWriter writer = new StreamWriter(m))
                {
                    GetSomeDTOSerializer().Serialize(writer, GetObject(10));

                    return m.ToArray();
                }
            }
        }

        public static SomeDTO DeserializeFromXML(byte[] xmlBytes)
        {
            using (MemoryStream m = new MemoryStream(xmlBytes))
            {
                using (StreamReader reader = new StreamReader(m))
                {
                    XmlSerializer serializer = GetSomeDTOSerializer();

                    return (SomeDTO)serializer.Deserialize(reader);
                }
            }
        }

        public static byte[] SerializeToBinary()
        {
            using (MemoryStream m = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();

                formatter.Serialize(m, GetObject(20));

                return m.ToArray();
            }
        }

        public static SomeDTO DeserializeFromBinary(byte[] bytes)
        {
            using (MemoryStream m = new MemoryStream(bytes))
            {
                IFormatter formatter = new BinaryFormatter();

                return  (SomeDTO)formatter.Deserialize(m);                                
            }
        }

        private static XmlSerializer GetSomeDTOSerializer()
        {
            return new XmlSerializer(typeof(SomeDTO));
        }

        private static SomeDTO GetObject(int id)
        {
            SomeDTO dto = new SomeDTO
            {
                Id = id,
                Description = "A bunch of stuff",
                SomethingNotSerialized = 15,
                ChildDTOs = new List<SomeOtherDTO>()
                {
                    new SomeOtherDTO {
                        Id = 110,
                        OtherDescription = "Other Stuff"
                    },
                    new SomeOtherDTO {
                        Id = 210,
                        OtherDescription = "Otherer Stuff"
                    }
                }
            };

            dto.SetPrivateVariable();

            return dto;
        }
    }

    [Serializable]
    public class SomeDTO : ISerializable
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public List<SomeOtherDTO> ChildDTOs { get; set; }


        [NonSerialized, XmlIgnore]
        public int SomethingNotSerialized;

        private int importantNumber;

        public SomeDTO() { }

        protected SomeDTO(SerializationInfo info, StreamingContext context)
        {
            Id = info.GetInt32("Id");
            Description = info.GetString("Description");
            ChildDTOs = (List<SomeOtherDTO>)info.GetValue("Children", typeof(List<SomeOtherDTO>));
            importantNumber = info.GetInt32("ImportantNumber");
        }

        public void SetPrivateVariable()
        {
            importantNumber = 77;
        }

        public int GetPrivateVariable()
        {
            return importantNumber;
        }

        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Id", Id);
            info.AddValue("Description", Description);
            info.AddValue("Children", ChildDTOs);
            info.AddValue("ImportantNumber", importantNumber);
        }
    }

    [Serializable]
    public class SomeOtherDTO
    {
        public int Id { get; set; }

        public string OtherDescription { get; set; }
    }
}