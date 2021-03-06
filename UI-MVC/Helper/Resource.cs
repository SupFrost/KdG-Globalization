﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;

namespace SC.UI.Web.MVC.Helper
{
    public class Resource
    {
        public string Culture { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        #region Helpers

        // Probably using reflection not the best approach.
        public static string GetPropertyValue(string propertyName)
        {
            return GetPropertyValue(propertyName, Thread.CurrentThread.CurrentUICulture.Name);
        }

        public static string GetPropertyValue(string propertyName, string culture)
        {
            Resource resource = new Resource
            {
                Culture = Language.currentCulture.TwoLetterISOLanguageName,
                Name = propertyName
            };
            resource.Value = TranslationTier.Resource.ResourceManager.GetString(resource.Name);


            return resource.Value;
        }

        #endregion
    }

    public class TranslatableAttribute : Attribute
    {
    }
}