namespace LinqAndLamdaExpressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Models;

    internal class Program
    {
        private static void Main(string[] args)
        {
            List<User> allUsers = ReadUsers("users.json");
            List<Post> allPosts = ReadPosts("posts.json");

            #region Demo

            // 1 - find all users having email ending with ".net".
            var users1 = from user in allUsers
                         where user.Email.EndsWith(".net")
                         select user;

            var users11 = allUsers.Where(user => user.Email.EndsWith(".net"));

            IEnumerable<string> userNames = from user in allUsers
                                            select user.Name;

            var userNames2 = allUsers.Select(user => user.Name);

            foreach (var value in userNames2)
            {
                Console.WriteLine(value);
            }

            IEnumerable<Company> allCompanies = from user in allUsers
                                                select user.Company;

            var users2 = from user in allUsers
                         orderby user.Email
                         select user;

            var netUser = allUsers.First(user => user.Email.Contains(".net"));
            Console.WriteLine(netUser.Username);

            #endregion

            // 2 - find all posts for users having email ending with ".net".
            IEnumerable<int> usersIdsWithDotNetMails = from user in allUsers
                                                       where user.Email.EndsWith(".net")
                                                       select user.Id;

            IEnumerable<Post> posts = from post in allPosts
                                      where usersIdsWithDotNetMails.Contains(post.UserId)
                                      select post;

            foreach (var post in posts)
            {
                //Console.WriteLine(post.Id + " " + "user: " + post.UserId);
            }

            // 3 - print number of posts for each user.

            var playersPerTeam =    from post in posts
                                    group post by post.UserId into postGroup
                                    select new
                                    {
                                     Count = postGroup.Count(),
                                    };


            // 4 - find all users that have lat and long negative.

            var negative = from user in allUsers
                           where (user.Address.Geo.Lat<0 && user.Address.Geo.Lng<0)
                           select user.Id;

            // 5 - find the post with longest body.

            var longest = from post in allPosts
                           orderby post.Body descending
                           select post;


            // 6 - print the name of the employee that have post with longest body.


            // 7 - select all addresses in a new List<Address>. print the list.
            
            List < Address > myList = new List<Address>();

            var copyAdress = from user in allUsers
                             select user.Address;
                             
            foreach (var adr in copyAdress)
            {
                myList.Add(adr);

            }


            //myList.ForEach(Console.WriteLine);




            // 8 - print the user with min lat

            var minLat = from user in allUsers
                          orderby user.Address.Geo.Lat ascending
                          select user;


            // 9 - print the user with max long

            var maxLong = from user in allUsers
                         orderby user.Address.Geo.Lng descending
                         select user;

            // 10 - create a new class: public class UserPosts { public User User {get; set}; public List<Post> Posts {get; set} }
            //    - create a new list: List<UserPosts>
            //    - insert in this list each user with his posts only

            // 11 - order users by zip code

            var orderByZC = from user in allUsers
                            orderby user.Address.Zipcode ascending
                            select user;

            // 12 - order users by number of posts
            



        }

        private static List<Post> ReadPosts(string file)
        {
            return ReadData.ReadFrom<Post>(file);
        }

        private static List<User> ReadUsers(string file)
        {
            return ReadData.ReadFrom<User>(file);
        }
    }
}
