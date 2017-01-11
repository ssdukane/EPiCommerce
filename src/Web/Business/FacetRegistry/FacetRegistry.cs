using System.Collections.Generic;

namespace EPICommerce.Web.Business.FacetRegistry
{
    public interface IFacetRegistry
    {
        List<FacetDefinition> FacetDefinitions { get; set; }
    }

    public class FacetRegistry : IFacetRegistry
    {
        public FacetRegistry()
        {
            FacetDefinitions = new List<FacetDefinition>();
        }

        public List<FacetDefinition> FacetDefinitions { get; set; }
        
    }
}