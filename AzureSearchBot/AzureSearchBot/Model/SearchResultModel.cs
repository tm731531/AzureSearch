using Newtonsoft.Json;

namespace AzureSearchBot.Model
{
    //Data model for search
    //public class SearchResult
    //{
    //    [JsonProperty("@odata.context")]
    //    public string odatacontext { get; set; }
    //    public SearchFacets searchfacets { get; set; }
    //    public Value[] value { get; set; }
    //    public string odatanextLink { get; set; }
    //}

    //Data model for fetching facets
    //public class FacetResult
    //{
    //    [JsonProperty("@odata.context")]
    //    public string odatacontext { get; set; }
    //    [JsonProperty("@search.facets")]
    //    public SearchFacets searchfacets { get; set; }
    //    public Value[] value { get; set; }
    //}

    //public class Value
    //{
    //    [JsonProperty("@search.score")]
    //    public float searchscore { get; set; }
    //    public string imageURL { get; set; }
    //    public string Name { get; set; }
    //    public string Era { get; set; }
    //    public string Description { get; set; }
    //    public string id { get; set; }
    //    public string rid { get; set; }
    //}

    //public class SearchFacets
    //{
    //    [JsonProperty("Era@odata.type")]
    //    public string Eraodatatype { get; set; }
    //    public Era[] Era { get; set; }
    //}

    //public class Era
    //{
    //    public int count { get; set; }
    //    public string value { get; set; }
    //}




    //public class FacetResult
    //{
    //    public string odatacontext { get; set; }
    //    public Value[] value { get; set; }
    //    public string odatanextLink { get; set; }
    //}

    //public class Value
    //{
    //    public int searchscore { get; set; }
    //    public string ID { get; set; }
    //    public string Description { get; set; }
    //    public string Problem_Code { get; set; }
    //    public string FAQ_ID { get; set; }
    //    public string Product_Name { get; set; }
    //    public string Product_Version { get; set; }
    //    public string Product_OS { get; set; }
    //}



    //public class FacetResult
    //{
    //    public string odatacontext { get; set; }
    //    public SearchFacets searchfacets { get; set; }
    //    public Value[] value { get; set; }
    //    public string odatanextLink { get; set; }
    //}

    //public class SearchFacets
    //{
    //    public string Product_Nameodatatype { get; set; }
    //    public Product_Name[] Product_Name { get; set; }
    //}

    //public class Product_Name
    //{
    //    public int count { get; set; }
    //    public string value { get; set; }
    //}

    //public class Value
    //{
    //    public int searchscore { get; set; }
    //    public string ID { get; set; }
    //    public string Description { get; set; }
    //    public string Problem_Code { get; set; }
    //    public string FAQ_ID { get; set; }
    //    public string Product_Name { get; set; }
    //    public string Product_Version { get; set; }
    //    public string Product_OS { get; set; }
    //}


    public class SearchResult
    {
        [JsonProperty("@odata.context")]
        public string odatacontext { get; set; }
        [JsonProperty("@search.facets")]
        public SearchFacets searchfacets { get; set; }
        public Value[] value { get; set; }
        public string odatanextLink { get; set; }
    }

    public class SearchFacets
    {
        public string Product_Nameodatatype { get; set; }
        public Product_Name[] Product_Name { get; set; }
        public Product_Version[] Product_Version { get; set; }
       // public Product_OS[] Product_OS { get; set; }
        
    }
    public class Product_Name
    {
        public int count { get; set; }
        public string value { get; set; }
    }
    public class Product_Version
    {
        public int count { get; set; }
        public string value { get; set; }
    }
  
    public class Product_OS
    {
        public int count { get; set; }
        public string value { get; set; }
    }
    public class Value
    {
        [JsonProperty("@search.score")]
        public float searchscore { get; set; }
        public string ID { get; set; }
        public string Description { get; set; }
        public string Problem_Code { get; set; }
        public string FAQ_ID_NEW { get; set; }
        public string Product_Name { get; set; }
        public string Product_Version { get; set; }
        public string Product_OS { get; set; }
    }

}