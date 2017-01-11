﻿/*
Commerce Starter Kit for EPiServer

All rights reserved. See LICENSE.txt in project root.

Copyright (C) 2013-2014 Oxx AS
Copyright (C) 2013-2014 BV Network AS

*/

using EPICommerce.Core.Objects;
using EPICommerce.Web.Models.PageTypes;

namespace EPICommerce.Web.Models.ViewModels
{
	public class LoginViewModel : PageViewModel<LoginPage>
	{
		public LoginViewModel()
		{

		}

		public LoginViewModel(LoginPage currentPage)
			: base(currentPage)
		{
		}

		public LoginForm LoginForm { get; set; }

		public string RegisterUserUrl { get; set; }
	}
}
