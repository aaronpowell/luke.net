﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using Luke.Net.Features.Overview.Services;
using Luke.Net.Infrastructure;
using Luke.Net.Models;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;

namespace Luke.Net.Features.Overview
{
    public class FieldsViewModel : NotificationObject
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IIndexOverviewService _indexOverviewService;
        private readonly List<FieldInfo> _fields = new List<FieldInfo>();

        public FieldsViewModel(IEventAggregator eventAggregator, IIndexOverviewService indexOverviewService)
        {
            _eventAggregator = eventAggregator;
            _indexOverviewService = indexOverviewService;
            InspectFields = new RelayCommand<IEnumerable<FieldInfo>>(InspectFieldsExecuted);
            Fields = new ListCollectionView(_fields);

            LoadModel();
        }

        private void LoadModel()
        {
            _fields.Clear();
            _fields.AddRange(_indexOverviewService.GetFields());
            Fields.Refresh();
        }

        public ICommand InspectFields { get; set; }

        void InspectFieldsExecuted(IEnumerable<FieldInfo> fields)
        {
        }

        public ICollectionView Fields { get; private set; }
    }
}
