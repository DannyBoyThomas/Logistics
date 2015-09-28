using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

[System.Serializable]
public class SaveTemplate 
{
    [XmlAttribute("levelName")]
    public string levelName;
    public int gridSize;
    public List<ObjectTemplate> objects;
	
}
[System.Serializable]
public class ObjectTemplate
{
    public int rot, x, y;
    public string name;
}
