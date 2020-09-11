using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MomentCapturer.Classes;
using SQLite;

namespace MomentCapturer.DataContext
{
    public static class Data
    {
        private static string _folderPath = System.Environment
            .GetFolderPath(System.Environment.SpecialFolder.Personal);

        private static string _fileMemoryName = "Memories.db3";

        private static string _fileUserName = "Users.db3";

        private static string _fileCollegue = "Collegues.db3";

        private static string _fileFamily = "Family.db3";

        private static string _fileFriend = "Friends.db3";

        public static string FullMemoryPath = System.IO.Path.Combine(_folderPath, _fileMemoryName);

        public static string FullUserPath = System.IO.Path.Combine(_folderPath, _fileUserName);

        public static string FullColleguePath = System.IO.Path.Combine(_folderPath, _fileCollegue);

        public static string FullFamilyPath = System.IO.Path.Combine(_folderPath, _fileFamily);

        public static string FullFriendPath = System.IO.Path.Combine(_folderPath, _fileFriend);

        //create table Moments
        //Return CreateTableResult
        public static CreateTableResult CreateMemoryTable()
        {
            using(var conn = new SQLiteConnection(FullMemoryPath))
            {
                return conn.CreateTable<Moment>();
            }
        }

        //add a moment
        public static int AddMoment(Moment moment)
        {
            using (var conn = new SQLiteConnection(FullMemoryPath))
            {
                return conn.Insert(moment);
            }
        }

        //Add a list of moments
        public static int AddMoments(List<Moment> moments)
        {
            using (var conn = new SQLiteConnection(FullMemoryPath))
            {
                return conn.InsertAll(moments);
            }
        }

        //remove a moment
        public static int RemoveMoment(Moment moment)
        {
            using (var conn = new SQLiteConnection(FullMemoryPath))
            {
                return conn.Delete(moment);
            }
        }

        //Empty moment db
        public static int RemoveAllMoments()
        {
            using (var conn = new SQLiteConnection(FullMemoryPath))
            {
                return conn.DeleteAll<Moment>();
            }
        }

        //Get Moments
        public static List<Moment> GetMoments()
        {
            using (var conn = new SQLiteConnection(FullMemoryPath))
            {
                return conn.Table<Moment>().ToList();
            }
        }

        public static List<Moment>GetMomentsByTitle(string title)
        {
            using (var conn = new SQLiteConnection(FullMemoryPath))
            {
                return conn.Table<Moment>()
                    .Where(m => m.Title.ToLower().Contains(title.ToLower()))
                    .ToList();
            }
        }

        public static IEnumerable<Moment>GetMomentsByUserId(int id)
        {
            using (var conn = new SQLiteConnection(FullMemoryPath))
            {
                return conn.Table<Moment>()
                   .Where(m => m.UserId == id).ToList();
            }
        }


        //create table User
        //Return CreateTableResult
        public static CreateTableResult CreateUserTable()
        {
            using (var conn = new SQLiteConnection(FullUserPath))
            {
                return conn.CreateTable<User>();
            }
        }

        //add a moment
        public static int AddUser(User user)
        {
            using (var conn = new SQLiteConnection(FullUserPath))
            {
                return conn.Insert(user);
            }
        }

        //Add a list of moments
        public static int AddUsers(List<User> users)
        {
            using (var conn = new SQLiteConnection(FullUserPath))
            {
                return conn.InsertAll(users);
            }
        }

        //remove a moment
        public static int RemoveUser(User user)
        {
            using (var conn = new SQLiteConnection(FullUserPath))
            {
                return conn.Delete(user);
            }
        }

        //Empty moment db
        public static int RemoveAllUsers()
        {
            using (var conn = new SQLiteConnection(FullUserPath))
            {
                return conn.DeleteAll<User>();
            }
        }

        //Get Moments
        public static List<User> GetUsers()
        {
            using (var conn = new SQLiteConnection(FullUserPath))
            {
                return conn.Table<User>().ToList();
            }
        }

        public static User GetUserByUsername(string username)
        {
            using (var conn = new SQLiteConnection(FullUserPath))
            {
                return conn.Table<User>()
                    .FirstOrDefault(u => u.Username.ToLower().Equals(username.ToLower()));
                    
            }
        }

        //create table Collegues
        //Return CreateTableResult
        public static CreateTableResult CreateCollegueTable()
        {
            using (var conn = new SQLiteConnection(FullColleguePath))
            {
                return conn.CreateTable<Collegue>();
            }
        }

        //add a collegue
        public static int AddCollegue(Collegue collegue)
        {
            using (var conn = new SQLiteConnection(FullColleguePath))
            {
                return conn.Insert(collegue);
            }
        }

        //Add a list of collegues
        public static int AddCollegues(List<Collegue> collegues)
        {
            using (var conn = new SQLiteConnection(FullColleguePath))
            {
                return conn.InsertAll(collegues);
            }
        }

        //remove a collegue
        public static int RemoveCollegue(Collegue collegue)
        {
            using (var conn = new SQLiteConnection(FullColleguePath))
            {
                return conn.Delete(collegue);
            }
        }

        //Empty collegue db
        public static int RemoveAllCollegues()
        {
            using (var conn = new SQLiteConnection(FullColleguePath))
            {
                return conn.DeleteAll<Collegue>();
            }
        }

        //Get Collegues
        public static List<Collegue> GetCollegues()
        {
            using (var conn = new SQLiteConnection(FullColleguePath))
            {
                return conn.Table<Collegue>().ToList();
            }
        }

        public static List<Collegue> GetColleguesByName(string name)
        {
            using (var conn = new SQLiteConnection(FullColleguePath))
            {
                return conn.Table<Collegue>()
                    .Where(c => c.Name.ToLower().Contains(name.ToLower()))
                    .ToList();
            }
        }

        public static IEnumerable<Collegue> GetColleguesByUserId(int id)
        {
            using (var conn = new SQLiteConnection(FullColleguePath))
            {
                return conn.Table<Collegue>()
                   .Where(m => m.UserId == id).ToList();
            }
        }

        //create table Family
        //Return CreateTableResult
        public static CreateTableResult CreateFamilyTable()
        {
            using (var conn = new SQLiteConnection(FullFamilyPath))
            {
                return conn.CreateTable<FamilyMember>();
            }
        }

        //add a family member
        public static int AddFamilyMember(FamilyMember member)
        {
            using (var conn = new SQLiteConnection(FullFamilyPath))
            {
                return conn.Insert(member);
            }
        }

        //Add a list of family members
        public static int AddFamilyMembers(List<FamilyMember> members)
        {
            using (var conn = new SQLiteConnection(FullFamilyPath))
            {
                return conn.InsertAll(members);
            }
        }

        //remove a family member
        public static int RemoveFamilyMember(FamilyMember member)
        {
            using (var conn = new SQLiteConnection(FullFamilyPath))
            {
                return conn.Delete(member);
            }
        }

        //Empty family db
        public static int RemoveAllFamilyMembers()
        {
            using (var conn = new SQLiteConnection(FullFamilyPath))
            {
                return conn.DeleteAll<FamilyMember>();
            }
        }

        //Get Family members
        public static List<FamilyMember> GetFamilyMembers()
        {
            using (var conn = new SQLiteConnection(FullFamilyPath))
            {
                return conn.Table<FamilyMember>().ToList();
            }
        }

        public static List<FamilyMember> GetFamilyMemberByName(string name)
        {
            using (var conn = new SQLiteConnection(FullFamilyPath))
            {
                return conn.Table<FamilyMember>()
                    .Where(m => m.Name.ToLower().Contains(name.ToLower()))
                    .ToList();
            }
        }

        public static IEnumerable<FamilyMember> GetFamilyMembersByUserId(int id)
        {
            using (var conn = new SQLiteConnection(FullFamilyPath))
            {
                return conn.Table<FamilyMember>()
                   .Where(m => m.UserId == id).ToList();
            }
        }

        //create table Friend
        //Return CreateTableResult
        public static CreateTableResult CreateFriendTable()
        {
            using (var conn = new SQLiteConnection(FullFriendPath))
            {
                return conn.CreateTable<Friend>();
            }
        }

        //add a friend
        public static int AddFriend(Friend friend)
        {
            using (var conn = new SQLiteConnection(FullFriendPath))
            {
                return conn.Insert(friend);
            }
        }

        //Add a list of friends
        public static int AddFriends(List<Friend> friends)
        {
            using (var conn = new SQLiteConnection(FullFriendPath))
            {
                return conn.InsertAll(friends);
            }
        }

        //remove a friend
        public static int RemoveFriend(Friend friend)
        {
            using (var conn = new SQLiteConnection(FullFriendPath))
            {
                return conn.Delete(friend);
            }
        }

        //Empty friend db
        public static int RemoveAllFriends()
        {
            using (var conn = new SQLiteConnection(FullFriendPath))
            {
                return conn.DeleteAll<Friend>();
            }
        }

        //Get Friends
        public static List<Friend> GetFriends()
        {
            using (var conn = new SQLiteConnection(FullFriendPath))
            {
                return conn.Table<Friend>().ToList();
            }
        }

        public static List<Friend> GetFriendsByName(string name)
        {
            using (var conn = new SQLiteConnection(FullFriendPath))
            {
                return conn.Table<Friend>()
                    .Where(f => f.Name.ToLower().Contains(name.ToLower()))
                    .ToList();
            }
        }

        public static IEnumerable<Friend> GetFriendsByUserId(int id)
        {
            using (var conn = new SQLiteConnection(FullFriendPath))
            {
                return conn.Table<Friend>()
                   .Where(f => f.UserId == id).ToList();
            }
        }






    }
}