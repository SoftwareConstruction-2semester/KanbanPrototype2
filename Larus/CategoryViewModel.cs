using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Larus
{
    class CategoryViewModel
    {
        private string _categoryName;

        public string CategoryName
        {
            get { return _categoryName; }
            set { _categoryName = value; }
        }

        public ObservableCollection<PostIt> Stickers { get; set; }

        public CategoryViewModel(string CategoryName)
        {
            _categoryName = CategoryName;
            Stickers = new ObservableCollection<PostIt>();
        }

        public CategoryViewModel(string CategoryName, List<string> savedStickers)
        {
            _categoryName = CategoryName;
            Stickers = new ObservableCollection<PostIt>();
            foreach (string savedSticker in savedStickers)
            {
                Stickers.Add(new PostIt() {Name = savedSticker});
            }
        }

        public override string ToString()
        {
            return this.CategoryName;
        }
    }
}
