using System.IO;
using System.Windows.Input;
using Lucene.Net.Store;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using Directory = Lucene.Net.Store.Directory;

namespace Luke.Net.Features.Popup
{
    public class ActiveIndexModel : NotificationObject
    {
        private readonly IEventAggregator _eventAggregator;

        // ToDo: this should go ASAP
        public ActiveIndexModel() : this(App.EventAggregator)
        {
            Path = @"..\..\..\..\LuceneIndex";
        }

        public ActiveIndexModel(IEventAggregator eventAggregator)
        {
            LoadIndexExecuted = new DelegateCommand(OnLoadIndexExecuted, CanLoadIndex);
            _eventAggregator = eventAggregator;
        }

        private bool CanLoadIndex()
        {
            // ToDo: Should check that the directory actually includes a lucene index
            return System.IO.Directory.Exists(Path);
        }

        private void OnLoadIndexExecuted()
        {
            _eventAggregator.GetEvent<IndexChangedEvent>().Publish(this);
        }

        private string _path;
        public string Path
        {
            get { return _path; }
            set
            {
                _path = value;
                // ToDo: I do not like this. Should find a way to avoid it
                LoadIndexExecuted.RaiseCanExecuteChanged(); 
                RaisePropertyChanged(() => Path);
            }
        }

        private bool _readOnly;
        public bool ReadOnly
        {
            get { return _readOnly; }
            set
            {
                _readOnly = value;
                RaisePropertyChanged(() => ReadOnly);
            }
        }

        public Directory Directory
        {
            get
            {
                if (string.IsNullOrEmpty(Path))
                    return null;

                return FSDirectory.Open(new DirectoryInfo(Path));
            }
        }

        public DelegateCommand LoadIndexExecuted { get; private set; }
    }
}
