using ACEOMM.Domain.Model;
using ACEOMM.Domain.Model.Businesses;
using System;
using System.Windows.Media;
using System.Xml;

namespace ACEOMM.Services.Converter.XmlToDomain
{
    public abstract class XmlToEntityConverter<T>
        where T : Entity, new()
    {
        protected string GetAttributeValue(XmlElement node, string attributeName)
        {
            if (!node.HasAttribute(attributeName))
                return string.Empty;
            return node.GetAttribute(attributeName);
        }

        protected string GetNodeValue(XmlElement node, string nodeName)
        {
            var childNode = node.SelectSingleNode(nodeName);
            if (childNode == null)
                return string.Empty;
            return childNode.InnerText;
        }

        protected Logo ConvertLogo(XmlElement node)
        {
            return new Logo
            {
                RemoteUrl = GetNodeValue(node, "Logo/Url"),
                LocalFilename = GetNodeValue(node, "Logo/File")
            };
        }

        protected FranchiseType ConvertFranchiseType(XmlElement node, string attributeName)
        {
            var enumText = GetAttributeValue(node, attributeName);
            FranchiseType parsedType;

            if (Enum.TryParse(enumText, out parsedType))
                return parsedType;

            return FranchiseType.Unknown;
        }

        protected bool ConvertBool(XmlElement node, string attributeName)
        {
            var text = GetAttributeValue(node, attributeName);
            bool value;
            if (bool.TryParse(text, out value))
                return value;

            return false;
        }

        protected Guid ConvertGuid(XmlElement node, string attributeName)
        {
            return Guid.Parse(GetAttributeValue(node, attributeName));
        }

        protected Color ConvertColor(XmlElement node, string attributeName)
        {
            var text = GetAttributeValue(node, attributeName);
            return text != string.Empty
                ? (Color)ColorConverter.ConvertFromString(text)
                : Colors.White;
        }

        protected int ConvertInt(XmlElement node, string attributeName)
        {
            var text = GetAttributeValue(node, attributeName);
            int value;
            if (int.TryParse(text, out value))
                return value;

            return 0;
        }

        protected virtual void Fill(T entity, XmlElement node)
        { 
            entity.Id = (ConvertGuid(node, "Id"));
            entity.Author = GetAttributeValue(node, "Author");
            entity.Name = GetAttributeValue(node, "Name");
            entity.Notes = GetAttributeValue(node, "Notes");
            entity.Version = GetAttributeValue(node, "Version");
            entity.Description = GetAttributeValue(node, "Description");
            entity.IsEditAllowed = ConvertBool(node, "IsEditAllowed");
            entity.Status = EntityStatus.Unchanged;
        }

        public T Convert(XmlElement node)
        { 
            var entity = new T();
            Fill(entity, node);
            entity.Status = EntityStatus.Unchanged;
            return entity;
        }
    }
}
