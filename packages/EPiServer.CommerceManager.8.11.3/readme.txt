EPiServer.CommerceManager
- Not part of public API

IMPORTANT: Sites installed with Deployment center
=================================================

This information is only valid the first time the EPiServer.CommerceManager package are added on a Commerce Manager site, which was created using Deployment Center.

During installation of this nuget package, any existing Apps directory have been overwritten with new files.

If you have the installed Commerce Manager with Deployment Center, you will need to go to your IIS and remove the virtual directory
for the "Apps" folder so that it from now on will use the Apps folder in the root of the site.

Any modification made to the content of the virtual directory will have to be remade in the new location.

TROUBLESHOOTING
===============

If you get issues running the site after upgrade, delete all files in the bin folder (always keep a backup
just in case) and rebuild the project to clean out old files that might be incompatible.

For more details, see the product specific information for Commerce on
http://world.episerver.com/installupdates

Update search index folder
==========================

If you are upgrading an existing Commerce Manager site, this step can be skipped. 
This package installation sets the search index folder to App_Data\Search\ECApplication of current project folder.
It is required for this site (Commerce Manager) and front-end site to have same search index folder.
To update the search index folder, in Configs\Mediachase.Search.Config, and set the basePath attribute of Indexers
section and the storage attribute of LuceneSearchProvider section to the path you want to use.

