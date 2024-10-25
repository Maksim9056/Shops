using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopClassLibrary.ModelShop
{
    public class Image
    {
        public int Id { get; set; }  // Первичный ключ

        public byte[] OriginalImageData { get; set; }  // Данные оригинального изображения (BLOB)

        public DateTime UploadDate { get; set; }  // Дата загрузки изображения

        public string Description { get; set; }  // Описание изображения

        // Связь один-ко-многим с таблицей ImageCopies
        public virtual ICollection<ImageCopy> ImageCopies { get; set; }
    }

}
 