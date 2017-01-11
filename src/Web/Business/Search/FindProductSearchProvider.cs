﻿using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using EPiServer;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Core;
using EPiServer.Find;
using EPiServer.Find.Framework;
using EPiServer.Framework.Localization;
using EPiServer.Globalization;
using EPiServer.Logging;
using EPiServer.ServiceLocation;
using EPiServer.Shell;
using EPiServer.Shell.Search;
using EPiServer.Shell.Web.Mvc.Html;
using Mediachase.Commerce.Catalog;
using EPICommerce.Web.Models.FindModels;

namespace EPICommerce.Web.Business.Search
{
    /// <summary>
    /// A search provider for the Catalog and CMS Edit UI, using Find to search
    /// for products.
    /// </summary>
    /// <remarks>
    /// This could also be implemented as a Commerce Search Provider, but that also
    /// needs to handle indexing, which we're not looking for here.
    /// </remarks>
    [SearchProvider]
    public class FindProductUiSearchProvider : ISearchProvider
    {
        private readonly ICatalogSystem _catalogSystem;
        protected ILogger _log = LogManager.GetLogger();
        private LocalizationService _localizationService;
        private ICatalogSystem _catalogContext;
        private ReferenceConverter _referenceConverter;
        private IContentLoader _contentLoader;

        public FindProductUiSearchProvider(LocalizationService localizationService, ICatalogSystem catalogSystem)
            : this(localizationService, 
                   catalogSystem, 
                   ServiceLocator.Current.GetInstance<ReferenceConverter>(), 
                   ServiceLocator.Current.GetInstance<IContentLoader>())
        {
        }

        public FindProductUiSearchProvider(LocalizationService localizationService, ICatalogSystem catalogSystem, ReferenceConverter referenceConverter, IContentLoader contentLoader)
        {
            _catalogSystem = catalogSystem;
            _localizationService = localizationService;
            _catalogContext = catalogSystem;
            _referenceConverter = referenceConverter;
            _contentLoader = contentLoader;
        }


        /// <summary>
        /// Executes a catalog search using EPiServer Find. This search works
        /// from the Catalog UI, the global search and 
        /// </summary>
        /// <remarks>
        /// This implementation is not generic, since it knows all about
        /// the catalog model, and how the products are indexed. Because
        /// of this, we can create a good product search for editors.
        /// </remarks>
        /// <param name="query">The query to execute</param>
        /// <returns>
        /// A list of search results. If the search fails with an exception, it will
        /// be logged, and the user will be notified that no content could be found.
        /// </returns>
        public virtual IEnumerable<SearchResult> Search(Query query)
        {
            query.MaxResults = 20;
            string keyword = query.SearchQuery;
            _log.Debug("Searching for: {0} using MaxResults: {1}", keyword, query.MaxResults);

            CultureInfo currentCulture = ContentLanguage.PreferredCulture;
            
            ITypeSearch<FindProduct> search = SearchClient.Instance.Search<FindProduct>();
            search = search.For(keyword)
                // Search with stemming on name and code
                .InField(p => p.DisplayName)
                .InField(p => p.Code)
                .InAllField() // Also search in _all field (without stemming)
                .Filter(p => p.Language.Match(currentCulture.Name))
                .Take(query.MaxResults);

            SearchResults<FindProduct> results = search.GetResult();

            _log.Debug("Find Search: {0} hits", results.TotalMatching);

            List<SearchResult> entryResults = new List<SearchResult>();
            foreach (FindProduct product in results)
            {

                // Get Content
                ContentReference contentLink = _referenceConverter.GetContentLink(product.Id, CatalogContentType.CatalogEntry, 0);
                EntryContentBase content = _contentLoader.Get<EntryContentBase>(contentLink);
                if(content != null)
                {
                    string url = GetEditUrl(content.ContentLink);
                    string preview = ""; // TextIndexer.StripHtml(product.Description.ToString(), 50);

                    _log.Debug("Found: {0} - {1}", product.DisplayName, url);
                    SearchResult result = new SearchResult(url, string.Format("{0} ({1})", product.DisplayName, content.Code), preview);
                    result.IconCssClass = "epi-resourceIcon epi-resourceIcon-page";
                    result.Language = content.Language.Name;
                    result.ToolTipElements.Add(new ToolTipElement() { Label = "Category", Value = product.CategoryName });
                    result.ToolTipElements.Add(new ToolTipElement() { Label = "Main Category", Value = product.MainCategoryName });
                    if (product.Color != null)
                    {
                        foreach (string color in product.Color)
                        {
                            result.ToolTipElements.Add(new ToolTipElement() { Label = "Color", Value = color });
                        }
                    }
                    if (product.Variants != null)
                    {
                        result.ToolTipElements.Add(new ToolTipElement() { Label = "# of Variations", Value = product.Variants.Count().ToString() });
                    }

                    result.Metadata.Add("parentId", content.ParentLink.ToString());
                    result.Metadata.Add("parentType", CatalogContentType.CatalogNode.ToString());
                    result.Metadata.Add("id", contentLink.ToString());
                    result.Metadata.Add("code", content.Code);
                    result.Metadata.Add("languageBranch", content.Language.Name);

                    entryResults.Add(result);
                }
                else
                {
                    _log.Debug("Cannot load: {0}", contentLink);
                }
            }
            return entryResults;
        }

        /// <summary>
        /// Gets the edit URL for the content which should work from
        /// the different searches
        /// </summary>
        /// <remarks>
        /// This implementation has knowledge of the UI internals,
        /// which should really have been solved by an API call.
        /// </remarks>
        /// <param name="contentLink">The content link.</param>
        /// <returns>A url that the UI can use to load the content</returns>
        protected string GetEditUrl(ContentReference contentLink)
        {
            string str = Paths.ToResource("Commerce", "Catalog");
            if (str == "/")
            {
                str = Paths.ToResource("CMS", null);
            }
            return string.Format("{0}#context=epi.cms.contentdata:///{1}", str, contentLink);
        }

        public string Area
        {
            get { return "Commerce"; }
        }

        public string Category
        {
            get { return "Catalog Products"; }
        }
    }
}