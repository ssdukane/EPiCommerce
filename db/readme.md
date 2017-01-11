# Getting the databases
In order to run the site locally, you need to download the CMS and Commerce databases from:

[/EPICommece-Database](https://github.com/ssdukane/EPiCommerce/edit/master/db)

Extract the databases into this directory, and the site should work.

## Technical
The connectionstrings.config files in the CMS and Commerce sites points to the DataDirectory. During startup of the sites, the DataDirectory variable [is changed](/blob/master/src/web/global.asax.cs) to point here. 



