using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using TaskDoTo.DataBase;

namespace TaskDoTo.Models
{
    public class Task : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyRaised(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }

        #region db related
        private static DBConnection DB = DBConnection.Init();
        private static ulong task_id = DBConnection.LastId("tasks");
        private static string[] STATUS = { "Assigned", "Done" };
        #endregion


        //gets all tasks of a user (usualy used on app start up)
        private static ObservableCollection<Task> finalTask(DataTable dset)
        {
            ObservableCollection<Task> tasks = new ObservableCollection<Task>();
            foreach (DataRow row in dset.Rows)
            {
                ulong _id = Convert.ToUInt64(row["id"]);
                string _title = Convert.ToString(row["title"]);
                string _description = Convert.ToString(row["description"]);
                int _status = Convert.ToInt32(row["status"]);

                tasks.Add(new Task(
                    _id, _title,
                    _description,
                    _status));
            }

            return tasks;
        }
        public static ObservableCollection<Task> Tasks()
        {
            DataTable tasksRaw = DB.Select($"SELECT * FROM tasks");
            return finalTask(tasksRaw);
        }
        public static ObservableCollection<Task> AssignedTasks()
        {
            DataTable tasksRaw = DB.Select($"SELECT * FROM tasks WHERE status = 0");
            return finalTask(tasksRaw);
        }
        public static ObservableCollection<Task> DoneTasks()
        {
            DataTable tasksRaw = DB.Select($"SELECT * FROM tasks WHERE status = 1");
            return finalTask(tasksRaw);
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

        private string title;
        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                this.title = value;
            }
        }

        private string description;
        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;
            }
        }

        private int status = 0;
        private int setStatus(int val)
        {
            if (val < 0)
                return 0;
            if (val > 2)
                return 0;

            return val;
        }
        public int StatusVal
        {
            get
            {
                return this.status;
            }
            set
            {
                this.status = this.setStatus(value);
            }
        }
        public string Status
        {
            get
            {
                return STATUS[this.status];
            }
        }
        #endregion

        #region constructor
        public Task(string _title, string _description, int _status)
        {
            this.title = _title;
            this.description = _description;
            this.status = this.setStatus(_status);
        }
        public Task(ulong _id, string _title, string _description, int _status)
        {
            this.id = _id;
            this.title = _title;
            this.description = _description;
            this.status = this.setStatus(_status);
        }

        public void Copy(Task t)
        {
            this.title = t.title;
            this.description = t.description;
            this.status = t.status;

            this.OnPropertyRaised("Updated");
        }
        public void Save()
        {
            if (this.id == 0)
                this.id = ++task_id;

            string sql = $"INSERT INTO tasks (id, title, description, status) VALUES " +
                $"({this.id}, '{this.title}', '{this.description}', {this.status})";
            DB.Modify(sql);
        }
        public void Update()
        {
            string sql = $"UPDATE tasks SET " +
                $"title='{this.title}', description='{this.description}', " +
                $"status={this.status} WHERE id={this.id}";
            DB.Modify(sql);
        }
        public void Delete()
        {
            string sql = $"DELETE FROM tasks WHERE id={this.id}";
            DB.Modify(sql);
        }
        #endregion
    }
}
