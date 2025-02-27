//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace Common.Models
{   
    public class Card
    {
        private string _description;
        private string _imgPath;
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
                _description = value;
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
                _imgPath = value;
            }
        }
        public Card() { _description = ""; _imgPath = ""; }
        public Card(string description, string imgPath)
        {
            if (description != null)
            {
                _description = description;
            }
            else
            {
                _description = "";
            }
            if (imgPath != null)
            {
                _imgPath = imgPath;
            }
            else
            {
                _imgPath = "";
            }
        }
    }
}
