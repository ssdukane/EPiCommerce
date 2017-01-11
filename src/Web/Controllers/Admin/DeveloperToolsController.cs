﻿/*
Commerce Starter Kit for EPiServer

All rights reserved. See LICENSE.txt in project root.

Copyright (C) 2013-2014 Oxx AS
Copyright (C) 2013-2014 BV Network AS

*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Framework.Blobs;
using EPiServer.ServiceLocation;
using EPICommerce.Web.Models.Files;

namespace EPICommerce.Web.Controllers.Admin
{
    [System.Web.Mvc.Authorize(Roles = "CmsAdmins")]
    public class DeveloperToolsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MetaClass()
        {
            return View("MetaClass/Index");
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult DeleteBlobById()
        {
            Stream req = Request.InputStream;
            req.Seek(0, System.IO.SeekOrigin.Begin);
            string data = new StreamReader(req).ReadToEnd();

            StringBuilder sb = new StringBuilder();
            BlobFactory blobFactory = ServiceLocator.Current.GetInstance<BlobFactory>();

            string[] idList = data.Split(new []{'\n'}, StringSplitOptions.RemoveEmptyEntries);
            foreach (string uri in idList)
            {
                Uri blobUri = new Uri(uri);
                sb.AppendFormat("Deleting: {0}", uri);
                DeleteBlob(blobUri, sb, blobFactory);
            }

            ContentResult result = new ContentResult();
            result.Content = sb.ToString();
            return result;
        }

        public ActionResult DeleteBlobs()
        {
            IContentRepository repo = ServiceLocator.Current.GetInstance<IContentRepository>();
            BlobFactory blobFactory = ServiceLocator.Current.GetInstance<BlobFactory>();
            var assetHelper = ServiceLocator.Current.GetInstance<ContentAssetHelper>();
            StringBuilder sb = new StringBuilder();

            IEnumerable<ContentReference> contentReferences = null;
            
            contentReferences = repo.GetDescendents(EPiServer.Core.ContentReference.GlobalBlockFolder);
            DeleteBlobs(contentReferences, repo, sb, blobFactory);
            DeleteContentInAssetFolders(contentReferences, assetHelper, repo, sb, blobFactory);

            contentReferences = repo.GetDescendents(EPiServer.Core.ContentReference.SiteBlockFolder);
            DeleteBlobs(contentReferences, repo, sb, blobFactory);
            DeleteContentInAssetFolders(contentReferences, assetHelper, repo, sb, blobFactory);

            // Private page folders too
            contentReferences = repo.GetDescendents(EPiServer.Core.ContentReference.StartPage);
            DeleteContentInAssetFolders(contentReferences, assetHelper, repo, sb, blobFactory);

            ContentResult result = new ContentResult();
            result.Content = sb.ToString();
            return result;
        }

        private static void DeleteContentInAssetFolders(IEnumerable<ContentReference> contentReferences, ContentAssetHelper assetHelper,
            IContentRepository repo, StringBuilder sb, BlobFactory blobFactory)
        {
            foreach (ContentReference reference in contentReferences)
            {
                ContentAssetFolder folder = assetHelper.GetAssetFolder(reference);
                if (folder != null && folder.ContentLink != null)
                {
                    var folderContents = repo.GetDescendents(folder.ContentLink);
                    DeleteBlobs(folderContents, repo, sb, blobFactory);
                }
            }
        }

        private static void DeleteBlobs(IEnumerable<ContentReference> contentReferences, IContentRepository repo, StringBuilder sb,
            BlobFactory blobFactory)
        {
            foreach (ContentReference reference in contentReferences)
            {
                ImageFile file = null;
                try
                {
                    file = repo.Get<ImageFile>(reference);
                }
                catch
                {
                }
                if (file != null)
                {

                    IContentVersionRepository versionRepo = ServiceLocator.Current.GetInstance<IContentVersionRepository>();
                    IEnumerable<ContentVersion> versions = versionRepo.List(file.ContentLink);
                    foreach (ContentVersion version in versions)
                    {
                        var versionOfFile = repo.Get<ImageFile>(version.ContentLink);
                        if (versionOfFile != null)
                        {
                            DeleteBlobInstances(sb, blobFactory, versionOfFile);
                        }
                    }

                    sb.AppendFormat("{0}<br>", file.Name);

                    // Delete old versions
                    DeleteOldVersions(file, sb);
                }
            }
        }

        private static void DeleteBlobInstances(StringBuilder sb, BlobFactory blobFactory, ImageFile file)
        {
            //DeleteBlob(file.LargeThumbnail, sb, blobFactory);
            //DeleteBlob(file.ListImage, sb, blobFactory);
            //DeleteBlob(file.RelatedProduct, sb, blobFactory);
            //DeleteBlob(file.SimilarProduct, sb, blobFactory);
            //DeleteBlob(file.SliderImage, sb, blobFactory);
            //DeleteBlob(file.box1130, sb, blobFactory);
            //DeleteBlob(file.box370, sb, blobFactory);
            //DeleteBlob(file.box560, sb, blobFactory);
            ////DeleteBlob(file.box750, sb, blobFactory);
            //DeleteBlob(file.width110, sb, blobFactory);
            ////DeleteBlob(file.width1130, sb, blobFactory);
            //DeleteBlob(file.width179, sb, blobFactory);
            //DeleteBlob(file.width279, sb, blobFactory);
            //DeleteBlob(file.width320, sb, blobFactory);
            //DeleteBlob(file.width370, sb, blobFactory);
            //DeleteBlob(file.width379, sb, blobFactory);
            //DeleteBlob(file.width560, sb, blobFactory);
            //DeleteBlob(file.width580, sb, blobFactory);
            ////DeleteBlob(file.width750, sb, blobFactory);
        }

        private static void DeleteOldVersions(ImageFile file, StringBuilder sb)
        {
            IContentVersionRepository versionRepo = ServiceLocator.Current.GetInstance<IContentVersionRepository>();
            IEnumerable<ContentVersion> versions = versionRepo.List(file.ContentLink);
            foreach (ContentVersion version in versions)
            {
                if (version.Status != VersionStatus.Published)
                {
                    sb.AppendFormat("Deleting version: {0}", version.ContentLink);

                    versionRepo.Delete(version.ContentLink);

                }
            }
            
        }

        private static void DeleteBlob(Blob blob, StringBuilder sb, BlobFactory blobFactory)
        {
            // Deleting 
            if (blob != null)
            {
                DeleteBlob(blob.ID, sb, blobFactory);
            }
        }

        private static void DeleteBlob(Uri blobId, StringBuilder sb, BlobFactory blobFactory)
        {
            sb.AppendFormat("Deleting: {0}<br>", blobId);
            blobFactory.Delete(blobId);
        }
    }
}
