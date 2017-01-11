﻿/*
EPi Commerce for EPiServer

All rights reserved. See LICENSE.txt in project root.

Copyright (C) 2013-2014 Oxx AS
Copyright (C) 2013-2014 BV Network AS

*/

using System;
using System.Data;
using System.Web.UI.WebControls;
using Mediachase.Commerce.Orders.Dto;
using Mediachase.Web.Console.Interfaces;

namespace EPICommerce.Core.PaymentProviders.DIBS
{
    public partial class ConfigurePayment : System.Web.UI.UserControl, IGatewayControl
    {
        // Fields
        private PaymentMethodDto _paymentMethodDto;
        private string _validationGroup;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurePayment"/> class.
        /// </summary>
        public ConfigurePayment()
        {
            this._validationGroup = string.Empty;
            this._paymentMethodDto = null;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.BindData();
        }

        /// <summary>
        /// Binds the data.
        /// </summary>
        public void BindData()
        {
            if ((this._paymentMethodDto != null) && (this._paymentMethodDto.PaymentMethodParameter != null))
            {                
                SetTextBoxValue(DIBSPaymentGateway.UserParameter, this.User);                

                SetTextBoxValue(DIBSPaymentGateway.ProcessingUrl,ProcessingUrl);

                SetTextBoxValue(DIBSPaymentGateway.KeyParameter,Key);
            }
            else
            {
                this.Visible = false;
            }
        }

        private void SetTextBoxValue(string keyname, TextBox ctrl)
        {
            PaymentMethodDto.PaymentMethodParameterRow parameterByName;
            parameterByName = this.GetParameterByName(keyname);
            if (parameterByName != null)
            {
                ctrl.Text = parameterByName.Value;
            }
        }

        #region IGatewayControl Members

        /// <summary>
        /// Loads the object.
        /// </summary>
        /// <param name="dto">The dto.</param>
        public void LoadObject(object dto)
        {
            this._paymentMethodDto = dto as PaymentMethodDto;
        }

        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// <param name="dto">The dto.</param>
        public void SaveChanges(object dto)
        {
            if (this.Visible)
            {
                this._paymentMethodDto = dto as PaymentMethodDto;
                if ((this._paymentMethodDto != null) && (this._paymentMethodDto.PaymentMethodParameter != null))
                {
                    Guid paymentMethodId = Guid.Empty;
                    if (this._paymentMethodDto.PaymentMethod.Count > 0)
                    {
                        paymentMethodId = this._paymentMethodDto.PaymentMethod[0].PaymentMethodId;
                    }                    
                 
                    SaveParameter(DIBSPaymentGateway.UserParameter, User, paymentMethodId);

                    SaveParameter(DIBSPaymentGateway.ProcessingUrl, ProcessingUrl, paymentMethodId);

                    SaveParameter(DIBSPaymentGateway.KeyParameter, Key, paymentMethodId);                 
                }
            }

        }

        private void SaveParameter(string key, TextBox ctrl, Guid paymentMethodId)
        {
            PaymentMethodDto.PaymentMethodParameterRow parameterByName;
            parameterByName = this.GetParameterByName(key);
            if (parameterByName != null)
            {
                parameterByName.Value = ctrl.Text;
            }
            else
            {
                this.CreateParameter(this._paymentMethodDto, key, ctrl.Text, paymentMethodId);
            }
        }

        private PaymentMethodDto.PaymentMethodParameterRow GetParameterByName(string name)
        {
            PaymentMethodDto.PaymentMethodParameterRow[] rowArray = (PaymentMethodDto.PaymentMethodParameterRow[])this._paymentMethodDto.PaymentMethodParameter.Select(string.Format("Parameter = '{0}'", name));
            if ((rowArray != null) && (rowArray.Length > 0))
            {
                return rowArray[0];
            }
            return null;
        }

        private void CreateParameter(PaymentMethodDto dto, string name, string value, Guid paymentMethodId)
        {
            PaymentMethodDto.PaymentMethodParameterRow row = dto.PaymentMethodParameter.NewPaymentMethodParameterRow();
            row.PaymentMethodId = paymentMethodId;
            row.Parameter = name;
            row.Value = value;
            if (row.RowState == DataRowState.Detached)
            {
                dto.PaymentMethodParameter.Rows.Add(row);
            }
        }

        /// <summary>
        /// Gets or sets the validation group.
        /// </summary>
        /// <value>The validation group.</value>
        public string ValidationGroup
        {
            get
            {
                return _validationGroup;
            }
            set
            {
                _validationGroup = value;
            }
        }

        #endregion
    }
}
