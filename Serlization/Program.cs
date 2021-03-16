using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Serlization
{
    [Serializable]
    public class Person {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class DataSerilization{

        public void JsonSerialize(object data, string filePath) {

            JsonSerializer jsonSerializer = new JsonSerializer();

            if (File.Exists(filePath)) {
                File.Delete(filePath);
            }

            StreamWriter sw = new StreamWriter(filePath);

            JsonWriter jsonWriter = new JsonTextWriter(sw);

            jsonSerializer.Serialize(jsonWriter, data);

            jsonWriter.Close();

            sw.Close();
        }

        public object JsonDeserialize(Type dataype,string filePath) {

            JObject obj = null;

            JsonSerializer jsonSerializer = new JsonSerializer();

            if (File.Exists(filePath)){
                StreamReader sr = new StreamReader(filePath);

                JsonReader jsonReader = new JsonTextReader(sr);

                obj = jsonSerializer.Deserialize(jsonReader) as JObject;

                jsonReader.Close();

                sr.Close();
            }

            return obj.ToObject(dataype);
        }

        public void XmlSerialize(Type dataType, object data, string filePath) {

            XmlSerializer xmlSerializer = new XmlSerializer(dataType);

            if (File.Exists(filePath)) {
                File.Delete(filePath);

                TextWriter writer = new StreamWriter(filePath);

                xmlSerializer.Serialize(writer, data);

                writer.Close();
            }
        }

        public object XmlDeserialize(Type dataType,string filePath) {

            object obj = null;

            XmlSerializer xmlSerializer = new XmlSerializer(dataType);

            if (File.Exists(filePath)) {
                TextReader textReader = new StreamReader(filePath);
                obj = xmlSerializer.Deserialize(textReader);
                textReader.Close();
            }

            return obj;
        }

        public void BinarySerialize(object data, string filePath) {

            FileStream fileStream;

            BinaryFormatter bf = new BinaryFormatter();

            if (File.Exists(filePath)) {
                File.Delete(filePath);
            }

            fileStream = File.Create(filePath);

            bf.Serialize(fileStream, data);

            fileStream.Close();
        }

        public object BinaryDeserialize(string filePath) {

            object obj = null;

            FileStream fileStream;

            BinaryFormatter bf = new BinaryFormatter();

            if (File.Exists(filePath)) {
                fileStream = File.OpenRead(filePath);

                obj = bf.Deserialize(fileStream);

                fileStream.Close();
            }

            return obj;
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            Person person = new Person() { FirstName = "Varun", LastName = "Tomar" };

            string filePath = "data.save";

            DataSerilization dataSerializer = new DataSerilization();

            Person p = null;

            //dataSerializer.BinarySerialize(person, filePath);

            //p = dataSerializer.BinaryDeserialize(filePath) as Person;

            //dataSerializer.XmlSerialize(typeof(Person),person, filePath);

            //p = dataSerializer.XmlDeserialize(typeof(Person) ,filePath) as Person;

            dataSerializer.JsonSerialize(person, filePath);

            p = dataSerializer.JsonDeserialize(typeof(Person), filePath) as Person;

            Console.WriteLine(p.FirstName + " " + p.LastName);

            Console.ReadLine();
        }
    }
}
