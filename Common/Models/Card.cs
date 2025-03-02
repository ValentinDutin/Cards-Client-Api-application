using System;

namespace Common.Models
{   
    public class Card
    {
        private Guid _id;
        private string _description;
        private string _imgPath;
        public Guid Id { get { return _id; } set { _id = value; } }
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                if (value == null)
                {
                    return;
                }
                _description = value ?? "";
            }
        }
        public string ImgPath
        {
            get
            {
                return _imgPath;
            }
            set
            {
                if (value == null)
                {
                    return;
                }
                _imgPath = value ?? "";
            }
        }
        public Card() 
        {
            _description = "";
            _imgPath = "";
        }
        public Card(string description, string imgPath)
        {
                _description = description ?? "";
            if (imgPath != null)
            {
                _imgPath = imgPath ?? "";
            }
            else
            {
                _imgPath = "";
            }
        }
        public Card(Guid id, string description, string imgPath)
        {
            _id = id;
            Description = description ?? "";
            ImgPath = imgPath ?? "";
        }
        public static Card CreateCard(string description, string imgPath)
        {
            Card card = new Card(description, imgPath);
            card._id = Guid.NewGuid();
            return card;
        }
    }
}
