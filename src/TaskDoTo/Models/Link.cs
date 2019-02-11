using System;
using System.Collections.ObjectModel;
using System.Data;
using TaskDoTo.DataBase;

namespace TaskDoTo.Models
{
    public class Link
    {
        #region auto id
        private static DBConnection DB = DBConnection.Init();
        private static ulong link_id = DBConnection.LastId("links");
        #endregion

        //gets all links of a user (usualy used on app start up)
        public static ObservableCollection<Link> Links()
        {
            ObservableCollection<Link> links = new ObservableCollection<Link>();
            DataTable linksRaw = DB.Select($"SELECT * FROM links");
            foreach (DataRow row in linksRaw.Rows)
            {
                ulong _id = Convert.ToUInt64(row["id"]);
                string _href = Convert.ToString(row["href"]);
                string _name = Convert.ToString(row["name"]);

                links.Add(new Link(
                    _id, _href, 
                    _name));
            }

            return links;
        }

        #region fields
        private ulong id = 0;
        public ulong ID
        {
            get
            {
                return this.id;
            }
        }

        private string href;
        public string HREF
        {
            get
            {
                return this.href;
            }
            set
            {
                this.href = value;
            }
        }

        private string name;
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
        #endregion

        #region constructor
        public Link(string _href, string _name)
        {
            this.href = _href;
            this.name = _name;
        }
        public Link(ulong _id, string _href, string _name)
        {
            this.id = _id;
            this.href = _href;
            this.name = _name;
        }

        public void Copy(Link l)
        {
            this.href = l.href;
            this.name = l.name;
        }
        public void Save()
        {
            if (this.id == 0)
                this.id = ++link_id;

            string sql = $"INSERT INTO links (id, href, name) VALUES " +
                $"({this.id}, '{this.href}', '{this.name}')";
            DB.Modify(sql);
        }
        public void Update()
        {
            string sql = $"UPDATE links SET " +
                $"href='{this.href}', name='{this.name}'" +
                $"WHERE id={this.id}";
            DB.Modify(sql);
        }
        public void Delete()
        {
            string sql = $"DELETE FROM links WHERE id={this.id}";
            DB.Modify(sql);
        }
        #endregion
    }
}
