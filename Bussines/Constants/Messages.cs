using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Bussines.Constants
{
    public static class Messages
    {
        public static string ProductAddedMessage = "Ürün Eklendi.";
        public static string ProductNameInalid = "Ürün ismi geçersiz.";
        public static string MaintenanceTime = "Sistem Bakımda";
        public static string ProductListed = "Ürünler Listelendi.";
        public static string ProductCountOfCategoryError="Kategorideki ürün sayısı 10'u geçemez";
        public static string ProductNameAlreadyExists="Ürün isimleri aynı olamaz";
        internal static string CategoryLimitExceded="Kategori limiti aşıldı";
        public static string AuthorizationDenied = "Yetkiniz yok";
        internal static string UserRegistered = "Kullanıcı kaydedildi";
        internal static string UserNotFound = "Kullanıcı bulunamadı";
        internal static string PasswordError = "Hatalı parola";
        internal static string SuccessfulLogin = "Giriş Başarılı";
        internal static string UserAlreadyExists = "Kullanıcı mevcut";
        internal static string AccessTokenCreated = "Token oluşturuldu";
    }
}
