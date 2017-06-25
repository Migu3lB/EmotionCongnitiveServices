﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EmotionPlatzi.web.Models
{
    public class EmotionPlatziwebContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public EmotionPlatziwebContext() : base("name=EmotionPlatziwebAzure")
        {
            Database.SetInitializer<EmotionPlatziwebContext>(new DropCreateDatabaseIfModelChanges<EmotionPlatziwebContext>());
        }

        public DbSet<EmoPicture> EmoPictures { get; set; }
        public DbSet<EmoFace> EmoFaces { get; set; }
        public DbSet<EmoEmotion> EmoEmotions { get; set; }

        public System.Data.Entity.DbSet<EmotionPlatzi.web.Models.Home> Homes { get; set; }
    }
}
