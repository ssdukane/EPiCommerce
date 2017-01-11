﻿/*
Commerce Starter Kit for EPiServer

All rights reserved. See LICENSE.txt in project root.

Copyright (C) 2013-2014 Oxx AS
Copyright (C) 2013-2014 BV Network AS

*/

using Mediachase.Commerce.Storage;

namespace EPICommerce.Core.Extensions
{
    public static class MetaStorageExtensions
    {
        public static string GetStringValue(this MetaStorageBase item, string fieldName)
        {
            return item.GetStringValue(fieldName, string.Empty);
        }

        public static int GetIntegerValue(this MetaStorageBase item, string fieldName)
        {
            return item.GetIntegerValue(fieldName, 0);
        }

        public static string GetStringValue(this MetaStorageBase item, string fieldName, string defaultValue)
        {
            return item[fieldName] != null ? item[fieldName].ToString() : defaultValue;
        }

        public static int GetIntegerValue(this MetaStorageBase item, string fieldName, int defaultValue)
        {
            if (item[fieldName] == null)
            {
                return defaultValue;
            }
            int retVal;
            return int.TryParse(item[fieldName].ToString(), out retVal) ? retVal : defaultValue;
        }

    }
}
