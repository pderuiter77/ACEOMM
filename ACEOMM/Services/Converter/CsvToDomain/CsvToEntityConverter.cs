using ACEOMM.Domain.Model;
using ACEOMM.Domain.Model.Businesses;
using System;
using System.Windows.Media;

namespace ACEOMM.Services.Converter.CsvToDomain
{
    public class CsvToEntityConverter<T>
        where T : Entity, new()
    {
        protected string FieldValue(string[] fields, int field)
        {
            if (field == -1)
                return string.Empty;

            if (fields.Length <= field)
                return string.Empty;

            return fields[field];
        }

        protected int ConvertInt(string[] fields, int field)
        {
            var text = FieldValue(fields, field);
            int value;
            if (int.TryParse(text, out value))
                return value;

            return 0;
        }

        protected Color ConvertColor(string[] fields, int field)
        {
            var text = FieldValue(fields, field);
            return text != string.Empty
                ? (Color)ColorConverter.ConvertFromString(text)
                : Colors.White;
        }

        protected Logo ConvertLogo(string[] fields, int field)
        {
            return new Logo
            {
                RemoteUrl = FieldValue(fields, field)
            };
        }

        protected Guid ConvertGuid(string[] fields, int field)
        {
            if (field == -1)
                return Guid.NewGuid();

            var value = FieldValue(fields, field);            
            return Guid.Parse(value);
        }

        protected FranchiseType ConvertFranchiseType(string[] fields, int field)
        {
            var enumText = FieldValue(fields, field);
            FranchiseType parsedType;

            if (Enum.TryParse(enumText, out parsedType))
                return parsedType;

            return FranchiseType.Unknown;
        }

        protected virtual void Fill(T entity, string[] fields)
        {
            entity.Id = ConvertGuid(fields, IdField);
            entity.Name = FieldValue(fields, NameField);
            entity.Notes = FieldValue(fields, NotesField);
            entity.Author = FieldValue(fields, AuthorField);
            entity.Description = FieldValue(fields, DescriptionField);
            entity.Version = FieldValue(fields, VersionField);
        }

        public T Convert(string[] fields)
        {
            var entity = new T();
            Fill(entity, fields);
            return entity;
        }

        public static int IdField = -1;
        public static int NameField = 0;
        public static int NotesField = 5;
        public static int AuthorField = -1;
        public static int DescriptionField = 2;
        public static int VersionField = -1;
    }
}
