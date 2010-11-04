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
        private readonly IEventAggregator _eventAggregator;
        private readonly IDocumentService _documentService;
        private readonly ObservableCollection<string> _fields;
        private IEnumerable<DocumentInfo> _foundDocuments;

        public BrowseByTermViewModel(IEventAggregator eventAggregator, IDocumentService documentService)
        {
            _eventAggregator = eventAggregator;
            _documentService = documentService;

            _eventAggregator.GetEvent<InspectDocumentsForTermEvent>().Subscribe(InspectDocuments);

            _fields = new ObservableCollection<string>(_documentService.GetFields());
            Fields = new CollectionView(_fields);
            RaisePropertyChanged(() => Fields);
        }

        private void InspectDocuments(TermToInspect termToInspect)
        {
            _foundDocuments = _documentService.SearchDocumentsFor(termToInspect).OrderBy(d => d.DocumentNumber);

            ResultCount = _foundDocuments.Count();
            IndexOfDocumentToBrowse = 0;
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

        private int _indexOfDocumentToBrowse;
        public int IndexOfDocumentToBrowse
        {
            get { return _indexOfDocumentToBrowse; }
            set
            {
                // Do nothing on an out of index value
                if(value >= ResultCount)
                    return;

                _indexOfDocumentToBrowse = value;
                RaisePropertyChanged(()=>IndexOfDocumentToBrowse);

                _eventAggregator.GetEvent<BrowseToDocumentEvent>().Publish(_foundDocuments.Skip(value).Take(1).Single());
            }
        }
    }
}
