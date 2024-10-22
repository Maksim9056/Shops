using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopClassLibrary.ModelShop
{
    public class ImageCopy
    {
        public int Id { get; set; }  // Первичный ключ

        public int ImageID { get; set; }  // Внешний ключ на таблицу Images

        public string Resolution { get; set; }  // Разрешение копии

        public byte[] CopyImageData { get; set; }  // Данные копии изображения (BLOB)

        public int FileSize { get; set; }  // Размер файла копии

        // Навигационное свойство для связи с таблицей Images
        public virtual Image Image { get; set; }
    }

}
