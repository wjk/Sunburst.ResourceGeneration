using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Linq;

namespace Sunburst.ResourceGeneration.ResourceModel
{
    public class ResourceMap
    {
        internal const string XML_NAMESPACE = "http://xml.sunsol.me/Resource-Generation/v1";

        public static ResourceMap ReadXml(XElement element)
        {
            const string ELEMENT_NAME = "Resources";

            if (element.Name.Namespace.NamespaceName != XML_NAMESPACE)
                throw new InvalidOperationException("Unrecognized XML namespace");
            if (element.Name.LocalName != ELEMENT_NAME)
                throw new InvalidOperationException($"Expected <{ELEMENT_NAME}> element");

            ResourceMap map = new ResourceMap();
            map.ClassName = element.Attribute(XName.Get("ClassName", XML_NAMESPACE))?.Value;
            map.Namespace = element.Attribute(XName.Get("Namespace", XML_NAMESPACE))?.Value;

            foreach (XElement child in element.Elements())
            {
                map.Resources.Add(Resource.ReadXml(child));
            }

            return map;
        }

        public string ClassName { get; set; } = null;
        public string Namespace { get; set; } = null;
        public List<Resource> Resources { get; } = new List<Resource>();
    }

    public class Resource
    {
        internal const string XML_NAMESPACE = "http://xml.sunsol.me/Resource-Generation/v1";

        public static Resource ReadXml(XElement element)
        {
            const string ELEMENT_NAME = "Resources";

            if (element.Name.Namespace.NamespaceName != XML_NAMESPACE)
                throw new InvalidOperationException("Unrecognized XML namespace");
            if (element.Name.LocalName != ELEMENT_NAME)
                throw new InvalidOperationException($"Expected <{ELEMENT_NAME}> element");

            Resource res = new Resource();
            res.Name = element.Attribute(XName.Get("Name", XML_NAMESPACE))?.Value;
            res.CustomType = element.Attribute(XName.Get("CustomType", XML_NAMESPACE))?.Value;

            foreach (XElement child in element.Elements())
            {
                res.Data.Add(ResourceData.ReadXml(child));
            }

            return res;
        }

        public string Name { get; set; } = null;
        public string CustomType { get; set; } = null;
        public List<ResourceData> Data { get; } = new List<ResourceData>();
    }

    public class ResourceData
    {
        internal const string XML_NAMESPACE = "http://xml.sunsol.me/Resource-Generation/v1";

        public static ResourceData ReadXml(XElement element)
        {
            if (element.Name.Namespace.NamespaceName != XML_NAMESPACE)
                throw new InvalidOperationException("Unrecognized XML namespace");
            if (element.Name.LocalName != "String" && element.Name.LocalName != "File")
                throw new InvalidOperationException($"Expected <String> or <File> element");

            ResourceData data = new ResourceData();
            data.Culture = CultureInfo.InvariantCulture;

            string cultureName = element.Attribute(XName.Get("Culture", XML_NAMESPACE))?.Value;
            if (cultureName != null)
            {
                data.Culture = new CultureInfo(cultureName);
            }

            if (element.Name.LocalName == "String")
            {
                data.Text = element.Attribute(XName.Get("Text", XML_NAMESPACE)).Value;
                data.FilePath = null;
            }
            else if (element.Name.LocalName == "File")
            {
                data.FilePath = element.Attribute(XName.Get("Path", XML_NAMESPACE)).Value;
                data.Text = null;
            }
            else
            {
                throw new InvalidOperationException("This can't happen: Neither <String> nor <File> element");
            }

            return data;
        }

        public CultureInfo Culture { get; set; } = null;
        public string Text { get; set; } = null;
        public string FilePath { get; set; } = null;
    }
}
