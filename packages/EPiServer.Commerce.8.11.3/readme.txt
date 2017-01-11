EPiServer.Commerce


Installation
============

During installation of this nuget package, any existing files and directory have been overwritten with new ones.

Update search index folder
==========================

If you are upgrading an existing Commerce site, this step can be skipped.
This package installation sets the search index folder to App_Data\Search\ECApplication of current project folder.
It is required for this site and Commerce Manager site to have same search index folder.
To update the search index folder, in Configs\Mediachase.Search.Config, set the basePath attribute of Indexers
section and the storage attribute of LuceneSearchProvider section to the path you want to use.


