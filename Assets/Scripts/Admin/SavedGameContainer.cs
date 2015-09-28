using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

[XmlRoot("SavedGameContainer")]
public class SavedGameContainer  {

   
    
    public SaveTemplate savedTemplate;

    public void Save(string path)
    {
        var serializer = new XmlSerializer(typeof(SavedGameContainer));
        using (var stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, this);
        }
    }

    public static SavedGameContainer Load(string path)
    {
        var serializer = new XmlSerializer(typeof(SavedGameContainer));
        using (var stream = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(stream) as SavedGameContainer;
        }
    }
}
