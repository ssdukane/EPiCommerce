EPiServer.Commerce.UI
- Not part of public API

IMPORTANT VERSION SPECIFIC NOTES
================================

New in 8.11.2
-------------
You are required to run the cmdlet Move-EPiServerProtectedModules provided by the EPiServer.Packaging nuget
package to be able to use this package. If you have already run that cmdlet you don't need to run it again.

MIGRATION STEPS
===============

This package contains some migration steps, that are required for the site to run properly. They will
fix certain issues with the content that cannot be done by sql scripts alone. Until all migration steps
are completed successfully, all requests to the site will be redirected to a migration page. From that
page you can start the migration and see information about the current progress. In order to access it
you need to be part of the "CommerceAdmins" role. When all steps have been completed, everything will
return to normal.

It is possible to configure the site to start the migration automatically during initialization.
That is done by adding an app setting called "AutoMigrateEPiServer" and setting the value to "true".
However, until all steps have been completed, any requests will still be redirected to the migration page.