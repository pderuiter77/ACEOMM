using ACEOMM.Domain.Model.Businesses;

namespace ACEOMM.Services.Converter.CsvToDomain
{
    public class CsvToProductConverter : CsvToEntityConverter<Product>
    {
        protected override void Fill(Product entity, string[] fields)
        {
            base.Fill(entity, fields);
            entity.Color = ConvertColor(fields, ColorField);
            entity.Logo = ConvertLogo(fields, LogoField);
            entity.Price = ConvertInt(fields, PriceField);
            entity.Type = ConvertFranchiseType(fields, TypeField);
        }

        public static int ColorField = 1;
        public static int LogoField = 4;
        public static int PriceField = 3;
        public static int TypeField = 6;
    }
}
