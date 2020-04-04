using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Lab8
{
    class Program
    {
        private static readonly string path = new Uri(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase)).LocalPath;

        static void Main(string[] args)
        {
            var rd = XmlReader.Create(path + "\\Computer.xml");
            var doc = XDocument.Load(rd);

            if (IsValidXmlBySchema(doc))
            {
                var computer = Deserialize<Computer>(doc);
                var computerJson = System.Text.Json.JsonSerializer.Serialize(computer);
                var deserialize = System.Text.Json.JsonSerializer.Deserialize<Computer>(computerJson);

                Console.WriteLine(computerJson);
                Console.WriteLine();
            }

            var xmlDoc = new XmlDocument();
            xmlDoc.Load(path + "\\Computer.xml");
            FindElement(xmlDoc);
            Console.WriteLine();


            Console.ReadKey();
        }

        private static void FindElement(XmlDocument doc)
        {
            var root = doc.DocumentElement;
            XmlNodeList nodes = root.SelectNodes("*");

            foreach (XmlNode node in nodes)
            {
                Console.WriteLine(node.OuterXml + " - " + node.Value);
            }

        }

        private static T Deserialize<T>(XDocument doc)
        {
            // Construct an instance of the XmlSerializer with the type
            // of object that is being deserialized.
            var mySerializer = new XmlSerializer(typeof(T));
            // Call the Deserialize method and cast to the object type.
            return (T)mySerializer.Deserialize(doc.CreateReader());
        }

        private static bool IsValidXmlBySchema(XDocument doc)
        {
            bool isValid = true;

            var schema = new XmlSchemaSet();
            schema.Add("http://tempuri.org/Computer.xsd", path + "\\Computer.xsd");

            doc.Validate(schema, ValidationEventHandler);

            return isValid;

            void ValidationEventHandler(object sender, ValidationEventArgs e)
            {
                XmlSeverityType type = XmlSeverityType.Warning;
                if (Enum.TryParse("Error", out type))
                {
                    if (type == XmlSeverityType.Error)
                    {
                        isValid = false;
                        //throw new Exception(e.Message);
                    }
                }
            }
        }
    }
}
