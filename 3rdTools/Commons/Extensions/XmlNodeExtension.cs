using System.Xml;

namespace Extensions;

public static class XmlNodeExtension
{
    public static bool ContainRoot(this XmlNode xmlNode, string root)
    {
        return xmlNode.SelectNodes(root).Count > 0;
    }
    public static string GetNodeValue(this XmlNode xmlNode, string root, string nodeName)
    {
        var value = string.IsNullOrEmpty(root) ? xmlNode.SelectSingleNode(nodeName)?.InnerText :
        xmlNode.ContainRoot(root) ?
        xmlNode.SelectNodes(root)[0].SelectSingleNode(nodeName)?.InnerText : null;
        return value;
    }

    public static string GetNodeAttributeValue(this XmlNode xmlNode, string root, string nodeName, string attributeName)
    {
        var value = string.IsNullOrEmpty(root) ? xmlNode.SelectSingleNode(nodeName)?.Attributes[attributeName]?.Value :
            xmlNode.ContainRoot(root) ? xmlNode.SelectNodes(root)[0].SelectSingleNode(nodeName)?.Attributes[attributeName]?.Value
            : null;
        return value;
    }

}

