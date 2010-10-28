using System.IO;
using System.Windows.Input;
using Lucene.Net.Store;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using Luke.Net.Infrastructure;
using Directory = Lucene.Net.Store.Directory;

namespace Luke.Net.Features.OpenIndex
{
    public class OpenIndexModel : NotificationObject
    {
        private readonly IEventAggregator _eventAggregator;

        public OpenIndexModel(IEventAggregator eventAggregator)
        {
            LoadIndexExecuted = new RelayCommand(OnLoadIndexExecuted, CanLoadIndex);

            // ToDo: to read the last open direct from user storage
            Path = @"..\..\..\..\LuceneIndex";
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

        public ICommand LoadIndexExecuted { get; private set; }
    }
}
