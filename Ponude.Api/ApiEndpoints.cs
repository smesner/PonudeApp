namespace Ponude.Api
{
    public static class ApiEndpoints
    {
        private const string ApiBase = "api";

        public static class Ponude
        {
            public const string Base = $"{ApiBase}/ponude";

            public const string Create = Base;
            public const string Get = $"{Base}/{{idOrBrojPonude}}";
            public const string GetByPage = $"{Base}/page/{{pageNumber:int}}";
            public const string Update = $"{Base}/{{id:guid}}";
            public const string Delete = $"{Base}/{{id:guid}}";
        }
        
        public static class Artikli
        {
            public const string Base = $"{ApiBase}/artikli";

            public const string Create = Base;
            public const string Get = $"{Base}/{{id}}";
            public const string Search = $"{Base}/search/{{upit}}";
            public const string GetAll = Base;
            public const string Update = $"{Base}/{{id:guid}}";
            public const string Delete = $"{Base}/{{id:guid}}";
        }
    }
}
