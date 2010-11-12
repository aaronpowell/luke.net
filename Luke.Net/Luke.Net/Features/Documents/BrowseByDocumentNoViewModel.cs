using Luke.Net.Features.Documents.Services;
using Luke.Net.Infrastructure;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;

namespace Luke.Net.Features.Documents
{
    public class BrowseByDocumentNoViewModel : NotificationObject
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IDocumentService _documentService;

        public BrowseByDocumentNoViewModel(IEventAggregator eventAggregator, IDocumentService documentService)
        {
            _eventAggregator = eventAggregator;
            _documentService = documentService;
            Count = _documentService.GetNumberOfDocuments();
            BrowseDocument = new RelayCommand<int>(ExecuteBrowseDocument);
        }

        private void ExecuteBrowseDocument(int documentNo)
        {
            var document = _documentService.GetDocumentInfo(documentNo);
            _eventAggregator.GetEvent<BrowseToDocumentEvent>().Publish(document);
        }

        private int _count;

        public int Count
        {
            get { return _count; }
            private set
            {
                _count = value;
                RaisePropertyChanged(()=>Count);
            }
        }

        public RelayCommand<int> BrowseDocument { get; private set; }
    }
}
