using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;

namespace EnginCan.Core.Helpers.System
{
    public class ModelSummaryRead
    {
        public string _xmlPath { get; protected internal set; }
        public XmlDocument _document { get; protected internal set; }

        public ModelSummaryRead(string assemblyName)
        {
            _xmlPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), $@"{assemblyName}.xml"); ;
            if (File.Exists(_xmlPath))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(this._xmlPath);
                _document = doc;
            }
            else
            {
                throw new FileNotFoundException(string.Format("Could not find the XmlDocument at the specified path: {0}\r\nCurrent Path: {1}", _xmlPath, Assembly.GetExecutingAssembly().Location));
            }
        }

        /// <summary>
        /// Retrievethe XML comments documentation for a given resource
        /// Eg. ITN.Data.Models.Entity.TestObject.MethodName
        /// </summary>
        /// <returns></returns>
        public string GetCommentsForResource(string resourcePath, XmlResourceType type)
        {
            XmlNode node = _document.SelectSingleNode(string.Format("//member[starts-with(@name, '{0}:{1}')]/summary", GetObjectTypeChar(type), resourcePath));
            if (node != null)
            {
                string xmlResult = node.InnerText;
                string trimmedResult = Regex.Replace(xmlResult, @"\s+", " ");
                trimmedResult = trimmedResult.Trim();
                trimmedResult = trimmedResult.Replace("'", "");
                return trimmedResult;
            }
            return string.Empty;
        }

        /// <summary>
        /// Retrievethe XML comments documentation for a given resource
        /// Eg. ITN.Data.Models.Entity.TestObject.MethodName
        /// </summary>
        /// <returns></returns>
        public ObjectDocumentation[] GetCommentsForResource(Type objectType)
        {
            List<ObjectDocumentation> comments = new List<ObjectDocumentation>();
            string resourcePath = objectType.FullName;

            PropertyInfo[] properties = objectType.GetProperties();
            FieldInfo[] fields = objectType.GetFields();
            List<ObjectDocumentation> objectNames = new List<ObjectDocumentation>();
            objectNames.AddRange(properties.Select(x => new ObjectDocumentation() { PropertyName = x.Name, Type = XmlResourceType.Property }).ToList());
            objectNames.AddRange(properties.Select(x => new ObjectDocumentation() { PropertyName = x.Name, Type = XmlResourceType.Field }).ToList());

            foreach (var property in objectNames)
            {
                XmlNode node = _document.SelectSingleNode(string.Format("//member[starts-with(@name, '{0}:{1}.{2}')]/summary", GetObjectTypeChar(property.Type), resourcePath, property.PropertyName));
                if (node != null)
                {
                    string xmlResult = node.InnerText;
                    property.Documentation = Regex.Replace(xmlResult, @"\s+", " ");
                    comments.Add(property);
                }
            }
            return comments.ToArray();
        }

        /// <summary>
        /// Retrievethe XML comments documentation for a given resource
        /// </summary>
        /// <param name="objectType">The type of class to retrieve documenation on</param>
        /// <param name="propertyName">The name of the property in the specified class</param>
        /// <param name="resourceType"></param>
        /// <returns></returns>
        public string GetCommentsForResource(Type objectType, string propertyName, XmlResourceType resourceType)
        {
            List<ObjectDocumentation> comments = new List<ObjectDocumentation>();
            string resourcePath = objectType.FullName;

            string scopedElement = resourcePath;
            if (propertyName != null && resourceType != XmlResourceType.Type)
                scopedElement += "." + propertyName;
            XmlNode node = _document.SelectSingleNode(string.Format("//member[starts-with(@name, '{0}:{1}')]/summary", GetObjectTypeChar(resourceType), scopedElement));
            if (node != null)
            {
                string xmlResult = node.InnerText;
                return Regex.Replace(xmlResult, @"\s+", " ");
            }
            return string.Empty;
        }

        private static string GetObjectTypeChar(XmlResourceType type)
        {
            switch (type)
            {
                case XmlResourceType.Field:
                    return "F";

                case XmlResourceType.Method:
                    return "M";

                case XmlResourceType.Property:
                    return "P";

                case XmlResourceType.Type:
                    return "T";
            }
            return string.Empty;
        }
    }

    public class ObjectDocumentation
    {
        public string PropertyName { get; set; }
        public string Documentation { get; set; }
        public XmlResourceType Type { get; set; }
    }

    public enum XmlResourceType
    {
        Method,
        Property,
        Field,
        Type
    }
}