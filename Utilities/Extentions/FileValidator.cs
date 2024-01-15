namespace LumiaPraktika.Utilities.Extentions
{
    public static class FileValidator
    {
        public static bool ValideType(this IFormFile file,string type = "image/")
        {
            if (file.ContentType.Contains(type))
            {
                return true;
            }
            return false;
        }
        public static bool ValideSize(this IFormFile file, int Limitkb)
        {
            if (file.Length<=Limitkb*1024)
            {
                return true;
            }
            return false;
        }
         public static async Task<string> CreatFileAsync(this IFormFile file ,string root,params string[] folders)
        {
            string filename = Guid.NewGuid().ToString() + file.FileName;
            string path = root;
            for (int i = 0; i < folders.Length; i++)
            {
                path = Path.Combine(path, folders[i]);
            }
            path = Path.Combine(path, filename);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return filename;
        }
        public static void DeleteFile(this string filename, string root, params string[] folders)
        {
            string path = root;
            for (int i = 0; i < folders.Length; i++)
            {
                path = Path.Combine(path, folders[i]);
            }
            path = Path.Combine(path, filename);

                if (File.Exists(path))
                {
                File.Delete(path);
                }
            

        }


    }
}
