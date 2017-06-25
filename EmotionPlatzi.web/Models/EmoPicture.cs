using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmotionPlatzi.web.Models
{
    public class EmoPicture
    {
        public int Id { get; set; }
        [Display(Name = "Nombre")]
        public string Name { get; set; }
        [Required]
        //[MaxLength(10)]
        public string Path { get; set; }

        //Indica que Por cada EmoPicture pueden haber varias EmoFaces
        public virtual  ObservableCollection<EmoFace> Faces { get; set; }
    }
}