namespace CommonFiles.Models
{
    public class Card
    {
        private Guid _id;
        private string _name;
        private string _description;
        private string _imgPath;
        public Guid Id { get { return _id; } set { _id = value; } }
        public string Description
        {
            get => _description;
            set
            {
                if (_description == value || value == null) return;
                _description = value;
            }
        }
        public string ImgPath
        {
            get => _imgPath;
            set
            {
                if (_imgPath == value || value == null) return;
                _imgPath = value;
            }
        }
        public string Name
        {
            get => _name;
            set 
            {
                if (_name == value || value == null) return;
                _name = value;
            }
        }
        public Card()
        {
            _description = "";
            _imgPath = "";
            _name = "";
        }
        public Card(string? description, string? imgPath)
        {
            _description = description ?? "";
            _imgPath = imgPath ?? "";
            if (!String.IsNullOrEmpty(_imgPath))
            {
                _name = Path.GetFileName(_imgPath);
            }
            else _name = "";
        }
        public Card(Guid id, string? description, string? imgPath)
        {
            _id = id;
            _description = description ?? "";
            _imgPath = imgPath ?? "";
            if (!String.IsNullOrEmpty(_imgPath))
            {
                _name = Path.GetFileName(_imgPath);
            }
            else _name = "";
        }
        public static Card CreateCard(string? description, string? imgPath)
        {
            Card card = new(description, imgPath)
            {
                _id = Guid.NewGuid()
            };
            return card;
        }
    }
}
