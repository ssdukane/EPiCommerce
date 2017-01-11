﻿/*
EPi Commerce for EPiServer

All rights reserved. See LICENSE.txt in project root.

Copyright (C) 2013-2014 Oxx AS
Copyright (C) 2013-2014 BV Network AS

*/

using System.Collections.Generic;
using EPiServer.Shell.ObjectEditing;

namespace EPICommerce.Web.EditorDescriptors.SelectionFactories
{
    public class WineClosureSelectionFactory : GenericSelectionFactory
    {
        public override IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            return GetSelectionFromArray("Cork", "Screw Cap", "Synthetic", "Glass");
        }
    }
}
