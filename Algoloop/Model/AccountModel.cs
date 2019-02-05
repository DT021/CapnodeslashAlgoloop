﻿/*
 * Copyright 2018 Capnode AB
 * 
 * Licensed under the Apache License, Version 2.0 (the "License"); 
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Algoloop.Model
{
    [Serializable]
    [DataContract]
    public class AccountModel : ModelBase
    {
        private AccountType _provider;

        public enum AccountType { Paper, Fxcm };
        public enum AccessType { Demo, Real };

        [DisplayName("Account name")]
        [Description("Name of the account.")]
        [Browsable(true)]
        [ReadOnly(false)]
        [DataMember]
        public string Name { get; set; } = "Account";

        [Browsable(false)]
        [ReadOnly(false)]
        [DataMember]
        public bool Active { get; set; }

        [Category("Account")]
        [DisplayName("Account provider")]
        [Description("Name of the broker.")]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(true)]
        [ReadOnly(false)]
        [DataMember]
        public AccountType Provider
        {
            get => _provider;
            set
            {
                _provider = value;
                Refresh();
            }
        }

        [Category("Account")]
        [DisplayName("Access type")]
        [Description("Type of login account at the broker.")]
        [Browsable(false)]
        [ReadOnly(false)]
        [DataMember]
        public AccessType Access { get; set; }

        [Category("Account")]
        [DisplayName("Login")]
        [Description("User login.")]
        [Browsable(false)]
        [ReadOnly(false)]
        [DataMember]
        public string Login { get; set; } = string.Empty;

        [Category("Account")]
        [DisplayName("Password")]
        [Description("User login password.")]
        [PasswordPropertyText(true)]
        [Browsable(false)]
        [ReadOnly(false)]
        [DataMember]
        public string Password { get; set; } = string.Empty;

        [Category("Account")]
        [DisplayName("Account number")]
        [Description("Account number.")]
        [Browsable(false)]
        [ReadOnly(false)]
        [DataMember]
        public string Id { get; set; } = string.Empty;

        [Browsable(false)]
        [ReadOnly(false)]
        [DataMember]
        public string DataFolder { get; set; }

        [Browsable(false)]
        [ReadOnly(false)]
        [DataMember]
        public List<OrderModel> Orders { get; } = new List<OrderModel>();

        public void Refresh()
        {
            switch (Provider)
            {
                case AccountType.Fxcm:
                    SetBrowsable("Access", true);
                    SetBrowsable("Login", true);
                    SetBrowsable("Password", true);
                    SetBrowsable("Id", true);
                    break;
                default:
                    SetBrowsable("Access", false);
                    SetBrowsable("Login", false);
                    SetBrowsable("Password", false);
                    SetBrowsable("Id", false);
                    break;
            }
        }
    }
}