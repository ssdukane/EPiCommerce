/*
EPi Commerce for EPiServer

All rights reserved. See LICENSE.txt in project root.

Copyright (C) 2013-2014 Oxx AS
Copyright (C) 2013-2014 BV Network AS

*/

using EPICommerce.Core.Objects;
using EPICommerce.Web.Models.PageTypes;

namespace EPICommerce.Web.Models.ViewModels
{
    public class ForgotPasswordViewModel : PageViewModel<LoginPage>
    {
        public ForgotPasswordViewModel()
        {

        }

        public ForgotPasswordViewModel(LoginPage currentPage)
            : base(currentPage)
        {
        }

        public ForgotPasswordForm ForgotPasswordForm { get; set; }

    }
}
