using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace EmotionPlatzi.web.Models
{
    public class EmoFace
    {
        public int Id { get; set; }
        public int EmoPictureId { get; set; }

        #region FaceAttributes
        public int Y { get; set; }
        public int X { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        #endregion

        //Indica que cada Face Pertenece a un Picture
        public virtual EmoPicture Picture { get; set; }
        public virtual ObservableCollection<EmoEmotion> Emotions { get; set; }


    }
}