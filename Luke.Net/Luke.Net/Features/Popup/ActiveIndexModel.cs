using System;
using System.IO;
using System.Windows.Input;
using Lucene.Net.Store;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Directory = Lucene.Net.Store.Directory;

namespace Luke.Net.Features.Popup
{
    public class ActiveIndexModel
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

        public string Path { get; set; }
        public bool ReadOnly { get; set; }
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
