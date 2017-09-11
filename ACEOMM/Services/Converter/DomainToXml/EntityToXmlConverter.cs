using ACEOMM.Domain.Model;
using System.Xml;

namespace ACEOMM.Services.Converter.DomainToXml
{
    public class EntityToXmlConverter<T>
        where T : Entity
    {
        public virtual void Convert(XmlElement node, XmlDocument document, T entity)
        {
            node.SetAttribute("Id", entity.Id.ToString());
            node.SetAttribute("Name", entity.Name);
            node.SetAttribute("Version", entity.Version);
            node.SetAttribute("Author", entity.Author);
            node.SetAttribute("Description", entity.Description);            
            node.SetAttribute("Notes", entity.Notes);
            node.SetAttribute("IsEditAllowed", entity.IsEditAllowed.ToString());
        }

        protected XmlNode ConvertLogo(XmlDocument document, Logo logo)
        {
            var result = document.CreateElement("Logo");
            var urlNode = document.CreateElement("Url");
            urlNode.InnerText = logo.RemoteUrl;
            var fileNode = document.CreateElement("File");
            fileNode.InnerText = logo.LocalFilename;
            result.AppendChild(urlNode);
            result.AppendChild(fileNode);
            return result;
        }
    }
}
