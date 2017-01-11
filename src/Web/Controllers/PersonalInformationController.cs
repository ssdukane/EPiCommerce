﻿/*
Commerce Starter Kit for EPiServer

All rights reserved. See LICENSE.txt in project root.

Copyright (C) 2013-2014 Oxx AS
Copyright (C) 2013-2014 BV Network AS

*/

using System.Web.Mvc;
using EPiServer.ServiceLocation;
using Mediachase.Commerce;
using EPICommerce.Core.Objects;
using EPICommerce.Core.Repositories;
using EPICommerce.Web.Business;
using EPICommerce.Web.Models.PageTypes;
using EPICommerce.Web.Models.ViewModels;

namespace EPICommerce.Web.Controllers
{
	public class PersonalInformationController : PageControllerBase<PersonalInformationPage>
	{
		[RequireSSL]
		public ActionResult Index(PersonalInformationPage currentPage)
		{
			PersonalInformationViewModel model = new PersonalInformationViewModel(currentPage);

			if (Request.IsAjaxRequest())
			{
				return PartialView(model);
			}

			return View(model);
		}

		[HttpPost]
		[RequireSSL]
		public ActionResult Index(PersonalInformationPage currentPage, PersonalSettingsForm personalSettingsForm)
        {
            PersonalInformationViewModel model = new PersonalInformationViewModel(currentPage);
			var currentMarket = ServiceLocator.Current.GetInstance<ICurrentMarket>();

            model.PersonalSettingsForm = personalSettingsForm;


            ContactRepository contactRepository = new ContactRepository();
            contactRepository.Save(model.PersonalSettingsForm.ContactInformation);

            CustomerAddressRepository addressRepository = new CustomerAddressRepository();
			
			personalSettingsForm.BillingAddress.CheckAndSetCountryCode();
			personalSettingsForm.BillingAddress.IsPreferredBillingAddress = true;
            addressRepository.Save(personalSettingsForm.BillingAddress);

			personalSettingsForm.ShippingAddress.CheckAndSetCountryCode();
			personalSettingsForm.ShippingAddress.IsPreferredShippingAddress = true;
            addressRepository.Save(personalSettingsForm.ShippingAddress);

            if (Request.IsAjaxRequest())
            {
                return PartialView(model);
            }


            //Save data
            return View(model);
        }
	}
}
