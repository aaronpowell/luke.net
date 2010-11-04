using System;
using System.Collections.ObjectModel;
using Luke.Net.Features.Documents.Services;
using Luke.Net.Models;
using Luke.Net.Models.Events;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;

namespace Luke.Net.Features.Documents
{
    public class BrowseByTermViewModel:NotificationObject
    {
        private readonly IDocumentService _documentService;
        private readonly IEventAggregator _eventAggregator;

        public BrowseByTermViewModel(IEventAggregator eventAggregator, IDocumentService documentService)
        {
            _documentService = documentService;

            eventAggregator.GetEvent<InspectDocumentsForTermEvent>().Subscribe(InspectDocument);

            LoadModel();
        }

        private void InspectDocument(TermInfo obj)
        {
            
        }

        private void LoadModel()
        {
            Fields = new ObservableCollection<string>(_documentService.GetFields());
            RaisePropertyChanged(() => Fields);
        }

        public ObservableCollection<string> Fields { get; private set; }
    }
}
