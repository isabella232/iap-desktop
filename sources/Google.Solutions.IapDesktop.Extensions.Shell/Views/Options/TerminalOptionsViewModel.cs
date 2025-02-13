﻿//
// Copyright 2020 Google LLC
//
// Licensed to the Apache Software Foundation (ASF) under one
// or more contributor license agreements.  See the NOTICE file
// distributed with this work for additional information
// regarding copyright ownership.  The ASF licenses this file
// to you under the Apache License, Version 2.0 (the
// "License"); you may not use this file except in compliance
// with the License.  You may obtain a copy of the License at
// 
//   http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing,
// software distributed under the License is distributed on an
// "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
// KIND, either express or implied.  See the License for the
// specific language governing permissions and limitations
// under the License.
//

using Google.Solutions.IapDesktop.Application.ObjectModel;
using Google.Solutions.IapDesktop.Application.Views.Options;
using Google.Solutions.IapDesktop.Extensions.Shell.Services.Settings;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Google.Solutions.IapDesktop.Extensions.Shell.Views.Options
{
    [Service(typeof(IOptionsDialogPane), ServiceLifetime.Transient, ServiceVisibility.Global)]
    [ServiceCategory(typeof(IOptionsDialogPane))]
    public class TerminalOptionsViewModel : ViewModelBase, IOptionsDialogPane
    {
        private bool isCopyPasteUsingCtrlCAndCtrlVEnabled;
        private bool isSelectAllUsingCtrlAEnabled;
        private bool isCopyPasteUsingShiftInsertAndCtrlInsertEnabled;
        private bool isSelectUsingShiftArrrowEnabled;
        private bool isQuoteConvertionOnPasteEnabled;
        private bool isNavigationUsingControlArrrowEnabled;

        private readonly TerminalSettingsRepository settingsRepository;

        private bool isDirty;

        public TerminalOptionsViewModel(
            TerminalSettingsRepository settingsRepository)
        {
            this.settingsRepository = settingsRepository;

            //
            // Read current settings.
            //
            // NB. Do not hold on to the settings object because other tabs
            // might apply changes to other application settings.
            //
            var settings = this.settingsRepository.GetSettings();

            this.IsCopyPasteUsingCtrlCAndCtrlVEnabled =
                settings.IsCopyPasteUsingCtrlCAndCtrlVEnabled.BoolValue;
            this.IsCopyPasteUsingShiftInsertAndCtrlInsertEnabled =
                settings.IsCopyPasteUsingShiftInsertAndCtrlInsertEnabled.BoolValue;
            this.IsSelectAllUsingCtrlAEnabled =
                settings.IsSelectAllUsingCtrlAEnabled.BoolValue;
            this.IsSelectUsingShiftArrrowEnabled =
                settings.IsSelectUsingShiftArrrowEnabled.BoolValue;
            this.IsQuoteConvertionOnPasteEnabled =
                settings.IsQuoteConvertionOnPasteEnabled.BoolValue;
            this.IsNavigationUsingControlArrrowEnabled =
                settings.IsNavigationUsingControlArrrowEnabled.BoolValue;

            this.isDirty = false;
        }


        public TerminalOptionsViewModel(IServiceProvider serviceProvider)
            : this(
                  serviceProvider.GetService<TerminalSettingsRepository>())
        {
        }

        //---------------------------------------------------------------------
        // IOptionsDialogPane.
        //---------------------------------------------------------------------

        public string Title => "Terminal";

        public UserControl CreateControl() => new TerminalOptionsControl(this);

        public bool IsDirty
        {
            get => this.isDirty;
            set
            {
                this.isDirty = value;
                RaisePropertyChange();
            }
        }

        public void ApplyChanges()
        {
            Debug.Assert(this.IsDirty);

            //
            // Save settings.
            //
            var settings = this.settingsRepository.GetSettings();

            settings.IsCopyPasteUsingCtrlCAndCtrlVEnabled.BoolValue = 
                this.IsCopyPasteUsingCtrlCAndCtrlVEnabled;
            settings.IsCopyPasteUsingShiftInsertAndCtrlInsertEnabled.BoolValue = 
                this.IsCopyPasteUsingShiftInsertAndCtrlInsertEnabled;
            settings.IsSelectAllUsingCtrlAEnabled.BoolValue = 
                this.IsSelectAllUsingCtrlAEnabled;
            settings.IsSelectUsingShiftArrrowEnabled.BoolValue = 
                this.IsSelectUsingShiftArrrowEnabled;
            settings.IsQuoteConvertionOnPasteEnabled.BoolValue =
                this.IsQuoteConvertionOnPasteEnabled;
            settings.IsNavigationUsingControlArrrowEnabled.BoolValue =
                this.IsNavigationUsingControlArrrowEnabled;

            this.settingsRepository.SetSettings(settings);

            this.IsDirty = false;
        }

        //---------------------------------------------------------------------
        // Observable properties.
        //---------------------------------------------------------------------

        public bool IsCopyPasteUsingCtrlCAndCtrlVEnabled
        {
            get => this.isCopyPasteUsingCtrlCAndCtrlVEnabled;
            set
            {
                this.IsDirty = true;
                this.isCopyPasteUsingCtrlCAndCtrlVEnabled = value;
                RaisePropertyChange();
            }
        }

        public bool IsCopyPasteUsingShiftInsertAndCtrlInsertEnabled
        {
            get => this.isCopyPasteUsingShiftInsertAndCtrlInsertEnabled;
            set
            {
                this.IsDirty = true;
                this.isCopyPasteUsingShiftInsertAndCtrlInsertEnabled = value;
                RaisePropertyChange();
            }
        }

        public bool IsSelectAllUsingCtrlAEnabled
        {
            get => this.isSelectAllUsingCtrlAEnabled;
            set
            {
                this.IsDirty = true;
                this.isSelectAllUsingCtrlAEnabled = value;
                RaisePropertyChange();
            }
        }

        public bool IsSelectUsingShiftArrrowEnabled
        {
            get => this.isSelectUsingShiftArrrowEnabled;
            set
            {
                this.IsDirty = true;
                this.isSelectUsingShiftArrrowEnabled = value;
                RaisePropertyChange();
            }
        }

        public bool IsQuoteConvertionOnPasteEnabled
        {
            get => this.isQuoteConvertionOnPasteEnabled;
            set
            {
                this.IsDirty = true;
                this.isQuoteConvertionOnPasteEnabled = value;
                RaisePropertyChange();
            }
        }

        public bool IsNavigationUsingControlArrrowEnabled
        {
            get => this.isNavigationUsingControlArrrowEnabled;
            set
            {
                this.IsDirty = true;
                this.isNavigationUsingControlArrrowEnabled = value;
                RaisePropertyChange();
            }
        }
    }
}
