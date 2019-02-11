using System.IO;

namespace TaskDoTo.DataBase
{
    public class CFile
    {
        //path to application data /bin/Debug
        private static string prePath = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);

        #region properties&fields
        //name of file
        private string name;
        //sufix of file
        private string type;

        //gets or sets the name
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }
        //gets or sets the sufix
        public string Type
        {
            get
            {
                return this.type;
            }
            set
            {
                this.type = value;
            }
        }
        //gets the name.sufix
        public string FullName
        {
            get
            {
                return this.getFullName();
            }
        }
        private string getFullName()
        {
            return $"{this.name}.{this.type}";
        }
        //gets the path to name.sufix using "prePath"
        public string FilePath
        {
            get
            {
                return this.getFilePath();
            }
        }
        private string getFilePath()
        {
            return Path.Combine(prePath, this.getFullName());
        }
        #endregion

        #region constructor
        //meant to use it for sqlite files, so "_type" will be default
        public CFile(string _name, string _type = "sqlite")
        {
            this.name = _name;
            this.type = _type;
        }
        #endregion

        #region methods
        public bool Exists() 
        {
            return System.IO.File.Exists(this.getFilePath());
        }
        #endregion
    }
}
