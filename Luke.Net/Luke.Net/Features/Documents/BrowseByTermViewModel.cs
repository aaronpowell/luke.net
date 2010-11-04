using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Data;
using Luke.Net.Features.Documents.Services;
using Luke.Net.Models;
using Luke.Net.Models.Events;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using System.Linq;

namespace Luke.Net.Features.Documents
{
    public class BrowseByTermViewModel:NotificationObject
    {
        private readonly IDocumentService _documentService;
        private readonly ObservableCollection<string> _fields;
        private IEnumerable<DocumentInfo> _foundDocuments;

        public BrowseByTermViewModel(IEventAggregator eventAggregator, IDocumentService documentService)
        {
            _documentService = documentService;

            eventAggregator.GetEvent<InspectDocumentsForTermEvent>().Subscribe(InspectDocuments);

            _fields = new ObservableCollection<string>(_documentService.GetFields());
            Fields = new CollectionView(_fields);
            RaisePropertyChanged(() => Fields);
        }

        private void InspectDocuments(TermToInspect termToInspect)
        {
            _foundDocuments = _documentService.SearchDocumentsFor(termToInspect);

            ResultCount = _foundDocuments.Count();
            Fields.MoveCurrentTo(_fields.Single(f => string.Equals(f, termToInspect.FieldName)));
            TermToInspect = termToInspect.TermName;
        }

        private string _termToInspect;
        public string TermToInspect
        {
            get { return _termToInspect; }
            set
            {
                _termToInspect = value;
                RaisePropertyChanged(()=> TermToInspect);
            }
        }

        public CollectionView Fields { get; private set; }

        private int _resultCount;
        public int ResultCount
        {
            get { return _resultCount; }
            private set
            {
                _resultCount = value;
                RaisePropertyChanged(()=> ResultCount);
            }
        }
    }
}
