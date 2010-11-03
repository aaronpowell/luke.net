using System.Collections.ObjectModel;
using Luke.Net.Features.Documents.Services;
using Luke.Net.Models;
using Microsoft.Practices.Prism.ViewModel;

namespace Luke.Net.Features.Documents
{
    public class BrowseByTermViewModel:NotificationObject
    {
        private readonly IDocumentService _documentService;

        public BrowseByTermViewModel(IDocumentService documentService)
        {
            _documentService = documentService;
            LoadModel();
        }

        private void LoadModel()
        {
            Fields = new ObservableCollection<string>(_documentService.GetFields());
            RaisePropertyChanged(() => Fields);
        }

        public ObservableCollection<string> Fields { get; private set; }
    }
}
