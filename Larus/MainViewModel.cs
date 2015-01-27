using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using GongSolutions.Wpf.DragDrop;

namespace Larus
{
    class MainViewModel : IDropTarget, INotifyPropertyChanged
    {

        private string _inputStickerName;
        private ICommand _addStickerCommand;
        private ICommand _removeStickerCommand;
        private ObservableCollection<ObservableCollection<PostIt>> _categoryCollection;
        ObservableCollection<CategoryViewModel> todoCategory = new ObservableCollection<CategoryViewModel>();
        ObservableCollection<CategoryViewModel> doingCategory = new ObservableCollection<CategoryViewModel>();
        ObservableCollection<CategoryViewModel> doneCategory = new ObservableCollection<CategoryViewModel>();
        private ObservableCollection<PostIt> _toDoList;
        private ObservableCollection<PostIt> _doingList;
        private ObservableCollection<PostIt> _doneList;
        private CategoryViewModel toDoHandler;
        private CategoryViewModel doingHandler;
        private CategoryViewModel doneHandler;

        public ICollectionView ToDoCategory { get; private set; }
        public ICollectionView DoingCategory { get; private set; }
        public ICollectionView DoneCategory { get; private set; }
        public ObservableCollection<ObservableCollection<PostIt>> CategoryCollection
        {
            get { return _categoryCollection; }
            set { _categoryCollection = value; }
        }

        public string InputStickerName
        {
            get { return _inputStickerName; }
            set { _inputStickerName = value;
            OnPropertyChanged("InputStickerName");}
        }

        public ICommand AddStickerCommand
        {
            get { return _addStickerCommand; }
            set { _addStickerCommand = value; }
        }

        public ICommand RemoveStickerCommand
        {
            get { return _removeStickerCommand; }
            set { _removeStickerCommand = value; }
        }

        public ObservableCollection<PostIt> ToDoList
        {
            get { return _toDoList; }
            set { _toDoList = value; }
        }

        public ObservableCollection<PostIt> DoingList
        {
            get { return _doingList; }
            set { _doingList = value; }
        }

        public ObservableCollection<PostIt> DoneList
        {
            get { return _doneList; }
            set { _doneList = value; }
        }

        public MainViewModel()
        {
            //_addStickerCommand = new RelayCommand(addSticker);
            //_removeStickerCommand = new RelayCommand(removeSticker);
            _categoryCollection = new ObservableCollection<ObservableCollection<PostIt>>();
            CategoryViewModel toDoHandler = new CategoryViewModel("ToDo");
            toDoHandler.Stickers.Add(new PostIt("Create Domain Model", 1, "Someone else?", new SolidColorBrush(Colors.Chartreuse)));
            toDoHandler.Stickers.Add(new PostIt("Copy ViewModel from Larus", 2, "You!", new SolidColorBrush(Colors.CadetBlue)));
            toDoHandler.Stickers.Add(new PostIt("Copy Paste XAML", 1, "You!", new SolidColorBrush(Colors.Chocolate)));
            todoCategory.Add(toDoHandler);
            ToDoList = toDoHandler.Stickers;
            CategoryViewModel doingHandler = new CategoryViewModel("Doing");
            doingCategory.Add(doingHandler);
            DoingList = doingHandler.Stickers;
            CategoryViewModel doneHandler = new CategoryViewModel("Done");
            doneCategory.Add(doneHandler);
            DoneList = doneHandler.Stickers;
            ToDoCategory = CollectionViewSource.GetDefaultView(todoCategory);
            DoingCategory = CollectionViewSource.GetDefaultView(doingCategory);
            DoneCategory = CollectionViewSource.GetDefaultView(doneCategory);
        }

        public void addSticker()
        {
            if (InputStickerName == "" && InputStickerName == "PostIt Needs a Name")
            {
                _inputStickerName = "PostIt Needs a Name";
            }
            else
            {
                ToDoList.Add(new PostIt(InputStickerName, 0, "", new SolidColorBrush(Colors.BlueViolet)));
            }
        }

        public void removeSticker()
        {
            foreach (ObservableCollection<PostIt> observableCollection in CategoryCollection)
            {
                
            }
        }

        public void DragOver(IDropInfo dropInfo)
        {
           // InputStickerName = dropInfo.TargetItem.ToString();
            if (dropInfo.Data is PostIt && dropInfo.TargetItem is CategoryViewModel)
            {
                dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
                dropInfo.Effects = DragDropEffects.Move;
            }
        }

        public void Drop(IDropInfo dropInfo)
        {
            CategoryViewModel fieldCollection = (CategoryViewModel)dropInfo.TargetItem;
            PostIt stick = (PostIt)dropInfo.Data;
            fieldCollection.Stickers.Add(stick);
            ((IList)dropInfo.DragInfo.SourceCollection).Remove(stick);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
