using Core.Config;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

namespace Core.Helpers
{
    class XmlHelper
    {
        /// <summary>
        /// This method will read data of particular given node from xml file 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="descendantNode"></param>
        public void ReadXmlData(string filePath, string descendantNode)
        {
            XDocument doc = XDocument.Load(filePath);
            var nodes = doc.Descendants(descendantNode);
            foreach (var node in nodes)
            {
                Console.WriteLine(node.Value);
            }
        }


        /// <summary>
        /// This method will read data of particular given node from xml file,
        /// And store this value in Title property in Setting class,
        /// Uri should contain complete xml file path
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        //public static IEnumerable<Settings> StreamBooks(string uri, string node)
        //{
        //    using (XmlReader reader = XmlReader.Create(uri))
        //    {
        //        string nodeText = null;
        //        reader.MoveToContent();
        //        while (reader.Read())
        //        {
        //            if (reader.NodeType == XmlNodeType.Element
        //                && reader.Name == "Settings")
        //            {
        //                while (reader.Read())
        //                {
        //                    if (reader.NodeType == XmlNodeType.Element &&
        //                        reader.Name == node)
        //                    {
        //                        nodeText = reader.ReadString();
        //                        break;
        //                    }
        //                }
        //                yield return new Settings() { Title = nodeText };
        //            }
        //        }
        //    }

        //}
    }
}
