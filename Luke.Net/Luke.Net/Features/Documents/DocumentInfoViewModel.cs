using System;
using System.Collections.Generic;
using Luke.Net.Features.Documents.Services;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;

namespace Luke.Net.Features.Documents
{
    public class DocumentInfoViewModel : NotificationObject
    {
        private readonly IEventAggregator _eventAggregator;

        public DocumentInfoViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<BrowseToDocumentEvent>().Subscribe(UpdateModel);
        }

        private void UpdateModel(DocumentInfo document)
        {
            DocumentNo = document.DocumentNumber;
            Fields = document.Fields;
        }

        private int _documentNo;
        public int DocumentNo
        {
            get { return _documentNo; }
            set
            {
                _documentNo = value;
                RaisePropertyChanged(()=>DocumentNo);
            }
        }

        private IEnumerable<FieldByDocumentInfo> _fields;
        public IEnumerable<FieldByDocumentInfo> Fields
        {
            get { return _fields; }
            set
            {
                _fields = value;
                RaisePropertyChanged(()=>Fields);
            }
        }
    }
}
